using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using Vixen.Sys;

namespace VixenModules.App.LipSyncApp
{
    public partial class LipSyncMapMatrixEditor : Form
    {
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        private LipSyncMapData _mapping;
        private DataTable currentDataTable;
        private static Dictionary<string, Bitmap> _phonemeBitmaps = null;
        private List<string> _rowNames = null;
        private bool _doMatrixUpdate = false;
        private int zoomSteps = 1;
        private const int ZOOM_STEP_DELTA = 1;
        private const int CELL_BASE_WIDTH = 50;
        private int currentPhonemeIndex;
        private string[] phonemeNames;

        public LipSyncMapMatrixEditor()
        {
            _rowNames = new List<string>();
            this.LibraryMappingName = "Default";
            InitializeComponent();
            LoadResourceBitmaps();
        }

        public LipSyncMapMatrixEditor(LipSyncMapData mapData)
        {
            InitializeComponent();
            _doMatrixUpdate = false;
            LoadResourceBitmaps();
            this.MapData = mapData;
            _doMatrixUpdate = true;
        }

        private string _name;

        public string LibraryMappingName
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        private int CalcNumDataGridRows()
        {
            return (horizontalRadio.Checked) ?
                Convert.ToInt32(stringsUpDown.Value) : Convert.ToInt32(pixelsUpDown.Value);
        }

        private int CalcNumDataGridCols()
        {
            return (horizontalRadio.Checked) ?
                Convert.ToInt32(pixelsUpDown.Value) : Convert.ToInt32(stringsUpDown.Value);
        }

        private LipSyncMapItem FindRenderMapItem (int row, int column)
        {

            int numCols = CalcNumDataGridCols();
            int numRows = CalcNumDataGridRows();

            int displayRow = (horizontalRadio.Checked) ? row : column;
            int displayCol = (verticalRadio.Checked) ? row : column;
            
            int startMapIndex = _mapping.MapItems.FindIndex(
            delegate(LipSyncMapItem mapItem)
            {
                return mapItem.Name.Equals(startingElementCombo.Text);
            });

            LipSyncMapItem retVal = null;


            int calcIndex = (horizontalRadio.Checked) ? 
                ((numCols * row) + column) + startMapIndex : ((numRows * column) + row) + startMapIndex;

            if (calcIndex < _mapping.MapItems.Count)
            {
                retVal = _mapping.MapItems.ElementAt(calcIndex);
            }

            return retVal;
        }

        private DataTable BuildDialogFromMap(LipSyncMapData data)
        {

            int numCols = CalcNumDataGridCols();
            int numRows = CalcNumDataGridRows();

            DataTable dt = new DataTable(_name);

            _name = data.LibraryReferenceName;

            for (int col = 0; col < numCols; col++)
            {
                dt.Columns.Add(col.ToString(), typeof(Color));
            }

            for (int row = 0; row < numRows; row++)
            {
                DataRow dr = dt.Rows.Add();
            }

            LipSyncMapItem mapItem = null;

            for (int row = 0; row < numRows; row++ )
            {
                for (int col = 0; col < numCols; col++)
                {
                    mapItem = FindRenderMapItem(row,col);
                    DataRow dr = dt.Rows[row];

                    if (mapItem != null)
                    {
                        dr[col] = 
                            (mapItem.PhonemeList[CurrentPhonemeString] == false) ? 
                            Color.Black : mapItem.ElementColor;
                    }
                    else
                    {
                        dr[col] = Color.Gray;
                    }
                }
            }

            return dt;
        }

        private string CurrentPhonemeString 
        {
            get
            {
                return phonemeNames[currentPhonemeIndex];
            }
        }

        private ElementNode FindElementNode(string elementName)
        {
            ElementNode theNode = VixenSystem.Nodes.ToList().Find(
                delegate(ElementNode node)
                {
                    if (node.IsLeaf)
                    {
                        return node.Element.Name.Equals(elementName);
                    }
                    else
                    {
                        return node.Name.Equals(elementName);
                    }
                    
                }
            );

            return theNode;
        }

        private void BuilMapDataFromDialog()
        {
            int currentRow = 0;

            _mapping.StringCount = _rowNames.Count();
            _mapping.MapItems.Clear();

            for (int stringNum = 0; stringNum < _rowNames.Count; stringNum++)
            {
                DataRow dr = currentDataTable.Rows[currentRow];
                string elementName = dr[0].ToString();
                LipSyncMapItem item = new LipSyncMapItem();
                ElementNode theNode = FindElementNode(elementName);
                    
                item.Name = dr[0].ToString();
                item.ElementGuid = theNode.Id;
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
                ElementNode tempNode = null;
                _mapping = value;
                _rowNames = new List<string>();
                try
                {
                    foreach(LipSyncMapItem mapItem in _mapping.MapItems)
                    {
                        tempNode = VixenSystem.Nodes.GetElementNode(mapItem.ElementGuid);
                        if (tempNode == null)
                        {
                            continue;
                        }
                        
                        if (tempNode.Element != null)
                        {
                            _rowNames.Add(tempNode.Element.Name);
                        }
                        else
                        {
                            _rowNames.Add(tempNode.Name);
                        }
                    }
                }
                catch (Exception e) { };

                configureStartingElementCombo();
                currentDataTable = BuildDialogFromMap(value);
                updatedataGridView1();
            }
        }

        public string HelperName
        {
            get { return "Phoneme Mapping"; }
        }

        private void NextPhonmeIndex()
        {
            currentPhonemeIndex++;
            currentPhonemeIndex %= phonemeNames.Count();
            SetPhonemePicture();
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void PrevPhonemeIndex()
        {
            currentPhonemeIndex = 
                (currentPhonemeIndex == 0) ? phonemeNames.Count() : currentPhonemeIndex;
            currentPhonemeIndex--;
            SetPhonemePicture();
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void SetPhonemePicture()
        {
            phonemePicture.Image = 
                new Bitmap(_phonemeBitmaps[CurrentPhonemeString], 48, 48);
            phonemeLabel.Text = CurrentPhonemeString;
        }

        private void LoadResourceBitmaps()
        {
            if (_phonemeBitmaps == null)
            {
                Assembly assembly = Assembly.Load("LipSyncApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                if (assembly != null)
                {
                    ResourceManager lipSyncRM = new ResourceManager("VixenModules.App.LipSyncApp.LipSyncResources", assembly);
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

            phonemeNames = _phonemeBitmaps.Keys.ToArray();
            currentPhonemeIndex = 0;
            SetPhonemePicture();
        }

        private void configureStartingElementCombo()
        {
            startingElementCombo.Items.Clear();
            startingElementCombo.Items.AddRange(_mapping.MapItems.ToArray());
            startingElementCombo.SelectedIndex = 0;
        }
        private void LipSyncMapSetup_Load(object sender, EventArgs e)
        {
            updatedataGridView1();
            configureStartingElementCombo();
        }

        private void updatedataGridView1()
        {
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dataGridView1.MultiSelect = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.DataSource = currentDataTable;

            int x = CalcNumDataGridCols();
            int y = CalcNumDataGridRows();

            DataGridViewColumn dgvCol;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                dgvCol = dataGridView1.Columns[j];
                dgvCol.Width = CELL_BASE_WIDTH + (int)(ZOOM_STEP_DELTA * zoomSteps);
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvCol.HeaderCell.Value = (verticalRadio.Checked) ? (j * y).ToString() : j.ToString();
            }

            DataGridViewRow dgvRow;
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                dgvRow = dataGridView1.Rows[j];
                dgvRow.Height = CELL_BASE_WIDTH + (int)(ZOOM_STEP_DELTA * zoomSteps);
                dgvRow.HeaderCell.Value = (horizontalRadio.Checked) ? (j * x).ToString() : j.ToString();
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            string colStr;

            if ((e.RowIndex == -1) && (e.ColumnIndex >= 0))
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.RotateTransform(270);
                colStr = e.FormattedValue.ToString();
                e.Graphics.DrawString(colStr, e.CellStyle.Font, Brushes.Black, 5, 5);

                e.Graphics.ResetTransform();
                e.Handled = true;

            }

            Color useColor;
            if (e.RowIndex > -1)
            {
 
                if (e.ColumnIndex >= 0)
                {
                    useColor = ((e.FormattedValue == null) ||
                        (e.FormattedValue.Equals(""))) ? Color.Black : (Color)e.Value;

                    using (SolidBrush paintBrush = new SolidBrush(useColor))
                    {
                        e.Graphics.FillRectangle(paintBrush,
                                                    e.CellBounds.X + 2,
                                                    e.CellBounds.Y + 2,
                                                    e.CellBounds.Width - 3,
                                                    e.CellBounds.Height - 3);

                        e.Graphics.DrawRectangle(new Pen(Color.Gray, 2), e.CellBounds);

                        e.CellStyle.ForeColor = useColor;
                        e.CellStyle.SelectionForeColor = useColor;
                        e.CellStyle.SelectionBackColor = useColor;
                        e.CellStyle.BackColor = useColor;
                    }

                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }


        private void LipSyncBreakdownSetup_Resize(object sender, EventArgs e)
        {
            //dataGridView1.Size = new Size(this.Size.Width - 40, this.Size.Height - 150);
        }

        private void reconfigureDataTable()
        {
            if (_doMatrixUpdate == true)
            {
                currentDataTable.Rows.Clear();
                currentDataTable = BuildDialogFromMap(_mapping);
                updatedataGridView1();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int bias = 0;
            foreach (DataGridViewCell selCell in dataGridView1.SelectedCells)
            {
                bias = (selCell.Value.Equals(lipSyncMapColorCtrl1.Color)) ? bias + 1 : bias - 1;
            }

            Color newValue = (bias > 0) ? Color.Black : lipSyncMapColorCtrl1.Color;

            foreach (DataGridViewCell selCell in dataGridView1.SelectedCells)
            {
                LipSyncMapItem item = 
                    FindRenderMapItem(selCell.OwningRow.Index, selCell.OwningColumn.Index);
                selCell.Value = (item == null) ? Color.Gray : newValue;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void colsUpDown_ValueChanged(object sender, EventArgs e)
        {
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void rowsUpDown_ValueChanged(object sender, EventArgs e)
        {
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void zoomTrackbar_ValueChanged(object sender, EventArgs e)
        {
            zoomSteps = zoomTrackbar.Value;
            if (zoomSteps == 0)
            {
                zoomSteps = 1;
            }
            updatedataGridView1();
        }

        private void Vertical_CheckedChanged(object sender, EventArgs e)
        {
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void Horizontal_CheckedChanged(object sender, EventArgs e)
        {
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LipSyncNodeSelect nodeSelectDlg = new LipSyncNodeSelect();
            nodeSelectDlg.NodeNames = _rowNames;

            DialogResult dr = nodeSelectDlg.ShowDialog();
            if ((dr == DialogResult.OK) && (nodeSelectDlg.Changed == true))
            {
                List<LipSyncMapItem> newMappings = new List<LipSyncMapItem>();
                LipSyncMapItem tempMapItem = null;

                _rowNames.Clear();
                _rowNames.AddRange(nodeSelectDlg.NodeNames);

                foreach (string nodeName in nodeSelectDlg.NodeNames)
                {
                    tempMapItem = _mapping.MapItems.Find(
                        delegate(LipSyncMapItem item)
                        {
                            return item.Name.Equals(nodeName);
                        });

                    if (tempMapItem != null)
                    {
                        newMappings.Add(tempMapItem);
                    }
                    else
                    {
                        newMappings.Add(new LipSyncMapItem(nodeName, -1));
                    }
                }

                _mapping.MapItems.Clear();

                int stringCount = 0;
                foreach (LipSyncMapItem mapItem in newMappings)
                {
                    mapItem.StringNum = stringCount++;
                    _mapping.MapItems.Add(mapItem);
                }

                configureStartingElementCombo();
                reconfigureDataTable();
            }
        }

        private void startingElementCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDataTable = BuildDialogFromMap(_mapping);
            updatedataGridView1();
        }

        private void prevPhonemeButton_Click(object sender, EventArgs e)
        {
            PrevPhonemeIndex();
        }

        private void nextPhonemeButton_Click(object sender, EventArgs e)
        {
            NextPhonmeIndex();
        }
    }
}
