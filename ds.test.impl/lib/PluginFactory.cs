using System;
using System.Drawing;

namespace lib
{
    /// <summary>
    /// Абстрактный класс с методом по умолчанию
    /// </summary>
    abstract class AbsPlug
    {
        /// <summary>
        /// Метод для переопределения
        /// </summary>
        /// <param name="input1">Первое число</param>
        /// <param name="input2">Второе число</param>
        /// <returns>Результат математической операции</returns>
        public virtual int Run(int input1, int input2)
        {
            return 0;
        }
    }

    class SomePlugin : AbsPlug , IPlugin
    {
        public string PluginName { get; }
        public string Version { get; }
        public Image Image { get; }
        public string Description { get; }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="ver">Версия</param>
        /// <param name="path">Путь к файлу изображения (может быть любым, стоит заглушка)</param>
        /// <param name="descr">Описание</param>
        public SomePlugin(string name, string ver, string path, string descr)
        {
            this.PluginName = name;
            this.Version = ver;
            this.Image = path == "Undefined" ? null : new Bitmap(10, 10);
            this.Description = descr;
        }

        /// <summary>
        /// Переопределенный метод выполнения математической операции
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns>Возвращает операцию, зависящую от входного названия плагина</returns>
        public override int Run(int a, int b)
        {
            switch (PluginName)
            {
                case "Sum": return a + b;
                case "Sub": return a - b;
                case "Mul": return a * b;
                case "Div":
                    if (b != 0) return a / b;
                    else
                    {
                        Console.WriteLine("Cant divide by zero");
                        return 0;
                    }
                default:
                    Console.WriteLine("Unknown operation");
                    return 0;
            }
        }
    }
}
