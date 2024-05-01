using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Euler : Form
    {
        public Euler()
        {
            InitializeComponent();
        }

        Thread th;
        Graphics g, fG;
        Bitmap btm;
    
        bool drawing = true;
    
        float angle = 360f;
        float rad = 250;
        
        Font font = new Font("Century", 18, FontStyle.Bold);

        Pen pen = new Pen(Brushes.Black, 3f);
        Pen pen2 = new Pen(Color.FromArgb(255, 219, 88), 0.5f);
        Pen pen3 = new Pen(Color.FromArgb(102, 153, 51), 6f);
        Pen pen4 = new Pen(Color.FromArgb(153, 51, 153), 6f);

        SolidBrush myBrush_yellow = new SolidBrush(Color.FromArgb(102, 153, 51));
        SolidBrush myBrush_purple = new SolidBrush(Color.FromArgb(153, 51, 153));

        RectangleF area = new RectangleF(50, 50, 500, 500);
        Rectangle circle = new Rectangle(0, 0, 17, 17);

        PointF org = new PointF(250, 250);
        PointF loc = PointF.Empty;
        PointF loc2 = PointF.Empty;
        Point img = new Point(20, 20);

        private void Form1_Load(object sender, EventArgs e)
        {
            btm = new Bitmap(650, 650);
            g = Graphics.FromImage(btm);
            fG = CreateGraphics();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            trackBar1.Enabled = false;
            textBox1.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        }

        private void draw_grid()
        {
            for (int i = 0; i <= 650; i += 25)
                g.DrawLine(pen2, i, 0, i, 650);
            for (int j = 0; j <= 650; j += 25)
                g.DrawLine(pen2, 0, j, 650, j);
            
            g.DrawString("O(0, 0)", font, Brushes.Black, 200, 320);

            g.DrawEllipse(pen, area);

            g.DrawLine(pen, 0, 300, 600, 300);
            g.DrawLine(pen, 1, 290, 1, 310);
            g.DrawLine(pen, 588, 290, 598, 300);
            g.DrawLine(pen, 598, 300, 588, 310);
            g.DrawString("cos(k)", font, myBrush_purple, 560, 320);

            g.DrawLine(pen, 300, 0, 300, 600);
            g.DrawLine(pen, 290, 10, 300, 1);
            g.DrawLine(pen, 300, 1, 310, 10);
            g.DrawLine(pen, 290, 598, 310, 598);
            g.DrawString("sin(k)", font, myBrush_yellow, 315, 0);
        }

        public PointF CirclePoint(float radius, float angleInDegrees, PointF origin)
        {
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F)) + origin.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F)) + origin.Y;

            return new PointF(x, y);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            g.DrawLine(pen3, loc.X + 50, org.Y + 50, loc.X + 50, loc.Y + 50);
            g.DrawLine(pen4, loc.X + 50, org.Y + 50, org.X + 50, org.Y + 50);

            textBox4.ForeColor = Color.FromArgb(102, 153, 51);
            textBox3.ForeColor = Color.FromArgb(153, 51, 153);

            textBox3.Text = Math.Cos((360-angle) * Math.PI / 180F).ToString("0.000");
            textBox4.Text = Math.Sin((360-angle) * Math.PI / 180F).ToString("0.000");
            fG.DrawImage(btm, img);
        }

        public void Draw()
        {
            while (drawing)
            {
                g.Clear(Color.White);
                
                draw_grid();

                loc = CirclePoint(rad, angle, org);

                g.DrawLine(pen, org.X + 50, org.Y + 50, loc.X + 50, loc.Y + 50);

                circle.X = (int)(loc.X - (circle.Width / 2) + area.X);
                circle.Y = (int)(loc.Y - (circle.Height / 2) + area.Y);

                g.DrawEllipse(pen, circle);
                fG.DrawImage(btm, img);

                if (angle > 0)
                    angle += -0.5f;
                else
                    angle = 360;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            draw_grid();
            fG.DrawImage(btm, img);
            angle = 0;
            button2.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            trackBar1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            th = new Thread(Draw);
            th.IsBackground = true;
            th.Start();
            button2.Enabled = false;
            button3.Enabled = true;
            button1.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            trackBar1.Enabled = false;
        }
        private void setAngleTextPosition()
        {
            if (360 - angle < 90)
            {
                loc2.X = loc.X + 70;
                loc2.Y = loc.Y + 15;
            }
            else if (360 - angle < 180)
            {
                loc2.X = loc.X;
                loc2.Y = loc.Y;
            }
            else if (360 - angle < 270)
            {
                loc2.X = loc.X;
                loc2.Y = loc.Y + 75;
            }
            else
            {
                loc2.X = loc.X + 65;
                loc2.Y = loc.Y + 65;
            }
        }

        private void Button_Click_Common(int angle) {
            button4.Enabled = true;
            g.Clear(Color.White);
            draw_grid();
            loc = CirclePoint(rad, angle, org);
            g.DrawLine(pen, org.X + 50, org.Y + 50, loc.X + 50, loc.Y + 50);

            circle.X = (int)(loc.X - (circle.Width / 2) + area.X);
            circle.Y = (int)(loc.Y - (circle.Height / 2) + area.Y);
            g.DrawEllipse(pen, circle);

            setAngleTextPosition();

            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            textBox2.Text = "   e         = cos(" + ((int)(360 - angle)).ToString() + "°) + isin(" +
            ((int)(360 - angle)).ToString() + "°)";

            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Text = ((int)(360 - angle)).ToString() + "°i";
            fG.DrawImage(btm, img);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button_Click_Common(330);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Button_Click_Common(315);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Button_Click_Common(300);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Button_Click_Common(270);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            angle = 360 - trackBar1.Value;
            g.Clear(Color.White);
            draw_grid();
            loc = CirclePoint(rad, angle, org); 
            g.DrawLine(pen, org.X + 50, org.Y + 50, loc.X + 50, loc.Y + 50); 
            circle.X = (int)(loc.X - (circle.Width / 2) + area.X);
            circle.Y = (int)(loc.Y - (circle.Height / 2) + area.Y);
            g.DrawEllipse(pen, circle);

            setAngleTextPosition();

            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            textBox2.Text = "   e         = cos(" + ((int)(360 - angle)).ToString() + "°) + isin(" +
            ((int)(360 - angle)).ToString() + "°)";
            textBox1.TextAlign = HorizontalAlignment.Center;

            textBox1.Text = ((int)(360 - angle)).ToString() + "°i";
            fG.DrawImage(btm, img);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            th.Abort();
            button2.Enabled = true;
            button3.Enabled = false;
            button1.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            trackBar1.Enabled = true;

            g.Clear(Color.White);
            draw_grid();
            loc = CirclePoint(rad, angle, org); 
            g.DrawLine(pen, org.X + 50, org.Y + 50, loc.X + 50, loc.Y + 50);
            circle.X = (int)(loc.X - (circle.Width / 2) + area.X);
            circle.Y = (int)(loc.Y - (circle.Height / 2) + area.Y);
            g.DrawEllipse(pen, circle);
            fG.DrawImage(btm, img);

            setAngleTextPosition();

            g.DrawString(((int)(360 - angle)).ToString() + "°", font, Brushes.Black, loc2.X, loc2.Y);
            textBox2.Text = "   e         = cos(" + ((int)(360 - angle)).ToString() + "°) + isin(" +
            ((int)(360 - angle)).ToString() + "°)";
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Text = ((int)(360 - angle)).ToString() + "°i";
            fG.DrawImage(btm, img);
        }
    }
}
