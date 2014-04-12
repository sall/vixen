using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows.Controls;
using Vixen.Sys;

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

        private PreviewPoint topLeftStart, topRightStart, bottomLeftStart, bottomRightStart;

        public PreviewLipSync(PreviewPoint point1, ElementNode selectedNode, double zoomLevel)
        {
            ZoomLevel = zoomLevel;
            _topLeft = PointToZoomPoint(point1);
            _topRight = new PreviewPoint(_topLeft);
            _bottomLeft = new PreviewPoint(_topLeft);
            _bottomRight = new PreviewPoint(_topLeft);

            _strings = new List<PreviewBaseShape>();

/*

            if (selectedNode != null)
            {
                List<ElementNode> children = PreviewTools.GetLeafNodes(selectedNode);
                int stringCount = selectedNode.Children.Count();
                if (stringCount >= 4)
                {
                    int spokePixelCount = 0;
                    foreach (ElementNode node in selectedNode.Children)
                    {
                        spokePixelCount = Math.Max(spokePixelCount, node.Children.Count());
                    }

                    StringType = StringTypes.Pixel;

                    foreach (ElementNode node in selectedNode.Children)
                    {
                        PreviewLine line;
                        line = new PreviewLine(new PreviewPoint(10, 10), new PreviewPoint(20, 20), spokePixelCount, node, ZoomLevel);

                        line.PixelColor = Color.White;
                        _strings.Add(line);
                    }
                }
            }

            if (_strings.Count == 0)
            {
                // Just add lines, they will be layed out in Layout()
                for (int i = 0; i < 8; i++)
                {
                    PreviewLine line;
                    line = new PreviewLine(new PreviewPoint(10, 10), new PreviewPoint(20, 20), 10, selectedNode, ZoomLevel);
                    line.PixelColor = Color.White;
                    _strings.Add(line);
                }
            }
*/
            Layout();
        }

        [OnDeserialized]
        private new void OnDeserialized(StreamingContext context)
        {
            Layout();
        }

        public override int Right
        {
            get
            {
                return _topRight.X;
            }
        }

        public override int Bottom
        {
            get
            {
                return _bottomLeft.Y;
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
            if (_topLeft != null && _bottomRight != null)
            {
                Bitmap testBitMap = new Bitmap("C:\\Users\\Administrator\\Documents\\Vixen 3\\Module Data Files\\LipSync\\Mouths\\Papagayo\\AI.jpg");
                _topRight.X = _topLeft.X + testBitMap.Width;
                _bottomRight.X = _topRight.X;
                _bottomLeft.Y = _topLeft.Y + testBitMap.Height;
                _bottomRight.Y = _bottomLeft.Y;
                Graphics g = Graphics.FromImage(testBitMap);
                g.DrawImage(testBitMap, _topLeft.X, _topLeft.Y, (_topRight.X - _topLeft.X), (_bottomLeft.Y - _topLeft.Y));
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
            List<PreviewPoint> points = new List<PreviewPoint>();
            points.Add(_topLeft);
            points.Add(_topRight);
            points.Add(_bottomLeft);
            points.Add(_bottomRight);
            SetSelectPoints(points, null);
        }

        public override bool PointInShape(PreviewPoint point)
        {
            foreach (PreviewPixel pixel in Pixels)
            {
                Rectangle r = new Rectangle(pixel.X - (SelectPointSize / 2), pixel.Y - (SelectPointSize / 2), SelectPointSize,
                                            SelectPointSize);
                if (point.X >= r.X && point.X <= r.X + r.Width && point.Y >= r.Y && point.Y <= r.Y + r.Height)
                {
                    return true;
                }
            }
            return false;
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

    }
}