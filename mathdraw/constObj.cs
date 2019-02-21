using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathdraw
{
    class constObj : RenderingObject
    {
        private double c;
        int mouseX;

        public override void Init(Form1 f, PictureBox p, List<double> parameters)
        {
            c = 1.0;
            if (parameters.Count() != 0) 
                c = parameters[0];

            double r = 2 * c;
            if (c > 0)
                p.SetBox(-c, -c,  c, c * 2, f.Width, f.Height);
            else
                p.SetBox( c, c * 2, -c, -c, f.Width, f.Height);

            mouseX = -1;

        }

        public override void drawGraphic(Graphics g, PictureBox p)
        {
            drawAxis(g, p);

            int cY = (int)(c / p.step) + p.origPoint.Y;
            if (cY >= 0 && cY < p.h) // c in view box
            {
                Pen pen = new Pen(Color.Blue, 1);
                g.DrawLine(pen, new Point(0, p.h - cY), new Point(p.w-1, p.h-cY));
                pen.Dispose();

            }

            markKeyCoordinates(g, p);
        }

        public override bool mouseMoved(Point mouseP)
        {
            if (mouseP.X > 0 && mouseP.X != mouseX)
            {
                mouseX = mouseP.X;
                return true;
            }
            return false;
        }

        protected override void markKeyCoordinates(Graphics g, PictureBox p)
        {
            markeCoordinate(g, p, 0, 0);
            markeCoordinate(g, p, 0, c);

            double mx = (mouseX - p.origPoint.X) * p.step;
            markeCoordinate(g, p, mx, c);
        }

    }
}
