using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SnowfallScreensaver
{
    public partial class Form1 : Form
    {
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private Image snowflakeImage;
        private Random random = new Random();
        private Timer timer;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            // ������� ����� � ��������� ����
            FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            // �������� �������� �����������
            BackgroundImage = Image.FromFile("background.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;

            // ������� ������
            timer = new Timer();
            timer.Interval = 50; // ���������� ������ 50 ��
            timer.Tick += Timer_Tick;
            timer.Start();

            snowflakeImage = Image.FromFile("snowflake.png");

            // ������� ��������� ��������
            CreateSnowflakes(1000);
        }

        // ����� ��� �������� ����� ��������
        private void CreateSnowflakes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(Screen.GetWorkingArea(this).Width);
                int y = random.Next(Screen.GetWorkingArea(this).Height / 4);
                int size = random.Next(10, 30); // ������ �������� �� 10 �� 30 ��������
                int speedx = random.Next(-4, 4);
                int speedy = random.Next(1, 5);
                snowflakes.Add(new Snowflake(x, y, size, speedx, speedy));
            }
        }

        // ����� ��� ����������� ���� ��������
        private void MoveSnowflakes()
        {
            foreach (var flake in snowflakes)
            {
                flake.Y += flake.SpeedY;
                flake.X += flake.SpeedX;

                if (flake.Y > Height + flake.Size)
                {
                    flake.Y = -flake.Size;
                }
                if (flake.X > Width + flake.Size)
                {
                    flake.X = -flake.Size;
                }
            }
        }

        // ����� ��� ��������� ��������
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            foreach (var flake in snowflakes)
            {
                g.DrawImage(snowflakeImage, flake.X, flake.Y, flake.Size, flake.Size);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveSnowflakes(); // ���������� ��������
            Invalidate();     // ����������� ����������� �����
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }

    class Snowflake
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }

        public Snowflake(int x, int y, int size, int speedx, int speedy)
        {
            X = x;
            Y = y;
            Size = size;
            SpeedX = speedx;
            SpeedY = speedy;
        }
    }
}