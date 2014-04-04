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
            row.ItemArray = new object[] { "Mouth Bottom", false, false, true, true, false, false, false, true, true, false };
            holidayCoroDataTable.Rows.Add(row);

        }

        public List<LipSyncBreakdownItem> BreakdownItems
        {
            get
            {
                /*
                return
                    tableLayoutPanelControls.Controls.OfType<LipSyncBreakdownItemControl>().Select(
                        itemControl => itemControl.LipSyncBreakdownItem).ToList();
                 */
                return null;
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

                    modulesCreated++;
                    modulesConfigured++;
                }
            }

            MessageBox.Show(modulesCreated + " Phoneme Curves created, " + modulesConfigured + " configured, and " + modulesSkipped + " skipped.");
            return true;
        }

        private void LipSyncBreakdownSetup_Load(object sender, EventArgs e)
        {


            // let's just make up some hardcoded templates. Can expand on this later; probably don't need to,
            // people can request new ones and stuff if they want.
            comboBoxTemplates.Items.Clear();
            comboBoxTemplates.Items.Add("HolidayCoro");
            comboBoxTemplates.SelectedIndex = 0;

            updatedataGridView1(defaultDataTable);


        }

        private void updatedataGridView1(DataTable dataSource)
        {

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 100;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dataGridView1.DataSource = dataSource;
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

        private void buttonAddString_Click(object sender, EventArgs e)
        {
            //addControl(new LipSyncBreakdownItemControl());
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

        /*
        private void removeControl(LipSyncBreakdownItemControl control)
        {
            
            if (!tableLayoutPanelControls.Controls.Contains(control))
                return;

            tableLayoutPanelControls.Controls.Remove(control);
            control.DeleteRequested -= control_DeleteRequested;
             
        }
        */

        /*
        private void addControl(LipSyncBreakdownItemControl control)
        {
            
            control.DeleteRequested += control_DeleteRequested;
            tableLayoutPanelControls.Controls.Add(control);
        }
        */

        private void buttonApplyTemplate_Click(object sender, EventArgs e)
        {


            string template = comboBoxTemplates.SelectedItem.ToString();
            switch (template)
            {
                case "HolidayCoro":
                    updatedataGridView1(holidayCoroDataTable);
                    break;

                default:
                    Logging.Error("Lipsync Breakdown Setup: got an unknown template to apply: " + template);
                    MessageBox.Show("Error applying template: Unknown template.");
                    break;
            }
        }

        private void LipSyncBreakdownSetup_Layout(object sender, LayoutEventArgs e)
        {

        }
    }
}
