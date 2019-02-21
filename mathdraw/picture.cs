using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mathdraw
{
    class Picture
    {
 
        private Form1 currentForm;
        private int statusChanged;
        private Object statusLock;

        private int mMaxWidth;
        private int mMaxHeight;
        private int mCoreCount;

        private int winWidth;
        private int winHeight;

        private Bitmap[] bmp;

        private int bmpGenerating;
        private int bmpReady;
        private int drawMode; // 0: pre-gen  1: draw on graphic

        private RenderingMethod renderingMethod;
        private List<double> renderingParameters;

        //private Bitmap p;

        private PictureBox picBox;
        private Graphics graphics;

        private RenderingObject renderingObj;

        public Picture(Form1 form) 
        {
            currentForm = form;
            statusChanged = 0;
            statusLock = new Object(); 

            // Get maximal resolution
            mMaxWidth = Screen.PrimaryScreen.Bounds.Height;
            mMaxHeight = Screen.PrimaryScreen.Bounds.Width;
            mCoreCount = Environment.ProcessorCount;

            winWidth = currentForm.Width;
            winHeight = currentForm.Height;

            // create two blocks for storing picture
            int cb = mMaxWidth * mMaxHeight * 3;

            bmp = new Bitmap[2];
           
            bmpGenerating = -1;
            bmpReady = -1;

            picBox = new PictureBox();
            
            graphics = currentForm.CreateGraphics();


        }

        ~Picture()
        {
        }

        public void Rendering()
        {
           //await Task.Delay(1);

           while (true)
           {
               System.Threading.Thread.Sleep(20); // 50fps
               int state = changed();
               if (state == -1) return;
               if (state == 0) continue;

               // state == 1
               BmpGen();
               BmpLoad();
           }
        }

        public void resize()
        {
            graphics = currentForm.CreateGraphics();
            picBox.resize(currentForm.Width, currentForm.Height);
            changed(1);
        }

        public void mouseClick()
        {
            if (renderingObj != null && renderingObj.mouseMoved(currentForm.mousePos))
            {
                changed(1);
            }
        }
        public string getCordinate(int x, int y)
        {
            return picBox.getCordinate(x, y);
        }

        private int changed(int setChanged = 0) // 0 : Get current status 1 : Set statue changed  -1: exit
        {
            int ret;
            lock (statusLock)
            {
                if (setChanged != 0) statusChanged = setChanged;
                ret = statusChanged;
                if (setChanged == 0) statusChanged = 0;
            }
            return ret;

        }

        private void BmpGen()
        {
            if(bmpGenerating == -1)
            {
                bmpGenerating = 0;
            }

            if (drawMode == 0)     // pre-gen
            {
                drawPicure(bmpGenerating);
            }
            else  // draw after load
            {
                InitPicure(bmpGenerating);
            }
            bmpReady = bmpGenerating;
        }

        private void BmpLoad()
        {
            if (statusChanged != -1 &&  bmp[bmpReady] != null)
            {
                graphics.DrawImage(bmp[bmpReady], 0, 0);
                if (drawMode == 0) //pre-gen
                {
                    // do nothing?
                }
                else // load then draw
                {
                    // draw
                    drawGraphic();
                }
                bmpGenerating = bmpReady ^ 1;
            }
        }

        private void drawPicure(int bmpId)
        {
            switch(renderingMethod)
            {
                case RenderingMethod.MANDELBROT_SET: // Mandelbrot set
                    drawMandelbrotset(bmpId); 
                    return;
                default:
                    break;
            }
            return ;
        }


        private void drawGraphic()
        {
            PictureBox p = picBox.makeCopy();

            renderingObj.drawGraphic(graphics, p);
      
            return;
        }

        public void setRenderingMethod(RenderingMethod method, List<double> parameters)
        {
            renderingMethod = method;
            int paramtersCount = 0;
            if (parameters != null)
            {
                paramtersCount = parameters.Count();
            }
            renderingParameters = parameters;
            drawMode = 1; // draw on graphic

            switch (renderingMethod)
            {
                case RenderingMethod.MANDELBROT_SET:  // mandelbrot Set
                    drawMode = 0; // pre-gen
                    picBox.SetBox(-2.0, -1.5, 1.0, 1.5, currentForm.Width, currentForm.Height);
                    //picBox.SetBox(-0.7265, -0.294, -0.7255, -0.2936, currentForm.Width, currentForm.Height);
                    break;
                case RenderingMethod.LINEAR: //  linear Function
                    if (paramtersCount == 0)
                    {
                        renderingParameters = new List<double>();
                        renderingParameters.Add(1.0);
                        // default : y=x
                    }
                    else if (renderingParameters[0] == 0)
                    {
                        renderingParameters.RemoveAt(0);
                        renderingObj = new constObj();
                        renderingObj.Init(currentForm, picBox, renderingParameters);
                        return;
                    }

                    renderingObj = new linearObj();
                    renderingObj.Init(currentForm, picBox, renderingParameters);
                    break;

                case RenderingMethod.QUADRATIC:
                    if (paramtersCount == 0)
                    {
                        renderingParameters = new List<double>();
                        renderingParameters.Add(1.0);
                        // default : y=x^2
                    }
                    else if (renderingParameters[0] == 0)
                    {
                        renderingParameters.RemoveAt(0);
                        setRenderingMethod(RenderingMethod.LINEAR, renderingParameters);
                        return;
                    }

                    renderingObj = new quadraticObj();
                    renderingObj.Init(currentForm, picBox, renderingParameters);

                    break;
                case RenderingMethod.SINE:
                case RenderingMethod.COSINE:
                    if (paramtersCount == 0)
                    {
                        renderingParameters = new List<double>();
                        renderingParameters.Add(1.0);
                        renderingParameters.Add(1.0);
                        renderingParameters.Add(0.0);
                        // default y = sinX
                    }

                    if (renderingParameters[0] == 0) // Degenerate to const  y = 0
                    {
                        setRenderingMethod(RenderingMethod.LINEAR, renderingParameters);
                        return;
                    }
                    if (renderingParameters[1] == 0) // Degenerate to const
                    {
                        double constV = renderingParameters[0] * Math.Sin(renderingParameters[2]);
                        renderingParameters.Clear();
                        renderingParameters.Add(constV);
                        setRenderingMethod(RenderingMethod.LINEAR, renderingParameters);
                        return;
                    }

                    if (renderingMethod == RenderingMethod.COSINE)
                    {
                        renderingParameters[2] += Constants.PI / 2;
                    }

                    renderingObj = new sineObj();
                    renderingObj.Init(currentForm, picBox, renderingParameters);

                    break;

                default:
                    break;
            }


            changed(1);
        }

        public void quit()
        {
            changed(-1);
        }

        public void saveCurrentPicture(string fileName)
        {
            if (bmpReady != -1)
            {
                bmp[bmpReady].Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public void enlarge(int enlarge)
        {
            if (bmpReady == -1) return;
            picBox.enlarge(enlarge);
            changed(1);
        }

        public void reCenter(int pos_x, int pos_y, double enlarge = 0)
        {
            if (bmpReady == -1) return;
            picBox.reCenter(pos_x, pos_y, enlarge);
            changed(1);
        }


        private Bitmap getBmp(PictureBox p , int bmpId)
        {
            if (bmp[bmpId] == null || bmp[bmpId].Width < p.w || bmp[bmpId].Height < p.h) bmp[bmpId] = new Bitmap(p.w, p.h);
            return bmp[bmpId];
        }

        private void InitPicure(int bmpId)
        {
            PictureBox p = picBox.makeCopy();
            if (bmp[bmpId] == null || bmp[bmpId].Width < p.w || bmp[bmpId].Height < p.h)
            {
                bmp[bmpId] = new Bitmap(p.w, p.h);
            }
            // reset all pixels to white
            for (int x = 0; x < p.w; ++x)
                 for (int y = 0; y < p.h; ++y)
                     bmp[bmpId].SetPixel(x, y, Color.White);
        }

      
        private void drawMandelbrotset(int bmpId)
        {
            PictureBox p = picBox.makeCopy();
            var m = getBmp(p, bmpId);

            int maxRepeat = 60;

            for (int x = 0; x < p.w; ++x)
            {
                if (x == p.origPoint.X )
                {
                    for (int y = 0; y < p.h; ++y)
                        m.SetPixel(x, y, Color.Red);
                    continue;
                }
                for (int y = 0; y < p.h; ++y)
                {
                    if ( y == p.origPoint.Y)
                    {
                        m.SetPixel(x, p.h - y - 1, Color.Red);
                        continue;
                    }

                    double ca = x * p.step + p.startCoordinates.x;
                    double cb = y * p.step + p.startCoordinates.y;
                    int repeat = 0;
                    double za = ca, zb = cb;
                    double mod = mod = za * za + zb * zb;
                    while (repeat < maxRepeat && mod < 4)
                    {
                        ++repeat;
                        // Z = Z^2 + C
                        double zaa = za * za - zb * zb + ca;
                        zb = 2 * za * zb + cb;
                        za = zaa;
                        mod = za * za + zb * zb;
                    }

                    if (repeat >= maxRepeat)
                    {
                        m.SetPixel(x,  p.h - y - 1, Color.Black);
                    }
                    else
                    {
                        int G = (int)(repeat * 255 / (maxRepeat - 1));

                        int B = 255;
                        double mod_c = mod - 0.25;
                        if ( mod_c < 3.75 && mod_c > 0)
                        {
                            B = (int)(mod_c / 3.75 * 255);
                        }
                        int R = (int)((Math.Asin(za / Math.Sqrt(mod)) / Constants.PI + 0.5) * 255);

                        m.SetPixel(x, p.h - y - 1, Color.FromArgb(R, G, B));
                    }
                }
            }
        }

    }
}
