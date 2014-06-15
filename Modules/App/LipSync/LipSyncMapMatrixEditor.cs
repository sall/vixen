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
        private LipSyncMapData _origMapping;
        private LipSyncMapData _newMapping;
        private Dictionary<PhonemeType, DataTable> dataTables;
        private static Dictionary<PhonemeType, Bitmap> _phonemeBitmaps = null;
        private List<string> _rowNames = null;
        private bool _doMatrixUpdate = true;
        private int zoomSteps = 1;
        private const int ZOOM_STEP_DELTA = 1;
        private const int CELL_BASE_WIDTH = 50;
        private int currentPhonemeIndex;
        private PhonemeType[] phonemeArray;
        private bool verticalChecked;
        private bool horizontalChecked;
        
        

        public LipSyncMapMatrixEditor()
        {
            _rowNames = new List<string>();
            this.LibraryMappingName = "Default";
            InitializeComponent();
            LoadResourceBitmaps();
            dataTables = new Dictionary<PhonemeType, DataTable>();
        }

        public LipSyncMapMatrixEditor(LipSyncMapData mapData)
        {
            InitializeComponent();
            LoadResourceBitmaps();
            dataTables = new Dictionary<PhonemeType, DataTable>();
            this.MapData = (LipSyncMapData)mapData.Clone();
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

        private int CalcNumDataGridRows
        {
            get
            {
                return (horizontalChecked) ?
                    Convert.ToInt32(stringsUpDown.Value) : Convert.ToInt32(pixelsUpDown.Value);
            }
        }

        private int CalcNumDataGridCols
        {
            get
            {
                return (horizontalChecked) ?
                    Convert.ToInt32(pixelsUpDown.Value) : Convert.ToInt32(stringsUpDown.Value);
            }
        }

        private LipSyncMapItem FindRenderMapItem (int row, int column)
        {
            int displayRow = (horizontalChecked) ? row : column;
            int displayCol = (verticalChecked) ? row : column;
            
            int startMapIndex = _newMapping.MapItems.FindIndex(
            delegate(LipSyncMapItem mapItem)
            {
                return mapItem.Name.Equals(startingElementCombo.Text);
            });

            LipSyncMapItem retVal = null;


            int calcIndex = (horizontalChecked) ? 
                ((CalcNumDataGridCols * row) + column) + startMapIndex : ((CalcNumDataGridRows * column) + row) + startMapIndex;

            if (calcIndex < _newMapping.MapItems.Count)
            {
                retVal = _newMapping.MapItems.ElementAt(calcIndex);
            }

            return retVal;
        }

        private DataTable BuildBlankTable()
        {
            DataTable dt = new DataTable(_name);

            int cols = CalcNumDataGridCols;
            int rows = CalcNumDataGridRows;

            for (int col = 0; col < cols; col++)
            {
                dt.Columns.Add(col.ToString(), typeof(Color));
            }

            for (int row = 0; row < rows; row++)
            {
                DataRow dr = dt.Rows.Add();

                for (int col = 0; col < cols; col++)
                {
                    dr[col] = Color.Black;
                }
            }
            return dt;
        }

        private DataTable BuildDialogFromMap(LipSyncMapData data)
        {
            _name = data.LibraryReferenceName;

            stringsUpDown.Value = (Decimal)_newMapping.MatrixStringCount;
            pixelsUpDown.Value = (Decimal)_newMapping.MatrixPixelsPerString;
            zoomTrackbar.Value = _newMapping.ZoomLevel;
            horizontalRadio.Checked = _newMapping.Horizontal;
            verticalRadio.Checked = _newMapping.Vertical;

            DataTable dt = BuildBlankTable();
            int cols = CalcNumDataGridCols;
            int rows = CalcNumDataGridRows;

            LipSyncMapItem mapItem = null;

            DataRow dr;
            for (int row = 0; row < rows; row++)
            {
                dr = dt.Rows[row];
                for (int col = 0; col < cols; col++)
                {
                    mapItem = FindRenderMapItem(row,col);
                    

                    if (mapItem != null)
                    {
                        dr[col] =
                            (mapItem.ElementColors.ContainsKey(phonemeArray[currentPhonemeIndex]) == false) ?
                                Color.Black : mapItem.ElementColors[phonemeArray[currentPhonemeIndex]];
                    }
                    else
                    {
                        dr[col] = Color.Gray;
                    }
                }
            }

            return dt;
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

        private void BuildMapDataFromDialog()
        {
            BuildMapDataFromDialog(phonemeArray[currentPhonemeIndex]);
        }

        private void BuildMapDataFromDialog(PhonemeType phoneme)
        {
            LipSyncMapItem mapItem;
            int numRows = CalcNumDataGridRows;
            int numCols = CalcNumDataGridCols;

            for (int row = 0; row < numRows; row++)
            {
                DataRow dr = currentDataTable.Rows[row];

                for (int col = 0; col < numCols; col++)
                {
                    mapItem = FindRenderMapItem(row, col);
                    if (mapItem != null)
                    {
                        if (mapItem.ElementColors == null)
                        {
                            mapItem.ElementColors = new Dictionary<PhonemeType, Color>();
                        }
                        mapItem.ElementColors[phoneme] = (Color)dr[col];
                    }
                }
            }

            _newMapping.IsMatrix = true;
            _newMapping.MatrixStringCount = Convert.ToInt32(stringsUpDown.Value);
            _newMapping.MatrixPixelsPerString = Convert.ToInt32(pixelsUpDown.Value);
            _newMapping.StartNode = startingElementCombo.Text;
            _newMapping.ZoomLevel = zoomTrackbar.Value;
            _newMapping.Horizontal = horizontalRadio.Checked;
            _newMapping.Vertical = verticalRadio.Checked;

        }

        private DataTable currentDataTable
        {
            get
            {
                PhonemeType currentPhoneme = phonemeArray[currentPhonemeIndex];

                if (!dataTables.ContainsKey(currentPhoneme))
                {
                    dataTables.Add(currentPhoneme, BuildBlankTable());
                }
                return dataTables[currentPhoneme];
            }

            set
            {
                dataTables[phonemeArray[currentPhonemeIndex]] = value;
            }
        }

        public LipSyncMapData MapData
        {
            get
            {
                return _newMapping;
            }

            set
            {
                ElementNode tempNode = null;
                _origMapping = value;
                _newMapping = (LipSyncMapData)_origMapping.Clone();

                _rowNames = new List<string>();
                try
                {
                    foreach(LipSyncMapItem mapItem in _newMapping.MapItems)
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
            }
        }

        public string HelperName
        {
            get { return "Phoneme Mapping"; }
        }

        private void NextPhonmeIndex()
        {
            BuildMapDataFromDialog(); 
            currentPhonemeIndex++;
            currentPhonemeIndex %= phonemeArray.Count();
            SetPhonemePicture();
            currentDataTable = BuildDialogFromMap(_newMapping);
            updatedataGridView1();
        }

        private void PrevPhonemeIndex()
        {
            BuildMapDataFromDialog();
            currentPhonemeIndex = 
                (currentPhonemeIndex == 0) ? phonemeArray.Count() : currentPhonemeIndex;
            currentPhonemeIndex--;
            SetPhonemePicture();
            currentDataTable = BuildDialogFromMap(_newMapping);
            updatedataGridView1();
        }

        private string CurrentPhonemeString
        {
            get
            {
                return phonemeArray[currentPhonemeIndex].ToString();
            }
        }

        private void SetPhonemePicture()
        {
            phonemePicture.Image = 
                new Bitmap(_phonemeBitmaps[phonemeArray[currentPhonemeIndex]], 48, 48);
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
                    _phonemeBitmaps = new Dictionary<PhonemeType, Bitmap>();
                    _phonemeBitmaps.Add(PhonemeType.AI, (Bitmap)lipSyncRM.GetObject("AI"));
                    _phonemeBitmaps.Add(PhonemeType.E, (Bitmap)lipSyncRM.GetObject("E"));
                    _phonemeBitmaps.Add(PhonemeType.etc, (Bitmap)lipSyncRM.GetObject("etc"));
                    _phonemeBitmaps.Add(PhonemeType.FV, (Bitmap)lipSyncRM.GetObject("FV"));
                    _phonemeBitmaps.Add(PhonemeType.L, (Bitmap)lipSyncRM.GetObject("L"));
                    _phonemeBitmaps.Add(PhonemeType.MBP, (Bitmap)lipSyncRM.GetObject("MBP"));
                    _phonemeBitmaps.Add(PhonemeType.O, (Bitmap)lipSyncRM.GetObject("O"));
                    _phonemeBitmaps.Add(PhonemeType.Rest, (Bitmap)lipSyncRM.GetObject("rest"));
                    _phonemeBitmaps.Add(PhonemeType.U, (Bitmap)lipSyncRM.GetObject("U"));
                    _phonemeBitmaps.Add(PhonemeType.WQ, (Bitmap)lipSyncRM.GetObject("WQ"));
                }
            }

            currentPhonemeIndex = 0;
            phonemeArray = _phonemeBitmaps.Keys.ToArray();
            SetPhonemePicture();
        }

        private void configureStartingElementCombo()
        {
            startingElementCombo.Items.Clear();
            startingElementCombo.Items.AddRange(_newMapping.MapItems.ToArray());
            startingElementCombo.SelectedIndex = 0;
        }
        private void LipSyncMapSetup_Load(object sender, EventArgs e)
        {
            configureStartingElementCombo();
            currentDataTable = BuildDialogFromMap(_newMapping);
            updatedataGridView1();
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

            int x = CalcNumDataGridCols;
            int y = CalcNumDataGridRows;

            DataGridViewColumn dgvCol;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                dgvCol = dataGridView1.Columns[j];
                dgvCol.Width = CELL_BASE_WIDTH + (int)(ZOOM_STEP_DELTA * zoomSteps);
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvCol.HeaderCell.Value = (verticalChecked) ? (j * y).ToString() : j.ToString();
            }

            DataGridViewRow dgvRow;
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                dgvRow = dataGridView1.Rows[j];
                dgvRow.Height = CELL_BASE_WIDTH + (int)(ZOOM_STEP_DELTA * zoomSteps);
                dgvRow.HeaderCell.Value = (horizontalChecked) ? (j * x).ToString() : j.ToString();
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
            BuildMapDataFromDialog();
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
                currentDataTable = BuildDialogFromMap(_newMapping);
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

        private void colsUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (pixelsUpDown.Value != _newMapping.MatrixPixelsPerString)
            {
                _newMapping.MatrixPixelsPerString = (int)pixelsUpDown.Value;
                currentDataTable = BuildDialogFromMap(_newMapping);
                updatedataGridView1(); 
                BuildMapDataFromDialog();
            }
        }

        private void rowsUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (stringsUpDown.Value != _newMapping.MatrixStringCount)
            {
                _newMapping.MatrixStringCount = (int)stringsUpDown.Value;
                currentDataTable = BuildDialogFromMap(_newMapping);
                BuildMapDataFromDialog();
                updatedataGridView1();
            }
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
            //if (verticalRadio.Checked != verticalChecked)
            //{
                //BuildMapDataFromDialog();
                horizontalChecked = horizontalRadio.Checked;
                verticalChecked = verticalRadio.Checked;
                //currentDataTable = BuildDialogFromMap(_newMapping);
                updatedataGridView1();
            //}
        }

        private void Horizontal_CheckedChanged(object sender, EventArgs e)
        {
            //if (horizontalRadio.Checked != horizontalChecked)
            //{

                //BuildMapDataFromDialog();
                horizontalChecked = horizontalRadio.Checked;
                verticalChecked = verticalRadio.Checked;
                //currentDataTable = BuildDialogFromMap(_newMapping);
                updatedataGridView1();
            //}
        }

        private void assignButton_Click(object sender, EventArgs e)
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
                    tempMapItem = _newMapping.MapItems.Find(
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

                _newMapping.MapItems.Clear();

                int stringCount = 0;
                foreach (LipSyncMapItem mapItem in newMappings)
                {
                    mapItem.StringNum = stringCount++;
                    _newMapping.MapItems.Add(mapItem);
                }

                configureStartingElementCombo();
                reconfigureDataTable();
            }
        }

        private void startingElementCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!startingElementCombo.Text.Equals(_newMapping.StartNode))
            {
                currentDataTable = BuildDialogFromMap(_newMapping);
                updatedataGridView1();
            }
        }

        private void prevPhonemeButton_Click(object sender, EventArgs e)
        {
            PrevPhonemeIndex();
        }

        private void nextPhonemeButton_Click(object sender, EventArgs e)
        {
            NextPhonmeIndex();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png|All Files(*.*)|*.*";
            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                LipSyncMapItem mapItem;
                Color pixelColor;

                Bitmap rawBitmap = new Bitmap(fileDlg.FileName);
                Bitmap scaledBitmap = new Bitmap(rawBitmap, CalcNumDataGridCols, CalcNumDataGridRows);
                
                int cols = CalcNumDataGridCols;
                int rows = CalcNumDataGridRows;
                for (int row = 0; row < rows; row++)
                {
                    DataRow dr = currentDataTable.Rows[row];
                    for (int col = 0; col < cols; col++)
                    {
                        mapItem = FindRenderMapItem(row, col);
                        if (mapItem != null)
                        {
                            pixelColor = scaledBitmap.GetPixel(col,row);
                            dr[col] = pixelColor;
                            mapItem.PhonemeList[CurrentPhonemeString] = (pixelColor != Color.Black);
                        }
                        else
                        {
                            dr[col] = Color.Gray;
                        }
                    }
                }
                BuildDialogFromMap(_newMapping);
                updatedataGridView1();
            }
        }
    }
}
