using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace mathdraw
{
    class quadraticObj : RenderingObject
    {
        private double a, b, c;
        private Coordinates H;
        private double delta; // b*b-4ac

        private Coordinates xCross0, xCross1, yCross;
        List<Coordinates> keyPoints;

        public override void Init(Form1 f, PictureBox p, List<double> parameters)
        {
            int count = parameters.Count();
            a = (count == 0) ? 1.0 : parameters[0];
            b = (count <= 1) ? 0 : parameters[1];
            c = (count <= 2) ? 0 : parameters[2];
            // y = ax^2 + bx + c
            // Assert( a != 0);

            delta = b * b - 4 * a * c;
            keyPoints = new List<Coordinates>();

            H.x = -b / (2 * a);
            H.y = -delta / (4 * a);
            keyPoints.Add(H);

            if (delta > 0)
            {
                xCross0.x = (-b + Math.Sqrt(delta))/(2 * a);
                xCross0.y = 0;
                xCross1.x = (-b - Math.Sqrt(delta))/(2 * a);
                xCross1.y =  0;
                keyPoints.Add(xCross0);
                keyPoints.Add(xCross1);
            }
            else if (delta == 0)
            {
                xCross0.x = -b / (2 * a);
                xCross0.y = 0;
                keyPoints.Add(xCross0);
            }

            yCross.x = 0;
            yCross.y = c;
            keyPoints.Add(yCross);

            // calculate range, should include all key points and (0,0)

            double x0 = 0, x1 = 0, y0 = 0, y1 = 0 ;  
            foreach(var point in keyPoints)
            {
                x0 = x0 < point.x ? x0 : point.x;
                x1 = x1 < point.x ? point.x : x1;
                y0 = y0 < point.y ? y0 : point.y;
                y1 = y1 < point.y ? point.y : y1;
            }

            double adjustx = (x1 - x0) / 4;
            double adjusty = (y1 - y0) / 4;
            if (adjustx == 0 && adjusty == 0)
            {
                y0 -= 0.5;
                y1 += 0.5;
            }
            else
            {
                x0 -= adjustx;
                x1 += adjustx;
                y0 -= adjusty;
                y1 += adjusty;
            }

            p.SetBox(x0, y0, x1, y1, f.Width, f.Height);
            mouseP.X = -1;
            mouseP.Y = -1;
        }

        public override void drawGraphic(Graphics g, PictureBox p)
        {
            drawAxis(g, p);

            bool inBox = false;
            Point previousP = new Point();
            Pen pen = new Pen(Color.Blue, 1);

            for (int x = 0; x < p.w; x++)
            {
                double dx = p.startCoordinates.x + x * p.step;
                double dy = a * dx * dx + b * dx + c;
                int y = (int)((dy - p.startCoordinates.y) / p.step);

                if (y < 0 || y >= p.h)
                {
                    inBox = false;
                    continue;
                }
                else
                {
                    if (inBox == false )
                    {
                        inBox = true;
                    }
                    else
                    {
                        g.DrawLine(pen, new Point(previousP.X, p.h - previousP.Y), new Point(x,p.h - y));
                    }
                    previousP.X = x;
                    previousP.Y = y;
                }
            }

            markKeyCoordinates(g, p);
        }

        public override bool mouseMoved(Point mp)
        {
            if (!mouseP.Equals(mp))
            {
                mouseP = mp;
                return true;
            }
            return false;
        }

        protected override void markKeyCoordinates(Graphics g, PictureBox p)
        {

            markeCoordinate(g, p, 0, 0);
            foreach (var point in keyPoints)
            {
                markeCoordinate(g, p, point.x, point.y);
            }


            double mx = (mouseP.X - p.origPoint.X) * p.step;
            double my = (p.h - mouseP.Y - p.origPoint.Y) * p.step;

            Pen pen = new Pen(Color.Cyan, 1);
            {
                double dy = a * mx * mx + b * mx + c;
                int py = (int)((dy - p.startCoordinates.y) / p.step);
                if (py > 0 && py < p.h)
                {
                    g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(mouseP.X, p.h - py));
                    markeCoordinate(g, p, mx, dy);
                }
            }

            double deltaTmp = b * b - 4 * a * (c - my);
            if (deltaTmp > 0)
            {
                double dx = (-b - Math.Sqrt(deltaTmp)) / (2 * a);
                int px = (int)((dx - p.startCoordinates.x) / p.step);
                g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(px, mouseP.Y));
                markeCoordinate(g, p, dx, my);

                dx = (-b + Math.Sqrt(deltaTmp)) / (2 * a);
                px = (int)((dx - p.startCoordinates.x) / p.step);
                g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(px, mouseP.Y));
                markeCoordinate(g, p, dx, my);
            }
            else if (deltaTmp == 0)
            {
                int py = (int)((H.y - p.startCoordinates.y) / p.step);
                int px = (int)((H.x - p.startCoordinates.x) / p.step);
                g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(px, p.h = py));
            }

            markeCoordinate(g, p, mx, my);

            pen.Dispose();
        }

    }
}
