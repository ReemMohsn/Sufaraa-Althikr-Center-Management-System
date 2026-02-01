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
    public partial class Students : Form
    {
        int ID_Y;
        SufaraAldhikrEntities1 s =new SufaraAldhikrEntities1();    
        public Students(int ID_y)
        {
            ID_Y = ID_y;
            InitializeComponent();
            load();
        }

        private void Students_Load(object sender, EventArgs e)
        {
           
        }
        private void load()
        {
            var StudentsInYear = s.Students
             .Where(st => st.ID_Y == ID_Y)
             .Select(st => st)
             .ToList();
            ListStudents.Items.Clear();
            foreach (var i in StudentsInYear)
            {
                ListStudents.Items.Add(new KeyValuePair<int, string>(i.ID_S, i.Name));
            }
            ListStudents.DisplayMember = "Value";
            ListStudents.ValueMember = "Key";
        }
        private void ListStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

    

        private void Search_studentTB_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Search_studentTB.Text))
            {
                load();
            }
            else
            {
              var StudentsInYear = s.Students
             .Where(st => st.ID_Y == ID_Y && st.Name.StartsWith(Search_studentTB.Text.Trim()))
             .Select(st => st)
             .ToList();
                ListStudents.Items.Clear();
                foreach (var i in StudentsInYear)
                {
                    ListStudents.Items.Add(new KeyValuePair<int, string>(i.ID_S, i.Name));
                }
                ListStudents.DisplayMember = "Value";
                ListStudents.ValueMember = "Key";

               // ListStudents.DataSource = null;  // إعادة تعيين DataSource
               // ListStudents.Items.Clear();
               // ListStudents.DataSource = s.Students
               // .Where(st => st.ID_Y == ID_Y && st.Name.StartsWith(Search_studentTB.Text.Trim()))
               //.Select(st => new KeyValuePair<int, string>(st.ID_S, st.Name))
               //.ToList();
               // ListStudents.DisplayMember = "Value";
               // ListStudents.ValueMember = "Key";
            }
        }
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (ListStudents.SelectedItem != null)
            {
                var selectedStudentID = (KeyValuePair<int, string>)ListStudents.SelectedItem;
                int StudentID = selectedStudentID.Key;
                //int selectedStudentID = (int)ListStudents.SelectedValue;

                this.guna2Panel1.Controls.Clear();
                Student_info student_info = new Student_info(ID_Y, StudentID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.guna2Panel1.Controls.Add(student_info);
                student_info.Show();
            }
            else
            {
                MessageBox.Show("الرجاء قم بتحديد طالب من القائمة");
            }
           
        }

        private void delete_Click(object sender, EventArgs e)
        {
            bool isYearNowActive = s.AcademicYears
                         .Where(a => a.ID_Y == ID_Y)
                        .Select(a => a.IsActive)
                        .FirstOrDefault();
            if (isYearNowActive)
            {
                if (ListStudents.SelectedItem!= null)
                {
                    //int selectedStudentID = (int)ListStudents.SelectedValue;
                    var selectedStudentID = (KeyValuePair<int, string>)ListStudents.SelectedItem;
                    int StudentID = selectedStudentID.Key;

                    DialogResult result = MessageBox.Show("هل تريد حذف هذه الطالبة؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        var studentToDelete = s.Students.FirstOrDefault(st => st.ID_S == StudentID && st.ID_Y == ID_Y);
                        if (studentToDelete != null)
                        {
                            s.Students.Remove(studentToDelete);
                            s.SaveChanges();
                            MessageBox.Show("تم حذف الطالبة بنجاح");
                            load();

                        }
                        else
                        {
                            MessageBox.Show("لم يتم العثور على الطالبة في قاعدة البيانات.");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("الرجاء قم بتحديد طالبة من القائمة");
                }
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع حدف طالبة منها");
            }
            
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
         
        }
        private void ListStudents_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void addSrudent_button_Click(object sender, EventArgs e)
        {
            DateTime ne = DateTime.Now;
            DateTime dateStop = s.AcademicYears
                       .Where(a => a.ID_Y == ID_Y)
                       .Select(a => a.StuRegSus)
                       .FirstOrDefault();
            bool isYearNowActive = s.AcademicYears
                         .Where(a => a.ID_Y == ID_Y)
                        .Select(a => a.IsActive)
                        .FirstOrDefault();
            if (isYearNowActive)
            {
                if (ne < dateStop)
                {
                    this.guna2Panel1.Controls.Clear();
                    AddStudentFromAdmin addStudentFromAdmin = new AddStudentFromAdmin(ID_Y) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                    this.guna2Panel1.Controls.Add(addStudentFromAdmin);
                    addStudentFromAdmin.Show();
                }
                else
                {
                    MessageBox.Show("تاريخ تسجيل الطلاب إنتهى بالفعل ,نتطلع لتسجيلك في السنة القادمة");
                }
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع إضافة طالبة فيها");
            }
                
        }
    }
}
