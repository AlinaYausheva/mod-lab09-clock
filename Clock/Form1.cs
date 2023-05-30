using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsState gs;
            gs = g.Save();

            int w = ClientSize.Width;
            int h = ClientSize.Height;
            g.TranslateTransform(w / 2, h / 2);
            int radius = (int)(Math.Min(w / 2, h / 2) - Math.Min(w / 2, h / 2) * 0.05);
            int fontSize = (int)(radius * 0.1);

            Pen thinPen = new Pen(Color.Black, 2);
            Pen thickPen = new Pen(Color.Black, 4);
            SolidBrush brush = new SolidBrush(Color.FromArgb(160, 82, 45));
            g.FillEllipse(brush, -radius, -radius, 2 * radius, 2 * radius);
            brush = new SolidBrush(Color.FromArgb(244, 164, 96));
            radius = (int)(radius - radius * 0.1);
            g.FillEllipse(brush, -radius, -radius, 2 * radius, 2 * radius);
            g.DrawEllipse(thickPen, -radius, -radius, 2 * radius, 2 * radius);
            brush = new SolidBrush(Color.Black);
            g.FillEllipse(brush, -5, -5, 10, 10);

            g.TranslateTransform(w / 2, h / 2);
            for (int i = 0; i <= 360; i += 6)
            {                
                if (i % 30 == 0)
                {
                    double angle = (i - 90) * Math.PI / 180;
                    int x = (int)((radius - radius * 0.1 * 2) * Math.Cos(angle) - fontSize * 0.7);
                    int y = (int)((radius - radius * 0.1 * 2) * Math.Sin(angle) - fontSize * 0.7);
                    g.TranslateTransform(w / 2, h / 2);
                    g.DrawString((i / 30).ToString(), new Font("Magneto", fontSize), Brushes.Black, x, y);
                    g.RotateTransform(i);
                    g.DrawLine(thickPen, 0, -radius, 0, (int)(-radius * 0.9));
                    g.Restore(gs);
                }
                else
                {
                    g.TranslateTransform(w / 2, h / 2);
                    g.RotateTransform(i);
                    g.DrawLine(thinPen, 0, -radius, 0, (int)(-radius * 0.94));
                    g.Restore(gs);
                }
                gs = g.Save();
            }
           
            g.TranslateTransform(w / 2, h / 2);

            DateTime dt = DateTime.Now;
            Point[] points = new Point[4];

            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour + dt.Minute / 60 + dt.Second / 3600));
            points[0] = new Point(0, 0);
            points[1] = new Point(-(int)(radius * 0.06), -(int)(radius * 0.2));
            points[2] = new Point(0, -radius + 60);
            points[3] = new Point((int)(radius * 0.06), -(int)(radius * 0.2));
            g.FillPolygon(Brushes.Black, points);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + dt.Second / 60));
            points = new Point[4];
            points[0] = new Point(0, 0);
            points[1] = new Point(-(int)(radius * 0.05), -(int)(radius*0.2));
            points[2] = new Point(0, -radius + 20);
            points[3] = new Point((int)(radius * 0.05), -(int)(radius * 0.2));
            g.FillPolygon(Brushes.Black, points);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            Pen pen = new Pen(Color.Red, 1);
            g.DrawLine(pen, 0, (int)(radius*0.2), 0, -radius + 10);
            g.Restore(gs);
        }
    }
}