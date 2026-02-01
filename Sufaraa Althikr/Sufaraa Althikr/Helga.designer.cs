namespace Sufaraa_Althikr
{
    partial class Helga
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.addHel_button = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.addHel_button);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 528);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // addHel_button
            // 
            this.addHel_button.BackColor = System.Drawing.Color.Transparent;
            this.addHel_button.BorderColor = System.Drawing.Color.Transparent;
            this.addHel_button.BorderRadius = 10;
            this.addHel_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addHel_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.addHel_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.addHel_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.addHel_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.addHel_button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(78)))), ((int)(((byte)(12)))));
            this.addHel_button.Font = new System.Drawing.Font("Mizan AR+LT", 15F);
            this.addHel_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.addHel_button.ImageSize = new System.Drawing.Size(30, 30);
            this.addHel_button.Location = new System.Drawing.Point(285, 405);
            this.addHel_button.Name = "addHel_button";
            this.addHel_button.Size = new System.Drawing.Size(244, 84);
            this.addHel_button.TabIndex = 5;
            this.addHel_button.Text = "إضافة حلقة";
            this.addHel_button.Click += new System.EventHandler(this.addHel_button_Click_1);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(62, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(682, 350);
            this.panel2.TabIndex = 6;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Helga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.ClientSize = new System.Drawing.Size(774, 528);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Helga";
            this.Text = "Helga";
            this.Load += new System.EventHandler(this.Helga_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button addHel_button;
        private System.Windows.Forms.Panel panel2;
    }
}