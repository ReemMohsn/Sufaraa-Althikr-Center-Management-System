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
    public partial class ReportsMain : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int ID_Y;
        int ID_hs;
        string Type;

        public ReportsMain(int ID_y, int id_HS, string type)
        {
            Type = type;
            ID_Y = ID_y;
            ID_hs = id_HS;
            InitializeComponent();
        }

        private void Hel_button_Click(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var activeYear = s.AcademicYears
            //            .Where(year => year.IsActive)
            //            .Select(year => year.ID_Y)
            //            .FirstOrDefault();
            //ID_Y = activeYear;
        }
        
        private void FirstTermReportButton_Click(object sender, EventArgs e)
        {
            this.guna2GradientPanel1.Controls.Clear();
            Tagreer tagreer = new Tagreer(ID_Y, ID_hs,"الأول", Type) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2GradientPanel1.Controls.Add(tagreer);
            tagreer.Show();

        }

        private void SecTermReportButton_Click(object sender, EventArgs e)
        {
            this.guna2GradientPanel1.Controls.Clear();
            Tagreer tagreer = new Tagreer(ID_Y, ID_hs, "الثاني", Type) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2GradientPanel1.Controls.Add(tagreer);
            tagreer.Show();
        }

        private void YealyReportButton_Click(object sender, EventArgs e)
        {
            this.guna2GradientPanel1.Controls.Clear();
            Yearly_report tagreer = new Yearly_report(ID_Y, ID_hs, Type) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2GradientPanel1.Controls.Add(tagreer);
            tagreer.Show();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
