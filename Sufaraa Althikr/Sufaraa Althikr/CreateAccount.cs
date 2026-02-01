using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Sufaraa_Althikr
{
    public partial class CreateAccount : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        int ID_Y = 0;
        public CreateAccount(int ID_y)
        {
            ID_Y = ID_y;
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateAccountButton_Click_1(object sender, EventArgs e)
        {

        }

        private void CreateAccountButton_Click_2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GmailTB.Text) && !string.IsNullOrEmpty(confirmPassword.Text) && !string.IsNullOrEmpty(PasswordTB.Text) && !string.IsNullOrEmpty(UsernameTB.Text))
            {
                string password = PasswordTB.Text;

                // شروط كلمة المرور
                bool isValid = ValidatePassword(password);

                if (isValid)
                {
                    if (PasswordTB.Text != confirmPassword.Text)
                    {
                        MessageBox.Show("كلمة المرور وتأكيد كلمة المرور غير متطابقين.");

                    }
                }
                else
                {
                    MessageBox.Show("كلمة المرور غير صحيحة. يجب أن تحتوي على 8 أحرف على الأقل، حرف كبير، حرف صغير، ورقم.");
                }
            }


            else
            {
                MessageBox.Show("يرجى ملء جميع الحقول.");
            }

            // البحث عن البريد الإلكتروني في جدول الحسابات
            bool emailExists = s.Accounts
                .Any(a => a.Email == GmailTB.Text);

            if (emailExists)
            {
                // إذا كان البريد الإلكتروني موجودًا بالفعل
                MessageBox.Show("هذا البريد الإلكتروني مستخدم بالفعل. يرجى استخدام بريد إلكتروني آخر.");
            }

            else
            {

                string email = GmailTB.Text;
                if (IsValidEmail(email))
                {
                    // إنشاء حساب جديد لأن البريد الإلكتروني غير موجود
                    Account newAccount = new Account
                    {
                        Email = GmailTB.Text,
                        Password = PasswordTB.Text,
                        UserName = UsernameTB.Text
                    };

                    s.Accounts.Add(newAccount);
                    int check = s.SaveChanges();

                    if (check > 0)
                    {
                        MessageBox.Show("تم إنشاء الحساب بنجاح.");
                        int ID_A = newAccount.ID_A;
                        bool isNowActive = s.AcademicYears
                        .Where(a => a.ID_Y == ID_Y)
                        .Select(a => a.IsActive)
                        .FirstOrDefault();
                        if (isNowActive)
                        {


                            rocordAsStudentTeacher RocordAsStudentTeacher = new rocordAsStudentTeacher(ID_Y, ID_A,"signup");
                            RocordAsStudentTeacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                            RocordAsStudentTeacher.Show();
                            this.Hide();
                        }
                        else
                        {
                            //MessageBox.Show(" تم إنشاء الحساب لا يوجد هنالك سنة دراسية جارية ,الرجاء المحاولة في وقت لاحق");
                        }
                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ أثناء إنشاء الحساب.");

                    }
                }
                else
                {
                    MessageBox.Show("يرجى إدخال بريد إلكتروني صالح.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    
        private bool ValidatePassword(string password)
        {
            // تحقق من الطول
            if (password.Length < 8)
                return false;

            // تحقق من وجود حرف كبير
            if (!password.Any(char.IsUpper))
                return false;

            // تحقق من وجود حرف صغير
            if (!password.Any(char.IsLower))
                return false;

            // تحقق من وجود رقم
            if (!password.Any(char.IsDigit))
                return false;
            return true;
            
        }

        private bool IsValidEmail(string email)
        {
            // التعبير المنتظم للتحقق من البريد الإلكتروني
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GmailTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            login.Show();
            this.Hide();
        }
    }
}
