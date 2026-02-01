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
    public partial class mainTeacher : Form
    {
        private Guna.UI2.WinForms.Guna2Button lastClickedButton = null; // متغير لحفظ آخر بوتن تم الضغط عليه

        int RoleAccountTeacherID;
        SufaraAldhikrEntities1 s=new SufaraAldhikrEntities1();
        public mainTeacher(int roleAccountTeacherID)
        {

            InitializeComponent();
            RoleAccountTeacherID = roleAccountTeacherID;

            guna2Button1.Click += Button_Click;
            guna2Button3.Click += Button_Click;
            LogAsStu_button.Click += Button_Click;
            LogOut_button.Click += Button_Click;
            
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
        private void mainTeacher_Load(object sender, EventArgs e)
        {
            var teacherid = s.RoleAccounts
                         .Where(a => a.ID_RA == RoleAccountTeacherID)
                        .Select(a => a)
                        .FirstOrDefault();
            int idA = teacherid.ID_A;
            int idr = teacherid.ID_A;
            int idy = teacherid.ID_Y;

            var numbers = s.RoleAccounts
                         .Where(a => a.ID_A == idA&&a.ID_R== idr&& a.ID_Y== idy)
                        .Select(a => a)
                        .ToList();
            if (numbers.Count > 1)
            {
                LogAsStu_button.Visible = false;
            }

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            TeacherProfile teacherProfile = new TeacherProfile(RoleAccountTeacherID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(teacherProfile);
            teacherProfile.Show();
        }

        private void LogOut_button_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            login.Show();
            this.Hide();

       
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            this.guna2Panel1.Controls.Clear();
            halgasTeacher halgasteacher = new halgasTeacher(RoleAccountTeacherID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(halgasteacher);
            halgasteacher.Show();
            
        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void LogAsStu_button_Click(object sender, EventArgs e)
        {

        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
