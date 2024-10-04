namespace P1
{
    internal static class Program
    {/// <summary>
     /// уэээээээээ
     /// </summary>
        private static void Main()
        {
            Console.WriteLine("Эта программа получает на вход 4 коэффициента и файл input с массивами и по формуле вычисляет значения для файла output");
            ConsoleKeyInfo exitKey;
            Methods.ReadAllCoefficients(out double[] coefficients);
            do
            {
                if (Methods.CheckInputFile())
                {
                    if (Methods.ReadInputData(out double[][] arrays))
                    {
                        if (Methods.CreateResultArray(coefficients, arrays, out double[][] arrayResult))
                        {
                            if (Methods.CreateOutputFiles(arrayResult))
                            {
                                
                            }
                        }
                    }
                }
                Console.WriteLine("Для выхода нажмите Esc, если хотите продолжить - нажмите любую клавишу");
                exitKey = Console.ReadKey();
            } while (exitKey.Key != ConsoleKey.Escape);
        }
    }
}

