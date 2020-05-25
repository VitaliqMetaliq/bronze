using System;
using System.Threading;

namespace IS_VQ
{
    /// <summary>
    /// Вспомогательный класс для генерации случайных чисел
    /// </summary>
    public static class RandomProvider
    {
        private static int seed = Environment.TickCount;

        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed))
        );

        /// <summary>
        /// Метод потокобезопасного рандома
        /// </summary>
        /// <returns></returns>
        public static Random GetThreadRandom()
        {
            return randomWrapper.Value;
        }
    }
}
