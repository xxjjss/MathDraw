using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathdraw
{
    struct Coordinates
    {
        public double x;
        public double y;
    };

     abstract class RenderingObject
    {
        protected Point mouseP;

        public abstract void Init(Form1 f, PictureBox p, List<double> parameters);
        public abstract bool mouseMoved(Point mouseP);

        public abstract void drawGraphic(Graphics g, PictureBox p);
        
        protected abstract void markKeyCoordinates(Graphics g, PictureBox p);

        protected void drawAxis(Graphics g, PictureBox p)
        {
            Pen pen = new Pen(Color.Black, 1);
            if (p.origPoint.X >= 0 && p.origPoint.X < p.w) // draw y axis
            {
                g.DrawLine(pen, new Point(p.origPoint.X, 0), new Point(p.origPoint.X, p.h));
            }
            if (p.origPoint.Y >= 0 && p.origPoint.Y < p.h) // draw x axis
            {
                g.DrawLine(pen, new Point(0, p.h - p.origPoint.Y), new Point(p.w, p.h - p.origPoint.Y));
            }
            pen.Dispose();

        }

        public void markeCoordinate(Graphics g, PictureBox p, int x, int y)
        {
            string text = "( " + x + ", " + y + " )";
            markeCoordinate(g, p, (double)x, (double)y, text);
        }

        public void markeCoordinate(Graphics g, PictureBox p, Coordinates c, string text = null)
        {
            markeCoordinate(g, p, c.x, c.y, text);
        }

        public void markeCoordinate(Graphics g, PictureBox p, double x, double y, string text = null)
        {
            if (x < p.startCoordinates.x || x > p.startCoordinates.x + p.step * p.w || y < p.startCoordinates.y || y > p.startCoordinates.y + p.step * p.h)  // out of box, do nothing
                return;
            // position
            int px = (int)((x - p.startCoordinates.x) / p.step);
            int py = (int)((y - p.startCoordinates.y) / p.step);
            Pen pen = new Pen(Color.Red, 1);
            g.DrawEllipse(pen, px - 1, p.h - py - 1, 2, 2);
            if (text == null)
            {
                text = "( " + x.ToString("0.00") + ", " + y.ToString("0.00") + " )";
            }

            RectangleF rectf = new RectangleF(px + 5, p.h - py + 3, 90, 50);
            g.DrawString(text, new Font("Tahoma", 8), Brushes.Red, rectf);
            pen.Dispose();

        }

    }
}
