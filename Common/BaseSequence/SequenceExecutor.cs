﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Threading;
using Vixen.Execution;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Module.Timing;
using Vixen.Module.Media;

namespace BaseSequence
{
	public class SequenceExecutor : ISequenceExecutor
	{
		private System.Timers.Timer _endCheckTimer;
		private SynchronizationContext _syncContext;
		private bool _isRunning;
		private bool _isPaused;
		private bool _loop;

		public event EventHandler<SequenceStartedEventArgs> SequenceStarted;
		public event EventHandler<SequenceStartedEventArgs> SequenceReStarted;
		public event EventHandler<SequenceEventArgs> SequenceEnded;
		public event EventHandler<ExecutorMessageEventArgs> Message;
		public event EventHandler<ExecutorMessageEventArgs> Error;

		public SequenceExecutor()
		{
			_endCheckTimer = new System.Timers.Timer(10);
			_endCheckTimer.Elapsed += _EndCheckTimerElapsed;
			_syncContext = AsyncOperationManager.SynchronizationContext;
		}

		#region Public

		// Because these are calculated values, changing the length of the sequence
		// during execution will not affect the end time.
		public TimeSpan StartTime { get; protected set; }

		public TimeSpan EndTime { get; protected set; }

		public ISequence Sequence { get; set; }

		public bool IsRunning
		{
			get { return _isRunning; }
			private set
			{
				_isRunning = value;
				if (_isRunning) {
					OnSequenceStarted(new SequenceStartedEventArgs(Sequence, TimingSource, StartTime, EndTime));
				}
				else {
					OnSequenceEnded(new SequenceEventArgs(Sequence));
				}
			}
		}

		public bool IsPaused
		{
			get { return _isPaused; }
			private set { _isPaused = value; }
		}

		public void Start()
		{
			Play(TimeSpan.Zero, TimeSpan.MaxValue);
		}

		public void Play(TimeSpan startTime, TimeSpan endTime)
		{
			_loop = false;
			_Play(startTime, endTime);
		}

		public void PlayLoop(TimeSpan startTime, TimeSpan endTime)
		{
			_loop = true;
			_Play(startTime, endTime);
		}

		public void Pause()
		{
			_Pause();
		}

		public void Resume()
		{
			_Resume();
		}

		public void Stop()
		{
			_Stop();
		}

		public IEnumerable<ISequenceFilterNode> SequenceFilters
		{
			get
			{
				if (Sequence != null) {
					return Sequence.GetAllSequenceFilters();
				}
				return Enumerable.Empty<ISequenceFilterNode>();
			}
		}

		public string Name
		{
			get
			{
				if (Sequence != null) {
					return Sequence.Name;
				}
				return null;
			}
		}

		#endregion

		#region Events

		protected virtual void OnSequenceStarted(SequenceStartedEventArgs e)
		{
			if (SequenceStarted != null) {
				SequenceStarted(null, e);
			}
		}

		protected virtual void OnSequenceReStarted(SequenceStartedEventArgs e)
		{
			if (SequenceReStarted != null)
			{
				SequenceReStarted(null, e);
			}
		}

		protected virtual void OnSequenceEnded(SequenceEventArgs e)
		{
			if (SequenceEnded != null) {
				SequenceEnded(null, e);
			}
		}

		protected virtual void OnMessage(ExecutorMessageEventArgs e)
		{
			if (Message != null) {
				Message(null, e);
			}
		}

		protected virtual void OnError(ExecutorMessageEventArgs e)
		{
			if (Error != null) {
				Error(Sequence, e);
			}
		}

		#endregion

		#region Private

		private void _Play(TimeSpan startTime, TimeSpan endTime)
		{
			if (IsRunning) return;
			if (Sequence == null) return;

			// Only hook the input stream during execution.
			// Hook before starting the behaviors.
			//_HookDataListener();

			// Bound the execution range.
			StartTime = _CoerceStartTime(startTime);
			EndTime = _CoerceEndTime(endTime);
			_IsTimedSequence = EndTime >= StartTime; 

			if (StartTime == EndTime)
			{
				//We are done before we get started
				_syncContext.Post(x => _Stop(), null);
			}

			TimingSource = Sequence.GetTiming() ?? _GetDefaultTimingSource();

			_LoadMedia();

			_StartMedia();

			TimingSource.Position = StartTime;
			TimingSource.Start();

			// Start the crazy train.
			IsRunning = true;

			while (TimingSource.Position == StartTime)
			{
				Thread.Sleep(1); //Give the train a chance to get out of the station.
			}

			_endCheckTimer.Start();

		}

		private void _loopPlay()
		{
			// Stop whatever is driving this crazy train.
			lock (_endCheckTimer)
			{
				_endCheckTimer.Enabled=false;
			}
			
			TimingSource.Stop();
			
			//Reset our position. No need to stop the media, we will just reset its position.
			TimingSource.Position = StartTime;
			
			OnSequenceReStarted(new SequenceStartedEventArgs(Sequence, TimingSource, StartTime, EndTime));
			
			//Fire it back up again.
			TimingSource.Start();
			
			while (TimingSource.Position == StartTime)
			{
				Thread.Sleep(1); //Give the train a chance to get out of the station.
			}

			_endCheckTimer.Enabled=true;
		}

		protected virtual void _HookDataListener()
		{
			Sequence.InsertDataListener += _DataListener;
		}

		protected virtual void _UnhookDataListener()
		{
			Sequence.InsertDataListener -= _DataListener;
		}

		protected virtual TimeSpan _CoerceStartTime(TimeSpan startTime)
		{
			return startTime < Sequence.Length ? startTime : Sequence.Length;
		}

		protected virtual TimeSpan _CoerceEndTime(TimeSpan endTime)
		{
			return endTime < Sequence.Length ? endTime : Sequence.Length;
		}

		protected virtual ITiming _GetDefaultTimingSource()
		{
			return TimingService.Instance.GetDefaultSequenceTiming();
		}

		protected virtual void _LoadMedia()
		{
			var sequenceMedia = Sequence.GetAllMedia();
            if (sequenceMedia != null && sequenceMedia.Any())
				foreach (IMediaModuleInstance media in sequenceMedia) {
					media.LoadMedia(StartTime);
				}
		}

		protected virtual void _StartMedia()
		{
			var sequenceMedia = Sequence.GetAllMedia();
			if (sequenceMedia != null && sequenceMedia.Any() )
				foreach (IMediaModuleInstance media in sequenceMedia) {
					media.Start();
				}
		}

		protected virtual void _PauseMedia()
		{
			var sequenceMedia = Sequence.GetAllMedia();
            if (sequenceMedia != null && sequenceMedia.Any())
				foreach (IMediaModuleInstance media in sequenceMedia) {
					media.Pause();
				}
		}

		protected virtual void _ResumeMedia()
		{
			var sequenceMedia = Sequence.GetAllMedia();
            if (sequenceMedia != null && sequenceMedia.Any())
				foreach (IMediaModuleInstance media in sequenceMedia) {
					media.Resume();
				}
		}

		protected virtual void _StopMedia()
		{
			var sequenceMedia = Sequence.GetAllMedia();
            if (sequenceMedia != null && sequenceMedia.Any())
				foreach (IMediaModuleInstance media in sequenceMedia) {
					media.Stop();
				}
		}

		private void _Pause()
		{
			if (!IsRunning || IsPaused) return;

			if (_endCheckTimer.Enabled) {
				IsPaused = true;

				TimingSource.Pause();

				_PauseMedia();

				_endCheckTimer.Enabled = false;
			}
		}

		private void _Resume()
		{
			if (!IsPaused) return;

			if (!_endCheckTimer.Enabled && Sequence != null) {
				IsPaused = false;

				TimingSource.Resume();

				_ResumeMedia();

				_endCheckTimer.Enabled = true;
			}
		}

		private void _Stop()
		{
			if (!IsRunning) return;

			// Stop whatever is driving this crazy train.
			lock (_endCheckTimer) {
				_endCheckTimer.Enabled = false;
			}

			// Release the hook before the behaviors are shut down so that
			// they can affect the sequence.
			//_UnhookDataListener();

			IsRunning = false;
			IsPaused = false;

			TimingSource.Stop();

			_StopMedia();
		}

		public ITiming TimingSource { get; private set; }

		protected virtual bool _DataListener(IEffectNode effectNode)
		{
			// We don't want any handlers beyond the executor to get live data.
			return true;
		}

		private void _EndCheckTimerElapsed(object sender, ElapsedEventArgs e)
		{

			// To catch events that may trail after the timer's been disabled
			// due to it being a threaded timer and Stop being called between the
			// timer message being posted and acted upon.
			if (_endCheckTimer == null || !_endCheckTimer.Enabled) return;

			lock (_endCheckTimer) {

				_endCheckTimer.Enabled = false;

				if (!_CheckForNaturalEnd() && IsRunning) {
					_endCheckTimer.Enabled = true;
				}
			}
		}

		private bool _CheckForNaturalEnd()
		{
			bool isEnd = _IsEndOfSequence();
			if (isEnd) {
				if (_loop)
				{
					_syncContext.Post(x => _loopPlay(), null);
				}
				else
				{
					_syncContext.Post(x => _Stop(), null);	
				}
				
			}
			return isEnd;
		}

		private bool _IsEndOfSequence()
		{
			TimeSpan position = TimingSource.Position;
			return _IsTimedSequence && (position >= EndTime || position == TimeSpan.Zero);
		}

		protected bool _IsTimedSequence { get; set; }

		#endregion

		#region Dispose

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_endCheckTimer != null)
				{
					_endCheckTimer.Elapsed -= _EndCheckTimerElapsed;
					_endCheckTimer.Dispose();
					
				}	
			}
			_endCheckTimer = null;
			_syncContext = null;
		}

		#endregion
	}
}