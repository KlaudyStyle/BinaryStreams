using System;
using System.IO;
using System.Collections.Generic;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
            /*Занести в файл 10 действительных чисел. Среди компонентов файла найти число, которое ближе
            всего к действительному числу х и дописать в конец данного файла это число.*/
            String readX = Console.ReadLine();
            double X = -1;
            if(!Double.TryParse(readX, out X))
            {
                Console.WriteLine("X не было введено! Или введено с ошибкой");
                return;
            }

            try
            {
                BinaryWriter fout = new BinaryWriter(new FileStream("binary.dat", FileMode.Create));
                int i = 1;
                double[] numbers = new double[11];
                Random random = new Random();
                while (i < numbers.Length)
                {
                    numbers[i] = random.NextDouble() * 100;
                    fout.Write(numbers[i]);
                    Console.WriteLine(numbers[i]);
                    i++;
                }
                fout.Close();
                Console.WriteLine();

                double[] newNumbers = new double[11];
                FileStream f = new FileStream("binary.dat", FileMode.Open);
                BinaryReader fin = new BinaryReader(f);
                int i1 = 1;
                try
                {
                    while (true)
                    {
                        newNumbers[i1] = fin.ReadDouble();
                        Console.WriteLine(newNumbers[i1]);
                        i1++;
                    }
                } catch (EndOfStreamException e) { }
                f.Close();
                fin.Close();

                double nearestNumber = getNearestNumber(newNumbers, X);
                Console.WriteLine("Ближайшее число - " + nearestNumber);
                Console.WriteLine("Число записано в конец файла. ");
                fout = new BinaryWriter(new FileStream("binary.dat", FileMode.Append));
                fout.Write(nearestNumber);
                fout.Close();

                /*ЗАДАНИЕ ДЛЯ САМОСТОЯТЕЛЬНОЙ РАБОТЫ СТУДЕНТОВ
                Обеспечьте перенаправление ввода на файл F, а вывода на файл G, и решите следующую
                задачу: в файле F хранятся средние температуры за каждый месяц года. В файле G для каждого
                месяца сохраните отклонение среднемесячной температуры от среднегодовой.*/
                Console.WriteLine("\nСледующее задание: \n");
                zd();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error end: " + e.Message);
                return;
            }
            Console.ReadKey();
        }

        public static void zd()
        {
            double[] monthlyTemperatures = {
                3.5, 4.2, 8.0, 13.5, 17.0, 20.1,
                22.5, 21.6, 17.5, 12.0, 7.5, 4.0
            };

            StreamWriter writer = new StreamWriter("F.txt");
            foreach (double temp in monthlyTemperatures)
            {
                Console.WriteLine(temp);
                writer.WriteLine(temp);
            }
            writer.Close();

            // назначение источника данных для ввода, вместо клавиатуры
            StreamReader inFile = new StreamReader("F.txt");
            Console.SetIn(inFile);
            // назначение приемника данных, вместо экрана
            StreamWriter outFile = new StreamWriter("G.txt");
            Console.SetOut(outFile);

            List<double> temperatures = new List<double>();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (double.TryParse(line, out double temp))
                {
                    temperatures.Add(temp);
                }
            }
            double sum = 0;
            foreach (double temp in temperatures)
            {
                sum += temp;
            }
            double annualAverage = sum / temperatures.Count;
            for (int i = 0; i < temperatures.Count; i++)
            {
                double temp = temperatures[i] - annualAverage;
                Console.WriteLine("Месяц {0:00}. Отклонение: {1:F2}", i + 1, temp);
            }
            inFile.Close();
            outFile.Close();
        }

        public static double getNearestNumber(double[] numbers, double x)
        {
            if (numbers == null || numbers.Length == 0)
            {
                throw new ArgumentException("Массив numbers не должен быть пустым.");
            }

            double closestNumber = numbers[0];
            double closestDistance = Math.Abs(numbers[0] - x);

            for (int i = 1; i < numbers.Length; i++)
            {
                double currentDistance = Math.Abs(numbers[i] - x);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestNumber = numbers[i];
                }
            }
            return closestNumber;
        }
    }
}
