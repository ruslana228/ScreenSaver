namespace screenSaver
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer timer;
        private Image snowflake;
        private Image scene;
        private List<Snowflake> snowflakes;
        private Random random;

        /// <summary>
        /// Количество снежинок
        /// </summary>
        private const int SNOWFLAKE_COUNT = 150;

        /// <summary>
        /// Интервал срабатывания таймера
        /// </summary>
        private const int INTERVAL_TIMER = 8;

        /// <summary>
        /// Маленький размер снежинок
        /// </summary>
        private const int SMALLSIZE = 40;

        /// <summary>
        /// Большой размер снежинок
        /// </summary>
        private const int BIGSIZE = 60;

        public MainForm()
        {
            InitializeComponent();

            random = new Random();
            snowflakes = new List<Snowflake>();
            scene = Properties.Resources.village;
            snowflake = Properties.Resources.snowflake;

            // Инициализация снежинок
            InitializeSnowflakes();

            // Настройка таймера
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = INTERVAL_TIMER;
            timer.Start();
        }

        /// <summary>
        /// Метод инициализации снежинок
        /// </summary>
        private void InitializeSnowflakes()
        {
            // Получение текущих размеров клиентской области
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;

            snowflakes.Clear();

            for (int i = 0; i < SNOWFLAKE_COUNT; i++)
            {
                snowflakes.Add(CreateSnowflake(width, height));
            }
        }

        /// <summary>
        /// Метод для создания снежинок
        /// </summary>
        private Snowflake CreateSnowflake(int width, int height)
        {
            var size = random.Next(30, 80); // Размер снежинки 

            // Скорость падения снежинки
            var speed = size switch
            {
                < SMALLSIZE => random.Next(3, 6),
                > BIGSIZE => random.Next(7, 10),
                _ => random.Next(5, 8)
            };

            var x = random.Next(0, width);
            var y = random.Next(-height * 2, -height / 2); // Появляется выше экрана

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
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Получение текущих размеров клиентской области
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;

            // Движение снежинок вниз на величину их скорости
            foreach (var sf in snowflakes)
            {
                sf.Y += sf.Speed; // Движение вниз

                // Если снежинка упала за нижнюю границу формы
                if (sf.Y > height)
                {
                    // Обновляем свойства существующей снежинки
                    var newSnowflake = CreateSnowflake(width, height);
                    sf.X = newSnowflake.X;
                    sf.Y = -newSnowflake.Size; // Появляется выше экрана
                    sf.Size = newSnowflake.Size;
                    sf.Speed = newSnowflake.Speed;
                }
            }

            this.Invalidate(); // Перерисовка формы
        }

        /// <summary>
        /// Отрисовка фона и снежинок
        /// </summary>
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
        /// Обработка нажатия клавиш - закрытие формы при нажатии на любую клавишу
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработка нажатия кнопок мыши - закрытие формы при нажатии на любую кнопку мыши
        /// </summary>
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработка изменения размеров формы
        /// </summary>
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            InitializeSnowflakes();
            this.Invalidate();
        }
    }
}