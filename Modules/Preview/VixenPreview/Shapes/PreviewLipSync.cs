using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows.Controls;
using System.Drawing.Design;
using Vixen.Sys;
using Vixen.Sys.Attribute;
using System.Resources;

namespace VixenModules.Preview.VixenPreview.Shapes
{
    [DataContract]
    public class PreviewLipSync : PreviewBitmapShape
    {
        [DataMember]
        private PreviewPoint _topLeft;
        [DataMember]
        private PreviewPoint _topRight;
        [DataMember]
        private PreviewPoint _bottomLeft;
        [DataMember]
        private PreviewPoint _bottomRight;

        private List<PreviewPoint> _cornerPoints;
        private FastPixel.FastPixel _myBitmapfp;
        private PreviewPoint topLeftStart, topRightStart, bottomLeftStart, bottomRightStart;
        

        [DataMember]
        private string _imageDir;

        private Bitmap _currentBitmap;

        public PreviewLipSync(PreviewPoint point1, ElementNode selectedNode, double zoomLevel)
        {
            _currentBitmap = PreviewLipSync.DefaultBitmap;

            ZoomLevel = zoomLevel;

            _topLeft = PointToZoomPoint(point1);
            _topRight = new PreviewPoint(_topLeft.X + _currentBitmap.Width, _topLeft.Y);
            _bottomLeft = new PreviewPoint(_topLeft.X, _topLeft.Y + _currentBitmap.Height);
            _bottomRight = new PreviewPoint(_topRight.X, _bottomLeft.Y);

            _strings = new List<PreviewBaseShape>();


            _cornerPoints = new List<PreviewPoint>();
            _cornerPoints.Add(_topLeft);
            _cornerPoints.Add(_topRight);
            _cornerPoints.Add(_bottomLeft);
            _cornerPoints.Add(_bottomRight);

            Layout();
        }

        static private Bitmap DefaultBitmap
        {
            get
            {
                ResourceManager rm = Properties.Resources.ResourceManager;
                return (Bitmap)rm.GetObject("Lips");
            }
        }

        [OnDeserialized]
        private new void OnDeserialized(StreamingContext context)
        {
            _cornerPoints = new List<PreviewPoint>();
            _cornerPoints.Add(_topLeft);
            _cornerPoints.Add(_topRight);
            _cornerPoints.Add(_bottomLeft);
            _cornerPoints.Add(_bottomRight);

            _currentBitmap = PreviewLipSync.DefaultBitmap;

            Layout();
        }

        public override void Draw(FastPixel.FastPixel fp, bool editMode, List<ElementNode> highlightedElements, bool selected,
                         bool forceDraw)
        {

            int pixelColor;
            _myBitmapfp.Lock();
            Int32 maxWidth = _myBitmapfp.Width;
            Int32 maxHeight = _myBitmapfp.Height;

            for (int x = 0; x < maxWidth; x++)
            {
                for (int y = 0; y < maxHeight; y++)
                {
                    pixelColor = _myBitmapfp.GetPixel(x, y).ToArgb();
                    if (selected)
                    {
                        if ((x == 0) ||
                            (y == 0) ||
                            (x == maxWidth - 1) ||
                            (y == maxHeight - 1))
                        {
                            pixelColor = (int)((long)pixelColor & 0xFF000000) + 0x0000FF00;
                        }
                        else
                        {
                            pixelColor = (0xFFFFFF ^ pixelColor);
                        }
                    }

                    fp.SetPixel(Convert.ToInt32(this._topLeft.X * ZoomLevel) + x,
                                Convert.ToInt32(this._topLeft.Y * ZoomLevel) + y,
                                Color.FromArgb(pixelColor));
                }
            }
            _myBitmapfp.Unlock(false);

            DrawSelectPoints(fp);
        }

        public override int Left
        {
            get
            {
                return _topLeft.X;
            }
        }

        public override int Right
        {
            get
            {
                return _topRight.X; 
            }
        }

        public override int Top
        {
            get
            {
                return _topLeft.Y;
            }
        }

        public override int Bottom
        {
            get
            {
                return _bottomLeft.Y;
            }
        }

        [DataMember,
         CategoryAttribute("Settings"),
         DescriptionAttribute("The name of this string. Used in templates to distinguish various strings."),
         DisplayName("Name")]
        public string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                FireOnPropertiesChanged(this, this);
            }
        }

        [CategoryAttribute("Position"),
         DisplayName("Top Left"),
         DescriptionAttribute("Image is defined defined by 2 points. This is point 1.")]
        public Point TopLeftPoint
        {
            get
            {
                if (_topLeft == null)
                    _topLeft = new PreviewPoint(10, 10);
                Point p = new Point(_topLeft.X, _topLeft.Y);
                return p;
            }
            set
            {
                _topLeft.X = value.X;
                _topLeft.Y = value.Y;
                _bottomLeft.X = value.X;
                _topRight.Y = value.Y;
                Layout();
            }
        }

        [CategoryAttribute("Position"),
         DisplayName("Bottom Right"),
         DescriptionAttribute("Image are defined by 2 points. This is point 2.")]
        public Point BottomRightPoint
        {
            get
            {
                Point p = new Point(_bottomRight.X, _bottomRight.Y);
                return p;
            }
            set
            {
                _bottomRight.X = value.X;
                _bottomRight.Y = value.Y;
                _topRight.X = value.X;
                _bottomLeft.Y = value.Y;
                Layout();
            }
        }

        public override void Match(PreviewBaseShape matchShape)
        {
            PreviewLipSync shape = (matchShape as PreviewLipSync);
            _topRight.X = _topLeft.X + (shape._topRight.X - shape._topLeft.X);
            _topRight.Y = _topLeft.Y + (shape._topRight.Y - shape._topLeft.Y);
            _bottomLeft.X = _topLeft.X + (shape._bottomLeft.X - shape._topLeft.X);
            _bottomLeft.Y = _topLeft.Y + (shape._bottomLeft.Y - shape._topLeft.Y);
            Layout();
        }

        public override void Layout()
        {
            if (_topLeft != null && _bottomRight != null && _currentBitmap != null)
            {
                Bitmap testBitMap = 
                    new Bitmap(_currentBitmap,
                    new Size(Math.Max(1,Convert.ToInt32((_topRight.X - _topLeft.X)*ZoomLevel)), 
                             Math.Max(1,Convert.ToInt32((_bottomLeft.Y - _topLeft.Y) * ZoomLevel))));

                _myBitmapfp = new FastPixel.FastPixel(testBitMap);


            }
        }

        public override void MouseMove(int x, int y, int changeX, int changeY)
        {
            PreviewPoint point = PointToZoomPoint(new PreviewPoint(x, y));
            if (_selectedPoint != null)
            {
                if (_selectedPoint == _bottomRight &&
                    System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
                {
                    int height = y - _topRight.Y;
                    _topRight.X = _topLeft.X + height;
                    _topRight.Y = _topLeft.Y;
                    _bottomLeft.X = _topLeft.X;
                    _bottomLeft.Y = _topRight.Y + height;

                    _selectedPoint.X = _topRight.X;
                    _selectedPoint.Y = _bottomLeft.Y;
                }
                else
                {
                    _selectedPoint.X = point.X;
                    _selectedPoint.Y = point.Y;
                }

                if (_selectedPoint == _topRight)
                {
                    _topLeft.Y = _topRight.Y;
                    _bottomRight.X = _topRight.X;
                }
                else if (_selectedPoint == _bottomLeft)
                {
                    _topLeft.X = _bottomLeft.X;
                    _bottomRight.Y = _bottomLeft.Y;
                }
                else if (_selectedPoint == _topLeft)
                {
                    _bottomLeft.X = _topLeft.X;
                    _topRight.Y = _topLeft.Y;
                }
                else if (_selectedPoint == _bottomRight)
                {
                    _topRight.X = _bottomRight.X;
                    _bottomLeft.Y = _bottomRight.Y;
                }
                Layout();
                SelectDragPoints();
            }
            // If we get here, we're moving
            else
            {
                _topLeft.X = Convert.ToInt32(topLeftStart.X * ZoomLevel) + changeX;
                _topLeft.Y = Convert.ToInt32(topLeftStart.Y * ZoomLevel) + changeY;
                _topRight.X = Convert.ToInt32(topRightStart.X * ZoomLevel) + changeX;
                _topRight.Y = Convert.ToInt32(topRightStart.Y * ZoomLevel) + changeY;
                _bottomLeft.X = Convert.ToInt32(bottomLeftStart.X * ZoomLevel) + changeX;
                _bottomLeft.Y = Convert.ToInt32(bottomLeftStart.Y * ZoomLevel) + changeY;
                _bottomRight.X = Convert.ToInt32(bottomRightStart.X * ZoomLevel) + changeX;
                _bottomRight.Y = Convert.ToInt32(bottomRightStart.Y * ZoomLevel) + changeY;

                PointToZoomPointRef(_topLeft);
                PointToZoomPointRef(_topRight);
                PointToZoomPointRef(_bottomLeft);
                PointToZoomPointRef(_bottomRight);

                Layout();
            }
        }

        public override void Select(bool selectDragPoints)
        {
            base.Select(selectDragPoints);
            connectStandardStrings = true;
        }

        public override void SelectDragPoints()
        {
            SetSelectPoints(_cornerPoints.ToList(), null);
        }

        private Rectangle MyRect()
        {
            return new Rectangle(Convert.ToInt32(_topLeft.X * ZoomLevel),
                                 Convert.ToInt32(_topLeft.Y * ZoomLevel),
                                 Convert.ToInt32((_topRight.X - _topLeft.X) * ZoomLevel),
                                 Convert.ToInt32((_bottomLeft.Y - _topLeft.Y) * ZoomLevel));
        }

        public override bool ShapeInRect(Rectangle rect)
        {
            if (rect.IntersectsWith(MyRect()))
            {
                return true;
            }
            return false;
        }

        public override bool PointInShape(PreviewPoint point)
        {
            Rectangle pointRect = 
                new Rectangle(point.ToPoint(), new Size(0, 0));
            return ShapeInRect(pointRect);
        }

        public override void SetSelectPoint(PreviewPoint point)
        {
            if (point == null)
            {
                topLeftStart = new PreviewPoint(_topLeft.X, _topLeft.Y);
                topRightStart = new PreviewPoint(_topRight.X, _topRight.Y);
                bottomLeftStart = new PreviewPoint(_bottomLeft.X, _bottomLeft.Y);
                bottomRightStart = new PreviewPoint(_bottomRight.X, _bottomRight.Y);
            }

            _selectedPoint = point;
        }

        public override void SelectDefaultSelectPoint()
        {
            _selectedPoint = _bottomRight;
        }

        public override void MoveTo(int x, int y)
        {
            Point topLeft = new Point();
            topLeft.X = Math.Min(_topLeft.X, Math.Min(Math.Min(_topRight.X, _bottomRight.X), _bottomLeft.X));
            topLeft.Y = Math.Min(_topLeft.Y, Math.Min(Math.Min(_topRight.Y, _bottomRight.Y), _bottomLeft.Y));

            int deltaX = x - topLeft.X;
            int deltaY = y - topLeft.Y;

            _topLeft.X += deltaX;
            _topLeft.Y += deltaY;
            _topRight.X += deltaX;
            _topRight.Y += deltaY;
            _bottomRight.X += deltaX;
            _bottomRight.Y += deltaY;
            _bottomLeft.X += deltaX;
            _bottomLeft.Y += deltaY;

            Layout();
        }

        public override void Resize(double aspect)
        {
            _topLeft.X = (int)(_topLeft.X * aspect);
            _topLeft.Y = (int)(_topLeft.Y * aspect);
            _topRight.X = (int)(_topRight.X * aspect);
            _topRight.Y = (int)(_topRight.Y * aspect);
            _bottomRight.X = (int)(_bottomRight.X * aspect);
            _bottomRight.Y = (int)(_bottomRight.Y * aspect);
            _bottomLeft.X = (int)(_bottomLeft.X * aspect);
            _bottomLeft.Y = (int)(_bottomLeft.Y * aspect);

            Layout();
        }

        public override void ResizeFromOriginal(double aspect)
        {
            _topLeft.X = topLeftStart.X;
            _topLeft.Y = topLeftStart.Y;
            _bottomRight.X = bottomRightStart.X;
            _bottomRight.Y = bottomRightStart.Y;
            _topRight.X = topRightStart.X;
            _topRight.Y = topRightStart.Y;
            _bottomLeft.X = bottomLeftStart.X;
            _bottomLeft.Y = bottomLeftStart.Y;

            Resize(aspect);
        }

        [Editor(typeof(PreviewSetImageDir), typeof(UITypeEditor)),
         CategoryAttribute("Settings"),
         DisplayName("Image Dir")]
        public virtual string ImageDir
        {
            get { return _imageDir; }
            set { _imageDir = value; }
        }

    }
}