using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vixen.Module.App;
using Vixen.Services;
using Vixen.Execution;
using Vixen.Execution.Context;
using Vixen.Sys;

namespace VixenModules.App.Exporter
{
    public class ExporterModule : AppModuleInstanceBase
    {
        private const string ID_MENU = "Exporter_Main";
        private IApplication _application;
        private ExporterForm _form;
        private ISequence _sequence;
        private OpenFileDialog openFileDialog = new OpenFileDialog();

        private ISequenceContext _context = null;
            
        public override IApplication Application
        {
            set { _application = value; }
        }

        public override void Loading()
        {
            InitializeForm();
            _AddMenu();


        }

        public override void Unloading()
        {
            if (_form != null)
            {
                _form.Dispose();
                _form = null;
            }

            _RemoveMenu();
        }

        private void InitializeForm()
        {
            _form = new ExporterForm();
            _form.Closed += _form_Closed;
        }

        private void OnMainMenuOnClick(object sender, EventArgs e)
        {
            _sequence = null;
            openFileDialog.InitialDirectory = SequenceService.SequenceDirectory;
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    _sequence = SequenceService.Instance.Load(openFileDialog.FileName);
                }

            }





            if (_sequence != null)
            {
                string[] timingSources;
                TimingProviders timingProviders = new TimingProviders(_sequence);

                try
                {
                    timingSources = timingProviders.GetAvailableTimingSources("Export");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

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





            if (_form == null)
            {
                InitializeForm();
            }

            _form.Show();


        }

        private void _AddMenu()
        {
            if (_application != null
                && _application.AppCommands != null)
            {
                AppCommand toolsMenu = _application.AppCommands.Find("Export");
                if (toolsMenu == null)
                {
                    toolsMenu = new AppCommand("Export", "Export");
                    _application.AppCommands.Add(toolsMenu);
                }
                var myMenu = new AppCommand(ID_MENU, "Falcon Pi Player");
                myMenu.Click += OnMainMenuOnClick;
                toolsMenu.Add(myMenu);
            }
        }

        private void _RemoveMenu()
        {
            if (_application != null
                && _application.AppCommands != null)
            {
                _application.AppCommands.Remove(ID_MENU);
            }
        }

        private void _form_Closed(object sender, EventArgs e)
        {
            _form.Dispose();
            _form = null;
        }
    }
}