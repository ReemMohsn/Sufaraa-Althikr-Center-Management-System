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
    public partial class studentMain : Form
    {
        private Guna.UI2.WinForms.Guna2Button lastClickedButton = null; // متغير لحفظ آخر بوتن تم الضغط عليه

        int RoleAccountStudentID;
        SufaraAldhikrEntities1 s=new SufaraAldhikrEntities1();
        string type = "s";
        int IDY;
        public studentMain(int roleAccountStudentID, int id_y)
        {
            RoleAccountStudentID= roleAccountStudentID;
             IDY = id_y;

            InitializeComponent();

            Profile_button.Click += Button_Click;
            Report_button.Click += Button_Click;
            AttAbs_button.Click += Button_Click;
            LogAsTeach_button.Click += Button_Click;
            guna2Button1.Click += Button_Click;

        }

        private void LogAsTeach_button_Click(object sender, EventArgs e)
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

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Profile_button_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            StudentProfile studentProfile = new StudentProfile(RoleAccountStudentID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(studentProfile);
            studentProfile.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            login.Show();
            this.Hide();
       
        }

        private void AttAbs_button_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            attendeandleave Attendeandleave = new attendeandleave(RoleAccountStudentID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(Attendeandleave);
            Attendeandleave.Show();
        }

        private void Report_button_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            ReportsMain reportsMain = new ReportsMain(IDY, RoleAccountStudentID, type) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(reportsMain);
            reportsMain.Show();
        }

        private void studentMain_Load(object sender, EventArgs e)
        {
            var teacherid = s.RoleAccounts
                         .Where(a => a.ID_RA == RoleAccountStudentID)
                        .Select(a => a)
                        .FirstOrDefault();
            int idA = teacherid.ID_A;
            int idr = teacherid.ID_A;
            int idy = teacherid.ID_Y;

            var numbers = s.RoleAccounts
                         .Where(a => a.ID_A == idA && a.ID_R == idr && a.ID_Y == idy)
                        .Select(a => a)
                        .ToList();
            if (numbers.Count > 1)
            {
                LogAsTeach_button.Visible = false;
            }
        }
    }
}
