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
    public partial class Teachers : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1(); 
        int ID_Y;
        public Teachers(int ID_y)
        {
            ID_Y = ID_y;
            InitializeComponent();
        }
        private void load()
        {
            var teacherIds = s.Teachers
          .Where(t => (t.ID_Y == ID_Y))
          .Select(t => t)
          .ToList();
            teachersList.Items.Clear();
            if (teacherIds != null && teacherIds.Count > 0)
            {
                foreach (var teacher in teacherIds)
                {
                    teachersList.Items.Add(new KeyValuePair<int, string>(teacher.ID_T, teacher.Name));
                }
            }

            teachersList.DisplayMember = "Value";
            teachersList.ValueMember = "Key";
        }
        private void Teachers_Load(object sender, EventArgs e)
        {
            load();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                load();
            }
            else
            {
                var teacherIds = s.Teachers
                .Where(st => st.ID_Y == ID_Y && st.Name.StartsWith(guna2TextBox1.Text.Trim()))
               .Select(st => st)
               .ToList();
                teachersList.Items.Clear();
                if (teacherIds != null )
                {
                    foreach (var teacher in teacherIds)
                    {
                        teachersList.Items.Add(new KeyValuePair<int, string>(teacher.ID_T, teacher.Name));
                    }
                }
                teachersList.DisplayMember = "Value";
                teachersList.ValueMember = "Key";
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (teachersList.SelectedItem != null)
            {
                //int selectedTeachertID = (int)teachersList.SelectedValue;
                var selectedTeachertID = (KeyValuePair<int, string>)teachersList.SelectedItem;
                int TeachertID = selectedTeachertID.Key;

                this.guna2Panel1.Controls.Clear();
                Teacher_info teacher_info = new Teacher_info(ID_Y, TeachertID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(teacher_info);
                teacher_info.Show();
            }
            else
            {
                MessageBox.Show("الرجاء قم بتحديد معلمة من القائمة");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            bool isYearNowActive = s.AcademicYears
                        .Where(a => a.ID_Y == ID_Y)
                       .Select(a => a.IsActive)
                       .FirstOrDefault();
            if (isYearNowActive)
            {
                if (teachersList.SelectedItem != null)
                {
                    //int selectedTeachertID = (int)teachersList.SelectedValue;
                    var selectedTeachertID = (KeyValuePair<int, string>)teachersList.SelectedItem;
                    int TeachertID = selectedTeachertID.Key;
                    DialogResult result = MessageBox.Show("هل تريد حذف هذه المعلمة؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        var teacherToDelete = s.Teachers.FirstOrDefault(st => st.ID_T == TeachertID && st.ID_Y == ID_Y);
                        if (teacherToDelete != null)
                        {
                            s.Teachers.Remove(teacherToDelete);
                            s.SaveChanges();
                            MessageBox.Show("تم حذف المعلمة بنجاح");
                            load();

                        }
                        else
                        {
                            MessageBox.Show("لم يتم العثور على المعلمة في قاعدة البيانات.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء قم بتحديد معلمة من القائمة");
                }
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع حدف معلمة منها");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            bool isYearNowActive = s.AcademicYears
                       .Where(a => a.ID_Y == ID_Y)
                      .Select(a => a.IsActive)
                      .FirstOrDefault();
            if (isYearNowActive)
            {            
                this.guna2Panel1.Controls.Clear();
                AddTeacher addTeacher = new AddTeacher(ID_Y) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(addTeacher);
                addTeacher.Show();
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع إضافة معلمة فيها");
            }

        }
    }
}
