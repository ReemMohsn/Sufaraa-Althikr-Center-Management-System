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
    public partial class CheackStudentExist : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        RoleAccount RA = new RoleAccount();
        int ID_Y = 0;
        int ID_A = 0;
        string Typee;
        public CheackStudentExist(int ID_y, int ID_a,string type)
        {
            Typee = type;
            ID_A = ID_a;
            ID_Y = ID_y;
            InitializeComponent();
        }

        private void student_Id_KeyPress(object sender, KeyPressEventArgs e)
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

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(studentName.Text)  && !string.IsNullOrEmpty(student_Id.Text))
            {
              int StudentID = s.Students
              .Where(a => a.ID_S.ToString() == student_Id.Text && a.Name.ToString() == studentName.Text && a.ID_Y == ID_Y)
              .Select(a => a.ID_S)
              .FirstOrDefault();
                if (StudentID != 0)
                {
                    RA.ID_R = 1;
                    RA.ID_A = ID_A;
                    RA.ID_Y = ID_Y;
                    RA.LinkedID = StudentID;
                    RA.ID_RA = s.RoleAccounts.Max(R => R.ID_RA) + 1;
                    s.RoleAccounts.Add(RA);
                    int check = s.SaveChanges();

                    if (check > 0)
                    {
                        string fullName = studentName.Text;

                        // تحقق من صحة الاسم الرباعي
                        bool isValid = ValidateFullName(fullName);

                        if (isValid)
                        {
                            MessageBox.Show("تم تسجيلك كطالب بنجاح");
                            int RoleAcountID = RA.ID_RA;
                            studentMain StudentMain = new studentMain(RoleAcountID, ID_Y);
                            StudentMain.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                            StudentMain.Show();
                            this.Hide();
                            

                        }
                        else
                        {
                            MessageBox.Show("الاسم غير صالح. يجب أن يكون رباعي");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ أثناء تسجيلك كطالب");

                    }
                }
                else
                {
                    MessageBox.Show("لم يتم إيجاد أي طالب بهذاالإسم و رقم التعريف");

                }


            }
            else
            {
                MessageBox.Show("إملاء الحقول أولاَ");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            rocordAsStudentTeacher ocordAsStudentTeacher = new rocordAsStudentTeacher(ID_Y, ID_A,Typee);
            ocordAsStudentTeacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            ocordAsStudentTeacher.Show();
            this.Hide();
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
