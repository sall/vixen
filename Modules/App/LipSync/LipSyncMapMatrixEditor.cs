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
        private bool stringsAreRows;
        private int startMapIndex = -1;
        private int stringCount = 1;
        private int pixelCount = 1;
        
        

        public LipSyncMapMatrixEditor()
        {
            _rowNames = new List<string>();
            this.LibraryMappingName = "Default";
            InitializeComponent();
            LoadResourceBitmaps();
            dataTables = new Dictionary<PhonemeType, DataTable>();
            stringsAreRows = true;
            startMapIndex = -1;
        }

        public LipSyncMapMatrixEditor(LipSyncMapData mapData)
        {
            InitializeComponent();
            LoadResourceBitmaps();
            dataTables = new Dictionary<PhonemeType, DataTable>();
            this.MapData = (LipSyncMapData)mapData.Clone();
            stringsAreRows = mapData.StringsAreRows;
            startMapIndex = -1;
        }

        public string LibraryMappingName
        {
            get { return nameTextBox.Text; }
            set
            {
                nameTextBox.Text = value;
            }
        }

        private int CalcNumDataGridRows
        {
            get
            {
                return (stringsAreRows) ? stringCount : pixelCount;
            }
        }

        private int CalcNumDataGridCols
        {
            get
            {
                return (stringsAreRows) ? pixelCount : stringCount;
            }
        }

        private LipSyncMapItem FindRenderMapItem (int row, int column)
        {
            int displayRow = (stringsAreRows) ? row : column;
            int displayCol = (!stringsAreRows) ? row : column;
            
            if (startMapIndex == -1)
            {
                startMapIndex = _newMapping.MapItems.FindIndex(
                delegate(LipSyncMapItem mapItem)
                {
                    return mapItem.Name.Equals(_newMapping.StartNode);
                });
            }

            LipSyncMapItem retVal = null;


            int calcIndex = (stringsAreRows) ? 
                ((CalcNumDataGridCols * row) + column) + startMapIndex : 
                ((CalcNumDataGridRows * column) + row) + startMapIndex;

            if (calcIndex < _newMapping.MapItems.Count)
            {
                retVal = _newMapping.MapItems.ElementAt(calcIndex);
            }

            return retVal;
        }

        private DataTable BuildBlankTable()
        {
            DataTable dt = new DataTable(nameTextBox.Text);

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
            nameTextBox.Text = data.LibraryReferenceName;

            stringCount = _newMapping.MatrixStringCount;
            pixelCount = _newMapping.MatrixPixelsPerString;
            zoomTrackbar.Value = _newMapping.ZoomLevel;

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
            _newMapping.MatrixStringCount = stringCount;
            _newMapping.MatrixPixelsPerString = pixelCount;
            _newMapping.ZoomLevel = zoomTrackbar.Value;
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

        private void LipSyncMapSetup_Load(object sender, EventArgs e)
        {
            while (string.IsNullOrWhiteSpace(_newMapping.StartNode))
            {
                assignNodes();
            }
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
                dgvCol.HeaderCell.Value = (!stringsAreRows) ? (j * y).ToString() : j.ToString();
            }

            DataGridViewRow dgvRow;
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                dgvRow = dataGridView1.Rows[j];
                dgvRow.Height = CELL_BASE_WIDTH + (int)(ZOOM_STEP_DELTA * zoomSteps);
                dgvRow.HeaderCell.Value = (stringsAreRows) ? (j * x).ToString() : j.ToString();
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
            doDataGridResize();
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
            if (pixelCount != _newMapping.MatrixPixelsPerString)
            {
                _newMapping.MatrixPixelsPerString = pixelCount;
                currentDataTable = BuildDialogFromMap(_newMapping);
                updatedataGridView1(); 
                BuildMapDataFromDialog();
            }
        }

        private void rowsUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (stringCount != _newMapping.MatrixStringCount)
            {
                _newMapping.MatrixStringCount = stringCount;
                currentDataTable = BuildDialogFromMap(_newMapping);
                BuildMapDataFromDialog();
                updatedataGridView1();
            }
        }

        private void doDataGridResize()
        {
            int height = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                height += row.Height;
            }
            height += dataGridView1.ColumnHeadersHeight;

            int width = 0;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                width += col.Width;
            }
            width += dataGridView1.RowHeadersWidth;

            width = Math.Min(width + 2, this.Width - 200);
            height = Math.Min(height + 2, this.Height - 200);

            dataGridView1.ClientSize = new Size(width + 2, height + 2);

            dataGridView1.Location =
                new Point(Math.Max(25, (this.Width - width - 160) / 2),
                            Math.Max(275, this.Height - height) / 2);
        }

        private void zoomTrackbar_ValueChanged(object sender, EventArgs e)
        {
            zoomSteps = zoomTrackbar.Value;
            if (zoomSteps == 0)
            {
                zoomSteps = 1;
            }
            updatedataGridView1();
            doDataGridResize();

        }


        private void assignButton_Click(object sender, EventArgs e)
        {
            assignNodes();
        }

        private void assignNodes()
        {
            LipSyncNodeSelect nodeSelectDlg = new LipSyncNodeSelect();
            nodeSelectDlg.MatrixOptionsOnly = true;
            nodeSelectDlg.StringsAreRows = _newMapping.StringsAreRows;
            nodeSelectDlg.NodeNames = _rowNames;

            DialogResult dr = nodeSelectDlg.ShowDialog();
            if ((dr == DialogResult.OK) && (nodeSelectDlg.Changed == true))
            {
                List<LipSyncMapItem> newMappings = new List<LipSyncMapItem>();
                LipSyncMapItem tempMapItem = null;

                _newMapping.StringsAreRows = nodeSelectDlg.StringsAreRows;
                stringsAreRows = _newMapping.StringsAreRows;

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

                _newMapping.StartNode = _newMapping.MapItems[0].Name;
                reconfigureDataTable();
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
