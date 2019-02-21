using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mathdraw
{
    public partial class Form1 : Form
    {
        private Picture pic;
        public Point mousePos;

        public Form1()
        {
            InitializeComponent();
            pic = new Picture(this);
            Thread thread = new Thread(ThreadRendering);
            thread.Start(pic);

        }

        ~Form1()
        {
            Application.Exit();
        }

        private static void ThreadRendering(object pic)
        {
            Picture p = pic as Picture;
            p.Rendering();
        }

        private void polynomialToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void linearFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new List<double>();
            InputBox inputBox = new InputBox(this, RenderingMethod.LINEAR, p);

            inputBox.WindowState = FormWindowState.Normal;
            inputBox.ShowDialog();

            pic.setRenderingMethod(RenderingMethod.LINEAR, p);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pic.resize();
        }

        private void Form1_CursorChanged(object sender, EventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
        }


        private void mandelbrotSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pic.setRenderingMethod(RenderingMethod.MANDELBROT_SET, null);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            pic.quit();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           // int x = Cursor.Position.X;
            //int y = Cursor.Position.Y;
            mousePos = this.PointToClient(Cursor.Position);
            label1.Text = pic.getCordinate(mousePos.X, mousePos.Y);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string filename = "c:\\pic\\" + now.Year + now.Month + now.Day + now.Hour + now.Minute + now.Second + ".jpg";
            pic.saveCurrentPicture(filename);
        }

        private void Form1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pic.enlarge(e.Delta);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            pic.reCenter(relativePoint.X, relativePoint.Y, 1.1);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            pic.mouseClick();
        }

        private void quadraticFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new List<double>();
            InputBox inputBox = new InputBox(this, RenderingMethod.QUADRATIC, p);

            inputBox.WindowState = FormWindowState.Normal;
            inputBox.ShowDialog();

            pic.setRenderingMethod(RenderingMethod.QUADRATIC, p);
        }

        private void sineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new List<double>();
            InputBox inputBox = new InputBox(this, RenderingMethod.SINE, p);

            inputBox.WindowState = FormWindowState.Normal;
            inputBox.ShowDialog();

            pic.setRenderingMethod(RenderingMethod.SINE, p);

        }

        private void cosineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new List<double>();
            InputBox inputBox = new InputBox(this, RenderingMethod.COSINE, p);

            inputBox.WindowState = FormWindowState.Normal;
            inputBox.ShowDialog();

            pic.setRenderingMethod(RenderingMethod.COSINE, p);
        }

        private void juliaSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = new List<double>();
            InputBox inputBox = new InputBox(this, RenderingMethod.JULIA_SET, p);

            inputBox.WindowState = FormWindowState.Normal;
            inputBox.ShowDialog();

            pic.setRenderingMethod(RenderingMethod.MANDELBROT_SET, null);

        }

    }
}
