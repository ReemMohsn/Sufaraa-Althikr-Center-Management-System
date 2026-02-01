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
    public partial class Term_rebort : Form
    {
        int ID_S;
        int ID_Y;
        string Type;
        private SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        public Term_rebort(int id_y, int id_s, string term)
        {
            ID_S = id_s;
            ID_Y = id_y;
            Type = term;
            InitializeComponent();
            Term_reportDGV.CellDoubleClick += Term_reportDGV_CellDoubleClick;
        }

        private void Term_rebort_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTermReportData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل بيانات التقرير الفصلي: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTermReportData()
        {
            // Fetch the term report for the student
            var termReport = s.TermReports
                .FirstOrDefault(r => r.ID_S == ID_S && r.ID_Y == ID_Y && r.Term == Type);

            if (termReport == null)
            {
                MessageBox.Show("لم يتم العثور على تقرير للفصل المحدد.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fetch student's name
            var studentName = s.Students
                .Where(stu => stu.ID_S == ID_S)
                .Select(stu => stu.Name)
                .FirstOrDefault() ?? "غير معروف";

            // Fetch the count of absent days
            int absentDays = s.AttendedAndAbsences
                .Where(a => a.ID_S == ID_S && a.IsAttended.Trim() == "0")
                .Count();

            // Calculate estimate
            calculat_degree est = new calculat_degree();
            int total = Convert.ToInt32(termReport.TestDegTotal ?? 0);
            string estimate = est.Estimate(total);

            // Populate DataGridView
            Term_reportDGV.Rows.Clear(); // Clear existing rows before adding new data
            Term_reportDGV.Rows.Add(
                studentName,                          // StuName
                termReport.SaveFrom ?? "غير متوفر", // KeepFrom
                termReport.SaveTo ?? "غير متوفر",   // KeepTo
                termReport.ReviewFrom ?? "غير متوفر", // ReviewFrom
                termReport.ReviewTo ?? "غير متوفر",   // ReviewTo
                termReport.TestResMon ?? 0,           // MonthlyResult
                termReport.TestDegRev ?? 0,           // TermTestDegree
                absentDays,                           // AbsCountDays
                total,                                // Total
                estimate                              // Estimate
            );
        }

        private void Term_reportDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header row double-clicks

            try
            {
                // Extract data from the selected row
                string stuName = Term_reportDGV.Rows[e.RowIndex].Cells["StuName"].Value?.ToString() ?? string.Empty;
                string keepFrom = Term_reportDGV.Rows[e.RowIndex].Cells["KeepFrom"].Value?.ToString() ?? string.Empty;
                string keepTo = Term_reportDGV.Rows[e.RowIndex].Cells["KeepTo"].Value?.ToString() ?? string.Empty;
                string reviewFrom = Term_reportDGV.Rows[e.RowIndex].Cells["ReviewFrom"].Value?.ToString() ?? string.Empty;
                string reviewTo = Term_reportDGV.Rows[e.RowIndex].Cells["ReviewTo"].Value?.ToString() ?? string.Empty;
                string monthlyResult = Term_reportDGV.Rows[e.RowIndex].Cells["MonthlyResult"].Value?.ToString() ?? "0";
                string termTestDegree = Term_reportDGV.Rows[e.RowIndex].Cells["TermTestDegree"].Value?.ToString() ?? "0";
                string absCountDays = Term_reportDGV.Rows[e.RowIndex].Cells["AbsCountDays"].Value?.ToString() ?? "0";
                string total = Term_reportDGV.Rows[e.RowIndex].Cells["Total"].Value?.ToString() ?? "0";
                string estimate = Term_reportDGV.Rows[e.RowIndex].Cells["Estimate"].Value?.ToString() ?? string.Empty;

                // Navigate to the TermReportT form, passing the data
                TermReportT termReportT = new TermReportT(ID_Y, ID_S, Type)
                {
                    StudentID = ID_S,
                    YearID = ID_Y,
                    KeepFrom = keepFrom,
                    KeepTo = keepTo,
                    ReviewFrom = reviewFrom,
                    ReviewTo = reviewTo,
                    MonthlyResult = monthlyResult,
                    TermTestDegree = termTestDegree,
                    AbsCountDays = absCountDays,
                    Total = total,
                    Estimate = estimate
                };
                termReportT.ShowDialog();

                // Reload data after the dialog is closed to reflect changes
                LoadTermReportData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء محاولة فتح السجل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Term_reportDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
