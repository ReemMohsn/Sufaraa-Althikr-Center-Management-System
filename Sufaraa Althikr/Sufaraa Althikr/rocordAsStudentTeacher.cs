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
    public partial class rocordAsStudentTeacher : Form
    {
        int ID_Y = 0;
        int ID_A = 0;
        string Typee;
        public rocordAsStudentTeacher(int ID_y,int ID_a,string type)
        {
            Typee=type;
            ID_Y = ID_y;
            ID_A = ID_a;
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

     

        

        private void guna2Button1_Click_2(object sender, EventArgs e)
        {
            checkTeacherExist CheckTeacherExist = new checkTeacherExist(ID_Y, ID_A, Typee);
            CheckTeacherExist.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            CheckTeacherExist.Show();
            this.Hide();
        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem.ToString() == "طالب جديد")
            {


                AddStudent addStudent = new AddStudent(ID_Y, ID_A, Typee);
                addStudent.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                addStudent.Show();
                this.Hide();
            }
            else
            {
                CheackStudentExist cheackStudentExist = new CheackStudentExist(ID_Y, ID_A, Typee);
                cheackStudentExist.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                cheackStudentExist.Show();
                this.Hide();

            }
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rocordAsStudentTeacher_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Typee== "login")
            {
                Login login = new Login();
                login.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                login.Show();
                this.Hide();
            }
            else
            {
                CreateAccount createAccount = new CreateAccount(ID_Y);
                createAccount.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                createAccount.Show();
                this.Hide();
            }
            
        }
    }
}
