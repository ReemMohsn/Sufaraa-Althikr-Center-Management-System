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
    public partial class StudentYearlyReport : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        int ID_Y;
        int ID_S;

        public StudentYearlyReport(int id_y, int id_s)
        {
            ID_Y = id_y;
            ID_S = id_s;
            InitializeComponent();
        }

        private void StudentYearlyReport_Load(object sender, EventArgs e)
        {
            try
            {
                // Fetch the yearly report for the student
                var yearlyReport = s.YearlyRrports
                    .FirstOrDefault(r => r.ID_S == ID_S && r.ID_Y == ID_Y);

                if (yearlyReport == null)
                {
                    MessageBox.Show("لم يتم ايجاد التقرير.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Fetch the Halaqa ID (ID_H) for the student
                var halaqaId = s.Students
                    .Where(student => student.ID_S == ID_S && student.ID_Y == ID_Y)
                    .Select(student => student.ID_H)
                    .FirstOrDefault();

                if (halaqaId == 0)
                {
                    MessageBox.Show("Halaqa ID not found for the selected student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Populate the form labels with yearly report data
                keepFromL.Text = yearlyReport.SaveFrom ?? string.Empty;
                keepToL.Text = yearlyReport.SaveTo ?? string.Empty;
                reviewFromL.Text = yearlyReport.ReviewFrom ?? string.Empty;
                ReviewToL.Text = yearlyReport.ReviewTo ?? string.Empty;
                keepDegreeL.Text = yearlyReport.SaveRevDeg?.ToString() ?? "0";

                // Determine the student category
                catogoryL.Text = (keepToL.Text == "البقرة") ? "حافظة" : "غير مجازة";

                // Count the number of completed curricula (ManhajeHalgas)
                int manhajeCount = s.ManhajeHalgas
                    .Where(m => m.ID_Y == ID_Y && m.ID_H == halaqaId)
                    .Select(m => m.ID_M)
                    .Count();

                // Calculate the total percentage
                if (!int.TryParse(keepDegreeL.Text, out int keepDegree))
                {
                    keepDegree = 0; // Default value if parsing fails
                }

                const double keepDegreeWeight = 70; // 70% weight
                const double manhajeCountWeight = 30; // 30% weight

                int totalScore = (int)((keepDegree * keepDegreeWeight / 100) +
                                       (manhajeCount * manhajeCountWeight / 100));
                totalL.Text = totalScore.ToString();

                // Calculate and display the estimate based on the total score
                calculat_degree estimator = new calculat_degree();
                estimateL.Text = estimator.Estimate(totalScore);

                // Dynamically generate labels for completed curricula
                GenerateDynamicLabels(manhajeCount);
            }
            catch (Exception ex)
            {
                // Handle unexpected runtime errors
                MessageBox.Show($"حدث خطأ: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerateDynamicLabels(int manhajeCount)
        {
            int startX1 = 457; // X-coordinate for the first column
            int startX2 = 77;  // X-coordinate for the second column
            int startY = 356;  // Initial Y-coordinate
            int yIncrement = 92; // Vertical spacing between rows

            for (int i = 0; i < manhajeCount; i++)
            {
                // Determine the Y-coordinate for the current row
                int row = i / 2; // Each row contains 2 labels
                int y = startY + (row * yIncrement);

                Label label = new Label
                {
                    Name = $"label{i}",
                    Text = $"Curriculum {i + 1}", // Example text
                    Location = new Point(i % 2 == 0 ? startX1 : startX2, y),
                    Size = new Size(150, 30), // Adjust the size as needed
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // Add the label to the form
                this.Controls.Add(label);
            }
        }
    }
}
