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

            // Убираем рамку и заголовок окна
            FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            // Загрузка фонового изображения
            BackgroundImage = Image.FromFile("background.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;

            // Создаем таймер
            timer = new Timer();
            timer.Interval = 50; // Обновление каждые 50 мс
            timer.Tick += Timer_Tick;
            timer.Start();

            snowflakeImage = Image.FromFile("snowflake.png");

            // Создаем начальные снежинки
            CreateSnowflakes(1000);
        }

        // Метод для создания новых снежинок
        private void CreateSnowflakes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(Screen.GetWorkingArea(this).Width);
                int y = random.Next(Screen.GetWorkingArea(this).Height / 4);
                int size = random.Next(10, 30); // Размер снежинок от 10 до 30 пикселей
                int speedx = random.Next(-4, 4);
                int speedy = random.Next(1, 5);
                snowflakes.Add(new Snowflake(x, y, size, speedx, speedy));
            }
        }

        // Метод для перемещения всех снежинок
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

        // Метод для отрисовки снежинок
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
            MoveSnowflakes(); // Перемещаем снежинки
            Invalidate();     // Запрашиваем перерисовку формы
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