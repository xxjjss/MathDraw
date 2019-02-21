using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathdraw
{
    class PictureBox
    {

        private Coordinates orig0, orig1;
        public Coordinates startCoordinates;

        public Point origPoint;

        public double step;

        public int w, h;

        private Object statusLock;

        public PictureBox()
        {
            statusLock = new Object(); 
        }

        private PictureBox(PictureBox c)
        {
            lock (c.statusLock)
            {
                orig0 = c.orig0;
                orig1 = c.orig1;
                startCoordinates = c.startCoordinates;

                step = c.step;

                origPoint = c.origPoint;
                w = c.w;
                h = c.h;
            }
            statusLock = new Object();
        }

        public void SetBox(double _x0, double _y0, double _x1, double _y1, int width, int height)
        {
            lock(statusLock)
            {
                if ( _x0 == _x1 && _y0 == _y1)
                {
                    _x0 -= 0.5;
                    _x1 += 0.5;
                }
                orig0.x = _x0;
                orig1.x = _x1;

                orig0.y = _y0;
                orig1.y = _y1;

                w = width;
                h = height;

                startCoordinates = orig0;

                double stepx = (orig1.x - startCoordinates.x) / width;
                double stepy = (orig1.y - startCoordinates.y) / height;
                step = stepx < stepy ? stepy : stepx;

                if (stepx < stepy)
                {
                    startCoordinates.x = startCoordinates.x - (stepy - stepx) * w / 2;
                }
                else
                {
                    startCoordinates.y = startCoordinates.y - (stepx - stepy) * h / 2;
                }

                origPoint.X = (int)((0 - startCoordinates.x) / step);
                origPoint.Y = (int)((0 - startCoordinates.y) / step);

            }
        }

        public void resize(int width, int height)
        {
            SetBox(orig0.x, orig0.y, orig1.x, orig1.y, width, height);
        }


        public PictureBox makeCopy()
        {
            return  new PictureBox(this);
        }

        public string getCordinate(int x, int y)
        {
            double dx = startCoordinates.x + x * step;
            double dy = startCoordinates.y + (h - y) * step;

            return "( " + dx + ", " + dy + " )";

        }

        public void reCenter(int pos_x, int pos_y, double enlarge = 0)
        {
            lock (statusLock)
            {
                // get new center point
                double center_x = startCoordinates.x + pos_x * step;
                double center_y = startCoordinates.y + (h - pos_y) * step;

                double new_y0 = center_y - (orig1.y - orig0.y) / 2;
                double new_y1 = center_y + (orig1.y - orig0.y) / 2;
                double new_x0 = center_x - (orig1.x - orig0.x) / 2;
                double new_x1 = center_x +  (orig1.x - orig0.x) / 2;
                SetBox(new_x0, new_y0, new_x1, new_y1, w, h);

                if (enlarge != 0 && enlarge != 1.0)
                {
                    int enlargePixel = (int) ( (enlarge - 1) * (w > h ? w : h)) ;
                    this.enlarge(enlargePixel);
                }
            }
        }

        public void enlarge(int enlarge)
        {
             lock(statusLock)
            {
                 int s = w > h ? w : h;
                 int new_s = s + enlarge;
                 if (new_s <= 0) return;

                 //calculate enlarge coefficient
                 double coefficient = (double)s / (double)new_s;
                 if (coefficient > 1.2) coefficient = 1.2;
                 if (coefficient < 0.8) coefficient = 0.8;
                 
                 // get center point
                 double center_x = startCoordinates.x + w / 2 * step;
                 double center_y = startCoordinates.y + h / 2 * step;

                 double new_y0 = center_y - coefficient * (orig1.y - orig0.y) / 2;
                 double new_y1 = center_y + coefficient * (orig1.y - orig0.y) / 2;
                 double new_x0 = center_x - coefficient * (orig1.x - orig0.x) / 2;
                 double new_x1 = center_x + coefficient * (orig1.x - orig0.x) / 2;

                 SetBox(new_x0, new_y0, new_x1, new_y1, w, h);
            }
        }
    }
}
