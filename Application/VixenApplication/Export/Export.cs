using System;

using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vixen.Module.Timing;
using Vixen.Module.App;
using Vixen.Services;
using Vixen.Execution;
using Vixen.Execution.Context;
using Vixen.Sys;

namespace VixenApplication
{
    public class Export
    {
        private ISequence _sequence = null;
        private ISequenceContext _context = null;
        private int _oldUpdateInterval;

        public void CancelExport()
        {
            if (_context != null)
            {
                _context.Stop();
            }
        }

        public void DoExport(string sequenceFileName)
        {
            _sequence = SequenceService.Instance.Load(sequenceFileName);

            if (_sequence != null)
            {

                _oldUpdateInterval = Vixen.Sys.VixenSystem.DefaultUpdateInterval;
                Vixen.Sys.VixenSystem.DefaultUpdateInterval = 500;

                string[] timingSources;
                TimingProviders timingProviders = new TimingProviders(_sequence);

                timingSources = timingProviders.GetAvailableTimingSources("Export");

                if (timingSources.Length > 0)
                {
                    SelectedTimingProvider exportTimingProvider = new SelectedTimingProvider("Export", timingSources.First());
                    _sequence.SelectedTimingProvider = exportTimingProvider;
                }


                _context = VixenSystem.Contexts.CreateSequenceContext(new ContextFeatures(ContextCaching.NoCaching), _sequence);
                if (_context == null)
                {
                   // Logging.Error(@"Null context when attempting to play sequence.");
                    MessageBox.Show(@"Unable to play this sequence.  See error log for details.");
                    return;
                }

                _context.Sequence.ClearMedia();
                
                _context.Play(TimeSpan.Zero, TimeSpan.MaxValue);

                _context.SequenceEnded += context_SequenceEnded;
            }
        }

        void context_SequenceEnded(object sender, EventArgs e)
        {
            _oldUpdateInterval = Vixen.Sys.VixenSystem.DefaultUpdateInterval;
            _context.ContextEnded -= context_SequenceEnded;
        }

        public double SequenceLenghth
        {
            get
            {
                double retVal = 0;
                if (_context != null)
                {
                    retVal = _context.Sequence.Length.TotalMilliseconds;
                }
                return retVal;
            }
        }

        public ITiming SequenceTiming
        {
            get
            {
                ITiming retVal = null;
                if (_context != null)
                {
                    retVal = _context.Sequence.GetTiming();
                }
                return retVal;
            }
        }

        public void SetContextEndHandler(System.EventHandler eventHandler)
        {
            if (_context != null)
            {
                _context.ContextEnded += eventHandler;
            }
        }

        public void ClearContextEndHandler(System.EventHandler eventHandler)
        {
            if (_context != null)
            {
                _context.ContextEnded -= eventHandler;
            }
        }
    }
}