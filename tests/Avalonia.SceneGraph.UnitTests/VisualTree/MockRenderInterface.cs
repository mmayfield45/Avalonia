﻿using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using Avalonia.Platform;

namespace Avalonia.SceneGraph.UnitTests.VisualTree
{
    class MockRenderInterface : IPlatformRenderInterface
    {
        public IFormattedTextImpl CreateFormattedText(
            string text,
            string fontFamilyName,
            double fontSize,
            FontStyle fontStyle,
            TextAlignment textAlignment,
            FontWeight fontWeight,
            TextWrapping wrapping)
        {
            throw new NotImplementedException();
        }

        public IRenderTarget CreateRenderer(IPlatformHandle handle)
        {
            throw new NotImplementedException();
        }

        public IRenderTargetBitmapImpl CreateRenderTargetBitmap(int width, int height)
        {
            throw new NotImplementedException();
        }

        public IStreamGeometryImpl CreateStreamGeometry()
        {
            return new MockStreamGeometry();
        }

        public IBitmapImpl LoadBitmap(Stream stream)
        {
            throw new NotImplementedException();
        }

        public IBitmapImpl LoadBitmap(string fileName)
        {
            throw new NotImplementedException();
        }

        class MockStreamGeometry : IStreamGeometryImpl
        {
            private MockStreamGeometryContext _impl = new MockStreamGeometryContext();
            public Rect Bounds
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public Matrix Transform
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public IStreamGeometryImpl Clone()
            {
                return this;
            }

            public bool FillContains(Point point)
            {
                return _impl.FillContains(point);
            }

            public Rect GetRenderBounds(double strokeThickness)
            {
                throw new NotImplementedException();
            }

            public IStreamGeometryContextImpl Open()
            {
                return _impl;
            }

            class MockStreamGeometryContext : IStreamGeometryContextImpl
            {
                private List<Point> points = new List<Point>();
                public void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection)
                {
                    throw new NotImplementedException();
                }

                public void BeginFigure(Point startPoint, bool isFilled)
                {
                    points.Add(startPoint);
                }

                public void CubicBezierTo(Point point1, Point point2, Point point3)
                {
                    throw new NotImplementedException();
                }

                public void Dispose()
                {
                }

                public void EndFigure(bool isClosed)
                {
                }

                public void LineTo(Point point)
                {
                    points.Add(point);
                }

                public void QuadraticBezierTo(Point control, Point endPoint)
                {
                    throw new NotImplementedException();
                }

                public void SetFillRule(FillRule fillRule)
                {
                }

                public bool FillContains(Point point)
                {
                    // Use the algorithm from http://www.blackpawn.com/texts/pointinpoly/default.html
                    // to determine if the point is in the geometry (since it will always be convex in this situation)
                    for (int i = 0; i < points.Count; i++)
                    {
                        var a = points[i];
                        var b = points[(i + 1) % points.Count];
                        var c = points[(i + 2) % points.Count];

                        Vector v0 = c - a;
                        Vector v1 = b - a;
                        Vector v2 = point - a;

                        var dot00 = v0 * v0;
                        var dot01 = v0 * v1;
                        var dot02 = v0 * v2;
                        var dot11 = v1 * v1;
                        var dot12 = v1 * v2;


                        var invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
                        var u = (dot11 * dot02 - dot01 * dot12) * invDenom;
                        var v = (dot00 * dot12 - dot01 * dot02) * invDenom;
                        if ((u >= 0) && (v >= 0) && (u + v < 1)) return true;
                    }
                    return false;
                }
            }
        }
    }

}
