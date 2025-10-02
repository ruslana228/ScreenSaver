namespace screenSaver
{
    /// <summary>
    /// Класс для хранения параметров снежинки
    /// </summary>
    public class Snowflake
    {
        /// <summary>
        /// Координата по оси X
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Координата по оси Y
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Размер снежинки
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Скорость падения снежинки
        /// </summary>
        public int Speed { get; set; }
    }
}
