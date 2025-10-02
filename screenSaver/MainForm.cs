namespace screenSaver
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer timer;
        private Image snowflake;
        private Image scene;
        private List<Snowflake> snowflakes;
        private Random random;
        private const int SNOWFLAKE_COUNT = 150; 

        public MainForm()
        {
            InitializeComponent();

            random = new Random();
            snowflakes = new List<Snowflake>();
            scene = Properties.Resources.village;
            snowflake = Properties.Resources.snowflake;

            InitializeSnowflakes();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 8;
            timer.Start();
        }

        private void InitializeSnowflakes()
        {
            snowflakes.Clear();

            for (int i = 0; i < SNOWFLAKE_COUNT; i++)
            {
                snowflakes.Add(CreateSnowflake());
            }
        }

        private Snowflake CreateSnowflake()
        {
            var width = this.ClientSize.Width;
            var height = this.ClientSize.Height;

            var size = random.Next(30, 80); 
            int speed;

            if (size < 40) 
            {
                speed = random.Next(3, 6);
            }
            else if (size > 60) 
            {
                speed = random.Next(7, 10);
            }
            else 
            {
                speed = random.Next(5, 8);
            }

            var x = random.Next(0, width);
            var y = random.Next(-height * 2, -height / 2); 

            return new Snowflake
            {
                X = x,
                Y = y,
                Size = size,
                Speed = speed
            };
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var width = this.ClientSize.Width;
            var height = this.ClientSize.Height;

            for (int i = 0; i < snowflakes.Count; i++)
            {
                var sf = snowflakes[i];

                sf.Y += sf.Speed;

                if (sf.Y > height)
                {
                    snowflakes[i] = CreateSnowflake();
                    snowflakes[i].Y = -snowflakes[i].Size; 
                }
            }

            this.Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(scene, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            foreach (var sf in snowflakes)
            {
                g.DrawImage(snowflake, sf.X, sf.Y, sf.Size, sf.Size);
            }

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

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