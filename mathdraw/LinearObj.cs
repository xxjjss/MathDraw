using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathdraw
{
    class linearObj : RenderingObject
    {
        private double a, b;

        public override void Init(Form1 f, PictureBox p, List<double> parameters)
        {
            int count = parameters.Count();
            a = (count == 0) ? 1.0 : parameters[0];
            b = (count <= 1) ? 0 : parameters[1];
            
            // y = ax + b
            // Assert( a != 0);
            double x0 = -1, x1 = 1;  // default range
            x0 = -b / a;
            if (Math.Abs(x0) <= 1.0)
            {
                x0 = -1;
            }
            else if (x0 > 1)
            {
                x1 = x0; x0 = -x0;
            }

            double y0 = -1, y1 = 1;  // default range
            if (Math.Abs(b) > 1.0)
            {
                if (b < -1.0)
                {
                    y0 = b; y1 = -b;
                }
                else  // b > 1.0
                {
                    y0 = -b; y1 = b;
                }
            }

            p.SetBox(x0 * 2, y0 * 2, x1 * 2, y1 * 2, f.Width, f.Height);
            mouseP.X = -1;
            mouseP.Y = -1;
        }

        public override void drawGraphic(Graphics g, PictureBox p)
        {
            drawAxis(g, p);

            double x, y;
            int px, py;
            Point p1, p2;
            x = p.startCoordinates.x;
            y = a * x + b;
            if (y < p.startCoordinates.y)
            {
                x = (p.startCoordinates.y - b) / a;
                px = (int)((x - p.startCoordinates.x) / p.step);
                py = 0;
            }
            else if (y > (p.startCoordinates.y + p.h * p.step))
            {
                y = p.startCoordinates.y + p.h * p.step;
                x = (y - b) / a;
                px = (int)((x - p.startCoordinates.x) / p.step);
                py = p.h;
            }
            else
            {
                px = 0;
                py = (int)((y - p.startCoordinates.y) / p.step);
            }
            p1 = new Point(px, p.h - py);

            x = p.startCoordinates.x + p.w * p.step;
            y = a * x + b;
            if (y < p.startCoordinates.y)
            {
                x = (p.startCoordinates.y - b) / a;
                px = (int)((x - p.startCoordinates.x) / p.step);
                py = 0;
            }
            else if (y > (p.startCoordinates.y + p.h * p.step))
            {
                y = p.startCoordinates.y + p.h * p.step;
                x = (y - b) / a;
                px = (int)((x - p.startCoordinates.x) / p.step);
                py = p.h;
            }
            else
            {
                px = p.w;
                py = (int)((y - p.startCoordinates.y) / p.step);
            }
            p2 = new Point(px, p.h - py);

            Pen pen = new Pen(Color.Blue, 1);
            g.DrawLine(pen, p1, p2);
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
            markeCoordinate(g, p, 0, b);
            markeCoordinate(g, p, -b/a, 0);

            double mx = (mouseP.X - p.origPoint.X) * p.step;
            double my = (p.h - mouseP.Y - p.origPoint.Y) * p.step;

            Pen pen = new Pen(Color.Cyan, 1);
            double y = a * mx  + b; 
            int py = (int)(y / p.step + p.origPoint.Y);
            if (py > 0 && py < p.h)
            {
                g.DrawLine( pen, new Point(mouseP.X, mouseP.Y), new Point(mouseP.X, p.h - py));
                markeCoordinate(g, p, mx, y);
            }

            double x = (my - b) / a;
            int px = (int)(x / p.step + p.origPoint.X);
            if (px > 0 && px < p.w)
            {
                g.DrawLine(pen, new Point(mouseP.X, mouseP.Y), new Point(px, mouseP.Y));
                markeCoordinate(g, p, x, my);
            }

            markeCoordinate(g, p, mx, my);
            pen.Dispose();
        }

    }
}
