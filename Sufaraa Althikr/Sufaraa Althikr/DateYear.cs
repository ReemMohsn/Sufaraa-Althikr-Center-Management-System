using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Infrastructure; // في حالة استخدام EF 6


namespace Sufaraa_Althikr
{
    public partial class DateYear : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        AcademicYear yr = new AcademicYear();
        public DateYear()
        {
            InitializeComponent();
        }

        private void DateYear_Load(object sender, EventArgs e)
        {
            setenddate();
        }
        void setenddate()
        {
            DateTime BeginDate = yaerBeginDate.Value; // التاريخ المدخل في dateTimePicker1
            if (BeginDate.Day > 10)
            {
                yaerEndDate.Value = BeginDate.AddMonths(7);

            }
            else
            {
                yaerEndDate.Value = BeginDate.AddMonths(6);
            }
        }

        private void yaerBeginDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void StartNewYearButton_Click_1(object sender, EventArgs e)
        {


            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

   

     

        private void StartNewYearButton_Click(object sender, EventArgs e)
        {
            DateTime ne = DateTime.Now; // التاريخ الحالي
            DateTime BeginDate = yaerBeginDate.Value; // التاريخ المدخل في dateTimePicker1
            DateTime EndDate = yaerEndDate.Value;
            if (BeginDate.Date > ne.Date)
            {
                DateTime EndRegesition = endRegesition.Value;
                if (EndRegesition.Date > BeginDate.Date && EndRegesition.Date <= EndDate.Date)
                {
                    try
                    {
                        yr.BeginOfYear = BeginDate.Date;
                        yr.EndOfYear = EndDate.Date;
                        yr.AcademicYear1 = $"{BeginDate.Year}-{EndDate.Year}";
                        yr.IsActive = true;
                        yr.StuRegSus = EndRegesition.Date;
                        s.AcademicYears.Add(yr);
                        int check = s.SaveChanges();
                        if (check > 0)
                        {
                            MessageBox.Show(" إنشاء السنة الدراسية تم بنجاح", "نجاح عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int year = yr.ID_Y;
                           


                            Main mainAdmin = new Main(year);
                            mainAdmin.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                            mainAdmin.Show();
                            this.Hide();
                        }
                        else
                        {

                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show("حدث خطأ غير متوقع: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(" تاريخ وقف تسجيل الطلاب ليس بين تاريخ بداية السنة الدراسية وتاريخ نهاية السنة الدراسية \nالرجاء إدخال تاريخ اخر");
                }

            }
            else
            {
                MessageBox.Show("تاريخ بداية السنة الدراسية قد مر بالفعل الرجاء إدخال تاريخ اخر");
            }
        }

        private void X_button_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yaerBeginDate_ValueChanged_2(object sender, EventArgs e)
        {
            setenddate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Selectyear selectyear = new Selectyear();
            selectyear.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            selectyear.Show();
            this.Hide();
        }
    }
}
