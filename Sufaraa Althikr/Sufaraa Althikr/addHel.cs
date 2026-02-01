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
    public partial class addHel : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        int YearID;
        public addHel(int yearID)
        {
            YearID = yearID;
            InitializeComponent();
            loadManaheg();
        }

        private void addHel_Load(object sender, EventArgs e)
        {
           
        }
        private void loadManaheg()
        {
            var Manhajes = s.Manhajes
                         .Select(t => t.Name)
                         .ToList();
            checkedListBox1.Items.Clear();
            foreach (var item in Manhajes)
            {
                checkedListBox1.Items.Add(item);
            }
        }

        private void ID_H_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // منع الإدخال
            }
        }
        

        private void From_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Teach_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            var teacherIds = s.Teachers
           .Where(t =>(t.Period.Trim() == Period.SelectedItem.ToString().Trim() || t.Period == "صباحية و مسائية")  && t.ID_Y == YearID)
           .Select(t => t.TeacherUniqueID)
           .ToList();

            var teacherIdInHalga = s.Halgas
               .Where(h => h.ID_Y == YearID && h.Period == Period.SelectedItem.ToString() && h.TeacherUniqueID != null)
               .Select(h => h.TeacherUniqueID.Value) // استخدم .Value لأننا تأكدنا أن القيمة ليست null
               .ToList();


            List<int> teacherIdsNotInHalga = teacherIds.Except(teacherIdInHalga).ToList();

              var  teachersNotInHalga = s.Teachers
                .Where(t => teacherIdsNotInHalga.Contains(t.TeacherUniqueID))
                .Select(t =>t) 
                .ToList();

            Teach_name.Items.Clear();
            foreach (var i in teachersNotInHalga)
            {
                Teach_name.Items.Add(new KeyValuePair<int, string>(i.TeacherUniqueID, i.Name));
            }
            //Teach_name.DataSource = teachersNotInHalga;  // تعيين DataSource مباشرة
            Teach_name.DisplayMember = "Value"; 
            Teach_name.ValueMember = "Key";     


        }

        private void Hel_nameTB_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
        
                // تحقق من الحقول الفارغة أو غير المحددة
                if (string.IsNullOrWhiteSpace(Hel_nameTB.Text) || Period.SelectedItem == null|| !(hafedat.Checked || hofed.Checked))
                {
                    MessageBox.Show("قم بملئ الحقول أولاً");
                    return;
                }
            if (Teach_name.SelectedItem == null)
            {
                DialogResult result = MessageBox.Show(
                    "سيتم إنشاء هذه الحلقة بدون معلمة، هل أنت متأكد من رغبتك في الإنشاء؟\nعلماً أنه يمكنك تعديل ذلك لاحقاً",
                    "تأكيد الإنشاء",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            if (hofed.Checked)
                {
                // تحقق من قيمة From و To
                if (From.Value > To.Value)
                {
                    MessageBox.Show("بداية الحفظ المسموح به في هذه الحلقة أكبر من نهاية الحفظ المسموح به في هذه الحلقة");
                    return;
                }
                }

                string name = Hel_nameTB.Text;
            string fullName = Hel_nameTB.Text;


            // تحقق إذا كان اسم الحلقة موجودًا بالفعل في السنة
            bool isNameExists = s.Halgas.Any(h => h.ID_Y == YearID && h.Name == name);
                if (isNameExists)
                {
                    MessageBox.Show("اسم الحلقة موجود بالفعل في هذه السنة. يرجى اختيار اسم آخر.");
                    Hel_nameTB.Clear();
                    Hel_nameTB.Focus();
                    return;
                }
            Halga halga = new Halga();
            halga.Name = name;
            halga.ID_Y = YearID;
            halga.Period = Period.SelectedItem.ToString();
            halga.HelgaType = hofed.Checked ? "حفظ" : "حافظات";
            if (Teach_name.SelectedItem != null)
            {
                var selectedPair = (KeyValuePair<int, string>)Teach_name.SelectedItem;
                halga.TeacherUniqueID = selectedPair.Key;
            }
                

                // تخصيص قيم حفظ من وإلى إذا كانت الحلقة "حفظ"
                if (hofed.Checked)
                {
                    halga.SaveFrom =Convert.ToInt32(From.Text);
                    halga.SaveTo = Convert.ToInt32(To.Text);
                }

                // تعيين ID_H بناءً على المدخلات أو تخصيص رقم جديد
                if (string.IsNullOrEmpty(ID_H.Text))
                {
                    halga.ID_H = s.Halgas.Any() ? s.Halgas.Max(h => h.ID_H) + 1 : 1;
                }
                else if (int.TryParse(ID_H.Text, out int enteredID_H))
                {
                    var existingHalga = s.Halgas.FirstOrDefault(h => h.ID_H == enteredID_H && h.Name == name);
                    if (existingHalga != null)
                    {
                        halga.ID_H = enteredID_H;
                    }
                    else
                    {
                        MessageBox.Show("لم يتم إجاد أي بيانات عن حلقة لرقم التعريف المدخل في السنوات الماضية. الرجاء التأكد من رقم التعريف المدخل.");
                        return;
                    }
                }

                // إضافة الحلقة إلى الكيان
                s.Halgas.Add(halga);

                // إضافة المناهج المرتبطة بالحلقات

                List<string> selectedManhajesNames = checkedListBox1.CheckedItems.Cast<string>().ToList();
                List<int> selectedManhajesIDs = s.Manhajes
                    .Where(m => selectedManhajesNames.Contains(m.Name))
                    .Select(m => m.ID_M)
                    .ToList();
                
                foreach (int manhajID in selectedManhajesIDs)
                {
                    ManhajeHalga manhajeHalga = new ManhajeHalga
                    {
                        ID_H = halga.ID_H,
                        ID_M = manhajID,
                        ID_Y = YearID
                    };
                    s.ManhajeHalgas.Add(manhajeHalga);
                }

                // تحديث الطلاب للحلقة
                List<Student> Students = new List<Student>();
                if (halga.HelgaType == "حافظات")
                {
                    Students = s.Students
                        .Where(s => s.ID_Y == YearID && s.Period == halga.Period && s.PreSavAom == 30 && s.ID_H != null)
                        .ToList();
                }
                else
                {
                Students = s.Students
                               .Where(st => st.ID_Y == YearID && st.Period == halga.Period && st.PreSavAom + 1 >= halga.SaveFrom && st.PreSavAom + 1 <= halga.SaveTo && st.ID_H != null)
                               .Select(h => h)
                               .ToList();
                }

            foreach (var student in Students)
                {
                student.ID_H = halga.ID_H;
                s.Entry(student).State = System.Data.Entity.EntityState.Modified;

                YearlyRrport yearlyRrport = new YearlyRrport();
                yearlyRrport.ID_S = student.ID_S;
                yearlyRrport.ID_H = student.ID_H.Value;
                yearlyRrport.ID_Y = student.ID_Y;
                s.YearlyRrports.Add(yearlyRrport);

                TermReport termReport = new TermReport();
                termReport.ID_Y = student.ID_Y;
                termReport.ID_H = student.ID_H.Value;
                termReport.ID_S = student.ID_S;
                termReport.Term = "الأول";
                s.TermReports.Add(termReport);

                TermReport termReport2 = new TermReport();
                termReport2.ID_Y = student.ID_Y;
                termReport2.ID_H = student.ID_H.Value;
                termReport2.ID_S = student.ID_S;
                termReport2.Term = "الثاني";
                s.TermReports.Add(termReport2);

                MothlyReport mothlyReport = new MothlyReport();
                mothlyReport.ID_Y = student.ID_Y;
                mothlyReport.ID_H = student.ID_H.Value;
                mothlyReport.ID_S = student.ID_S;
                mothlyReport.Term = "الأول";
                mothlyReport.Month = 1;
                s.MothlyReports.Add(mothlyReport);

                MothlyReport mothlyReport2 = new MothlyReport();
                mothlyReport2.ID_Y = student.ID_Y;
                mothlyReport2.ID_H = student.ID_H.Value;
                mothlyReport2.ID_S = student.ID_S;
                mothlyReport2.Term = "الأول";
                mothlyReport2.Month = 2;
                s.MothlyReports.Add(mothlyReport2);

                MothlyReport mothlyReport3 = new MothlyReport();
                mothlyReport3.ID_Y = student.ID_Y;
                mothlyReport3.ID_H = student.ID_H.Value;
                mothlyReport3.ID_S = student.ID_S;
                mothlyReport3.Term = "الثاني";
                mothlyReport3.Month = 1;
                s.MothlyReports.Add(mothlyReport3);

                MothlyReport mothlyReport4 = new MothlyReport();
                mothlyReport4.ID_Y = student.ID_Y;
                mothlyReport4.ID_H = student.ID_H.Value;
                mothlyReport4.ID_S = student.ID_S;
                mothlyReport4.Term = "الثاني";
                mothlyReport4.Month = 2;
                s.MothlyReports.Add(mothlyReport4);


            }

            // حفظ التغييرات النهائية
            int check = s.SaveChanges();
                if (check > 0)
                {
                    MessageBox.Show("تم إضافة الحلقة بنجاح", "نجاح عملية الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("فشلت عملية الإضافة", "فشل عملية الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            

           
        
    }

        private void hafedat_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void hofed_CheckedChanged(object sender, EventArgs e)
        {
            if (hofed.Checked == true)
            {
                label6.Visible = true;
                label8.Visible = true;
                From.Visible= true;
                To.Visible= true;
                label9.Visible=true;
            }
            else
            {
                label6.Visible = false;
                label8.Visible = false;
                From.Visible = false;
                To.Visible = false;
                label9.Visible = false;
            }        
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
