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
    public partial class checkTeacherExist : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        RoleAccount RA = new RoleAccount();
        int ID_Y = 0;
        int ID_A= 0;
        string Typee;
        public checkTeacherExist(int ID_y,int ID_a, string type)
        {
            Typee= type;
            ID_A = ID_a;
            ID_Y = ID_y;
            InitializeComponent();
        }

        private void checkTeacherExist_Load(object sender, EventArgs e)
        {

        }




        private void Teach_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // منع الإدخال
            }
        }
        private bool ValidateFullName(string fullName)
        {
            // تحقق من أن الإدخال ليس فارغًا
            if (string.IsNullOrWhiteSpace(fullName))


                // إزالة المسافات الزائدة
                fullName = fullName.Trim();

            // السماح بالأسماء المركبة (مثل "بن ناشيء")
            string[] parts = fullName.Split(' ');

            // دمج الأجزاء المركبة مثل "بن ناشيء" في جزء واحد
            List<string> validParts = new List<string>();

            return false;

            for (int i = 0; i < parts.Length; i++)
            {
                if (i > 0 && parts[i - 1].ToLower() == "بن") // تحقق إذا الجزء السابق هو "بن"
                {
                    validParts[validParts.Count - 1] += " " + parts[i]; // دمج "بن" مع الجزء التالي
                    return true;
                }
                else
                {
                    validParts.Add(parts[i]);
                    return false;
                }

            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(teacherName.Text) && !string.IsNullOrEmpty(Teach_ID.Text))
            {

                int teacherID = s.Teachers
               .Where(a => a.ID_T.ToString() == Teach_ID.Text && a.Name == teacherName.Text && a.ID_Y == ID_Y)
              .Select(a => a.ID_T)
              .FirstOrDefault();
                if (teacherID != 0)
                {
                    RA.ID_R = 2;
                    RA.ID_A = ID_A;
                    RA.ID_Y = ID_Y;
                    RA.LinkedID = teacherID;
                    RA.ID_RA = s.RoleAccounts.Max(R => R.ID_RA) + 1;
                    s.RoleAccounts.Add(RA);
                    int check = s.SaveChanges();
                    if (check > 0)
                    {
                        string fullName = teacherName.Text;

                        // تحقق من صحة الاسم الرباعي
                        bool isValid = ValidateFullName(fullName);

                        if (isValid)
                        {
                            MessageBox.Show("تم تسجيلك كمعلم في هذه السنة الدراسية");
                            int RoleAcountID = RA.ID_RA;
                            mainTeacher MainTeacher = new mainTeacher(RoleAcountID);
                            MainTeacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                            MainTeacher.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("الاسم غير صالح. يجب أن يكون رباعي");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ أثناء تسجيلك كمعلم");

                    }
                }
                else
                {
                    // إذا كان المعلم غير موجود
                    MessageBox.Show("أنت غير مسجل في المركز كمعلم الرجاء التحقق من المعلومات المدخلة أو مراجعة إدارة المركز");
                }

            }
            else
            {
                MessageBox.Show("إملاء الحقول أولاَ");
            }
        }

        private void teacherName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Teach_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {

        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            rocordAsStudentTeacher ocordAsStudentTeacher = new rocordAsStudentTeacher(ID_Y,ID_A,Typee);
            ocordAsStudentTeacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            ocordAsStudentTeacher.Show();
            this.Hide();
        }
    }
}
