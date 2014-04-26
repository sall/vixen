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
        private DataTable currentDataTable;
        private ResourceManager lipSyncRM;
        private static Dictionary<string, Bitmap> _phonemeBitmaps = null;
        private List<string> _rowNames = new List<string>();

        public LipSyncBreakdownSetup()
        {
            InitializeComponent();
            LoadResourceBitmaps();
        }

        public LipSyncBreakdownSetup(LipSyncBreakdownData breakdownData)
        {
            InitializeComponent();
            _data = breakdownData;
            LoadResourceBitmaps();
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
            dt.Columns.Add("BaseColor", typeof(Color));

            foreach(string stringName in strings)
            {
                DataRow row = dt.NewRow();
                object[] data = new object[dt.Columns.Count];
                data[0] = stringName;
                for (int j = 1; j < dt.Columns.Count - 1; j++)
                {
                    data[j] = false;
                }
                
                data[dt.Columns.Count - 1] = Color.Green;

                row.ItemArray = data;
                dt.Rows.Add(row);
            }

            return dt;
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

                    for(int theCount = 1; theCount < dr.ItemArray.Count() - 1; theCount++)
                    {
                        item.PhonemeList.Add(
                            dr.Table.Columns[theCount].ColumnName,(Boolean)dr[theCount]
                         );
                    }

                    item.DefaultColor = (Color)dr[dr.ItemArray.Count() - 1];

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
                currentDataTable.Columns.Add("Default Color", typeof(Color));

                foreach (LipSyncBreakdownItem lsbItem in value)
                {
                    DataRow dr = currentDataTable.Rows.Add();
                    dr[0] = lsbItem.Name;
                    foreach(string key in theKeys)
                    {
                        dr[key] = lsbItem.PhonemeList[key];                    
                    }
                    dr["Default Color"] = lsbItem.DefaultColor;
                }
              }
        }
        
        public string HelperName
        {
            get { return "Phoneme Mapping"; }
        }

        private IEnumerable<IDataFlowComponentReference> _FindLeafOutputsOrPhonemeFilters(IDataFlowComponent component)
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

                if ((!children.Any()) || (component is LipSyncBreakdownModule))
                {
                    yield return new DataFlowComponentReference(component, i);
                }
                else
                {
                    foreach (IDataFlowComponent child in children)
                    {
                        foreach (IDataFlowComponentReference result in _FindLeafOutputsOrPhonemeFilters(child))
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
            string leafRootName;
            _rowNames.Clear();
            
            IEnumerable<ElementNode> leafElements = selectedNodes.SelectMany(x => x.GetLeafEnumerator()).Distinct();
            List<LipSyncBreakdownItem> preloadItems = new List<LipSyncBreakdownItem>();

            foreach (ElementNode leafNode in leafElements)
            {
                IDataFlowComponent elementComponent = VixenSystem.DataFlow.GetComponent(leafNode.Element.Id);
                IEnumerable<IDataFlowComponentReference> references = _FindLeafOutputsOrPhonemeFilters(elementComponent);
                foreach (IDataFlowComponentReference reference in references)
                {
                    if (reference.Component is LipSyncBreakdownModule)
                    {
                        LipSyncBreakdownModule currentModule = (reference.Component as LipSyncBreakdownModule);
                        preloadItems.AddRange(currentModule.BreakdownItems);
                        continue;
                    }
                }

                leafRootName = getFullTreeName(leafNode);
                if (leafRootName != null)
                {
                    _rowNames.Add(leafRootName);
               }
            }

            if (preloadItems.Count() == 0)
            {
                currentDataTable = SetupDataTable("Default", _rowNames.ToArray());
            }
            else
            {
                this.BreakdownItems = preloadItems;
            }
           
            leafElements = selectedNodes.SelectMany(x => x.GetLeafEnumerator()).Distinct();
            int modulesCreated = 0;
            int modulesConfigured = 0;
            int modulesSkipped = 0;

            DialogResult dr = ShowDialog();
            if (dr != DialogResult.OK)
                return false;

            foreach (ElementNode leafNode in leafElements)
            {
                IDataFlowComponent elementComponent = VixenSystem.DataFlow.GetComponent(leafNode.Element.Id);
                IEnumerable<IDataFlowComponentReference> references = _FindLeafOutputsOrPhonemeFilters(elementComponent);

                LipSyncBreakdownItem currentItem = null;
                foreach (LipSyncBreakdownItem item in this.BreakdownItems)
                {
                    if (item.Name == getFullTreeName(leafNode))
                    {
                        currentItem = item;
                        break;
                    }
                }

                foreach (IDataFlowComponentReference reference in references)
                {
                    int outputIndex = reference.OutputIndex;

                    if ((reference.Component is LipSyncBreakdownModule) && (currentItem != null))
                    {
                        LipSyncBreakdownModule currentModule = (reference.Component as LipSyncBreakdownModule);
                        currentModule.BreakdownItems = new List<LipSyncBreakdownItem>();
                        currentModule .BreakdownItems.Add(currentItem);
                        currentModule.CreateOutputs();
                        modulesConfigured++;
                        continue;
                    }

                    LipSyncBreakdownModule breakdownModule = ApplicationServices.Get<IOutputFilterModuleInstance>(LipSyncBreakdownDescriptor.ModuleId) as LipSyncBreakdownModule;
                    VixenSystem.DataFlow.SetComponentSource(breakdownModule, reference.Component, outputIndex);
                    VixenSystem.Filters.AddFilter(breakdownModule);

                    breakdownModule.BreakdownItems = new List<LipSyncBreakdownItem>();
                    breakdownModule.BreakdownItems.Add(currentItem);
                    breakdownModule.CreateOutputs();

                    modulesCreated++;
                    modulesConfigured++;
                }
            }

            MessageBox.Show(modulesCreated + " Phoneme Curves created, " + modulesConfigured + " configured, and " + modulesSkipped + " skipped.");
            return true;
        }
        
        private string getFullTreeName(ElementNode leafNode)
        {
            string retVal = null;

            if (leafNode.Parents.First().Name != "Root")
            {
                retVal = getFullTreeName(leafNode.Parents.First()) + "-" + leafNode.Name;
            }
            else
            {
                retVal = leafNode.Name;
            }
            return retVal;
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

            if ((e.RowIndex > -1) && (e.ColumnIndex == currentDataTable.Columns.Count - 1))
            {
                using (SolidBrush paintBrush = new SolidBrush((Color)e.Value))
                {
                    e.Graphics.FillRectangle(paintBrush,e.CellBounds);
                    e.CellStyle.ForeColor = (Color)e.Value;
                    e.CellStyle.SelectionForeColor = (Color)e.Value;
                }
                
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        }

        private void control_DeleteRequested(object sender, EventArgs e)
        {

        }
        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex > -1) && (e.ColumnIndex == currentDataTable.Columns.Count - 1))
            {
                // Show the color dialog.
                DialogResult result = colorDialog1.ShowDialog();
                
                // See if user pressed ok.
                if (result == DialogResult.OK)
                {
                    DataGridViewCell cell = (DataGridViewCell) dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Set form background to the selected color.
                    cell.Value = colorDialog1.Color;
                }
            }

        }

    }
}
