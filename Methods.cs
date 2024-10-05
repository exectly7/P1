using System.Globalization;

namespace P1
{
    public static class Methods
    {
        /// <summary>
        /// Метод считывает коэффициенты a, b, c, d
        /// Проверяет корректность ввода
        /// </summary>
        /// <param name="coefficients">Возвращает массив коэффициентов, нужных для дальнейшей работы программы</param>
        public static void ReadAllCoefficients(out double[] coefficients)
        {
            Console.WriteLine("Сейчас Вам нужно будет ввести значения коэффициентов a, b, c, d, которые лежат в интервале (0, 1):");
            
            //Массив нужен для понимания пользователем, какой именно коэф. вводится
            char[] coefficientsNames =  ['a', 'b', 'c', 'd'];
            coefficients = new double[4];
            
            //Цикл проходит по каждому элементу возвращаемого массива, заполняя его
            for (int i = 0; i < coefficientsNames.Length; i++)
            {
                Console.WriteLine($"Введите коэффициент {coefficientsNames[i]}: ");
                while (true)
                {
                    try
                    {
                        coefficients[i] = double.Parse(Console.ReadLine()?.Replace('.', ',') ??
                                                       throw new InvalidOperationException());
                        if (coefficients[i] <= 0 || coefficients[i] >= 1)
                        {
                            Console.WriteLine("Значение коэффицинта не входит в интервал (0, 1), попробуйте ещё раз: ");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Коэффициент введён неверно, попробуйте ещё раз: ");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Коэффициент введён неверно, попробуйте ещё раз: ");
                    }
                }
            }
        }
        
        /// <summary>
        /// Метод проверяет существование и возможность чтения файла input.txt
        /// При возникновении проблем метод выдает ошибку в консоль
        /// </summary>
        /// <returns>возвращает true, если файл существует и с ним нет проблем, иначе false</returns>
        public static bool CheckInputFile()
        {
            if (!File.Exists("input.txt"))
            {
                Console.WriteLine("Входной файл на диске отсутсвует");
                return false;
            }

            try
            {
                File.ReadLines("input.txt");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Проблемы с открытием файла");
            }
            catch (IOException)
            {
                Console.WriteLine($"Проблема c чтением данных из файла");
            }

            return false;
        }

        /// <summary>
        /// Метод считывает числовую информацию из файла, при помощи метода ConvertData преобразует ее и записывает в массив данных
        /// </summary>
        /// <param name="arrays">Массив со всеми данными из файла input</param>
        /// <returns>Возвращает true, если пользователь хочет продолжить выполнение программы с некорректными данными, иначе false</returns>
        public static bool ReadInputData(out double[][] arrays)
        {
            string[] input = File.ReadAllLines("input.txt");
            //Проверка на коректное количество строк
            if (input.Length % 2 != 0)
            {
                Console.WriteLine(
                    "Количество строк неккоректно, нажмите enter, если хотите продолжить выполнение программы (последняя строка не будет учтена):");
                ConsoleKeyInfo continueIndicator = Console.ReadKey();
                if (continueIndicator.Key != ConsoleKey.Enter)
                {
                    arrays = [];
                    return false;
                }
            }
            //Заполняем выходной массив данными при помощи метода ConvertData
            arrays = new double[input.Length / 2 * 2][];
            for (int i = 0; i < input.Length / 2; i++)
            {
                ConvertData(input[i * 2], input[(i * 2) + 1], out double[] arrayX, out double[] arrayY);
                arrays[i * 2] = arrayX;
                arrays[(i * 2) + 1] = arrayY;
            }

            return true;
        }

        /// <summary>
        /// Метод преобразует строки файла в массивы чисел double
        /// </summary>
        /// <param name="input1">Входная строка 1</param>
        /// <param name="input2">Входная строка 2</param>
        /// <param name="arrayX">Выходной массив из 1 строки</param>
        /// <param name="arrayY">Выходной массив из 2 строки</param>
        private static void ConvertData(string input1, string input2, out double[] arrayX, out double[] arrayY)
        {
            string[] formattedInput1 = input1.Split(' ');
            string[] formattedInput2 = input2.Split(' ');
            int correctValues1 = 0;
            int correctValues2 = 0;
            
            //Cчитаем количество корректных данных в каждой строке
            foreach (string t in formattedInput1)
            {
                if (double.TryParse(t, out double _))
                {
                    correctValues1++;
                }
            }

            foreach (string t in formattedInput2)
            {
                if (double.TryParse(t, out double _))
                {
                    correctValues2++;
                }
            }
            
            //Если количество корректных данных не совпадает, заполняем массивы элементами с нулем (по условию варианта)
            if (correctValues1 != correctValues2)
            {
                arrayX = [0.0];
                arrayY = [0.0];
            }
            else
            {
                //Заполняем массивы корректными данными
                arrayX = new double[correctValues1];
                arrayY = new double[correctValues1];
                int counter1 = 0;
                for (int i = 0; i < formattedInput1.Length; i++)
                {
                    if (double.TryParse(formattedInput1[i], out double x))
                    {
                        arrayX[counter1] = x;
                        counter1++;
                    }
                }

                int counter2 = 0; 
                for (int i = 0; i < formattedInput2.Length; i++)
                {
                    if (double.TryParse(formattedInput2[i], out double x))
                    {
                        arrayY[counter2] = x;
                        counter2++;
                    }
                }
            }
        }

        /// <summary>
        /// Вычисляет итоговый массив по формуле индивидуального варианта
        /// </summary>
        /// <param name="coefficients">Введеные раннее коэффициенты abcd в массиве double</param>
        /// <param name="arrays">Массив строк даных input</param>
        /// <param name="arrayResult">Результирующий массив чисел double</param>
        /// <returns>true - если преобразование удалось или пользователя устраивает деление на 0, иначе false</returns>
        public static bool CreateResultArray(double[] coefficients, double[][] arrays, out double[][] arrayResult)
        {
            int numberOfPairs = arrays.Length / 2;
            arrayResult = new double[numberOfPairs][];
    
            // Цикл проходит по парам строк данных
            for (int i = 0; i < numberOfPairs; i++)
            {
                int lengthX = arrays[i * 2].Length;

                arrayResult[i] = new double[lengthX];  // Создаем результирующий массив для этой пары

                // Цикл по каждому элементу массива
                for (int j = 0; j < lengthX; j++)
                {
                    try
                    {
                        arrayResult[i][j] = ((coefficients[0] * arrays[i * 2][j]) + coefficients[1]) /
                                            ((arrays[(i * 2) + 1][j] * coefficients[2]) + coefficients[3]);
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Деление на 0, устанавливаем результат в 0.");
                        arrayResult[i][j] = 0;  // Если деление на 0, устанавливаем результат в 0
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Переполнение при вычислениях, устанавливаем результат в 0.");
                        arrayResult[i][j] = 0;  // Если переполнение, также устанавливаем результат в 0
                    }
                }
            }

            return true;
        }
        

        /// <summary>
        /// Создает выходные файлы output и config
        /// </summary>
        /// <param name="arrayResult">Массив итоговых данных, которые нужно записать в выходные файлы</param>
        /// <returns>true - если запись удалась, иначе false</returns>
        public static bool CreateOutputFiles(double[][] arrayResult)
        {
            try
            {
                File.Delete("config.txt");
                File.WriteAllText("config.txt", "");
                for (int i = 0; i < arrayResult.Length; i++)
                {
                    string[] lines = Array.ConvertAll(arrayResult[i], x => x.ToString(CultureInfo.CurrentCulture));
                    File.Delete($"output-{i + 1}.txt");
                    File.WriteAllLines($"output-{i + 1}.txt", lines);
                    File.AppendAllText("config.txt", $"Создан файл output-{i + 1}.txt");
                }
                return true;
            }
            catch
            {
                Console.WriteLine("Проблемы с записью данных в файл");
                return false;
            }
        }
    }
}