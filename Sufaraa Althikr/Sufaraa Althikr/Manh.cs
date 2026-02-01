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
using Guna.UI2.WinForms;
namespace Sufaraa_Althikr
{
    public partial class Manh : Form
    {
        SufaraAldhikrEntities1 s = new SufaraAldhikrEntities1();
        public Manh()
        {
            InitializeComponent();
        }

        private void Manh_Load(object sender, EventArgs e)
        {
            loadManhaje();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void loadManhaje()
        {
            var curriculaList = s.Manhajes.Select(m => m).ToList();
            int xPosition = 0;

            foreach (var curriculum in curriculaList)
            {
                Panel curriculumPanel = new Panel();
                curriculumPanel.Size = new Size(220, 255);
                curriculumPanel.BackColor = Color.FromArgb(255, 240, 219);
                curriculumPanel.Location = new Point(xPosition, 10);

                PictureBox PictureManhaje = new PictureBox();
                PictureManhaje.Size = new Size(80, 185);
                PictureManhaje.SizeMode = PictureBoxSizeMode.StretchImage;
                PictureManhaje.Dock = DockStyle.Top;

                try
                {
                    string imagePath = curriculum.Picture;
                    if (File.Exists(imagePath))
                    {
                        PictureManhaje.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        PictureManhaje.Image = null;
                    }
                }
                catch (Exception)
                {
                    PictureManhaje.Image = null;
                }

                Label nameLabel = new Label();
                nameLabel.Text = curriculum.Name;
                nameLabel.Font = new Font("Mizan AR+LT", 13, FontStyle.Regular);
                nameLabel.ForeColor = Color.FromArgb(55, 41, 21);
                nameLabel.TextAlign = ContentAlignment.MiddleCenter;
                nameLabel.Location = new Point(0, PictureManhaje.Bottom + 5);
                nameLabel.Width = curriculumPanel.Width;

                Guna2Button openButton = new Guna2Button();
                openButton.Text = "فتح";
                openButton.Size = new Size(150, 35);
                //openButton.BackColor = Color.FromArgb(55, 41, 21);
                openButton.FillColor = Color.FromArgb(55, 41, 21);
                openButton.BorderThickness = 0;
                openButton.UseTransparentBackground = true;
                openButton.ForeColor = Color.FromArgb(255, 240, 219);
                openButton.BorderRadius = 10;
                openButton.Font = new Font("Mizan AR+LT", 15, FontStyle.Regular);
                //openButton.FlatStyle = FlatStyle.Flat;
                openButton.Location = new Point((curriculumPanel.Width - openButton.Width) / 2, nameLabel.Bottom + 5);

                // إضافة حدث الكلك لفتح المنهج
                openButton.Click += (sender, e) =>
                {
                    try
                    {
                        // الحصول على المسار المخزن في قاعدة البيانات للملف PDF أو المستند الخاص بالمنهج
                        string relativeFilePath = curriculum.URL;

                        // تكوين المسار الكامل بإضافة Application.StartupPath
                        string fullPath = Path.Combine(Application.StartupPath, relativeFilePath);

                        if (File.Exists(fullPath))
                        {
                            System.Diagnostics.Process.Start(fullPath); // فتح الملف باستخدام التطبيق الافتراضي
                        }
                        else
                        {
                            MessageBox.Show("الملف غير موجود في المسار المحدد.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطأ أثناء فتح الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                curriculumPanel.Controls.Add(PictureManhaje);
                curriculumPanel.Controls.Add(nameLabel);
                curriculumPanel.Controls.Add(openButton);
                flowLayoutPanel1.Controls.Add(curriculumPanel);
                xPosition += curriculumPanel.Width + 10;

            }
        }

        private void addSrudent_button_Click(object sender, EventArgs e)
        {
            this.panel2.Controls.Clear();
            addManh AddManh = new addManh() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panel2.Controls.Add(AddManh);
            AddManh.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
