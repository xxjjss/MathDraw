namespace mathdraw
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.polynomialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quadraticFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polynomialToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.trigonometricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tangentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mandelbrotSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.juliaSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polynomialToolStripMenuItem,
            this.trigonometricToolStripMenuItem,
            this.specialToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(928, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // polynomialToolStripMenuItem
            // 
            this.polynomialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linearFunctionToolStripMenuItem,
            this.quadraticFunctionToolStripMenuItem,
            this.polynomialToolStripMenuItem1});
            this.polynomialToolStripMenuItem.Name = "polynomialToolStripMenuItem";
            this.polynomialToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.polynomialToolStripMenuItem.Text = "Polynomial";
            this.polynomialToolStripMenuItem.Click += new System.EventHandler(this.polynomialToolStripMenuItem_Click);
            // 
            // linearFunctionToolStripMenuItem
            // 
            this.linearFunctionToolStripMenuItem.Name = "linearFunctionToolStripMenuItem";
            this.linearFunctionToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.linearFunctionToolStripMenuItem.Text = "Linear";
            this.linearFunctionToolStripMenuItem.Click += new System.EventHandler(this.linearFunctionToolStripMenuItem_Click);
            // 
            // quadraticFunctionToolStripMenuItem
            // 
            this.quadraticFunctionToolStripMenuItem.Name = "quadraticFunctionToolStripMenuItem";
            this.quadraticFunctionToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.quadraticFunctionToolStripMenuItem.Text = "Quadratic";
            this.quadraticFunctionToolStripMenuItem.Click += new System.EventHandler(this.quadraticFunctionToolStripMenuItem_Click);
            // 
            // polynomialToolStripMenuItem1
            // 
            this.polynomialToolStripMenuItem1.Name = "polynomialToolStripMenuItem1";
            this.polynomialToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.polynomialToolStripMenuItem1.Text = "Cubic";
            // 
            // trigonometricToolStripMenuItem
            // 
            this.trigonometricToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sineToolStripMenuItem,
            this.cosineToolStripMenuItem,
            this.tangentToolStripMenuItem});
            this.trigonometricToolStripMenuItem.Name = "trigonometricToolStripMenuItem";
            this.trigonometricToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.trigonometricToolStripMenuItem.Text = "Trigonometric";
            // 
            // sineToolStripMenuItem
            // 
            this.sineToolStripMenuItem.Name = "sineToolStripMenuItem";
            this.sineToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.sineToolStripMenuItem.Text = "sine";
            this.sineToolStripMenuItem.Click += new System.EventHandler(this.sineToolStripMenuItem_Click);
            // 
            // cosineToolStripMenuItem
            // 
            this.cosineToolStripMenuItem.Name = "cosineToolStripMenuItem";
            this.cosineToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.cosineToolStripMenuItem.Text = "cosine";
            this.cosineToolStripMenuItem.Click += new System.EventHandler(this.cosineToolStripMenuItem_Click);
            // 
            // tangentToolStripMenuItem
            // 
            this.tangentToolStripMenuItem.Name = "tangentToolStripMenuItem";
            this.tangentToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.tangentToolStripMenuItem.Text = "tangent";
            // 
            // specialToolStripMenuItem
            // 
            this.specialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mandelbrotSetToolStripMenuItem,
            this.juliaSetToolStripMenuItem});
            this.specialToolStripMenuItem.Name = "specialToolStripMenuItem";
            this.specialToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.specialToolStripMenuItem.Text = "Special";
            // 
            // mandelbrotSetToolStripMenuItem
            // 
            this.mandelbrotSetToolStripMenuItem.Name = "mandelbrotSetToolStripMenuItem";
            this.mandelbrotSetToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.mandelbrotSetToolStripMenuItem.Text = "Mandelbrot set";
            this.mandelbrotSetToolStripMenuItem.Click += new System.EventHandler(this.mandelbrotSetToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = " ";
            // 
            // juliaSetToolStripMenuItem
            // 
            this.juliaSetToolStripMenuItem.Name = "juliaSetToolStripMenuItem";
            this.juliaSetToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.juliaSetToolStripMenuItem.Text = "Julia Set";
            this.juliaSetToolStripMenuItem.Click += new System.EventHandler(this.juliaSetToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AccessibleName = "main";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 492);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "Math Draw";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.CursorChanged += new System.EventHandler(this.Form1_CursorChanged);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem polynomialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quadraticFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polynomialToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trigonometricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cosineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tangentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mandelbrotSetToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem juliaSetToolStripMenuItem;
    }
}

