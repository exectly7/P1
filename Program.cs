using static System.ConsoleKey;

namespace P1
{
    /// <summary>
    /// Основной класс, в котором содержится метод мейн, который является точкой входа в программу
    /// </summary>
    internal static class Program
    {   
         /// <summary>
         /// Точка входа в программу, реализует основную логику выполнения
         /// </summary>
        private static void Main()
        {
            Console.WriteLine("Эта программа получает на вход 4 коэффициента и файл input с массивами и по формуле вычисляет значения для файла output");
            Methods.ReadAllCoefficients(out double[] coefficients);
            do
            {
                Methods.ClearOutputFiles();
                if (!Methods.CheckInputFile())
                {
                    continue;
                }

                if (!Methods.ReadInputData(out double[][] arrays))
                {
                    continue;
                }
                if (!Methods.CreateResultArray(coefficients, arrays, out double[][] arrayResult))
                {
                    continue;
                }

                if (!Methods.CreateOutputFiles(arrayResult))
                {
                    continue;
                }
                Console.WriteLine("Данные успешно записаны");
                Console.WriteLine("Для выхода нажмите Esc, если хотите продолжить - нажмите любую клавишу");
            } while (Console.ReadKey().Key != Escape);
        }
    }
}

