namespace Sufaraa_Althikr
{
    partial class form
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
            this.LBDATE = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.calendar = new System.Windows.Forms.TableLayoutPanel();
            this.Cancel_button = new Guna.UI2.WinForms.Guna2Button();
            this.Save_button = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // LBDATE
            // 
            this.LBDATE.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBDATE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(41)))), ((int)(((byte)(21)))));
            this.LBDATE.Location = new System.Drawing.Point(261, 47);
            this.LBDATE.Name = "LBDATE";
            this.LBDATE.Size = new System.Drawing.Size(276, 42);
            this.LBDATE.TabIndex = 77;
            this.LBDATE.Text = "MONTH YEAR";
            this.LBDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(388, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 27);
            this.label5.TabIndex = 75;
            // 
            // calendar
            // 
            this.calendar.AutoScroll = true;
            this.calendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(41)))), ((int)(((byte)(21)))));
            this.calendar.ColumnCount = 2;
            this.calendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.calendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.calendar.Location = new System.Drawing.Point(12, 109);
            this.calendar.Name = "calendar";
            this.calendar.Padding = new System.Windows.Forms.Padding(3);
            this.calendar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.calendar.RowCount = 2;
            this.calendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.calendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.calendar.Size = new System.Drawing.Size(750, 330);
            this.calendar.TabIndex = 93;
            this.calendar.Paint += new System.Windows.Forms.PaintEventHandler(this.calendar_Paint);
            // 
            // Cancel_button
            // 
            this.Cancel_button.BorderRadius = 10;
            this.Cancel_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Cancel_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Cancel_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Cancel_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Cancel_button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(78)))), ((int)(((byte)(12)))));
            this.Cancel_button.Font = new System.Drawing.Font("Mizan AR+LT", 15F);
            this.Cancel_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.Cancel_button.Location = new System.Drawing.Point(163, 457);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(184, 48);
            this.Cancel_button.TabIndex = 95;
            this.Cancel_button.Text = "الشهر السابق";
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // Save_button
            // 
            this.Save_button.BorderRadius = 10;
            this.Save_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Save_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Save_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Save_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Save_button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(41)))), ((int)(((byte)(21)))));
            this.Save_button.Font = new System.Drawing.Font("Mizan AR+LT", 15F);
            this.Save_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.Save_button.Location = new System.Drawing.Point(449, 457);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(184, 48);
            this.Save_button.TabIndex = 94;
            this.Save_button.Text = "الشهر التالي";
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.ClientSize = new System.Drawing.Size(774, 528);
            this.ControlBox = false;
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.calendar);
            this.Controls.Add(this.LBDATE);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form";
            this.Text = "atendentAndLeave";
            this.Load += new System.EventHandler(this.atendentAndLeave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBDATE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel calendar;
        private Guna.UI2.WinForms.Guna2Button Cancel_button;
        private Guna.UI2.WinForms.Guna2Button Save_button;
    }
}