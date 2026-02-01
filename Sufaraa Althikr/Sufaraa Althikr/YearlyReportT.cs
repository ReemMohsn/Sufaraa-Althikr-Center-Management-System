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
    public partial class YearlyReportT : Form
    {
        //SufaraAldhikrEntities s = new SufaraAldhikrEntities();

        int ID_Y;
        int ID_S;
        int total;
        public YearlyReportT()
        {
            //ID_Y = id_y;
            //ID_S = id_s;
            InitializeComponent();
            LoadYearlyReportData();
        }

        private void السنوي_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void LoadYearlyReportData()
        {
            using (var a = new SufaraAldhikrEntities1())
            {
                // Query for the existing record
                var record = a.YearlyRrports
                                    .Where(r => r.ID_S == ID_S && r.ID_Y == ID_Y)
                                    .FirstOrDefault();

                var stu = a.TermReports.Find(ID_S);
                keepFromL.Text = stu.SaveFrom;
                totalL.Text = stu.TestDegTotal.ToString();

                var KeepTo = a.TermReports
                   .Where(q => q.ID_S == ID_S && q.Term == "الثاني")
                   .Select(p => p.SaveTo)
                   .FirstOrDefault();
                KeepToCB.SelectedItem = KeepTo;
                KeepToCB.DropDownStyle = ComboBoxStyle.DropDownList;
                catogoryCB.DropDownStyle = ComboBoxStyle.DropDownList;
                calculat_degree est = new calculat_degree();
                estimateL.Text = est.Estimate(total);



                if (record != null)
                {
                    //// Populate form fields with the existing data
                    //txtStudentName.Text = a.Students
                    //                             .Where(s => s.ID_S == id_s)
                    //                             .Select(s => s.Name)
                    //                             .FirstOrDefault(); // Load student name
                    //txtID_S.Text = record.ID_S;
                    //txtID_Y.Text = record.ID_Y;
                    //txtOtherField1.Text = record.SomeOtherField1; // Example field
                    //txtOtherField2.Text = record.SomeOtherField2; // Example field
                }
                else
                {
                    //// If no record exists, set up for manual entry
                    //txtStudentName.Text = a.Students
                    //                             .Where(s => s.ID_S == id_s)
                    //                             .Select(s => s.Name)
                    //                             .FirstOrDefault(); // Load student name
                    //txtID_S.Text = id_s;
                    //txtID_Y.Text = id_y;

                    //// Leave other fields empty
                    //txtOtherField1.Text = string.Empty;
                    //txtOtherField2.Text = string.Empty;
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            //using (var a = new SufaraAldhikrEntities())
            //{
            //    // Check if the record exists
            //    var record = a.YearlyRrports
            //                        .Where(r => r.ID_S == id_s && r.ID_Y == id_y)
            //                        .FirstOrDefault();

            //    if (record != null)
            //    {
            //        // Update existing record
            //        record.SomeOtherField1 = txtOtherField1.Text;
            //        record.SomeOtherField2 = txtOtherField2.Text;
            //        // Update other fields as needed
            //    }
            //    else
            //    {
            //        // Insert new record
            //        var newRecord = new YearlyReport
            //        {
            //            ID_S = id_s,
            //            ID_Y = id_y,
            //            SomeOtherField1 = txtOtherField1.Text,
            //            SomeOtherField2 = txtOtherField2.Text,
            //            // Set other fields as needed
            //        };
            //        a.YearlyRrports.Add(newRecord);
            //    }

            //    // Save changes to the database
            //    a.SaveChanges();

            //    MessageBox.Show("Data saved successfully!");
            //}
        }
    }
}
