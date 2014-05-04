﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace VixenModules.App.LipSyncMap
{
    public partial class LipSyncMapEditor : Form
    {
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        private DataTable currentDataTable;
        private ResourceManager lipSyncRM;
        private static Dictionary<string, Bitmap> _phonemeBitmaps = null;
        private List<string> _rowNames = null;
        private static string COLOR_COLUMN_NAME = "Color";
        private bool _doMatrixUpdate = false;
        private static object predicateArg = null;

        public LipSyncMapEditor()
        {
            _rowNames = new List<string>();
            this.LibraryMappingName = "Default";
            InitializeComponent();
            LoadResourceBitmaps();
        }

        public LipSyncMapEditor(LipSyncMapData mapData)
        {
            InitializeComponent();
            LoadResourceBitmaps();
            this.MapData = mapData;
        }

        public string LibraryMappingName
        {
            get { return nameTextBox.Text; }
            set
            {
                nameTextBox.Text = value;
            }
        }

        private LipSyncMapData _mapping;


        private DataTable BuildDialogFromMap(LipSyncMapData data)
        {
            nameTextBox.Text = data.LibraryReferenceName;
            data.MapItems.Sort(delegate(LipSyncMapItem x, LipSyncMapItem y)
            {
                if (x.StringNum < y.StringNum)
                {
                    return -1;
                }
                else if (x.StringNum > y.StringNum)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            DataTable dt = new DataTable(nameTextBox.Text);
            dt.Columns.Add(" ", typeof(string));

            foreach (string key in _phonemeBitmaps.Keys)
            {
                dt.Columns.Add(key, typeof(System.Boolean));
            }

            dt.Columns.Add(COLOR_COLUMN_NAME, typeof(Color));
            
            if (data.MapItems.Count == 0)
            {
                DataRow dr = dt.Rows.Add();
                dr[0] = "String 1";
                dr[COLOR_COLUMN_NAME] = Color.White;
            }
            else
            {
                bool result = false;
                foreach (LipSyncMapItem lsbItem in data.MapItems)
                {
                    DataRow dr = dt.Rows.Add();
                    dr[0] = lsbItem.Name;
                    foreach (string key in _phonemeBitmaps.Keys)
                    {
                        if (lsbItem.PhonemeList.TryGetValue(key, out result) == true)
                        {
                            dr[key] = result;
                        }
                        else
                        {
                            dr[key] = false;
                        }
                    }
                    dr[COLOR_COLUMN_NAME] = lsbItem.ElementColor;
                }
            }

            dt.Columns[" "].ReadOnly = true;
            dt.Columns[COLOR_COLUMN_NAME].ReadOnly = true;

            return dt;
        }

        private static bool MatchNamePredicate(LipSyncMapItem item)
        {
            return item.Name.Equals((string)predicateArg);
        }

        private void BuilMapDataFromDialog()
        {
            int currentRow = 0;

            _mapping.StringCount = stringsUpDown.SelectedIndex;
            _mapping.MapItems.Clear();

            for (int stringNum = 0; stringNum < (int)stringsUpDown.SelectedItem; stringNum++)
            {
                DataRow dr = currentDataTable.Rows[currentRow];
                LipSyncMapItem item = new LipSyncMapItem();
                //item.Name = dr[0].ToString();
                item.StringNum = stringNum;

                for (int theCount = 1; theCount < dr.ItemArray.Count() - 1; theCount++)
                {
                    bool checkVal =
                        (dr[theCount].GetType() == typeof(Boolean)) ? (Boolean)dr[theCount] : false;
                    item.PhonemeList.Add(
                        dr.Table.Columns[theCount].ColumnName, checkVal
                        );
                }
                item.ElementColor = (Color)dr[dr.ItemArray.Count() - 1];

                _mapping.MapItems.Add(item);
                currentRow++;
                if (currentRow >= currentDataTable.Rows.Count)
                {
                    return;
                }
            }
        }

        public LipSyncMapData MapData
        {
            get
            {
                BuilMapDataFromDialog();
                return _mapping;
            }

            set
            {
                _mapping = value;
                currentDataTable = BuildDialogFromMap(value);
                updatedataGridView1();
            }
        }

        public string HelperName
        {
            get { return "Phoneme Mapping"; }
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
                    _phonemeBitmaps.Add("REST", (Bitmap)lipSyncRM.GetObject("rest"));
                    _phonemeBitmaps.Add("U", (Bitmap)lipSyncRM.GetObject("U"));
                    _phonemeBitmaps.Add("WQ", (Bitmap)lipSyncRM.GetObject("WQ"));
                }
            }
        }

        private void LipSyncMapSetup_Load(object sender, EventArgs e)
        {
            stringsUpDown.Items.Clear();

            _doMatrixUpdate = false;
            for (int j = 1; j < 1000; j++)
            {
                stringsUpDown.Items.Add(j);
            }

            _doMatrixUpdate = false;
            stringsUpDown.SelectedIndex = _mapping.StringCount;
            _doMatrixUpdate = true;

            updatedataGridView1();
        }

        private void updatedataGridView1()
        {
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 100;
            dataGridView1.MultiSelect = true;
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
            dataGridView1.Columns[COLOR_COLUMN_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;

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

            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex >= currentDataTable.Columns.Count - 1)
                {
                    using (SolidBrush paintBrush = new SolidBrush((Color)e.Value))
                    {
                        e.Graphics.FillRectangle(paintBrush,
                                                    e.CellBounds.X + 2,
                                                    e.CellBounds.Y + 2,
                                                    e.CellBounds.Width - 3,
                                                    e.CellBounds.Height - 3);

                        e.Graphics.DrawRectangle(new Pen(Color.Gray, 2), e.CellBounds);

                        e.CellStyle.ForeColor = (Color)e.Value;
                        e.CellStyle.SelectionForeColor = (Color)e.Value;
                        e.CellStyle.SelectionBackColor = Color.Black;
                        e.CellStyle.BackColor = (Color)e.Value;
                    }
                }
                else
                {
                    e.CellStyle.BackColor =
                        ((e.RowIndex % 2) == 0) ? Color.White : Color.LightGreen;
                }
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }


        private void LipSyncBreakdownSetup_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(this.Size.Width - 40, this.Size.Height - 150);
        }

        private static bool ShrinkMapPredicate(LipSyncMapItem item)
        {
            return (item.StringNum > ((int)predicateArg)) ? true : false;
        }

        private void reconfigureDataTable()
        {
            LipSyncMapItem mapItem;

            if (_doMatrixUpdate == true)
            {
                //If Shrinking
                //BuilMapDataFromTable();
                predicateArg = (int)stringsUpDown.SelectedItem - 1;
                _mapping.MapItems.RemoveAll(ShrinkMapPredicate);

                for (int currentString = 0; currentString < (int)stringsUpDown.SelectedItem; currentString++)
                {
                    mapItem =
                        _mapping.MapItems.Find(x => (x.StringNum == currentString));

                    if (mapItem == null)
                    {
                        mapItem = new LipSyncMapItem();
                        mapItem.ElementColor = Color.White;
                        mapItem.StringNum = currentString;

                        _mapping.MapItems.Add(mapItem);
                    }
                }
                currentDataTable.Rows.Clear();
                currentDataTable = BuildDialogFromMap(_mapping);
                updatedataGridView1();
            }
        }

        private void stringsUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            reconfigureDataTable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex > -1) && (e.ColumnIndex > 0))
            {
                BuilMapDataFromDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int lastColumn = dataGridView1.Columns.Count - 1;
            if (e.RowIndex > -1)
            {
                if ((e.ColumnIndex > 0) && (e.ColumnIndex < lastColumn))
                {
                    int bias = 0;
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if ((cell.ColumnIndex > 0) && (cell.ColumnIndex < lastColumn))
                        {
                            bias = ((bool)cell.Value) ? bias + 1 : bias - 1;
                        }

                    }

                    bool newValue = (bias > 0) ? false : true;

                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if ((cell.ColumnIndex > 0) && (cell.ColumnIndex < lastColumn))
                        {
                            cell.Value = newValue;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int lastColumn = currentDataTable.Columns.Count - 1;
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == lastColumn)
                {
                    // Show the color dialog.
                    DialogResult result = colorDialog1.ShowDialog();

                    // See if user pressed ok.
                    if (result == DialogResult.OK)
                    {
                        currentDataTable.Columns[COLOR_COLUMN_NAME].ReadOnly = false;
                        foreach (DataGridViewCell selCell in dataGridView1.SelectedCells)
                        {
                            if (selCell.ColumnIndex == lastColumn)
                            {
                                selCell.Value = colorDialog1.Color;
                            }
                        }
                        DataGridViewCell cell = (DataGridViewCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        cell.Value = colorDialog1.Color;
                        currentDataTable.Columns[COLOR_COLUMN_NAME].ReadOnly = true;
                    }
                }
            }
        }
        
    }
}


/*
        public bool Perform(IEnumerable<ElementNode> selectedNodes)
        {
            return false;

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
                    if (reference.Component is LipSyncBreakdownLibrary)
                    {
                        LipSyncBreakdownLibrary currentLibrary = (reference.Component as LipSyncBreakdownLibrary);
                        preloadItems.AddRange(currentLibrary.BreakdownItems);
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
                    }
                }

                foreach (IDataFlowComponentReference reference in references)
                {
                    int outputIndex = reference.OutputIndex;

                    if ((reference.Component is LipSyncBreakdownLibrary) && (currentItem != null))
                    {
                        LipSyncBreakdownLibrary currentModule = (reference.Component as LipSyncBreakdownLibrary);
                        currentModule.BreakdownItems = new List<LipSyncBreakdownItem>();
                        currentModule.BreakdownItems.Add(currentItem);
                        modulesConfigured++;
                        continue;
                    }

                    LipSyncBreakdownLibrary breakdownLibrary = ApplicationServices.Get<IOutputFilterModuleInstance>(LipSyncBreakdownDescriptor.ModuleID) as LipSyncBreakdownLibrary;

                    breakdownLibrary.BreakdownItems = new List<LipSyncBreakdownItem>();
                    breakdownLibrary.BreakdownItems.Add(currentItem);

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
                retVal = getFullTreeName(leafNode.Parents.First()) + "\\" + leafNode.Name;
            }
            else
            {
                retVal = leafNode.Name;
            }
            return retVal;
        }
*/