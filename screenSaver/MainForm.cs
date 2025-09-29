namespace screenSaver
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer timer;
        private Image snowflake;
        private Image scene;
        private Snowflake snowflakes;
        private Random random;

        public MainForm()
        {
            InitializeComponent();

            random = new Random();

            scene = Properties.Resources.village;
            snowflake = Properties.Resources.snowflake;

            // Инициализируем снежинки
            InitializeSnowflakes();

            // Настраиваем таймер
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 10;
            timer.Start();
        }

        /// <summary>
        /// Метод инициализации снежинки
        /// </summary>
        private void InitializeSnowflakes()
        {
            snowflakes = CreateSnowflake();
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
            int speed = random.Next(6, 9); // Скорость падения снежинки

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

            // Двигаем снежинку вниз на велечину ее скорости
            snowflakes.Y += snowflakes.Speed;

            // Если снежинка упала за нижнюю границу, то создаем новую
            if (snowflakes.Y > height)
            {
                snowflakes = CreateSnowflake();
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

            // Отрисовка снежинки в текущей позиции с текущим размером
            g.DrawImage(snowflake, snowflakes.X, snowflakes.Y, snowflakes.Size, snowflakes.Size);

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
        /// Обработка изменения размера формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (snowflakes != null)
            {
                InitializeSnowflakes();
                this.Invalidate();
            }
        }
    }
}