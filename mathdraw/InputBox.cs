using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mathdraw
{
    public partial class InputBox : Form
    {
        private RenderingMethod method;

        public InputBox(Form1 form, RenderingMethod method, List<double> p)
        {
            InitializeComponent();
            currentForm = form;
            renderingMethod = method;
            parameters = p;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;
            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;
            // Display the form as a modal dialog box.
            this.method = method;

            switch(method)
            {
                case RenderingMethod.LINEAR:
                     function.Text = "Y = aX + b";
                     labelC.Hide();
                     textBoxC.Hide();
                     break;
                case RenderingMethod.QUADRATIC:
                     function.Text = "Y = aX*X + bX + c";
                     break;
                case RenderingMethod.SINE:
                     function.Text = "Y = aSin(bX + c)";
                     textBoxB.Text = "1.0";
                     break;
                case RenderingMethod.COSINE:
                     function.Text = "Y = aCos(bX + c)";
                     textBoxB.Text = "1.0";
                     break;
                case RenderingMethod.JULIA_SET:
                     function.Text = "f(z) = Z^2 + C  ==> C = A + Bi";
                     labelA.Text = "A";
                     labelB.Text = "B";
                     textBoxA.Text = "0.0";
                     textBoxB.Text = "0.0";
                     labelC.Hide();
                     textBoxC.Hide();
                     break;
                default:
                     break;
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                parameters.Add(Convert.ToDouble(textBoxA.Text));
            }
            catch
            {
                parameters.Add(1.0);
            }
            try
            {
                parameters.Add(Convert.ToDouble(textBoxB.Text));
            }
            catch
            {
                if (method == RenderingMethod.SINE)
                {
                    parameters.Add(1);
                }
                else
                {
                    parameters.Add(0);
                }
            }
            try
            {
                parameters.Add(Convert.ToDouble(textBoxC.Text));
            }
            catch
            {
                parameters.Add(0);
            }
        }
    
    }
}
