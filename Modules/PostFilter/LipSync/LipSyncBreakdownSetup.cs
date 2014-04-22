using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vixen.Data.Flow;
using Vixen.Module.Effect;
using Vixen.Module.OutputFilter;
using Vixen.Module.Property;
using Vixen.Rule;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Module;
using System.Resources;
using System.Reflection;
namespace VixenModules.OutputFilter.LipSyncBreakdown
{
    public partial class LipSyncBreakdownSetup : Form, IElementSetupHelper
    {
        private LipSyncBreakdownData _data;
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        private DataTable holidayCoroDataTable;
        private DataTable defaultDataTable;
        private DataTable currentDataTable;
        private ResourceManager lipSyncRM;
        private static Dictionary<string, Bitmap> _phonemeBitmaps = null;

        public LipSyncBreakdownSetup()
        {
            InitializeComponent();
            LoadResourceBitmaps();
            SetupTemplates();
        }

        public LipSyncBreakdownSetup(LipSyncBreakdownData breakdownData)
        {
            InitializeComponent();
            _data = breakdownData;
            LoadResourceBitmaps();
            SetupTemplates();
            this.BreakdownItems = _data.BreakdownItems;
        }

        private DataTable SetupDataTable(string name, string[] strings)
        {
            DataTable dt = new DataTable(name);
            dt.Columns.Add(" ", typeof(string));

            foreach (string key in _phonemeBitmaps.Keys)
            {
                if (key != "PREVIEW")
                {
                    dt.Columns.Add(key, typeof(System.Boolean));
                }
            }

            foreach(string stringName in strings)
            {
                DataRow row = dt.NewRow();
                object[] data = new object[dt.Columns.Count];
                data[0] = stringName;
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    data[j] = false;
                }
                row.ItemArray = data;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private void SetupTemplates()
        {
            defaultDataTable = SetupDataTable("Default", new string[] {"String #1"});



            holidayCoroDataTable = SetupDataTable("HolidayCoro", 
                new string[] { "Outline", "Eyes Top", "Eyes Bottom", "Mouth Top", 
                                "Mouth Middle", "Mouth Bottom", "Mouth Narrow", "Mouth O" });

            currentDataTable = defaultDataTable.Copy();

            if (comboBoxTemplates.Items.Count == 0)
            {
                // let's just make up some hardcoded templates. Can expand on this later; probably don't need to,
                // people can request new ones and stuff if they want.
                comboBoxTemplates.Items.Clear();
                comboBoxTemplates.Items.Add("Default");
                comboBoxTemplates.Items.Add("HolidayCoro");
                comboBoxTemplates.SelectedIndex = 0; 
            }
        }

        public List<LipSyncBreakdownItem> BreakdownItems
        {
            get
            {
                List<LipSyncBreakdownItem> retVal = new List<LipSyncBreakdownItem>();
                foreach(DataRow dr in currentDataTable.Rows)
                {
                    LipSyncBreakdownItem item = new LipSyncBreakdownItem();
                    item.Name = dr[0].ToString();

                    for(int theCount = 1; theCount < dr.ItemArray.Count(); theCount++)
                    {
                        item.PhonemeList.Add(
                            dr.Table.Columns[theCount].ColumnName,(Boolean)dr[theCount]
                         );
                    }

                    retVal.Add(item);
                }
                return retVal;
            }

            set
            {
                currentDataTable = new DataTable();
                currentDataTable.Columns.Add(" ", typeof(string));
                Dictionary<string,Boolean>.KeyCollection theKeys =  value[0].PhonemeList.Keys;
                foreach(string columnName in theKeys)
                {
                    currentDataTable.Columns.Add(columnName, typeof(Boolean));
                }

                foreach (LipSyncBreakdownItem lsbItem in value)
                {
                    DataRow dr = currentDataTable.Rows.Add();
                    dr[0] = lsbItem.Name;
                    foreach(string key in theKeys)
                    {
                        dr[key] = lsbItem.PhonemeList[key];                    
                    }
                }
              }
        }
        
        public string HelperName
        {
            get { return "Phoneme Mapping"; }
        }

        private IEnumerable<IDataFlowComponentReference> _FindLeafOutputsOrDimmingCurveFilters(IDataFlowComponent component)
        {
            if (component == null)
            {
                yield break;
            }

            if (component.Outputs == null || component.OutputDataType == DataFlowType.None)
            {
                yield break;
            }

            for (int i = 0; i < component.Outputs.Length; i++)
            {
                IEnumerable<IDataFlowComponent> children = VixenSystem.DataFlow.GetDestinationsOfComponentOutput(component, i);

                if (!children.Any())
                {
                    yield return new DataFlowComponentReference(component, i);
                }
                else
                {
                    foreach (IDataFlowComponent child in children)
                    {
                        foreach (IDataFlowComponentReference result in _FindLeafOutputsOrDimmingCurveFilters(child))
                        {
                            yield return result;
                        }
                    }
                }
            }
        }

        private void LoadResourceBitmaps()
        {
            if (_phonemeBitmaps == null)
            {
                Assembly assembly = Assembly.Load("LipSync, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                if (assembly != null)
                {
                    lipSyncRM = new ResourceManager("VixenModules.Effect.LipSync.LipSyncResources", assembly);
                    _phonemeBitmaps = new Dictionary<string, Bitmap>();
                    _phonemeBitmaps.Add("AI", (Bitmap)lipSyncRM.GetObject("AI"));
                    _phonemeBitmaps.Add("E", (Bitmap)lipSyncRM.GetObject("E"));
                    _phonemeBitmaps.Add("ETC", (Bitmap)lipSyncRM.GetObject("etc"));
                    _phonemeBitmaps.Add("FV", (Bitmap)lipSyncRM.GetObject("FV"));
                    _phonemeBitmaps.Add("L", (Bitmap)lipSyncRM.GetObject("L"));
                    _phonemeBitmaps.Add("MBP", (Bitmap)lipSyncRM.GetObject("MBP"));
                    _phonemeBitmaps.Add("O", (Bitmap)lipSyncRM.GetObject("O"));
                    _phonemeBitmaps.Add("PREVIEW", (Bitmap)lipSyncRM.GetObject("Preview"));
                    _phonemeBitmaps.Add("REST", (Bitmap)lipSyncRM.GetObject("rest"));
                    _phonemeBitmaps.Add("U", (Bitmap)lipSyncRM.GetObject("U"));
                    _phonemeBitmaps.Add("WQ", (Bitmap)lipSyncRM.GetObject("WQ"));
                }
            }
        }

        private void LipSyncBreakdownSetup_Load(object sender, EventArgs e)
        {
             updatedataGridView1();
        }

        public bool Perform(IEnumerable<ElementNode> selectedNodes)
        {
            DialogResult dr = ShowDialog();
            if (dr != DialogResult.OK)
                return false;

            IEnumerable<ElementNode> leafElements = selectedNodes.SelectMany(x => x.GetLeafEnumerator()).Distinct();
            int modulesCreated = 0;
            int modulesConfigured = 0;
            int modulesSkipped = 0;

            foreach (ElementNode leafNode in leafElements)
            {

                // get the leaf 'things' to deal with -- ie. either existing dimming curves on a filter branch, or data component outputs
                // (if we're adding new ones, ignore any existing dimming curves: always go to the outputs and we'll add new ones)
                IDataFlowComponent elementComponent = VixenSystem.DataFlow.GetComponent(leafNode.Element.Id);
                IEnumerable<IDataFlowComponentReference> references = _FindLeafOutputsOrDimmingCurveFilters(elementComponent);

                foreach (IDataFlowComponentReference reference in references)
                {
                    int outputIndex = reference.OutputIndex;

                    outputIndex = 0;

                    // assuming we're making a new one and going from there
                    LipSyncBreakdownModule breakdownModule = ApplicationServices.Get<IOutputFilterModuleInstance>(LipSyncBreakdownDescriptor.ModuleId) as LipSyncBreakdownModule;
                    VixenSystem.DataFlow.SetComponentSource(breakdownModule, reference.Component, outputIndex);
                    VixenSystem.Filters.AddFilter(breakdownModule);

                    breakdownModule.BreakdownItems = this.BreakdownItems;
                    breakdownModule.CreateOutputs();

                    modulesCreated++;
                    modulesConfigured++;
                }
            }

            MessageBox.Show(modulesCreated + " Phoneme Curves created, " + modulesConfigured + " configured, and " + modulesSkipped + " skipped.");
            return true;
        }
        
        private void updatedataGridView1()
        {
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 100;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dataGridView1.DataSource = currentDataTable;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            for (int j = 1; j < dataGridView1.Columns.Count; j++)
            {
                dataGridView1.Columns[j].Width = 53;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            string phonemeStr;
            Bitmap phonemeBitmap;

            if ((e.RowIndex == -1) && (e.ColumnIndex >= 0))
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.RotateTransform(270);
                phonemeStr = e.FormattedValue.ToString();
                if (_phonemeBitmaps.TryGetValue(phonemeStr, out phonemeBitmap))
                {
                    e.Graphics.DrawImage(new Bitmap(_phonemeBitmaps[phonemeStr], 48, 48), 5, 5);
                    e.Graphics.DrawString(phonemeStr, e.CellStyle.Font, Brushes.Black, 55, 5);
                }
                else
                {
                    e.Graphics.DrawString(phonemeStr, e.CellStyle.Font, Brushes.Black, 5, 5);
                }
   
                e.Graphics.ResetTransform();
                e.Handled = true;
            }
        }

        private void addNewString(string stringName)
        {
            DataRow dr = currentDataTable.Rows.Add(stringName);

            int cellCount = currentDataTable.Columns.Count;

            for (int j = 1; j < cellCount; j++)
            {
                dr[j] = false;
            }
        }

        private void buttonAddString_Click(object sender, EventArgs e)
        {
            addNewString("New String");
        }

        private void control_DeleteRequested(object sender, EventArgs e)
        {

        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            string template = comboBoxTemplates.SelectedItem.ToString();
            switch (template)
            {
                case "Default":
                    {
                        currentDataTable = defaultDataTable.Copy();
                        updatedataGridView1();
                        break;
                    }
                case "HolidayCoro":
                    {
                        currentDataTable = holidayCoroDataTable.Copy();
                        updatedataGridView1();
                        break;
                    }

                default:
                    Logging.Error("Lipsync Breakdown Setup: got an unknown template to apply: " + template);
                    MessageBox.Show("Error applying template: Unknown template.");
                    break;
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }
    }
}
