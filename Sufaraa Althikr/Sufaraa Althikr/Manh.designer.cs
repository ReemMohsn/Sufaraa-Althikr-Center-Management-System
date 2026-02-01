namespace Sufaraa_Althikr
{
    partial class Manh
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.addSrudent_button = new Guna.UI2.WinForms.Guna2Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.addSrudent_button);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(774, 528);
            this.panel2.TabIndex = 40;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // addSrudent_button
            // 
            this.addSrudent_button.BackColor = System.Drawing.Color.Transparent;
            this.addSrudent_button.BorderColor = System.Drawing.Color.Transparent;
            this.addSrudent_button.BorderRadius = 10;
            this.addSrudent_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addSrudent_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.addSrudent_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.addSrudent_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.addSrudent_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.addSrudent_button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(78)))), ((int)(((byte)(12)))));
            this.addSrudent_button.Font = new System.Drawing.Font("Mizan AR+LT", 15F);
            this.addSrudent_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.addSrudent_button.ImageSize = new System.Drawing.Size(30, 30);
            this.addSrudent_button.Location = new System.Drawing.Point(274, 453);
            this.addSrudent_button.Name = "addSrudent_button";
            this.addSrudent_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.addSrudent_button.Size = new System.Drawing.Size(222, 63);
            this.addSrudent_button.TabIndex = 40;
            this.addSrudent_button.Text = "إضافة منهج";
            this.addSrudent_button.Click += new System.EventHandler(this.addSrudent_button_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(749, 388);
            this.flowLayoutPanel1.TabIndex = 41;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Manh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.ClientSize = new System.Drawing.Size(774, 528);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Manh";
            this.Text = "Manh";
            this.Load += new System.EventHandler(this.Manh_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button addSrudent_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}