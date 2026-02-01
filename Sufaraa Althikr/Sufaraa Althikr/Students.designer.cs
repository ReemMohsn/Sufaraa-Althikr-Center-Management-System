namespace Sufaraa_Althikr
{
    partial class Students
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.delete = new Guna.UI2.WinForms.Guna2Button();
            this.ListStudents = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Search_studentTB = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addSrudent_button = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.guna2Button1);
            this.guna2Panel1.Controls.Add(this.delete);
            this.guna2Panel1.Controls.Add(this.ListStudents);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.Search_studentTB);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.addSrudent_button);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(774, 528);
            this.guna2Panel1.TabIndex = 19;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(41)))), ((int)(((byte)(21)))));
            this.guna2Button1.Font = new System.Drawing.Font("Mizan AR+LT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.guna2Button1.Location = new System.Drawing.Point(293, 373);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(192, 41);
            this.guna2Button1.TabIndex = 25;
            this.guna2Button1.Text = "عرض بيانات الطالبة";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click_1);
            // 
            // delete
            // 
            this.delete.BorderRadius = 10;
            this.delete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.delete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.delete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.delete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.delete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(41)))), ((int)(((byte)(21)))));
            this.delete.Font = new System.Drawing.Font("Mizan AR+LT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.delete.Location = new System.Drawing.Point(517, 373);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(97, 41);
            this.delete.TabIndex = 24;
            this.delete.Text = "حدف";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // ListStudents
            // 
            this.ListStudents.FormattingEnabled = true;
            this.ListStudents.ItemHeight = 19;
            this.ListStudents.Location = new System.Drawing.Point(121, 126);
            this.ListStudents.Name = "ListStudents";
            this.ListStudents.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ListStudents.Size = new System.Drawing.Size(615, 232);
            this.ListStudents.TabIndex = 23;
            this.ListStudents.SelectedIndexChanged += new System.EventHandler(this.ListStudents_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mizan AR+LT", 15F);
            this.label2.Location = new System.Drawing.Point(621, 24);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(67, 36);
            this.label2.TabIndex = 22;
            this.label2.Text = "بحت:";
            // 
            // Search_studentTB
            // 
            this.Search_studentTB.BorderRadius = 5;
            this.Search_studentTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Search_studentTB.DefaultText = "بحث";
            this.Search_studentTB.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Search_studentTB.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Search_studentTB.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Search_studentTB.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Search_studentTB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Search_studentTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Search_studentTB.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Search_studentTB.Location = new System.Drawing.Point(227, 20);
            this.Search_studentTB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Search_studentTB.Name = "Search_studentTB";
            this.Search_studentTB.PasswordChar = '\0';
            this.Search_studentTB.PlaceholderText = "";
            this.Search_studentTB.SelectedText = "";
            this.Search_studentTB.Size = new System.Drawing.Size(387, 45);
            this.Search_studentTB.TabIndex = 21;
            this.Search_studentTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Search_studentTB.TextChanged += new System.EventHandler(this.Search_studentTB_TextChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mizan AR+LT", 15F);
            this.label1.Location = new System.Drawing.Point(634, 88);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(108, 36);
            this.label1.TabIndex = 20;
            this.label1.Text = "الطالبات:";
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
            this.addSrudent_button.Location = new System.Drawing.Point(33, 445);
            this.addSrudent_button.Name = "addSrudent_button";
            this.addSrudent_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.addSrudent_button.Size = new System.Drawing.Size(222, 63);
            this.addSrudent_button.TabIndex = 19;
            this.addSrudent_button.Text = "إضافة طالبة ";
            this.addSrudent_button.Click += new System.EventHandler(this.addSrudent_button_Click);
            // 
            // Students
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(219)))));
            this.ClientSize = new System.Drawing.Size(774, 528);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Students";
            this.Text = "Students";
            this.Load += new System.EventHandler(this.Students_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button delete;
        private System.Windows.Forms.ListBox ListStudents;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox Search_studentTB;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button addSrudent_button;
    }
}