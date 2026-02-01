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
    public partial class Tagreer : Form
    {
        int ID_Y;
        int ID;
        string Term;
        string Type;
        public Tagreer(int ID_y,int id,string term, string type)
        {
            ID_Y = ID_y;
            ID = id;
            Term = term;
            Type = type;
            InitializeComponent();
        }

        private void Tagreer_Load(object sender, EventArgs e)
        {

        }

        private void FirstMonthReportBT_Click(object sender, EventArgs e)
        {

        }

        private void FirstMonthReportBT_Click_1(object sender, EventArgs e)
        {
            if (Type == "s")
            {
                this.guna2Panel1.Controls.Clear();
                StudentMonthlyReport studentMonthlyReport = new StudentMonthlyReport(ID_Y, ID, Term, 1) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(studentMonthlyReport);
                studentMonthlyReport.Show();
            }
            else
            {
                this.guna2Panel1.Controls.Clear();
                Monthly_rebort monthly_rebort = new Monthly_rebort(ID_Y, ID, Term, 1) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(monthly_rebort);
                monthly_rebort.Show();
            }
        }

        private void SecMonthReportBT_Click(object sender, EventArgs e)
        {
            if (Type == "s")
            {
                this.guna2Panel1.Controls.Clear();
                StudentMonthlyReport studentMonthlyReport = new StudentMonthlyReport(ID_Y, ID, Term, 2) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(studentMonthlyReport);
                studentMonthlyReport.Show();
            }
            else
            {
                this.guna2Panel1.Controls.Clear();
                Monthly_rebort monthly_rebort = new Monthly_rebort(ID_Y, ID, Term, 2) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(monthly_rebort);
                monthly_rebort.Show();
            }
        }

        private void TermReportBT_Click(object sender, EventArgs e)
        {
            if (Type == "s")
            {
                this.guna2Panel1.Controls.Clear();
                StudentTermReport studentTermReport = new StudentTermReport(ID_Y, ID, Term) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(studentTermReport);
                studentTermReport.Show();
            }
            else
            {
                this.guna2Panel1.Controls.Clear();
                Term_rebort term_rebort = new Term_rebort(ID_Y, ID, Term) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(term_rebort);
                term_rebort.Show();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
