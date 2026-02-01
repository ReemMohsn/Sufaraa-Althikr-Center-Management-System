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
    public partial class Monthly_rebort : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        int ID_Y;
        int ID_H;
        string Term;
        int Type;
        public Monthly_rebort(int id_y, int id_h, string term, int type)
        {
            ID_Y = id_y;
            ID_H = id_h;
            Term = term;
            Type = type;
            InitializeComponent();
            Monthly_reportDGV.CellDoubleClick += Monthly_reportDGV_CellDoubleClick;
        }

        private void ShowMonthlyReport(int id_y, int id_h, string term, int type)
        {
            // Get the student names using a join
            var studentNames = s.YearlyRrports
                .Where(yearHel => yearHel.ID_Y == id_y && yearHel.ID_H == id_h)
                .Join(s.Students,
                      yearHel => yearHel.ID_S, // Match Student ID in YearlyReports
                      student => student.ID_S, // Match Student ID in Students
                      (yearHel, student) => new
                      {
                          yearHel.ID_S,        // Student ID
                  StudentName = student.Name // Student name from Students table
              })
                .ToList();

            // Get the monthly reports for the specified term and type
            var monthlyReports = s.MothlyReports
                .Where(monthRep =>
                    studentNames.Any(sn => sn.ID_S == monthRep.ID_S) &&
                    monthRep.Term == term &&
                    monthRep.Month == type)
                .Select(monthRep => new
                {
                    monthRep.ID_S,
                    monthRep.SaveFrom,
                    monthRep.SaveTo,
                    monthRep.ReviewFrom,
                    monthRep.ReviewTo,
                    monthRep.TestDegSave,
                    monthRep.TestDegReview,
                })
                .ToList();

            // Clear the DataGridView rows
            Monthly_reportDGV.Rows.Clear();

            // Populate the DataGridView with the retrieved data
            foreach (var student in studentNames)
            {
                // Find the student's monthly report
                var studentMonthlyReport = monthlyReports
                    .FirstOrDefault(mr => mr.ID_S == student.ID_S);
                int ID_S = student.ID_S;

                int absentDays = s.AttendedAndAbsences
                   .Where(a => a.ID_S == ID_S && a.IsAttended.Trim() == "0")
                   .Count();

                // Add a new row for the student
                int rowIndex = Monthly_reportDGV.Rows.Add();
                DataGridViewRow row = Monthly_reportDGV.Rows[rowIndex];

                // Fill static columns with data
                row.Cells["stuName"].Value = student.StudentName;
                row.Cells["keepFrom"].Value = studentMonthlyReport?.SaveFrom ?? string.Empty;
                row.Cells["keepTo"].Value = studentMonthlyReport?.SaveTo ?? string.Empty;
                row.Cells["reviewFrom"].Value = studentMonthlyReport?.ReviewFrom ?? string.Empty;
                row.Cells["reviewTo"].Value = studentMonthlyReport?.ReviewTo ?? string.Empty;
                row.Cells["keepDegree"].Value = studentMonthlyReport?.TestDegSave ?? 0;
                row.Cells["reviewDegree"].Value = studentMonthlyReport?.TestDegReview ?? 0;
                row.Cells["absCountDays"].Value = absentDays;

                // Calculate total and estimate
                decimal keepDegree = studentMonthlyReport?.TestDegSave ?? 0;
                decimal reviewDegree = studentMonthlyReport?.TestDegReview ?? 0;
                int absCountDays = absentDays;

                decimal total = keepDegree + reviewDegree - (absCountDays * 2); // Example logic for total
                row.Cells["total"].Value = total;

                calculat_degree est = new calculat_degree();
                row.Cells["estmate"].Value = est.Estimate((int)total);
            }
        }

        private void Monthly_rebort_Load(object sender, EventArgs e)
        {
            ShowMonthlyReport(ID_Y, ID_H, Term, Type);
        }

        private void Monthly_reportDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                // Get the clicked row
                DataGridViewRow selectedRow = Monthly_reportDGV.Rows[e.RowIndex];

                // Extract data from the row
                string stuName = selectedRow.Cells["stuName"].Value?.ToString() ?? string.Empty;
                string keepFrom = selectedRow.Cells["keepFrom"].Value?.ToString() ?? string.Empty;
                string keepTo = selectedRow.Cells["keepTo"].Value?.ToString() ?? string.Empty;
                string reviewFrom = selectedRow.Cells["reviewFrom"].Value?.ToString() ?? string.Empty;
                string reviewTo = selectedRow.Cells["reviewTo"].Value?.ToString() ?? string.Empty;
                decimal keepDegree = selectedRow.Cells["keepDegree"].Value != null
                                    ? Convert.ToDecimal(selectedRow.Cells["keepDegree"].Value)
                                    : 0;
                decimal reviewDegree = selectedRow.Cells["reviewDegree"].Value != null
                                      ? Convert.ToDecimal(selectedRow.Cells["reviewDegree"].Value)
                                      : 0;
                int absCountDays = selectedRow.Cells["absCountDays"].Value != null
                                  ? Convert.ToInt32(selectedRow.Cells["absCountDays"].Value)
                                  : 0;

                // Extract the Student ID (you need to ensure the DataGridView includes this column)
                int id_s = Convert.ToInt32(selectedRow.Cells["ID_S"].Value);

                // Open the MonthlyReportT form
                MonthlyReportT monthlyReportForm = new MonthlyReportT();

                // Pass data to the MonthlyReportT form
                monthlyReportForm.StudentID = id_s;  // Pass the student ID
                monthlyReportForm.YearID = ID_Y;    // Pass the year ID
                monthlyReportForm.Term = Term;      // Pass the term
                monthlyReportForm.Type = Type;      // Pass the type (month)
                monthlyReportForm.StudentName = stuName;
                monthlyReportForm.KeepFrom = keepFrom;
                monthlyReportForm.KeepTo = keepTo;
                monthlyReportForm.ReviewFrom = reviewFrom;
                monthlyReportForm.ReviewTo = reviewTo;
                monthlyReportForm.KeepDegree = keepDegree;
                monthlyReportForm.ReviewDegree = reviewDegree;
                monthlyReportForm.AbsCountDays = absCountDays;

                // Show the form as a dialog
                monthlyReportForm.ShowDialog();

                // Optionally refresh the DataGridView after changes
                ShowMonthlyReport(ID_Y, ID_H, Term, Type);
            }
        }
        
    }
}
