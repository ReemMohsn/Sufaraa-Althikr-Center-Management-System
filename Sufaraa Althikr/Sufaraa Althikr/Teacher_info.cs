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
    public partial class Teacher_info : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int ID_Y;
        int ID_T;
        public Teacher_info(int ID_y, int ID_t)
        {
            ID_Y = ID_y;
            ID_T = ID_t;
            InitializeComponent();
        }


        private void Edit_button_Click(object sender, EventArgs e)
        {
            NameL.Visible = false;
            LevelL.Visible = false;
            periodL.Visible = false;
            phoneNoL.Visible = false;
            Edit_button.Visible = false;
            IDT.Visible=false;
            Save_button.Visible = true;

            Teach_nameTB.Visible = true;
            Teach_levelCB.Visible = true;
            PeriodCB.Visible = true;
            TeachPhoneNoTB.Visible = true;

            Teach_nameTB.Text = NameL.Text;
            Teach_levelCB.SelectedItem = LevelL.Text.Trim();
            PeriodCB.SelectedItem = periodL.Text.Trim();
            TeachPhoneNoTB.Text = phoneNoL.Text;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Teach_nameTB.Text)|| string.IsNullOrEmpty(TeachPhoneNoTB.Text) || Teach_levelCB.SelectedItem==null|| PeriodCB.SelectedItem==null)
            {
                MessageBox.Show("قم بملاء الحقول أولاَ");
            }
            else
            {
                NameL.Text = Teach_nameTB.Text;
                LevelL.Text = Teach_levelCB.SelectedItem.ToString();
                periodL.Text = PeriodCB.SelectedItem.ToString();
                phoneNoL.Text = TeachPhoneNoTB.Text;

                IDT.Visible = true;
                NameL.Visible = true;
                LevelL.Visible = true;
                periodL.Visible = true;
                phoneNoL.Visible = true;
                Edit_button.Visible = true;

                Teach_nameTB.Visible = false;
                Teach_levelCB.Visible = false;
                PeriodCB.Visible = false;
                TeachPhoneNoTB.Visible = false;
                Edit_button.Visible = true;
                Save_button.Visible = false;

                var teacherId = s.Teachers
                .Where(t => (t.ID_Y == ID_Y && t.ID_T == ID_T))
                .Select(t => t)
                .FirstOrDefault();
                teacherId.ID_T =int.Parse(IDT.Text);
               teacherId.Name = NameL.Text ;
                teacherId.Level= LevelL.Text ;
                teacherId.Period= periodL.Text ;
                teacherId.PhoneNumber= phoneNoL.Text ;
                s.SaveChanges();


            }
           
        }

        private void loadData()
        {
         var teacherId = s.Teachers
        .Where(t => (t.ID_Y == ID_Y && t.ID_T== ID_T))
        .Select(t => t)
        .FirstOrDefault();

            IDT.Text = teacherId.ID_T.ToString();
            NameL.Text = teacherId.Name;
            LevelL.Text = teacherId.Level;
            periodL.Text = teacherId.Period;
            phoneNoL.Text = teacherId.PhoneNumber;

            guna2ComboBox1.Items.Clear();
            var halaqats = s.Halgas.Where(a => a.TeacherUniqueID == teacherId.TeacherUniqueID && a.ID_Y == teacherId.ID_Y).ToList();
            foreach (var i in halaqats)
            {
                guna2ComboBox1.Items.Add(i.Name);
            }

        }
        private void Teacher_info_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
