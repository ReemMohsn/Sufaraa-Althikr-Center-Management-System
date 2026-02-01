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
    public partial class halgasTeacher : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        int roleAccountTeacherID;
        public halgasTeacher(int RoleAccountTeacherID)
        {
            roleAccountTeacherID = RoleAccountTeacherID;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
       
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void halgasTeacher_Load(object sender, EventArgs e)
        {
            int idy = s.RoleAccounts
                        .Where(a => a.ID_RA == roleAccountTeacherID)
                       .Select(a => a.ID_Y)
                       .FirstOrDefault();
            LoadHalaqat(idy);
        }
        private void LoadHalaqat(int yearID)
        {
            // تفعيل التمرير التلقائي
            panel2.AutoScroll = true;
            panel2.Controls.Clear();

            // إعدادات الزر
            int buttonWidth = 190;
            int buttonHeight = 100;
            int buttonMargin = 10;
            int buttonsPerRow = 2;

            // تحديد موقع البداية
            int xOffset = buttonMargin;
            int yOffset = buttonMargin;
            int buttonsInCurrentRow = 0;

            int teacherid = s.RoleAccounts
                          .Where(a => a.ID_RA == roleAccountTeacherID)
                         .Select(a => a.LinkedID)
                         .FirstOrDefault();

            int idy = s.RoleAccounts
                        .Where(a => a.ID_RA == roleAccountTeacherID)
                       .Select(a => a.ID_Y)
                       .FirstOrDefault();

            int teacherUniqueID = s.Teachers
                        .Where(a => a.ID_T == teacherid && a.ID_Y == idy)
                       .Select(a => a.TeacherUniqueID)
                       .FirstOrDefault();



            // جلب الحلقات من قاعدة البيانات
            var halaqat = s.Halgas.Where(a => a.TeacherUniqueID == teacherUniqueID && a.ID_Y == idy).Select(a => a).ToList();

            foreach (var halaqa in halaqat)
            {
                Guna.UI2.WinForms.Guna2Button btnHalaqa = new Guna.UI2.WinForms.Guna2Button();
                btnHalaqa.Text = halaqa.Name;
                btnHalaqa.Width = buttonWidth;
                btnHalaqa.Height = buttonHeight;
                btnHalaqa.FillColor = Color.FromArgb(55, 41, 21);
                btnHalaqa.ForeColor = Color.FromArgb(255, 240, 219);
                btnHalaqa.Font = new Font("Mizan AR+LT", 15, FontStyle.Regular);
                btnHalaqa.TextAlign = HorizontalAlignment.Center;
                btnHalaqa.BorderRadius = 10;
                btnHalaqa.Margin = new Padding(buttonMargin);

                // ضبط موقع الزر الحالي
                btnHalaqa.Location = new Point(xOffset, yOffset);

                // إضافة الحدث عند النقر على الزر
                btnHalaqa.Click += (sender, e) =>
                {
                    this.panel1.Controls.Clear();
                    Helga_info helga_info = new Helga_info(yearID, halaqa.ID_H) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                    this.panel1.Controls.Add(helga_info);
                    helga_info.Show();
                };

                // إضافة الزر إلى اللوحة
                panel2.Controls.Add(btnHalaqa);

                // تحديث عدد الأزرار في الصف الحالي
                buttonsInCurrentRow++;

                // إذا تم إضافة زرين في الصف، ننتقل إلى الصف التالي
                if (buttonsInCurrentRow >= buttonsPerRow)
                {
                    buttonsInCurrentRow = 0;
                    xOffset = buttonMargin; // إعادة التعيين للإزاحة الأفقية
                    yOffset += buttonHeight + buttonMargin; // الانتقال للصف التالي
                }
                else
                {
                    // التحرك للإزاحة الأفقية للزر التالي في نفس الصف
                    xOffset += buttonWidth + buttonMargin;
                }
            }
        }

    }
}
