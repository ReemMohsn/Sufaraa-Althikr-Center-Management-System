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
    public partial class Helga : Form
    {
        int YearID;
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        public Helga(int yearID)
        {
            YearID = yearID;
            InitializeComponent();
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

            // جلب الحلقات من قاعدة البيانات
            var halaqat = GetHalaqatByYear(yearID);

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
        private void Helga_Load(object sender, EventArgs e)
        {
            LoadHalaqat(YearID);
        }
        private List<Halga> GetHalaqatByYear(int yearID)
        {
            using (var context = new SufaraAldhikrEntities1())
            {
                return context.Halgas
                              .Where(h => h.ID_Y == yearID)
                              .ToList();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void addHel_button_Click(object sender, EventArgs e)
        {

        }

        private void addHel_button_Click_1(object sender, EventArgs e)
        {
            bool isYearNowActive = s.AcademicYears
                         .Where(a => a.ID_Y == YearID)
                        .Select(a => a.IsActive)
                        .FirstOrDefault();
            if (isYearNowActive)
            {
                this.panel1.Controls.Clear();
                addHel AddHel = new addHel(YearID) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.panel1.Controls.Add(AddHel);
                AddHel.Show();
            }
            else
            {
                MessageBox.Show("هذه السنة مؤرشفة أنت لا تستطيع إضافة حلقة جديدة فيها");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
