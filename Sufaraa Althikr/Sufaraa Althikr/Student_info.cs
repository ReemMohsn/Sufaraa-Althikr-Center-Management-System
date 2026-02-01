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
    public partial class Student_info : Form
    {
        SufaraAldhikrEntities1 s =new SufaraAldhikrEntities1();    
        int ID_Y;
        int ID_S;
        public Student_info(int ID_y,int ID_s)
        {
            ID_Y = ID_y;
            ID_S = ID_s;
            InitializeComponent();
        }

        private void Edit_button_Click(object sender, EventArgs e)
        {
            bool isYearNowActive = s.AcademicYears
                         .Where(a => a.ID_Y == ID_Y)
                        .Select(a => a.IsActive)
                        .FirstOrDefault();

            if (isYearNowActive)
            {
                StudentNameTB.Visible = true;
                hlg.Visible = true;
                PhoneNoTB.Visible = true;
                HomeNoTB.Visible = true;
                ParentNoTB.Visible = true;
                ReletiveTB.Visible = true;
                AddressTB.Visible = true;
                peroid2.Visible = true;
                Save_button.Visible = true;
                BeginSave2.Visible = true;
                KeepToCB.Visible=true;

                NameL.Visible = false;
                IDL.Visible = false;
                phoneNoL.Visible = false;
                HomeNoL.Visible = false;
                ParentNoL.Visible = false;
                RelativeL.Visible = false;
                AddressL.Visible = false;
                ToL.Visible = false;
                peroid.Visible = false;
                BeginSave.Visible = false;
                HelgaKindL.Visible = false;
                Edit_button.Visible = false;


                StudentNameTB.Text = NameL.Text;
                hlg.Text = HelgaKindL.Text;
                PhoneNoTB.Text = phoneNoL.Text;
                HomeNoTB.Text = HomeNoL.Text;
                ParentNoTB.Text = ParentNoL.Text;
                ReletiveTB.Text = RelativeL.Text;
                AddressTB.Text = AddressL.Text;
                KeepToCB.Value = int.Parse(ToL.Text);
                if (peroid.Text.Trim()=="صباحية")
                {
                    peroid2.SelectedIndex = 0;
                }else if (peroid.Text.Trim() == "مسائية")
                {
                    peroid2.SelectedIndex = 1;
                }
                int index = BeginSave2.FindStringExact(BeginSave.Text.Trim());
                if (index != -1)
                {
                    BeginSave2.SelectedIndex = index;
                }
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع تعديل بيانات الطالبة ");
            }
           



        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StudentNameTB.Text)||
                string.IsNullOrEmpty(PhoneNoTB.Text) ||
                string.IsNullOrEmpty(HomeNoTB.Text) ||
                string.IsNullOrEmpty(ParentNoTB.Text) ||
                string.IsNullOrEmpty(ReletiveTB.Text) ||
                string.IsNullOrEmpty(AddressTB.Text) ||
                peroid2.SelectedItem == null || BeginSave2.SelectedItem == null)
            {
                MessageBox.Show("قم بملاء جميع الحقول أولاً");
                return;
            }
            else
            {
                NameL.Text = StudentNameTB.Text;
                if (string.IsNullOrEmpty(hlg.Text))
                {
                    HelgaKindL.Text = "لا يوجد لهذا الطالبة حلقة ";
                }
                else
                {
                    HelgaKindL.Text = hlg.Text;
                }
                phoneNoL.Text = PhoneNoTB.Text;
                HomeNoL.Text = HomeNoTB.Text;
                ParentNoL.Text = ParentNoTB.Text;
                RelativeL.Text = ReletiveTB.Text;
                AddressL.Text = AddressTB.Text;
                peroid.Text = peroid2.SelectedItem.ToString();
                ToL.Text = KeepToCB.Value.ToString();
                BeginSave.Text = BeginSave2.SelectedItem.ToString();

                var student = s.Students
                              .Where(st => st.ID_S == ID_S && st.ID_Y == ID_Y)
                              .Select(a => a)
                              .FirstOrDefault();

                student.Name = NameL.Text;
                student.PhoneNumber = phoneNoL.Text;
                student.HomeNum = HomeNoL.Text;
                student.GuardianPhoNum = ParentNoL.Text;
                student.Guardian = RelativeL.Text;
                student.Address = AddressL.Text;
                student.PreSavAom = int.Parse(ToL.Text);
                student.Period = peroid.Text;
                student.BeginOfSave = BeginSave.Text;
                if (!string.IsNullOrEmpty(HelgaKindL.Text))
                {
                    var helga = s.Halgas
                    .Where(h => h.Name == HelgaKindL.Text && h.ID_Y == student.ID_Y)
                              .Select(a => a)
                              .FirstOrDefault();
                    if (helga != null)
                    {
                        student.ID_H = helga.ID_H;
                    }
                    else
                    {
                        MessageBox.Show("لم يتم إضافة هذا الطالب إلى حلقة معينه");
                    }
                }
               int check=s.SaveChanges();
                if (check>0)
                {
                    MessageBox.Show("تم تعديل بيانات الطالبة بنجاح");
                }
                else
                {
                    MessageBox.Show("فشلة عمليه تعديل الطالبة");
                    return;
                }


                StudentNameTB.Visible = false;
                hlg.Visible = false;
                PhoneNoTB.Visible = false;
                HomeNoTB.Visible = false;
                ParentNoTB.Visible = false;
                ReletiveTB.Visible = false;
                AddressTB.Visible = false;
                peroid2.Visible = false;
                Save_button.Visible = false;
                BeginSave2.Visible = false;


                IDL.Visible = true;
                phoneNoL.Visible = true;
                HomeNoL.Visible = true;
                ParentNoL.Visible = true;
                RelativeL.Visible = true;
                AddressL.Visible = true;
                ToL.Visible = true;
                peroid.Visible = true;
                BeginSave.Visible = true;
                HelgaKindL.Visible = true;
                Edit_button.Visible = true;
            }
            

        }

        private void Student_info_Load(object sender, EventArgs e)
        {
            var student= s.Students
                           .Where(st => st.ID_S == ID_S && st.ID_Y==ID_Y)
                           .Select(a => a)
                           .FirstOrDefault();
            var helga=s.Halgas
                 .Where(h=>h.ID_H==student.ID_H && h.ID_Y==student.ID_Y)
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
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ToL_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void AddressL_Click(object sender, EventArgs e)
        {

        }

        private void RelativeL_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NameL_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void IDL_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void phoneNoL_Click(object sender, EventArgs e)
        {

        }
    }
}
