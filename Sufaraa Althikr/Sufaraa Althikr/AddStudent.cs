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
    public partial class AddStudent : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        RoleAccount RA = new RoleAccount();
        Student st =new Student();
        int ID_Y = 0;
        int ID_A = 0;
        string Typee;
        public AddStudent(int ID_y,int ID_a,string type)
        {
            Typee = type;
            ID_Y = ID_y;
            ID_A = ID_a;
            InitializeComponent();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            PhoneNoTB.KeyPress += new KeyPressEventHandler(PhoneNoTB_KeyPress);
            PhoneNoTB.TextChanged += new EventHandler(PhoneNoTB_TextChanged);
            PhoneNoTB.KeyPress += new KeyPressEventHandler(ParentNo_KeyPress);
            PhoneNoTB.TextChanged += new EventHandler(ParentNo_TextChanged);

            ReletiveTB.KeyPress += new KeyPressEventHandler(ReletiveTB_KeyPress);
            AddressTB.KeyPress += new KeyPressEventHandler(AddressTB_KeyPress);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void PhoneNoTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط للأرقام
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        private void HomeNoTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط للأرقام
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        private void ParentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط للأرقام
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void PhoneNoTB_TextChanged (object sender, EventArgs e)
        {
            // التأكد من أن طول الرقم لا يتجاوز 9 أرقام
            if (PhoneNoTB.Text.Length > 9)
            {
                MessageBox.Show("رقم الهاتف يجب أن يكون مكونًا من 9 أرقام فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PhoneNoTB.Text = PhoneNoTB.Text.Substring(0, 9); // تقليص الرقم إلى 9 أرقام فقط
            }
        }
        private void ParentNo_TextChanged(object sender, EventArgs e)
        {
            // التأكد من أن طول الرقم لا يتجاوز 9 أرقام
            if (ParentNo.Text.Length > 9)
            {
                MessageBox.Show("رقم الهاتف يجب أن يكون مكونًا من 9 أرقام فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ParentNo.Text = ParentNo.Text.Substring(0, 9); // تقليص الرقم إلى 9 أرقام فقط
            }
        }


        private void ID_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأرقام
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // منع الإدخال
            }
        }

        private void ReletiveTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأحرف (سواء كانت صغيرة أو كبيرة)
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;  // منع إدخال الحروف غير الأبجدية
            }
        }
        private void AddressTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح فقط بالأحرف (سواء كانت صغيرة أو كبيرة)
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;  // منع إدخال الحروف غير الأبجدية
            }
        }



        private bool ValidateFullName(string fullName)
        {
            // تحقق من أن الإدخال ليس فارغًا
            if (string.IsNullOrWhiteSpace(fullName))
                

            // إزالة المسافات الزائدة
            fullName = fullName.Trim();

            // السماح بالأسماء المركبة (مثل "بن ناشيء")
            string[] parts = fullName.Split(' ');

            // دمج الأجزاء المركبة مثل "بن ناشيء" في جزء واحد
            List<string> validParts = new List<string>();

            return false;

            for (int i = 0; i < parts.Length; i++)
            {
                if (i > 0 && parts[i - 1].ToLower() == "بن") // تحقق إذا الجزء السابق هو "بن"
                {
                    validParts[validParts.Count - 1] += " " + parts[i]; // دمج "بن" مع الجزء التالي
                    return true;
                }
                else
                {
                    validParts.Add(parts[i]);
                    return false;
                }
                
            }
        }
            private void Save_button_Click_1(object sender, EventArgs e)
        {
            DateTime ne = DateTime.Now;
            DateTime dateStop = s.AcademicYears
                       .Where(a => a.ID_Y == ID_Y)
                       .Select(a => a.StuRegSus)
                       .FirstOrDefault();
            if (ne < dateStop)
            {
                if (string.IsNullOrWhiteSpace(StudentNameTB.Text) ||
              string.IsNullOrWhiteSpace(PhoneNoTB.Text) ||
              string.IsNullOrWhiteSpace(HomeNoTB.Text) ||
              string.IsNullOrWhiteSpace(ParentNo.Text) ||
              string.IsNullOrWhiteSpace(ReletiveTB.Text) ||
              string.IsNullOrWhiteSpace(AddressTB.Text) ||
              peroid.SelectedItem == null ||                  
              KeepFromCB.SelectedItem == null
              )
            {
                MessageBox.Show("قم بملئ جميع الحقول أولاً");
            }
            else
            {
                    string fullName = StudentNameTB.Text;

                    // تحقق من صحة الاسم الرباعي
                    bool isValid = ValidateFullName(fullName);

                    if (isValid)
                    {
                        st.Name = StudentNameTB.Text;
                    }
                    else
                    {
                        MessageBox.Show("الاسم غير صالح. يجب أن يكون رباعي");
                    }
                    
                    st.PhoneNumber = PhoneNoTB.Text;
                    st.HomeNum = HomeNoTB.Text;
                    st.Period = peroid.SelectedItem.ToString();
                    st.GuardianPhoNum = ParentNo.Text;
                    st.Guardian = ReletiveTB.Text;
                    st.Address = AddressTB.Text;
                    st.PreSavAom =(int)numericUpDown1.Value;
                    st.BeginOfSave = KeepFromCB.SelectedItem.ToString();

                    st.ID_Y = ID_Y;
                    if (string.IsNullOrEmpty(ID_s.Text))
                    {
                        st.ID_S = s.Students.Any() ? s.Students.Max(stt => stt.ID_S) + 1 : 1;
                    }
                    else
                    {
                        int IDStudent = int.Parse(ID_s.Text);
                        var existingStudent = s.Students.FirstOrDefault(a => a.ID_S == IDStudent && a.Name == StudentNameTB.Text);
                        if (existingStudent != null)
                        {
                            st.ID_S = IDStudent;
                        }
                        else
                        {
                            MessageBox.Show("لم يتم إجاد أي بيانات لرقم التعريف المدخل في السنيين الماضية الرجاء التأكد من رقم التعريف المدخل ");
                            return;
                        }
                    }
                    int id_h=0;
                    if (st.PreSavAom == 30)
                    {
                         id_h = s.Halgas.Where(h => h.ID_Y == ID_Y && h.Period == st.Period && h.HelgaType=="حافظات")
                                             .Select(h => h.ID_H)
                                              .FirstOrDefault();
                    }
                    else
                    {
                         id_h = s.Halgas.Where(h => h.ID_Y == ID_Y && h.Period == st.Period && st.PreSavAom+1 >= h.SaveFrom && st.PreSavAom+1 <= h.SaveTo)
                                            .Select(h => h.ID_H)
                                            .FirstOrDefault();
                    }
                    if (id_h != 0)
                    {
                        st.ID_H=id_h;
                        YearlyRrport yearlyRrport = new YearlyRrport();
                        yearlyRrport.ID_S = st.ID_S;
                        yearlyRrport.ID_H = id_h;
                        yearlyRrport.ID_Y = ID_Y;
                        s.YearlyRrports.Add(yearlyRrport);

                        TermReport termReport = new TermReport();
                        termReport.ID_Y = ID_Y;
                        termReport.ID_H = id_h;
                        termReport.ID_S = st.ID_S;
                        termReport.Term = "الأول";
                        s.TermReports.Add(termReport);

                        TermReport termReport2 = new TermReport();
                        termReport2.ID_Y = ID_Y;
                        termReport2.ID_H = id_h;
                        termReport2.ID_S = st.ID_S;
                        termReport2.Term = "الثاني";
                        s.TermReports.Add(termReport2);

                        MothlyReport mothlyReport = new MothlyReport();
                        mothlyReport.ID_Y = ID_Y;
                        mothlyReport.ID_H = id_h;
                        mothlyReport.ID_S = st.ID_S;
                        mothlyReport.Term = "الأول";
                        mothlyReport.Month = 1;
                        s.MothlyReports.Add(mothlyReport);

                        MothlyReport mothlyReport2 = new MothlyReport();
                        mothlyReport2.ID_Y = ID_Y;
                        mothlyReport2.ID_H = id_h;
                        mothlyReport2.ID_S = st.ID_S;
                        mothlyReport2.Term = "الأول";
                        mothlyReport2.Month = 2;
                        s.MothlyReports.Add(mothlyReport2);

                        MothlyReport mothlyReport3 = new MothlyReport();
                        mothlyReport3.ID_Y = ID_Y;
                        mothlyReport3.ID_H = id_h;
                        mothlyReport3.ID_S = st.ID_S;
                        mothlyReport3.Term = "الثاني";
                        mothlyReport3.Month = 1;
                        s.MothlyReports.Add(mothlyReport3);

                        MothlyReport mothlyReport4 = new MothlyReport();
                        mothlyReport4.ID_Y = ID_Y;
                        mothlyReport4.ID_H = id_h;
                        mothlyReport4.ID_S = st.ID_S;
                        mothlyReport4.Term = "الثاني";
                        mothlyReport4.Month = 2;
                        s.MothlyReports.Add(mothlyReport4);
                    }

                        s.Students.Add(st);
                        int check = s.SaveChanges();
                        if (check > 0)
                        {
                            MessageBox.Show(" إضافة الطالبة تم بنجاح", "نجاح عملية الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RA.ID_R = 1;
                            RA.ID_A = ID_A;
                            RA.ID_Y = ID_Y;
                            RA.LinkedID = st.ID_S;
                            RA.ID_RA = s.RoleAccounts.Max(R => R.ID_RA) + 1;
                            s.RoleAccounts.Add(RA);
                            int check2 = s.SaveChanges();
                            if (check2 > 0)
                            {
                                MessageBox.Show("تم تسجيلك كطالبة بنجاح");
                            int RoleAcountID = RA.ID_RA;
                            studentMain StudentMain = new studentMain(RoleAcountID, ID_Y);
                            StudentMain.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
                            StudentMain.Show();
                            this.Hide();
                        }
                            else
                            {
                                MessageBox.Show("حدث خطأ أثناء تسجيلك كطالب");

                            }
                        }
                        else
                        {
                            MessageBox.Show(" إضافة الطالبة فشلة ", "فشل عملية الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
            }
            }
            else
            {
                MessageBox.Show("تاريخ تسجيل الطلاب إنتهى بالفعل ,نتطلع لتسجيلك في السنة القادمة");
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 30)
            {
                label10.Text = "بداية المراجعة: ";
            }
            else
            {
                label10.Text = "بداية الحفظ: ";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            rocordAsStudentTeacher ocordAsStudentTeacher = new rocordAsStudentTeacher(ID_Y, ID_A, Typee);
            ocordAsStudentTeacher.FormClosed += (s, args) => this.Close(); // إغلاق الفورم الحالية عند إغلاق الثانية
            ocordAsStudentTeacher.Show();
            this.Hide();
        }

        private void X_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
