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


namespace VixenModules.OutputFilter.LipSyncBreakdown
{
    public partial class LipSyncBreakdownSetup : Form, IElementSetupHelper
    {
        private LipSyncBreakdownData _data;
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        private DataTable holidayCoroDataTable;
        private DataTable defaultDataTable;
        private DataTable currentDataTable;

        public LipSyncBreakdownSetup()
        {
            InitializeComponent();
            SetupTemplates();
        }

        public LipSyncBreakdownSetup(LipSyncBreakdownData breakdownData)
        {
            InitializeComponent();
            _data = breakdownData;
            SetupTemplates();
            this.BreakdownItems = _data.BreakdownItems;
        }

        private void SetupTemplates()
        {
            defaultDataTable = new DataTable("Default");
            defaultDataTable.Columns.Add(" ", typeof(string));
            defaultDataTable.Columns.Add("Phoneme1", typeof(System.Boolean));

            //String #1
            DataRow row = defaultDataTable.NewRow();
            row.ItemArray = new object[] { "String#1", true };
            defaultDataTable.Rows.Add(row);


            holidayCoroDataTable = new DataTable("HolidaCoro");
            holidayCoroDataTable.Columns.Add(" ", typeof(string));
            holidayCoroDataTable.Columns.Add("AI", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("E", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("O", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("U", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("FV", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("L", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("MPB", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("WQ", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("etc", typeof(System.Boolean));
            holidayCoroDataTable.Columns.Add("Rest", typeof(System.Boolean));

            //Outline
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Outline", true, true, true, true, true, true, true, true, true, true };
            holidayCoroDataTable.Rows.Add(row);

            //Eyes Top
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Eyes Top", true, true, true, true, true, true, true, true, true, true };
            holidayCoroDataTable.Rows.Add(row);

            //Eyes Bottom
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Eyes Bottom", true, true, true, true, true, true, true, true, true, true };
            holidayCoroDataTable.Rows.Add(row);

            //Mouth Top
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Mouth Top", true, true, false, false, false, true, true, false, false, false };
            holidayCoroDataTable.Rows.Add(row);

            //Mouth Middle
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Mouth Middle", false, true, false, false, false, true, false, false, false, false };
            holidayCoroDataTable.Rows.Add(row);

            //Mouth Bottom
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Mouth Bottom", true, false, false, false, false, true, false, false, false, false };
            holidayCoroDataTable.Rows.Add(row);

            //Mouth Narrow
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Mouth Narrow", false, false, false, false, true, false, false, false, true, false };
            holidayCoroDataTable.Rows.Add(row);

            //Mouth Bottom
            row = holidayCoroDataTable.NewRow();
            row.ItemArray = new object[] { "Mouth O", false, false, true, true, false, false, false, true, true, false };
            holidayCoroDataTable.Rows.Add(row);

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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.RowIndex == -1) && (e.ColumnIndex >= 0))
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.RotateTransform(270);
                e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
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
                dr[j] = true;
            }
        }

        private void buttonAddString_Click(object sender, EventArgs e)
        {
            addNewString("New String");
        }

        private void control_DeleteRequested(object sender, EventArgs e)
        {
            /*
            LipSyncBreakdownItemControl control = sender as LipSyncBreakdownItemControl;
            if (control == null)
                return;

            removeControl(control);
             */
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

        private void button2_Click(object sender, EventArgs e)
        {
            LipSyncBreakdownAddPhoneme addDialog = new LipSyncBreakdownAddPhoneme();
            DialogResult result = addDialog.ShowDialog();
            bool doAdd = true;

            if (result == DialogResult.OK)
            {
                string requestedName = addDialog.PhonemeName;
                foreach(DataColumn dc in currentDataTable.Columns)
                {
                    if (dc.ColumnName == requestedName)
                    {
                        doAdd = false;
                        break;
                    }
                }
               
                if ((doAdd) && 
                    (!String.IsNullOrWhiteSpace(requestedName)) &&
                    (!String.IsNullOrEmpty(requestedName)))
                {
                    currentDataTable.Columns.Add(requestedName, typeof(System.Boolean));

                    int columnIndex = currentDataTable.Columns.Count - 1;
                    foreach (DataRow dr in currentDataTable.Rows)
                    {
                        dr[columnIndex] = true;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid or Duplicate Phoneme Name");
                }
            }
        }
    }
}
