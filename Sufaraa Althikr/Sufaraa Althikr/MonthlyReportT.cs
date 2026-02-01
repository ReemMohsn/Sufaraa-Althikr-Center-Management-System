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
    public partial class MonthlyReportT : Form
    {
        public string StudentName { get; set; }
        public string KeepFrom { get; set; }
        public string KeepTo { get; set; }
        public string ReviewFrom { get; set; }
        public string ReviewTo { get; set; }
        public decimal KeepDegree { get; set; }
        public decimal ReviewDegree { get; set; }
        public int AbsCountDays { get; set; }
        public int YearID { get; set; }
        public int StudentID { get; set; }
        public string Term { get; set; }
        public int Type { get; set; }


        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();


        public MonthlyReportT()
        {
            InitializeComponent();
        }

        private void MonthlyReportT_Load(object sender, EventArgs e)
        {
            KeepFromCB.SelectedItem = KeepFromCB.Items.Contains(KeepFrom) ? KeepFrom : null;
            KeepToCB.SelectedItem = KeepToCB.Items.Contains(KeepTo) ? KeepTo : null;
            ReviewFromCB.SelectedItem = ReviewFromCB.Items.Contains(ReviewFrom) ? ReviewFrom : null;
            ReviewToCB.SelectedItem = ReviewToCB.Items.Contains(ReviewTo) ? ReviewTo : null;
            KeepDigreeTB.Text = KeepDegree.ToString();
            ReveiwDigreeTB.Text = ReviewDegree.ToString();
            AbsWithoutExTB.Text = AbsCountDays.ToString();

            decimal keepDegree = decimal.Parse(KeepDigreeTB.Text);
            decimal reviewDegree = decimal.Parse(ReveiwDigreeTB.Text);
            int absCountDays = int.Parse(AbsWithoutExTB.Text);

            decimal total = keepDegree + reviewDegree - (absCountDays * 2);


            calculat_degree est = new calculat_degree();
            MonthyEstimateL.Text = est.Estimate((int)Math.Floor(total));


        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(KeepDigreeTB.Text) ||
                    string.IsNullOrWhiteSpace(ReveiwDigreeTB.Text) ||
                    string.IsNullOrWhiteSpace(AbsWithoutExTB.Text))
                {
                    MessageBox.Show("يرجى ملء جميع الحقول المطلوبة.", "خطأ في التحقق", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse input values
                string keepFrom = KeepFromCB.SelectedItem?.ToString() ?? string.Empty;
                string keepTo = KeepToCB.SelectedItem?.ToString() ?? string.Empty;
                string reviewFrom = ReviewFromCB.SelectedItem?.ToString() ?? string.Empty;
                string reviewTo = ReviewToCB.SelectedItem?.ToString() ?? string.Empty;
                decimal keepDegree = decimal.Parse(KeepDigreeTB.Text);
                decimal reviewDegree = decimal.Parse(ReveiwDigreeTB.Text);
                int absCountDays = int.Parse(AbsWithoutExTB.Text);

                // Calculate total and estimate
                decimal total = keepDegree + reviewDegree - (absCountDays * 2);
                calculat_degree est = new calculat_degree();
                string estimate = est.Estimate((int)Math.Floor(total));

                var ID_h = s.Students
                            .Where(a => a.ID_S == StudentID && a.ID_Y == YearID)
                            .Select(a => a.ID_H)
                            .FirstOrDefault();

                int id_h = ID_h.Value; 

                // Create or update the database record
                using (SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1())
                {
                    // Check if the record exists
                    var existingReport = s.MothlyReports
                        .FirstOrDefault(r => r.ID_S == StudentID && r.Term == Term && r.Month == Type);

                    if (existingReport != null)
                    {
                        // Update existing record
                        existingReport.SaveFrom = keepFrom;
                        existingReport.SaveTo = keepTo;
                        existingReport.ReviewFrom = reviewFrom;
                        existingReport.ReviewTo = reviewTo;
                        existingReport.TestDegSave = keepDegree;
                        existingReport.TestDegReview = reviewDegree;
                    }
                    else
                    {
                        // Insert new record
                        var newReport = new MothlyReport
                        {
                            ID_Y = YearID,
                            ID_S = StudentID,
                            ID_H = id_h,
                            Term = Term,
                            Month = Type,
                            SaveFrom = keepFrom,
                            SaveTo = keepTo,
                            ReviewFrom = reviewFrom,
                            ReviewTo = reviewTo,
                            TestDegSave = keepDegree,
                            Notes = "",
                        };
                        s.MothlyReports.Add(newReport);
                    }

                    // Save changes
                    s.SaveChanges();
                }

                MessageBox.Show("تم حفظ البيانات بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("هل أنت متأكد أنك تريد الإلغاء؟ سيتم فقدان التغييرات غير المحفوظة.",
                                         "تأكيد الإلغاء",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
