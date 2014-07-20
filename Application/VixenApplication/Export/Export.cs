using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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


        public void DoExport(string sequenceFileName)
        {
            _sequence = SequenceService.Instance.Load(sequenceFileName);

            if (_sequence != null)
            {
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
                //_context.SequenceStarted += context_SequenceStarted;
                //_context.SequenceEnded += context_SequenceEnded;
                //_context.ContextEnded += context_ContextEnded;

                _context.Play(TimeSpan.Zero, TimeSpan.MaxValue);
            }



        }

    }
}