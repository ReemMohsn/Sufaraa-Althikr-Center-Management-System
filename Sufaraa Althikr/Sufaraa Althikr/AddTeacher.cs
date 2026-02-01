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
    public partial class AddTeacher : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        Teacher te= new Teacher();
        int ID_Y;
        public AddTeacher(int ID_y)
        {
            ID_Y = ID_y;
            InitializeComponent();
        }

        private void AddTeacher_Load(object sender, EventArgs e)
        {
            TeachPhoneNoTB.KeyPress += new KeyPressEventHandler(TeachPhoneNoTB_KeyPress);
            TeachPhoneNoTB.TextChanged += new EventHandler(TeachPhoneNoTB_TextChanged);
        }

        private void ID_t_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط للأرقام
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }


        private void TeachPhoneNoTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط للأرقام
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void TeachPhoneNoTB_TextChanged(object sender, EventArgs e)
        {
            // التأكد من أن طول الرقم لا يتجاوز 9 أرقام
            if (TeachPhoneNoTB.Text.Length > 9)
            {
                MessageBox.Show("رقم الهاتف يجب أن يكون مكونًا من 9 أرقام فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TeachPhoneNoTB.Text = TeachPhoneNoTB.Text.Substring(0, 9); // تقليص الرقم إلى 9 أرقام فقط
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Teach_nameTB.Text) || string.IsNullOrEmpty(TeachPhoneNoTB.Text) || Teach_level.SelectedItem == null || Period.SelectedItem == null)
            {
                MessageBox.Show("قم بملاء الحقو أولاَ");
            }
            else
            {

                string fullName = Teach_nameTB.Text;

                // تحقق من صحة الاسم الرباعي
                bool isValid = ValidateFullName(fullName);

                if (isValid)
                {
                    te.Name = Teach_nameTB.Text;
                }
                else
                {
                    MessageBox.Show("الاسم غير صالح. يجب أن يكون رباعي");
                }
                
                te.Level = Teach_level.SelectedItem.ToString();
                te.PhoneNumber = TeachPhoneNoTB.Text;
                te.Period = Period.SelectedItem.ToString();
                te.ID_Y = ID_Y;
                if (string.IsNullOrEmpty(ID_t.Text))
                {
                    te.ID_T = s.Teachers.Any() ? s.Teachers.Max(tt => tt.ID_T) + 1 : 1;
                }
                else
                {
                    int ID_tt = int.Parse(ID_t.Text);
                    bool isexsit = false;
                    isexsit = s.Teachers.Where(Te=> Te.ID_T== ID_tt && Te.ID_Y==ID_Y)
                                   .Select(h => true)
                                   .FirstOrDefault();
                    if(isexsit)
                    {
                        MessageBox.Show("رقم التعريف المدخل موجود بالفعل في هذه السنة ");
                        return;
                    }
                    else
                    {
                        var existingteacher = s.Teachers.FirstOrDefault(a => a.ID_T == ID_tt && a.Name == Teach_nameTB.Text);
                        if (existingteacher != null)
                        {
                            te.ID_T = int.Parse(ID_t.Text);
                        }
                        else
                        {
                            MessageBox.Show("لم يتم إجاد أي بيانات لرقم التعريف المدخل في السنيين الماضية الرجاء التأكد من رقم التعريف المدخل ");
                            return;
                        }
                    }
                   
                }
                s.Teachers.Add(te);
                int check = s.SaveChanges();
                if (check > 0)
                {
                    MessageBox.Show(" إضافة المعلمة تم بنجاح", "نجاح عملية الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(" إضافة المعلمة فشلة ", "فشل عملية الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void Teach_nameTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
