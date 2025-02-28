using System;
using System.IO;
namespace ConsoleApplication1
{
    class Program
    {
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

        static void Main()
        {
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
                fout = new BinaryWriter(new FileStream("binary.dat", FileMode.Append));
                fout.Write(nearestNumber);
                fout.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error end: " + e.Message);
                return;
            }
        }
    }
}
