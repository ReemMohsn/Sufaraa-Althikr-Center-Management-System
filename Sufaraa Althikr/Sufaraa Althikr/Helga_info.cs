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
    public partial class Helga_info : Form
    {
        SufaraAldhikrEntities1 s =new SufaraAldhikrEntities1();    
        int ID_Y;
        int ID_H;
        Halga halga;

        public Helga_info(int ID_y,int ID_h)
        {
            ID_Y = ID_y;
            ID_H = ID_h;
            InitializeComponent();
        }
        private void loadInformation()
        {
             halga = s.Halgas
                         .Where(a => a.ID_Y == ID_Y && a.ID_H==ID_H)
                        .Select(a => a)
                        .FirstOrDefault();
            if (halga != null)
            {
                Teacher teacher = s.Teachers
                       .Where(a => a.ID_Y == ID_Y && a.TeacherUniqueID == halga.TeacherUniqueID)
                       .Select(a => a)
                       .FirstOrDefault();

                id2.Text= halga.ID_H.ToString();
                NameH.Text = halga.Name;
                if (teacher != null)
                {
                    TeacherL.Text = teacher.Name;
                }

                HelgaPeL.Text = halga.Period;
                HelLevL.Text = halga.HelgaType;
                if (halga.HelgaType.Trim() == "حفظ")
                {
                    label6.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    FromL.Visible = true;
                    ToL.Visible = true;
                    FromL.Text = halga.SaveFrom.ToString();
                    ToL.Text = halga.SaveTo.ToString();
                }
                var manahegNames = s.Manhajes
                          .Join(
                           s.ManhajeHalgas,
                           manhaj => manhaj.ID_M,               // العمود من جدول Manhaje (ID_M)
                           manhajHalga => manhajHalga.ID_M,     // العمود من جدول ManhajeHalga (ID_M)
                           (manhaj, manhajHalga) => manhaj // النتيجة المطلوبة (اسم المنهج من جدول Manhaje)
                           )
                          .ToList();
                if (manahegNames.Any())
                {
                    manaheg.Items.Clear();
                    foreach (var i in manahegNames)
                    {
                        manaheg.Items.Add(new KeyValuePair<int, string>(i.ID_M, i.Name));
                    }
                    manaheg.DisplayMember = "Value";
                    manaheg.ValueMember = "Key";
                }
                var StudentsInYear = s.Students
                  .Where(st => st.ID_Y == ID_Y && st.ID_H == ID_H)
                  .Select(st => st)
                  .ToList();

                students.Items.Clear();
                foreach (var i in StudentsInYear)
                {
                    students.Items.Add(new KeyValuePair<int, string>(i.ID_S, i.Name));
                }
                students.DisplayMember = "Value";
                students.ValueMember = "Key";
            }
            else
            {
                MessageBox.Show("الحلقة غير موجود");
            }            

        }
        private void Helga_info_Load(object sender, EventArgs e)
        {
            loadInformation();
        }
        private void TeacherL_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void HelgaPeL_Click(object sender, EventArgs e)
        {
         
        }

        private void FromL_Click(object sender, EventArgs e)
        {

        }

        private void ToL_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void hafedat_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void To_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Edit_button_Click(object sender, EventArgs e)
        {
           
        }

        private void Edit_button_Click_1(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            ReportsMain form1 = new ReportsMain(ID_Y, ID_H,"أدمن") { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(form1);
            form1.Show();

        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var MH = s.Manhajes
                        .Select(a => a)
                        .ToList();
            bool isYearNowActive = s.AcademicYears
                         .Where(a => a.ID_Y == ID_Y)
                        .Select(a => a.IsActive)
                        .FirstOrDefault();
            if (isYearNowActive)
            {
                id2.Enabled = false;
                NameH.Visible = false;
                TeacherL.Visible = false;
                HelgaPeL.Visible = false;
                HelLevL.Visible = false;
                manaheg.Visible = false;
                edit.Visible = false;
                exsits.Visible = false;
                tagareer.Visible = false;


                Hel_nameTB.Visible = true;
                Period.Visible = true;
                Teach_name.Visible = true;
                checkedListBox1.Visible = true;
                Save_button.Visible = true;
                hafedat.Visible = true;
                hofed.Visible = true;

                if (HelLevL.Text.Trim() == "حفظ")
                {
                    hofed.Checked = true;
                    FromL.Visible = false;
                    ToL.Visible = false;
                    From.Visible = true;
                    To.Visible = true;
                }
                else
                {
                    hafedat.Checked = true;
                }

                Hel_nameTB.Text = NameH.Text;
                Period.SelectedItem = HelgaPeL.Text.Trim();
                addteachersToList();
                foreach (var item in Teach_name.Items)
                {
                    if (((KeyValuePair<int, string>)item).Key == halga.TeacherUniqueID)
                    {
                        Teach_name.SelectedItem = item;
                        break;
                    }
                }

                var Manhajes = s.Manhajes
                        .Select(t => t)
                        .ToList();
                checkedListBox1.Items.Clear();
                foreach (var item in Manhajes)
                {
                    checkedListBox1.Items.Add(new KeyValuePair<int, string>(item.ID_M, item.Name));
                }

                checkedListBox1.DisplayMember = "Value";
                checkedListBox1.ValueMember = "Key";


                if (!(FromL.Text == "Note" && ToL.Text == "Note"))
                {
                    From.Value = int.Parse(FromL.Text);
                    To.Value = int.Parse(ToL.Text);
                }
                students.Enabled = false;
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع تعديل بيانات الحلقة ");
            }


        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Hel_nameTB.Text)|| Period.SelectedItem==null|| Teach_name.SelectedItem==null|| checkedListBox1.Items==null||(!hofed.Checked && !hafedat.Checked))
            {
                MessageBox.Show("قم بملاء الحقول أولاَ");
                return;
            }
            else
            {
                List<int> ids = new List<int>();
                NameH.Text = Hel_nameTB.Text;
                HelgaPeL.Text = Period.SelectedItem.ToString();
                TeacherL.Text = Teach_name.SelectedItem.ToString();
                foreach (KeyValuePair<int, string> item in manaheg.SelectedItems)
                {
                    var mid = (KeyValuePair<int, string>)Teach_name.SelectedItem;
                    int mmid = mid.Key;
                      ids.Add(mmid);
                }

                var manhajehalgas = s.ManhajeHalgas
                    .Where(a => ids.Contains(a.ID_M)&&a.ID_H==halga.ID_H&&a.ID_Y==halga.ID_Y)
                    .Select(t => t)
                .ToList();
                if (manhajehalgas != null)
                {
                    foreach (var i in manhajehalgas)
                    {
                        s.ManhajeHalgas.Remove(i);
                        s.SaveChanges();
                    }
                }

                manaheg.Items.Clear();
                foreach (KeyValuePair<int, string> item in checkedListBox1.SelectedItems)
                {
                    manaheg.Items.Add(item);
                }

                manaheg.DisplayMember = "Value";
                manaheg.ValueMember = "Key";

                if (hofed.Checked)
                {
                    HelLevL.Text = "حفظ";
                    FromL.Text = From.Value.ToString();
                    ToL.Text = To.Value.ToString();
                    FromL.Visible = true;
                    ToL.Visible = true;
                    From.Visible = false;
                    To.Visible = false;
                }
                else
                {
                    HelLevL.Text = "حافظات";
                }

                

                foreach (KeyValuePair<int, string> manhajID in manaheg.Items)
                {
                    ManhajeHalga manhajeHalga = new ManhajeHalga
                    {
                        ID_H = halga.ID_H,
                        ID_M = manhajID.Key,
                        ID_Y = ID_Y
                    };
                    s.ManhajeHalgas.Add(manhajeHalga);
                }

                halga.Name = NameH.Text ;
                if (TeacherL.Text != null)
                {
                    var teach_name = (KeyValuePair<int, string>)Teach_name.SelectedItem;
                    int teach_id = teach_name.Key;
                    halga.TeacherUniqueID= teach_id;
                }

                 halga.Period= HelgaPeL.Text;
                 halga.HelgaType= HelLevL.Text;

                if (HelLevL.Text == "حفظ")
                {
                    //label6.Visible = true;
                    //label8.Visible = true;
                    //label9.Visible = true;
                    halga.SaveFrom =int.Parse(FromL.Text);
                    halga.SaveTo =int.Parse(ToL.Text);
                }
                s.SaveChanges();


                id2.Enabled = true;
                NameH.Visible = true;
                TeacherL.Visible = true;
                HelgaPeL.Visible = true;
                HelLevL.Visible = true;
                manaheg.Visible = true;
                edit.Visible = true;
                exsits.Visible = true;
                tagareer.Visible = true;


                Hel_nameTB.Visible = false;
                Period.Visible = false;
                Teach_name.Visible = false;
                checkedListBox1.Visible = false;
                Save_button.Visible = false;
                hafedat.Visible = false;
                hofed.Visible = false;


            }
        }
        private void addteachersToList()
        {
            if (Period.SelectedItem!=null)
            {
                List<int> teacherIds = s.Teachers
               .Where(t => (t.Period.Trim() == Period.SelectedItem.ToString() || t.Period.Trim() == "صباحي و مسائي") && t.ID_Y == ID_Y)
              .Select(t => t.TeacherUniqueID)
                .ToList();

                var teacherIdInHalga = s.Halgas
                   .Where(h => h.ID_Y == ID_Y && h.Period == Period.SelectedItem.ToString() && h.TeacherUniqueID != null)
                   .Select(h => h.TeacherUniqueID.Value) // استخدم .Value لأننا تأكدنا أن القيمة ليست null
                   .ToList();
                List<int> teacherIdsNotInHalga = teacherIds.Except(teacherIdInHalga).ToList();

                if (halga.TeacherUniqueID.HasValue) // التأكد من أن TeacherUniqueID ليست null
                {
                    teacherIdsNotInHalga.Add(halga.TeacherUniqueID.Value); // إضافة معلم الحلقة بدون null
                }
                var teachersNotInHalga = s.Teachers
                   .Where(t => teacherIdsNotInHalga.Contains(t.TeacherUniqueID))
                   .Select(t => t)
                   .ToList();

                Teach_name.Items.Clear();
                foreach (var i in teachersNotInHalga)
                {
                    Teach_name.Items.Add(new KeyValuePair<int, string>(i.TeacherUniqueID, i.Name));
                }
                Teach_name.DisplayMember = "Value";
                Teach_name.ValueMember = "Key";

                //if (halga.TeacherUniqueID.HasValue) // التأكد من أن TeacherUniqueID ليست null
                //{
                //    Teach_name.SelectedValue = halga.TeacherUniqueID;
                //}
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            addteachersToList();
        }

        private void hofed_CheckedChanged(object sender, EventArgs e)
        {
            if (hofed.Checked==true)
            {
                label6.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                From.Visible = true;
                To.Visible = true;
            }
            else
            {
                label6.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                From.Visible = false;
                From.Visible = false;
                To.Visible = false;
            }
        }

        private void exsits_Click(object sender, EventArgs e)
        {
            this.guna2Panel1.Controls.Clear();
            form createAccount = new form(ID_Y, ID_H) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.guna2Panel1.Controls.Add(createAccount);
            createAccount.Show();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
