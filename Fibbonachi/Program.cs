using System;
using System.Linq;

namespace Fibbonachi
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstNumber;
            int count;
            if (args.Length == 2)
            {
                if (!int.TryParse(args[0], out firstNumber) || !int.TryParse(args[1], out count))
                {
                    Console.WriteLine("Не удалось прочитать аргументы, требуется ручной ввод:");
                    (firstNumber, count) = GetUserInput();
                }
            }
            else
            {
                Console.WriteLine("Неверное кол-во аргументов или аргументы отсутствуют, требуется ручной ввод:");
                (firstNumber, count) = GetUserInput();
            }

            try
            {
                Console.WriteLine(FibbonachiRow.GetRow(firstNumber, count)
                                               .Select(x => x.ToString())
                                               .Take(count) // на случай count = 1, например
                                               .Aggregate((elem, str) => elem + "," + str)
                                               );
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Неизвестная ошибка!");
            }
            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }

        /// <summary>
        /// Функция для получения пользовательского ввода
        /// </summary>
        /// <returns>Пара чисел</returns>
        private static (int firstNumber, int count) GetUserInput()
        {
            int firstNumber = 0;
            int count = 0;
            var isValid = false;
            while (!isValid)
            {
                Console.Write("Первое число последовательности:\n>");
                var firstNumberStr = Console.ReadLine();
                Console.Write("Длина последовательности:\n>");
                var countStr = Console.ReadLine();
                if (int.TryParse(firstNumberStr, out firstNumber) && int.TryParse(countStr, out count))
                {
                    if (firstNumber > 0 && firstNumber > 0)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Аргументы должны быть больше 0!");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректные аргументы!");
                }
            }
            return (firstNumber, count);
        }
    }
}
