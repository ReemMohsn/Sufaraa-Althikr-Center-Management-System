using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms;
using System.Data.Entity; 
namespace Sufaraa_Althikr
{
    public partial class addManh : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        public addManh()
        {
            InitializeComponent();
        }
        private string selectedImagePath = string.Empty;
        private string selectedPdfPath = string.Empty;
        private void ImportManhPicB_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.JPG;*.PNG";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ManhPicture.Image = Image.FromFile(openFileDialog1.FileName);
                selectedImagePath = openFileDialog1.FileName; // حفظ المسار المؤقت
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "PDF Files|*.pdf";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                manhFilePathTB.Text = openFileDialog2.FileName;
                selectedPdfPath = openFileDialog2.FileName; // حفظ المسار المؤقت
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Manh_nameTB.Text) || string.IsNullOrEmpty(selectedImagePath) || string.IsNullOrEmpty(selectedPdfPath))
            {
                MessageBox.Show("يرجى إدخال جميع البيانات المطلوبة.");
                return;
            }

            try
            {
                // إنشاء مجلدات إذا لم تكن موجودة
                string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                string pdfsFolder = Path.Combine(Application.StartupPath, "PDFs");
                Directory.CreateDirectory(imagesFolder);
                Directory.CreateDirectory(pdfsFolder);

                // حفظ الصورة في مجلد الصور داخل المشروع
                string imageFileName = Path.GetFileName(selectedImagePath);
                string savedImagePath = Path.Combine(imagesFolder, imageFileName);
                File.Copy(selectedImagePath, savedImagePath, true);

                // حفظ ملف PDF في مجلد الملفات داخل المشروع
                string pdfFileName = Path.GetFileName(selectedPdfPath);
                string savedPdfPath = Path.Combine(pdfsFolder, pdfFileName);
                File.Copy(selectedPdfPath, savedPdfPath, true);

                // حفظ المسارات النسبية في قاعدة البيانات
                Manhaje mh = new Manhaje()
                {
                    Name = Manh_nameTB.Text,
                    URL = Path.Combine("PDFs", pdfFileName), // مسار نسبي لملف PDF
                    Picture = Path.Combine("Images", imageFileName) // مسار نسبي للصورة
                };
                s.Manhajes.Add(mh);
                s.SaveChanges();
                MessageBox.Show("تم حفظ المنهج بنجاح.");

                // تنظيف الحقول بعد الحفظ
                Manh_nameTB.Text = "";
                manhFilePathTB.Text = "";
                ManhPicture.Image = null;
                selectedImagePath = string.Empty;
                selectedPdfPath = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message);
            }
        }

        private void addManh_Load(object sender, EventArgs e)
        {

        }
    }
}
