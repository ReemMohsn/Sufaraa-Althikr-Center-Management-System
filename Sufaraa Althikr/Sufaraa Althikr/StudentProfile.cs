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
    public partial class StudentProfile : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int roleAccountStudentID;

        public StudentProfile(int RoleAccountStudentID)
        {
            InitializeComponent();
            roleAccountStudentID = RoleAccountStudentID;
        }

        private void StudentProfile_Load(object sender, EventArgs e)
        {
            try
            {
                Load_Student_Profile(roleAccountStudentID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل الملف الشخصي: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Load_Student_Profile(int RoleAccountStudentID)
        {

            int studentid = s.RoleAccounts
                        .Where(a => a.ID_RA == roleAccountStudentID)
                       .Select(a => a.LinkedID)
                       .FirstOrDefault();

            int idy = s.RoleAccounts
                        .Where(a => a.ID_RA == roleAccountStudentID)
                       .Select(a => a.ID_Y)
                       .FirstOrDefault();

            var student = s.Students
                        .Where(a => a.ID_S == studentid && a.ID_Y == idy)
                       .Select(a => a)
                       .FirstOrDefault();


            //var student = s.Students
            //              .Where(st => st.ID_S == ID_S && st.ID_Y == ID_Y)
            //              .Select(a => a)
            //              .FirstOrDefault();


            var helga = s.Halgas
                 .Where(h => h.ID_H == student.ID_H && h.ID_Y == student.ID_Y)
                           .Select(a => a)
                           .FirstOrDefault();
            if (student != null)
            {
                NameL.Text = student.Name;
                IDL.Text = student.ID_S.ToString();
                phoneNoL.Text = student.PhoneNumber;
                HomeNoL.Text = student.HomeNum;
                ParentNoL.Text = student.GuardianPhoNum;
                RelativeL.Text = student.Guardian;
                AddressL.Text = student.Address;
                ToL.Text = student.PreSavAom.ToString();
                peroid.Text = student.Period;
                BeginSave.Text = student.BeginOfSave;
                if (helga != null)
                {
                    HelgaKindL.Text = helga.Name;
                }
            }
            else
            {
                MessageBox.Show("هذا الطالب غير موجود في السنة الحالية");
            }
        }
    }
    
    
}
