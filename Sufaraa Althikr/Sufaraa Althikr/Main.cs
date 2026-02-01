using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sufaraa_Althikr
{
    public partial class Main : Form
    {
        private Guna.UI2.WinForms.Guna2Button lastClickedButton = null; // متغير لحفظ آخر بوتن تم الضغط عليه
        int YearID;
        public Main(int yearID)
        {
            InitializeComponent();
            YearID = yearID;

            Hel_button.Click += Button_Click;
            Stu_button.Click += Button_Click;
            Teach_button.Click += Button_Click;
            Manh_button.Click += Button_Click;
            LogOut_button.Click += Button_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // لما تضغطي على البوتن
        private void Button_Click(object sender, EventArgs e)
        {


            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;

            // إذا كان في بوتن تم الضغط عليه مسبقًا، يرجع لونه إلى الافتراضي
            if (lastClickedButton != null)
            {
                lastClickedButton.FillColor = Color.FromArgb(55, 41, 21);
            }

            // تغيير لون البوتن اللي تم الضغط عليه الآن
            clickedButton.FillColor = Color.Black;

            // تحديث آخر بوتن تم الضغط عليه
            lastClickedButton = clickedButton;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
        
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Hel_button_Click(object sender, EventArgs e)
        {

            this.guna2Panel1.Controls.Clear();
            Helga helga = new Helga(YearID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(helga);
            helga.Show();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Stu_button_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            Students students = new Students(YearID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(students);
            students.Show();
        }

        private void Teach_button_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            Teachers teachers = new Teachers(YearID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(teachers);
            teachers.Show();

        }

        private void Manh_button_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            Manh manh = new Manh() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(manh);
            manh.Show();
        }

        private void LogOut_button_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            login.Show();
            this.Hide();
        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }
    }
}
