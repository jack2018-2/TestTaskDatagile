using System;
using System.Collections.Generic;
using System.Linq;

namespace Fibbonachi
{
    /// <summary>
    /// Реализация ряда Фиббоначи
    /// </summary>
    public static class FibbonachiRow
    {
        /// <summary>
        /// Первый член последовательности Фиббоначи
        /// </summary>
        private static long _zeroFibbonachiRowNumber = 0;

        /// <summary>
        /// Второй член последовательности Фиббоначи
        /// </summary>
        private static long _firstFibbonachiRowNumber = 1;

        /// <summary>
        /// Получить ряд Фиббоначи
        /// </summary>
        /// <param name="givenNumber">Заданное число</param>
        /// <param name="count">Кол-во чисел в ряду</param>
        /// <returns><see cref="IEnumerable{long}"/> ряд</returns>
        public static IEnumerable<long> GetRow(int givenNumber, int count)
        {
            var (zeroRowNumber, firstRowNumber) = GetFirstPairOfRow(givenNumber);
            return Fib(new List<long>() { zeroRowNumber, firstRowNumber }, (_, curCount) => curCount >= count);
        }

        /// <summary>
        /// Получение первой пары чисел ряда
        /// </summary>
        /// <param name="givenNumber">Заданное число</param>
        /// <returns></returns>
        private static (long, long) GetFirstPairOfRow(int givenNumber)
        {
            var row = Fib(new List<long>() { _zeroFibbonachiRowNumber, _firstFibbonachiRowNumber }, (x, _) => x > givenNumber);
            return (row[^2], row.Last());
        }

        /// <summary>
        /// Получение ряда фиббоначи
        /// </summary>
        /// <param name="row">Начальное состояние ряда, должно содержать не менее двух элементов</param>
        /// <param name="stopCondition">Условие окончания вычисления ряда</param>
        /// <returns>Ряд Фиббоначи по заданному условию</returns>
        private static List<long> Fib(List<long> row, Func<long, int, bool> stopCondition)
        {
            if (row.Count < 2)
            {
                throw new ArgumentException("Начальная цепочка для ряда Фиббоначи имела менее двух элементов. КАК?");
            }

            if (stopCondition(row[^2], row.Count))
            {
                return row;
            }
            else
            {
                row.Add(row.Last() + row[^2]);
                return Fib(row, stopCondition);
            }
        }
    }
}
