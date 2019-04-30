using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace OpenCVTest
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Init()
        {
            Bitmap bitmapImage = new Bitmap(Application.StartupPath + @"\dogs2.jpg");
            pictureBox1.Image = bitmapImage;
            System.Diagnostics.Debug.WriteLine("메시지 "+Application.StartupPath);
        }

        private void Find_Click(object sender, EventArgs e)
        {
            TemplateMatching();
        }

        private void TemplateMatching() // 템플릿 매칭 : 동일한 크기와 방향을 가진 똑같은 이미지를 가지고 검색할 때 이용. 서로 다른 이미지 유사도 확인 어려움
        {
            Mat screen = null, find = null, res = null;
            try
            {
                screen = BitmapConverter.ToMat(new Bitmap(Application.StartupPath + @"\dogs2.jpg"));
                find = BitmapConverter.ToMat(new Bitmap(Application.StartupPath + @"\dog.jpg"));

                //이미지 유사도 찾기

                res = screen.MatchTemplate(find, TemplateMatchModes.CCoeffNormed);

                double min, max = 0;
                Point minLoc, maxLoc;

                Cv2.MinMaxLoc(res, out min, out max, out minLoc, out maxLoc);
                Cv2.Rectangle(screen, maxLoc, new Point(maxLoc.X + find.Width, maxLoc.Y + find.Height), Scalar.Aqua, 3);
                pictureBox1.Image = BitmapConverter.ToBitmap(screen);

                lb_result_text.Text = max.ToString();
            }
            catch (Exception ex)
            {
                lb_result_text.Text = ex.Message.ToString();
            }
            finally
            {
                screen.Dispose();
                find.Dispose();
                res.Dispose();
            }
        }

    }
}