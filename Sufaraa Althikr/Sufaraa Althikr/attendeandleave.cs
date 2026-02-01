using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Sufaraa_Althikr
{
    public partial class attendeandleave : Form
    {
        int month, year;
        int ID_Y;
        int ID_S;
        DateTime now;
        string studentname;
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();

        int roleAccountStudentID;
        public attendeandleave(int RoleAccountStudentID)
        {
            roleAccountStudentID = RoleAccountStudentID;    
            InitializeComponent();
        }

        private void attendeandleave_Load(object sender, EventArgs e)
        {
             ID_S = s.RoleAccounts
                          .Where(a => a.ID_RA == roleAccountStudentID)
                         .Select(a => a.LinkedID)
                         .FirstOrDefault();

             ID_Y = s.RoleAccounts
                        .Where(a => a.ID_RA == roleAccountStudentID)
                       .Select(a => a.ID_Y)
                       .FirstOrDefault();

             studentname = s.Students
                          .Where(a => a.ID_Y == ID_Y && a.ID_S== ID_S)
                         .Select(a => a.Name)
                         .FirstOrDefault();

            DisplayMonthlyAttendance(studentname);
        }
        private void DisplayMonthlyAttendance(string studentNames)
        {
            // إعداد التاريخ والشهر
            var academicyear = s.AcademicYears.Where(a => a.ID_Y == ID_Y).Select(a => a.BeginOfYear).FirstOrDefault();
            now = academicyear;
            completdata(studentNames);
        }

        private void completdata(string studentNames)
        {
            int month = now.Month;
            int year = now.Year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = $"{monthName} {year}";

            DateTime startOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime endOfMonth = new DateTime(year, month, daysInMonth);

            // تحديد الأيام المطلوبة (السبت، الإثنين، الأربعاء)
            var allowedDays = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Monday, DayOfWeek.Wednesday };

            // استخراج جميع الأيام المطلوبة في الشهر
            List<DateTime> selectedDates = new List<DateTime>();
            for (DateTime date = startOfMonth; date <= endOfMonth; date = date.AddDays(1))
            {
                if (allowedDays.Contains(date.DayOfWeek))
                {
                    selectedDates.Add(date);
                }
            }

            // إعداد خصائص الجدول
            calendar.Controls.Clear();
            calendar.ColumnCount = selectedDates.Count + 1; // عمود اسم الطالب + أعمدة الأيام
            calendar.RowCount =  2; // صف العناوين + صف لكل طالب

            // تهيئة عرض الأعمدة
            int studentColumnWidth = 150; // عرض العمود الأول (أسماء الطلاب)
            int cellSize = 50; // حجم الخلايا الأخرى (عرض وطول)

            calendar.ColumnStyles.Clear();
            calendar.RowStyles.Clear();

            // إعداد الأعمدة
            calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, studentColumnWidth)); // العمود الأول
            for (int i = 1; i < calendar.ColumnCount; i++)
            {
                calendar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellSize)); // بقية الأعمدة
            }

            // إعداد الصفوف
            for (int i = 0; i < calendar.RowCount; i++)
            {
                calendar.RowStyles.Add(new RowStyle(SizeType.Absolute, cellSize));
            }

            // إعداد صف العناوين
            Label lblStudentHeader = new Label
            {
                Text = "الطالبة",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = Color.LightGray,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            calendar.Controls.Add(lblStudentHeader, 0, 0); // عنوان "الطالب"

            // إضافة عناوين الأعمدة للأيام
            int colIndex = 1;
            foreach (var date in selectedDates)
            {
                Label lblDayHeader = new Label
                {
                    Text = $"{GetArabicDayName(date.DayOfWeek)} {date.Day}", // مثال: السبت 7
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BackColor = Color.LightGray,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                calendar.Controls.Add(lblDayHeader, colIndex, 0); // عناوين الأيام
                colIndex++;
            }

            // إضافة صفوف الطلاب
                // إضافة اسم الطالب في العمود الأول
                Label lblStudentName = new Label
                {
                    Text = studentNames,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Font = new Font("Arial", 10)
                };
                calendar.Controls.Add(lblStudentName, 0, 1);

                // إضافة الأزرار للأيام في الأعمدة المتبقية
                colIndex = 1;
                foreach (var date in selectedDates)
                {
                    Button dayButton = new Button
                    {
                        Text = "", // الزر فارغ مبدئيًا
                        Dock = DockStyle.Fill,
                        BackColor = Color.LightGray,
                        Height = cellSize, // طول الزر
                        Width = cellSize // عرض الزر
                    };

                    // حدث للنقر لتغيير حالة الحضور
                    dayButton.Click += (s, e) =>
                    {
                        Button btn = s as Button;
                        if (btn.BackColor == Color.LightGray) btn.BackColor = Color.Green; // حاضر
                        else if (btn.BackColor == Color.Green) btn.BackColor = Color.Red; // غائب بعذر
                        else btn.BackColor = Color.LightGray; // غائب بدون عذر
                    };

                    calendar.Controls.Add(dayButton, colIndex, 1);
                    colIndex++;
                }
            

            // إجبار أشرطة التمرير على الظهور عند الحاجة فقط
            calendar.HorizontalScroll.Visible = calendar.DisplayRectangle.Width > calendar.ClientSize.Width;
            calendar.VerticalScroll.Visible = calendar.DisplayRectangle.Height > calendar.ClientSize.Height;

        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            var academicyearend = s.AcademicYears.Where(a => a.ID_Y == ID_Y).Select(a => a.EndOfYear).FirstOrDefault();
            DateTime newdata = now.AddMonths(1);
            if (newdata < academicyearend)
            {
                now = newdata;
                completdata(studentname);
            }
            else
            {
                MessageBox.Show("لا يوجد شهر إضافي في هذه السنه الدراسية");
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            var academicyearbegian = s.AcademicYears.Where(a => a.ID_Y == ID_Y).Select(a => a.BeginOfYear).FirstOrDefault();
            DateTime newdata = now.AddMonths(-1);
            if (newdata >= academicyearbegian)
            {
                now = newdata;
                completdata(studentname);
            }
            else
            {
                MessageBox.Show("لا يوجد شهر قبل هذه الشهر في هده السنه الدراسية");
            }
        }

        // دالة لتحويل اسم اليوم إلى اللغة العربية
        private string GetArabicDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Saturday:
                    return "السبت";
                case DayOfWeek.Sunday:
                    return "الأحد";
                case DayOfWeek.Monday:
                    return "الإثنين";
                case DayOfWeek.Tuesday:
                    return "الثلاثاء";
                case DayOfWeek.Wednesday:
                    return "الأربعاء";
                case DayOfWeek.Thursday:
                    return "الخميس";
                case DayOfWeek.Friday:
                    return "الجمعة";
                default:
                    return "";
            }
        }
    }
}
