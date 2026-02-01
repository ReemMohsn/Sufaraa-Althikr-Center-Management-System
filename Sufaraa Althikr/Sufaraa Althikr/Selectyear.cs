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
    public partial class Selectyear : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();


        public Selectyear()
        {
            InitializeComponent();
        }

    

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
         
        }

        private void Selectyear_Load(object sender, EventArgs e)
        {
            
                foreach (var i in s.AcademicYears)
                {
                    SelectYearCB.Items.Add(new KeyValuePair<int, string>(i.ID_Y, i.AcademicYear1));
                }
            SelectYearCB.DisplayMember = "Value";
            SelectYearCB.ValueMember = "Key";


        }

  

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (SelectYearCB.SelectedItem == null)
            {
                MessageBox.Show(" قم بإختيار سنة دراسية من القائمة إذا لم يوجد قم بإنشاء سنة دراسية جديدة");
            }
            else
            {
                var selectedPair = (KeyValuePair<int, string>)SelectYearCB.SelectedItem;
                int year = selectedPair.Key;

                Main mainAdmin = new Main(year);
                mainAdmin.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                mainAdmin.Show();
                this.Hide();

            }
        }

        private void NewYearButton_Click(object sender, EventArgs e)
        {
            bool isActive = false;
            foreach (var i in s.AcademicYears)
            {
                if (i.IsActive == true)
                {
                    MessageBox.Show("يوجد سنة دراسية جارية \nأنت لا تستطيع إنشاء سنة دراسية جديدة قبل إنتهاء السنه الحالية");
                    isActive = true;
                }
            }
            if (isActive == false)
            {
                DateYear dateYear = new DateYear();
                dateYear.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                dateYear.Show();
                this.Hide();

            }
        }

        private void X_button_Click_1(object sender, EventArgs e)
        {
            this.Close();

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
