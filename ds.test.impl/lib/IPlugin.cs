using System.Drawing;

namespace lib
{
    /// <summary>
    /// Интерфейс, описывающий плагин
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Название плагина
        /// </summary>
        string PluginName { get; }
        /// <summary>
        /// Версия плагина
        /// </summary>
        string Version { get; }
        /// <summary>
        /// Изображение
        /// </summary>
        Image Image { get; }
        /// <summary>
        /// Описание плагина
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Метод, выполняющий математическую операцию
        /// </summary>
        /// <param name="input1">Первое число</param>
        /// <param name="input2">Второе число</param>
        /// <returns>Результат математической операции, зависящий от конкретного плагина</returns>
        int Run(int input1, int input2);
    }
}
