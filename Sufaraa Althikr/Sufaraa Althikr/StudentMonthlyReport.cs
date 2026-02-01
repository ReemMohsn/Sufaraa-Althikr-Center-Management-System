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
    public partial class StudentMonthlyReport : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int ID_Y;
        int ID_S;
        string Term;
        int Type;
        public StudentMonthlyReport(int id_y, int id_s, string term, int type)
        {
            InitializeComponent();
            ID_Y = id_y;
            ID_S = id_s;
            Term = term;
            Type = type;
        }

        private void StudentMonthlyReport_Load(object sender, EventArgs e)
        {
            try
            {
                // Fetch the specific monthly report for the student
                var stu = s.MothlyReports
                    .FirstOrDefault(r => r.ID_S == ID_S && r.ID_Y == ID_Y && r.Term == Term && r.Month == Type);

                if (stu == null)
                {
                    MessageBox.Show("لم يتم العثور على تقرير شهري للطالب.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InitializeEmptyFields();
                    return;
                }

                // Populate report data
                keepFromL.Text = stu.SaveFrom ?? string.Empty;
                keepToL.Text = stu.SaveTo ?? string.Empty;
                reviewFromL.Text = stu.ReviewFrom ?? string.Empty;
                ReviewToL.Text = stu.ReviewTo ?? string.Empty;
                keepDegreeL.Text = stu.TestDegSave?.ToString() ?? "0";
                reviewDegreeL.Text = stu.TestDegReview?.ToString() ?? "0";

                // Count absent days
                int absentDays = s.AttendedAndAbsences
                    .Where(a => a.ID_S == ID_S && a.IsAttended.Trim() == "0")
                    .Count();
                abbDaysL.Text = absentDays.ToString();

                // Display total test degree
                totalL.Text = stu.TestDegTotal?.ToString() ?? "0";

                // Calculate the estimate
                int total = Convert.ToInt32(stu.TestDegTotal);
                calculat_degree est = new calculat_degree();
                estimateL.Text = est.Estimate(total);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show($"حدث خطأ أثناء تحميل التقرير: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InitializeEmptyFields();
            }
        }
        private void InitializeEmptyFields()
        {
            keepFromL.Text = string.Empty;
            keepToL.Text = string.Empty;
            reviewFromL.Text = string.Empty;
            ReviewToL.Text = string.Empty;
            keepDegreeL.Text = "0";
            reviewDegreeL.Text = "0";
            abbDaysL.Text = "0";
            totalL.Text = "0";
            estimateL.Text = string.Empty;
        }


    }
    public class calculat_degree
    {
        public string Estimate(int total)
        {
            string estimation;

            if (total >= 90)
            {
                estimation = "ممتاز";
            }
            else if (total >= 75)
            {
                estimation = "جيد جدًا";
            }
            else if (total >= 60)
            {
                estimation = "جيد";
            }
            else if (total >= 50)
            {
                estimation = "مقبول";
            }
            else
            {
                estimation = "راسب";
            }

            return estimation;
        }
    }
}
