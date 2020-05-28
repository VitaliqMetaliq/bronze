using System;
using lib;

namespace ds.test.impl
{
    class Program
    {
        /// <summary>
        /// Входная точка приложения
        /// </summary>
        /// <param name="args">Параметры среды</param>
        static void Main(string[] args)
        {
            IPlugin plugin = Plugins.GetPlugin("Sum");
            Console.WriteLine(plugin.Run(1, 2));
            plugin = Plugins.GetPlugin("Div");
            Console.WriteLine(plugin.Run(1, 0));
            Console.WriteLine(Plugins.GetPluginNames[0]);
            plugin = Plugins.GetPlugin("sds");
            Console.WriteLine(plugin.Run(1, 0));
            Console.WriteLine(Plugins.PluginCount);
            Console.ReadKey();
        }
    }
}
