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
    public partial class TermReportT : Form
    {
        public string StuName { get; set; }
        public string KeepFrom { get; set; }
        public string KeepTo { get; set; }
        public string ReviewFrom { get; set; }
        public string ReviewTo { get; set; }
        public string MonthlyResult { get; set; }
        public string TermTestDegree { get; set; }
        public string AbsCountDays { get; set; }
        public string Total { get; set; }
        public string Estimate { get; set; }
        public int StudentID { get; set; }
        public int YearID { get; set; }



        private readonly SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int ID_S;
        int ID_Y;
        string Type;
        public TermReportT(int id_y, int id_s, string type)
        {
            ID_S = id_s;
            ID_Y = id_y;
            Type = type;
            InitializeComponent();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void TermReportT_Load(object sender, EventArgs e)
        {
            KeepFromL.Text = KeepFrom;
            KeepToCB.Text = KeepTo;
            ReviewFromL.Text = ReviewFrom;
            ReviewToCB.Text = ReviewTo;
            MonthlyResultL.Text = MonthlyResult;
            TermDegreeTB.Text = TermTestDegree;
            TermTotalL.Text = Total;
            TermEstimateL.Text = Estimate;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Find existing record in TermReport
                var existingReport = s.TermReports
                    .FirstOrDefault(r => r.ID_Y == ID_Y && r.ID_S == ID_S && r.Term == Type);

                var ID_h = s.Students
                  .Where(a => a.ID_S == StudentID && a.ID_Y == YearID)
                  .Select(a => a.ID_H)
                  .FirstOrDefault();

                int id_h = ID_h.Value;
                if (existingReport != null)
                {
                    // Update existing record
                    existingReport.SaveFrom = KeepFromL.Text;
                    existingReport.SaveTo = KeepToCB.Text;
                    existingReport.ReviewFrom = ReviewFromL.Text;
                    existingReport.ReviewTo = ReviewToCB.Text;
                    existingReport.TestDegSave = decimal.TryParse(TermDegreeTB.Text, out var saveDegree) ? saveDegree : (decimal?)null;
                    existingReport.TestDegRev = decimal.TryParse(MonthlyResultL.Text, out var reviewDegree) ? reviewDegree : (decimal?)null;
                    existingReport.TestDegTotal = decimal.TryParse(TermTotalL.Text, out var totalDegree) ? totalDegree : (decimal?)null;
                    existingReport.Notes = string.Empty; // Add a field or input for Notes if necessary
                    existingReport.TestResMonTTT = decimal.TryParse(MonthlyResultL.Text, out var testRes) ? testRes : (decimal?)null;
                }
                else
                {
                    // Insert new record
                    var newReport = new TermReport
                    {
                        ID_Y = ID_Y,
                        ID_S = ID_S,
                        ID_H = id_h,
                        Term = Type,
                        SaveFrom = string.IsNullOrWhiteSpace(KeepFromL.Text) ? null : KeepFromL.Text,
                        SaveTo = string.IsNullOrWhiteSpace(KeepToCB.Text) ? null : KeepToCB.Text,
                        ReviewFrom = string.IsNullOrWhiteSpace(ReviewFromL.Text) ? null : ReviewFromL.Text,
                        ReviewTo = string.IsNullOrWhiteSpace(ReviewToCB.Text) ? null : ReviewToCB.Text,
                        TestDegSave = decimal.TryParse(TermDegreeTB.Text, out var saveDegree) ? saveDegree : (decimal?)null,
                        TestDegRev = decimal.TryParse(TermDegreeTB.Text, out var reviewDegree) ? reviewDegree : (decimal?)null,
                        TestDegTotal = decimal.TryParse(TermTotalL.Text, out var totalDegree) ? totalDegree : (decimal?)null,
                        Notes = string.Empty, // Consider adding a textbox for Notes
                        TestResMonTTT = decimal.TryParse(MonthlyResultL.Text, out var testResTTT) ? testResTTT : (decimal?)null,
                        TestResMon = decimal.TryParse(MonthlyResultL.Text, out var testResMon) ? testResMon : (decimal?)null
                    };


                    s.TermReports.Add(newReport);
                }

                // Save changes to the database
                s.SaveChanges();
                MessageBox.Show("تم الحفظ بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الحفظ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("هل أنت متأكد من إلغاء التعديلات والعودة؟",
                             "تأكيد الإلغاء",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
