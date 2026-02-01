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
    public partial class choseRole : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int RoleAccountStudentID;
        int RoleAccountTeacherID;
        List<int> RoleAccountIDs;
        int ID_Y = 0;

        public choseRole(List<int> roleAccountIDs)
        {
            RoleAccountIDs = roleAccountIDs;
            loaddata();
            InitializeComponent();


        }
        private void loaddata()
        {
            for (int i = 0; i < RoleAccountIDs.Count; i++)
            {
                int check = RoleAccountIDs[i];
                var nameRole = s.RoleAccounts
               .Join(s.Roles,
                     ra => ra.ID_R,
                     ro => ro.ID_R,
                     (ra, ro) => new { RoleAccount = ra, Role = ro })
               .Where(x => x.RoleAccount.ID_RA == check)
               .Select(x => x.Role.Name)
               .FirstOrDefault();
                if (nameRole == "Student")
                {
                    RoleAccountStudentID = RoleAccountIDs[i];
                }
                else
                {
                    RoleAccountTeacherID = RoleAccountIDs[i];
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void choseRole_Load(object sender, EventArgs e)
        {

        }
        private void LoginButton_Click_1(object sender, EventArgs e)
        {
            studentMain StudentMain = new studentMain(RoleAccountStudentID, ID_Y);
            StudentMain.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            StudentMain.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            mainTeacher mainteacher = new mainTeacher(RoleAccountTeacherID);
            mainteacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            mainteacher.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            login.Show();
            this.Hide();
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
