namespace screenSaver
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer timer;
        private Image snowflake;
        private Image scene;
        private List<Snowflake> snowflakes;
        private Random random;
        private const int SNOWFLAKE_COUNT = 150; // Количество снежинок

        public MainForm()
        {
            InitializeComponent();

            random = new Random();
            snowflakes = new List<Snowflake>();
            scene = Properties.Resources.village;
            snowflake = Properties.Resources.snowflake;

            // Инициализируем снежинки
            InitializeSnowflakes();

            // Настройка таймера
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 8;
            timer.Start();
        }

        /// <summary>
        /// Метод инициализации снежинки
        /// </summary>
        private void InitializeSnowflakes()
        {
            snowflakes.Clear();

            for (int i = 0; i < SNOWFLAKE_COUNT; i++)
            {
                snowflakes.Add(CreateSnowflake());
            }
        }

        /// <summary>
        /// Метод для создания снежинки
        /// </summary>
        /// <returns></returns>
        private Snowflake CreateSnowflake()
        {
            // Получение текущих размеров клиентской области
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            int size = random.Next(30, 80); // Размер снежинки
            int speed; // Скорость падения снежинок

            // Настраиваем скорость в зависимости от размера
            if (size < 40) // Мелкие снежинки
            {
                speed = random.Next(3, 6);
            }
            else if (size > 60) // Крупные снежинки
            {
                speed = random.Next(7, 10);
            }
            else // Средние снежинки
            {
                speed = random.Next(5, 8);
            }

            int x = random.Next(0, width);
            int y = random.Next(-height * 2, -height / 2); // Появляется выше экрана

            // Создание и возврат снежинки
            return new Snowflake
            {
                X = x,
                Y = y,
                Size = size,
                Speed = speed
            };
        }

        /// <summary>
        /// Обработчик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Получение текущих размеров клиентской области
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            // Двигаем снежинки вниз на велечину их скорости
            for (int i = 0; i < snowflakes.Count; i++)
            {
                var sf = snowflakes[i];

                // Двигаем снежинку вниз
                sf.Y += sf.Speed;

                // Если снежинка упала за нижнюю границу
                if (sf.Y > height)
                {
                    snowflakes[i] = CreateSnowflake();
                    snowflakes[i].Y = -snowflakes[i].Size; // Чтобы снежинка появилась выше экрана
                }
            }

            this.Invalidate(); // Перерисовка формы
        }

        /// <summary>
        /// Отрисовка фона и снежинки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; // Получение объекта для рисования

            // Отрисовка фона на всю форму
            g.DrawImage(scene, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            // Отрисовка снежинок в текущей позиции с текущим размером
            foreach (var sf in snowflakes)
            {
                g.DrawImage(snowflake, sf.X, sf.Y, sf.Size, sf.Size);
            }

        }

        /// <summary>
        /// Обработка закрытия формы - закрытие формы при нажатии на любую клавишу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработка закрытия формы - закрытие формы при нажатии на любую кнопку мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработка изменения размеров формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (snowflakes != null)
            {
                InitializeSnowflakes();
                this.Invalidate();
            }
        }
    }
}