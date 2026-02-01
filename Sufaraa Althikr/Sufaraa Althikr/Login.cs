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
    public partial class Login : Form

    {
        bool IsyearNewActive = false;
        //إذا كان بيساوي صفر معناه لايوجد سنة دراسية
        int ID_Y = 0;

        SufaraAldhikrEntities1 s =new SufaraAldhikrEntities1();
        //RoleAccount roleAccount=new RoleAccount();

        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }



        private void Login_Load(object sender, EventArgs e)
        {
            loadYear();

        }

        private void LoginButton_Click_1(object sender, EventArgs e)
        {
           
        }
        private void loadYear()
        {
            IsyearNewActive = s.AcademicYears
        .Where(a => a.IsActive)
        .Select(a => a.IsActive)
        .FirstOrDefault();
            ID_Y = s.AcademicYears
          .Where(a => a.IsActive)
          .Select(a => a.ID_Y)
          .FirstOrDefault();
        }

        private void CreateNewAccL_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void GmailTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PasswordTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

        }

        private void CreateNewAccL_Click_1(object sender, EventArgs e)
        {
         
        }

        private void LoginButton_Click_2(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GmailTB.Text) && !string.IsNullOrEmpty(PasswordTB.Text))
            {
                MessageBox.Show("قم بملاء الحقول أولاً");
            }
            else
            {
                //if (GmailTB.Text == "Admin46@gmail.com" && PasswordTB.Text == "12345Admin")
                    if (GmailTB.Text == "1" && PasswordTB.Text == "1")
                {
                    string email = GmailTB.Text;
                    Selectyear selectyear = new Selectyear();
                    selectyear.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                    selectyear.Show();
                    this.Hide();
                }
                else
                {
                    bool IsExist = false;
                    Account account = s.Accounts
                      .Where(a => a.Email == GmailTB.Text && a.Password == PasswordTB.Text)
                      .FirstOrDefault();
                    if (account != null)
                    {
                        IsExist = true;
                        List<int> RoleAccountIDs = new List<int>();
                        foreach (var Ro in s.RoleAccounts)
                        {
                            bool year = false;
                            year = s.AcademicYears
                            .Where(a => a.ID_Y == Ro.ID_Y)
                            .Select(a => a.IsActive)
                            .FirstOrDefault();
                            if (Ro.ID_A == account.ID_A && year)
                            {
                                RoleAccountIDs.Add(Ro.ID_R);
                            }

                        }
                        if (RoleAccountIDs.Count > 1)
                        {
                            choseRole ChoseRole = new choseRole(RoleAccountIDs);
                            ChoseRole.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                            ChoseRole.Show();
                            this.Hide();
                        }
                        else if (RoleAccountIDs.Count == 1)
                        {
                            int check = RoleAccountIDs[0];
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
                                studentMain StudentMain = new studentMain(check, ID_Y);
                                StudentMain.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                                StudentMain.Show();
                                this.Hide();
                              
                            }
                            else
                            {

                                mainTeacher mainteacher = new mainTeacher(RoleAccountIDs[0]);
                                mainteacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                                mainteacher.Show();
                                this.Hide();
                            }
                        }
                        else
                        {

                            if (IsyearNewActive)
                            {
                                MessageBox.Show("هذا الحساب موجود ولكن أنت غير مسجل كطالب أو كمعلم في هذه السنة الدراسية \nالرجاء القيام بالتسجيل");
                             
                                rocordAsStudentTeacher RocordAsStudentTeacher = new rocordAsStudentTeacher(ID_Y, account.ID_A,"login");
                                RocordAsStudentTeacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                                RocordAsStudentTeacher.Show();
                                this.Hide();

                            }
                            else
                            {
                                MessageBox.Show("هذا الحساب موجود ولكن لايوجد هنالك سنة دراسية جارية \nالرجاء المحاولة في وقت لاحق");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("هذا الحساب غير موجود\nقم بإنشاء حساب جديد");

                    }
                }
            }
        }

        private void CreateNewAccL_Click_2(object sender, EventArgs e)
        {

            CreateAccount createAccount = new CreateAccount(ID_Y);
            createAccount.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            createAccount.Show();
            this.Hide();
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
