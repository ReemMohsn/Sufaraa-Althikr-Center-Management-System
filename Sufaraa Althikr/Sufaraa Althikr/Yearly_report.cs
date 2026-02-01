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
    public partial class Yearly_report : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        int ID_Y;
        int ID_hs;
        string Type;
        public Yearly_report(int id_y, int id_hs, string type)
        {
            ID_hs = id_hs;
            ID_Y = id_y;
            Type = type;
            InitializeComponent();
        }

        private void Yearly_reportDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Yearly_report_Load(object sender, EventArgs e)
        {
            if (Type == "s")
            {
                StudentYearlyReport studentYearlyReport = new StudentYearlyReport(ID_Y, ID_hs) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                studentYearlyReport.Show();
            }
            else
            {
                ShowYearlyRebort(ID_Y, ID_hs);
            }

            int manhcount = s.ManhajeHalgas
                .Where(a => a.ID_Y == ID_Y && a.ID_H == ID_hs)
                .Select(a => a.ID_M).Count();

            // Add new manhcount columns after the 5th column
            for (int i = 0; i < manhcount; i++)
            {
                Yearly_reportDGV.Columns.Insert(5 + i, new DataGridViewTextBoxColumn
                {
                    Name = $"ManhColumn{i + 1}",
                    HeaderText = $"منهج {i + 1}",
                    Width = 100 // Set width or any other properties as needed
                });
            }

            // Ensure the last two columns remain the last
            var lastColumn1 = Yearly_reportDGV.Columns["toal"]; // Adjust names if different
            var lastColumn2 = Yearly_reportDGV.Columns["estmate"]; // Adjust names if different

            Yearly_reportDGV.Columns.Remove(lastColumn1);
            Yearly_reportDGV.Columns.Remove(lastColumn2);

            Yearly_reportDGV.Columns.Add(lastColumn1);
            Yearly_reportDGV.Columns.Add(lastColumn2);

        }

        //private void ShowYearlyRebort(int id_y, int id_h)
        //{
        //    // Get the student names using a join
        //    var studentNames = s.YearlyRrports
        //        .Where(yearHel => yearHel.ID_Y == id_y && yearHel.ID_H == id_h)
        //        .Join(s.Students,
        //              yearHel => yearHel.ID_S, // Match Student ID in YearlyReports
        //              student => student.ID_S, // Match Student ID in Students
        //              (yearHel, student) => new
        //              {
        //                  yearHel.ID_S,        // Student ID
        //          StudentName = student.Name // Student name from Students table
        //      })
        //        .ToList();

        //    // Clear the DataGridView rows
        //    Yearly_reportDGV.Rows.Clear();

        //    // Populate rows for each student
        //    foreach (var student in studentNames)
        //    {
        //        // Add a new row
        //        int rowIndex = Yearly_reportDGV.Rows.Add();
        //        DataGridViewRow row = Yearly_reportDGV.Rows[rowIndex];

        //        // Set the student name in the first column
        //        row.Cells["StuName"].Value = student.StudentName;

        //        // Retrieve the corresponding YearlyRrports data for this student
        //        var report = s.YearlyRrports
        //            .FirstOrDefault(yearHel => yearHel.ID_Y == id_y && yearHel.ID_S == student.ID_S);

        //        if (report != null)
        //        {
        //            // Fill in the static columns
        //            row.Cells["KeepFrom"].Value = report.SaveFrom;
        //            row.Cells["KeepTo"].Value = report.SaveTo;
        //            row.Cells["Degree"].Value = report.SaveRevDeg;

        //            string catagory;
        //            if (report.SaveTo == "البقرة")
        //            {
        //                catagory = "حافظة";
        //            }
        //            else
        //            {
        //                catagory = "غير مجازة";
        //            }
        //            row.Cells["Catogory"].Value = catagory;

        //            // Dynamically fill the `manhcount` columns
        //            var manhajeScores = report.ManhajeScores; // Assuming `ManhajeScores` is a list or array
        //            if (manhajeScores != null)
        //            {
        //                for (int j = 0; j < manhajeScores.Count; j++)
        //                {
        //                    string columnName = $"ManhColumn{j + 1}";
        //                    if (Yearly_reportDGV.Columns.Contains(columnName))
        //                    {
        //                        row.Cells[columnName].Value = manhajeScores[j];
        //                    }
        //                }
        //            }

        //            // Calculate and populate the `Total` and `Estimate` columns
        //            row.Cells["Total"].Value = CalculateTotal(report, manhajeScores); // Implement `CalculateTotal` as needed
        //            row.Cells["Estmate"].Value = GetEstimate(report.Total);          // Implement `GetEstimate` as needed
        //        }
        //    }
        //}

        private void ShowYearlyRebort(int id_y, int id_h)
        {
            // Fetch student names and related yearly report data
            var studentReports = s.YearlyRrports
                .Where(yr => yr.ID_Y == id_y && yr.ID_H == id_h)
                .Join(s.Students,
                      yr => yr.ID_S,
                      student => student.ID_S,
                      (yr, student) => new
                      {
                          yr.ID_S,
                          yr.ID_Report,         // Needed to fetch degrees
                          yr.SaveFrom,
                          yr.SaveTo,
                          yr.SaveRevDeg,
                          student.Name
                      })
                .ToList();

            // Fetch all `ManhajeHalga` for the provided ID_Y and ID_H
            var manhajeHalga = s.ManhajeHalgas
                .Where(mh => mh.ID_Y == id_y && mh.ID_H == id_h)
                .Select(mh => new
                {
                    mh.ID_M,    // Manhaje ID
            mh.ID_MH    // ReportCourse foreign key
        })
                .ToList();



            // Clear the DataGridView rows
            Yearly_reportDGV.Rows.Clear();

            // Populate the DataGridView
            foreach (var student in studentReports)
            {
                // Add a new row
                int rowIndex = Yearly_reportDGV.Rows.Add();
                DataGridViewRow row = Yearly_reportDGV.Rows[rowIndex];

                // Fill static columns
                row.Cells["StuName"].Value = student.Name;
                row.Cells["KeepFrom"].Value = student.SaveFrom;
                row.Cells["KeepTo"].Value = student.SaveTo;
                row.Cells["Degree"].Value = student.SaveRevDeg;

                string Category;
                if (student.SaveTo == "البقرة")
                {
                    Category = "حافظة";
                }
                else
                {
                    Category = "غير مجازة";
                }

                row.Cells["Catogory"].Value = Category;

                // Populate dynamic `manhcount` columns
                var manhDegrees = s.ReportCourses
                    .Where(rc => rc.ID_Report == student.ID_Report && manhajeHalga.Any(mh => mh.ID_MH == rc.ID_MH))
                    .Select(rc => new
                    {
                        rc.ID_MH,
                        rc.Degree
                    })
                    .ToList();

                for (int j = 0; j < manhDegrees.Count; j++)
                {
                    string columnName = $"ManhColumn{j + 1}";
                    if (Yearly_reportDGV.Columns.Contains(columnName))
                    {
                        row.Cells[columnName].Value = manhDegrees[j].Degree;
                    }
                }

                int keepDegree = int.Parse(student.SaveRevDeg.ToString()); // Convert KeepDegree from the Label text

                // Calculate the total weighted percentage
                double keepDegreePercentage = 70; // Percentage weight for KeepDegree
                double manhcountPercentage = 30;  // Percentage weight for ManhCount

                int total = (int)((keepDegree * keepDegreePercentage / 100) +
                                  (manhDegrees.Count * manhcountPercentage / 100));
                calculat_degree est = new calculat_degree();


                // Calculate and populate Total and Estimate columns
                row.Cells["Total"].Value = total;
                row.Cells["Estmate"].Value = est.Estimate(total);
            }
        }


        private void Yearly_reportDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    // Get the selected row
            //    DataGridViewRow selectedRow = Yearly_reportDGV.Rows[e.RowIndex];

            //    // Extract data from the selected row
            //    string id_s = selectedRow.Cells["StuName"].Value?.ToString(); // Adjust based on column name
            //    string keepFrom = selectedRow.Cells["KeepFrom"].Value?.ToString();
            //    string keepTo = selectedRow.Cells["KeepTo"].Value?.ToString();
            //    string degree = selectedRow.Cells["Degree"].Value?.ToString();
            //    string category = selectedRow.Cells["Catogory"].Value?.ToString();
            //    string total = selectedRow.Cells["Total"].Value?.ToString();
            //    string estimate = selectedRow.Cells["Estmate"].Value?.ToString();

            //    // Create an instance of the YearlyReportT form
            //    YearlyReportT yearlyReportForm = new YearlyReportT();

            //    // Pass data to the YearlyReportT form using public properties or methods
            //    //yearlyReportForm. = id_s;
            //    yearlyReportForm.keepFrom = keepFrom;
            //    yearlyReportForm.KeepTo = keepTo;
            //    yearlyReportForm.Degree = degree;
            //    yearlyReportForm.Category = category;
            //    yearlyReportForm.Total = total;
            //    yearlyReportForm.Estimate = estimate;

            //    // Close the current form
            //    this.Close();

            //    // Show the new form
            //    yearlyReportForm.ShowDialog(); // Or use Show() for non-modal
            //}
        }
    }
}
