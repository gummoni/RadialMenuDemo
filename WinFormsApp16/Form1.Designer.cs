namespace WinFormsApp16
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.radialMenuControl1 = new WinFormsApp16.RadialMenuControl();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenuControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Leave";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radialMenuControl1
            // 
            this.radialMenuControl1.BackColor = System.Drawing.Color.Transparent;
            this.radialMenuControl1.CenterBorder = 5F;
            this.radialMenuControl1.CenterSize = 60F;
            this.radialMenuControl1.FocusBorder = 5F;
            this.radialMenuControl1.Image = ((System.Drawing.Image)(resources.GetObject("radialMenuControl1.Image")));
            this.radialMenuControl1.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("radialMenuControl1.Items")));
            this.radialMenuControl1.Location = new System.Drawing.Point(246, 147);
            this.radialMenuControl1.Name = "radialMenuControl1";
            this.radialMenuControl1.Size = new System.Drawing.Size(300, 300);
            this.radialMenuControl1.Step = 5F;
            this.radialMenuControl1.TabIndex = 4;
            this.radialMenuControl1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 559);
            this.Controls.Add(this.radialMenuControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "RadialMenuDemo";
            ((System.ComponentModel.ISupportInitialize)(this.radialMenuControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Button button1;
        private Button button2;
        private RadialMenuControl radialMenuControl1;
    }
}