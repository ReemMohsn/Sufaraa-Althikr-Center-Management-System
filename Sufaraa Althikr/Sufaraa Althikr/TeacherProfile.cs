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
    public partial class TeacherProfile : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int roleAccountTeacherID;

        public TeacherProfile(int RoleAccountTeacherID)
        {
            InitializeComponent();
            roleAccountTeacherID = RoleAccountTeacherID;
        }

        private void TeacherProfile_Load(object sender, EventArgs e)
        {
            Load_Teacher_Profile(roleAccountTeacherID);
        }

        private void Load_Teacher_Profile(int RoleAccountTeacherID)
        {
            int teacherid = s.RoleAccounts
                         .Where(a => a.ID_RA == roleAccountTeacherID)
                        .Select(a => a.LinkedID)
                        .FirstOrDefault();

            int idy = s.RoleAccounts
                        .Where(a => a.ID_RA == roleAccountTeacherID)
                       .Select(a => a.ID_Y)
                       .FirstOrDefault();

            var teacher1 = s.Teachers
                        .Where(a => a.ID_T == teacherid && a.ID_Y == idy)
                       .Select(a => a)
                       .FirstOrDefault();


            IDT.Text = teacher1.ID_T.ToString();
            NameL.Text = teacher1.Name;
            LevelL.Text = teacher1.Level;
            periodL.Text = teacher1.Period;
            phoneNoL.Text = teacher1.PhoneNumber;
        }
    }
}
