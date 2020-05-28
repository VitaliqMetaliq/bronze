using System.Collections.Generic;

namespace lib
{
    /// <summary>
    /// Статический класс Плагинов
    /// </summary>
    public static class Plugins 
    {
        /// <summary>
        /// Количество доступных плагинов
        /// </summary>
        public static int PluginCount { get => PluginsList.Count; }
        /// <summary>
        /// Массив наименований доступных плагинов
        /// </summary>
        public static string[] GetPluginNames { get; }
        /// <summary>
        /// Коллекция доступных плагинов
        /// </summary>
        static List<IPlugin> PluginsList;

        /// <summary>
        /// Статический конструктов, вызывается один раз при инициализации приложения
        /// </summary>
        static Plugins()
        { 
            PluginsList = new List<IPlugin>();
            Fill();
            GetPluginNames = new string[PluginCount];
            for (int i = 0; i < PluginCount; i++)
            {
                GetPluginNames[i] = PluginsList[i].PluginName;
            }
        }
        /// <summary>
        /// Метод заполнения коллекции доступными плагинами (hardcode)
        /// </summary>
        private static void Fill()
        {
            PluginsList.Add(new SomePlugin("Sum", "1.0", "custom_path", "Plugin for additional"));
            PluginsList.Add(new SomePlugin("Sub", "1.1", "custom_path", "Plugin for subtraction"));
            PluginsList.Add(new SomePlugin("Mul", "1.2", "custom_path", "Plugin for multiplication"));
            PluginsList.Add(new SomePlugin("Div", "1.3", "custom_path", "Plugin for division"));
           
        }

        /// <summary>
        /// Метод, возвращающий плагин по его названию
        /// </summary>
        /// <param name="pluginName">Название плагина</param>
        /// <returns>Плагин математической операции</returns>
        public static IPlugin GetPlugin(string pluginName)
        {
            int i;
            bool flag = false;
            for(i=0; i<PluginCount; i++)
            {
                if (PluginsList[i].PluginName == pluginName)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag) return new SomePlugin("Undefined", "Undefined", "Undefined", "Undefined");
            else return PluginsList[i];
        }
    }
}
