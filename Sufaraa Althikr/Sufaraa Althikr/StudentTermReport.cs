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
    public partial class StudentTermReport : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int ID_Y;
        int ID_S;
        string Term;
        public StudentTermReport(int id_y, int id_s, string term)
        {
            ID_Y = id_y;
            ID_S = id_s;
            Term = term;
            InitializeComponent();
        }

        private void StudentTermReport_Load(object sender, EventArgs e)
        {
            try
            {
                // Fetch the term report for the student
                var studentTermReport = s.TermReports
                    .FirstOrDefault(r => r.Term == Term && r.ID_Y == ID_Y && r.ID_S == ID_S);

                if (studentTermReport == null)
                {
                    MessageBox.Show("لم يتم العثور على تقرير الطالب لهذا الفصل الدراسي.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Populate term report data
                keepFromL.Text = studentTermReport.SaveFrom ?? string.Empty;
                keepToL.Text = studentTermReport.SaveTo ?? string.Empty;
                reviewFromL.Text = studentTermReport.ReviewFrom ?? string.Empty;
                ReviewToL.Text = studentTermReport.ReviewTo ?? string.Empty;

                // Calculate monthly sums for both terms
                decimal monthly1Sum = s.MothlyReports
                    .Where(report => report.ID_S == ID_S && report.Term == "الأول")
                    .Sum(report => (decimal?)report.TestDegTotal) ?? 0m;

                decimal monthly2Sum = s.MothlyReports
                    .Where(report => report.ID_S == ID_S && report.Term == "الثاني")
                    .Sum(report => (decimal?)report.TestDegTotal) ?? 0m;

                // Calculate and display the average monthly sum
                monthlySumL.Text = ((monthly1Sum + monthly2Sum) / 2).ToString("F2"); // "F2" for 2 decimal places

                // Calculate total number of absent days
                int absentDays = s.AttendedAndAbsences
                    .Where(att => att.ID_S == ID_S && att.IsAttended.Trim() == "0")
                    .Count();
                abbDaysL.Text = absentDays.ToString();

                // Display total test degree and calculate the estimate
                totalL.Text = studentTermReport.TestDegTotal?.ToString() ?? "0";

                int total = Convert.ToInt32(studentTermReport.TestDegTotal);
                calculat_degree est = new calculat_degree();
                estimateL.Text = est.Estimate(total);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show($"حدث خطأ أثناء تحميل التقرير: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
