using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathdraw
{
    class sineObj : RenderingObject
    {
        private double a, b, c;
        List<Coordinates> keyPoints;
        double halfCycle;

        public override void Init(Form1 f, PictureBox p, List<double> parameters)
        {
            int count = parameters.Count();
            a = (count == 0) ? 1.0 : parameters[0];
            b = (count <= 1) ? 1.0 : parameters[1];
            c = (count <= 2) ? 0 : parameters[2];

            double yRange = a * 1.2;
            double y0 = yRange > 0 ? -yRange : yRange;
            double y1 = yRange > 0 ? yRange : -yRange;

            halfCycle = Constants.PI / (2 * b);

            double x1 = 2 * Constants.PI / b;
            double x0 = -x1;

            p.SetBox(x0 , y0 , x1, y1 , f.Width, f.Height);
            mouseP.X = -1;
            mouseP.Y = -1;
            keyPoints = new List<Coordinates>();
        }

        public override void drawGraphic(Graphics g, PictureBox p)
        {
            drawAxis(g, p);
            keyPoints.Clear();

            bool inBox = false;
           
            Pen pen = new Pen(Color.Blue, 1);

            int cycleX = (int)(( 2 * Constants.PI) / (p.step * b ));
            int cycle = p.w / cycleX;
            int endP_x = cycle < 1 ? p.w : cycleX;

            List<Point> previousPoints = new List<Point>();
            List<Point> cycleStartPoints = new List<Point>();

            for (int x = 0; x < endP_x; x++)
            {
                double dx = p.startCoordinates.x + x * p.step;
                double dy = a * Math.Sin(b * dx + c);
                int y = (int)((dy - p.startCoordinates.y) / p.step);

                if (y < 0 || y >= p.h)
                {
                    inBox = false;
                    continue;
                }
                else
                {
                    if (inBox == false)
                    {
                        inBox = true;
                        for (int i = 0; i <= cycle + 1; ++i)
                        {
                            Point cycleStartPoint = new Point(x + i * cycleX, p.h - y);
                            previousPoints.Add(cycleStartPoint );
                            cycleStartPoints.Add(cycleStartPoint );
                        }
                        continue;
                    }
                    else
                    {
                        for (int i = 0; i <= cycle + 1; ++i)
                        {
                            if (x + i * cycleX < p.w)
                            {
                                Point current = new Point(x + i * cycleX, p.h - y);
                                g.DrawLine(pen, previousPoints[i], current);
                                previousPoints[i] = current;
                                if ( x == endP_x - 1 && i < cycle && cycleStartPoints[i+1] != null ) // last Point
                                {
                                    g.DrawLine(pen, current, cycleStartPoints[i+1]);
                                }
                            }
                        }
                    }


                    if( x == p.origPoint.X || y == p.origPoint.Y)
                    {
                        if (Math.Abs(dx) > halfCycle || Math.Abs(dy) > (a / 2))
                        {
                            Coordinates keyP;
                            keyP.x = dx;
                            keyP.y = dy;
                            keyPoints.Add(keyP);
                        }
                    }
                }
            }
            pen.Dispose();

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

            double mx = (mouseP.X - p.origPoint.X) * p.step;
            double my = (p.h - mouseP.Y - p.origPoint.Y) * p.step;

            Pen pen = new Pen(Color.Cyan, 1);
            /*
            double y = a * mx + b;
            int py = (int)(y / p.step + p.origPoint.Y);
            if (py > 0 && py < p.h)
            {
                g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(mouseP.X, p.h - py));
                markeCoordinate(g, p, mx, y);
            }

            double x = (my - b) / a;
            int px = (int)(x / p.step + p.origPoint.X);
            if (px > 0 && px < p.w)
            {
                g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(px, mouseP.Y));
                markeCoordinate(g, p, x, my);
            }
            */
            foreach (var keyp in keyPoints)
            {
                markeCoordinate(g, p, keyp.x, keyp.y);
            }
            pen.Dispose();
        }

    }
}
