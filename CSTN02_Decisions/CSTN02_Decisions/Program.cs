using System;

namespace CSTN02_Decisions
{
    class Program
    {
        static void Main(string[] args)
        {
            //int x = 8;
            //int y = 8;
            //if (x > y)
            //{
            //    Console.WriteLine($"Subtraction: {x - y}");
            //}
            //else if (x == y)
            //{
            //    Console.WriteLine($"{x} = {y}");
            //}
            //else
            //{
            //    Console.WriteLine($"Addition: {x + y}");
            //}

            //int input = -5;
            //string classify = (input > 0) ? "positive" : "negative";
            //Console.WriteLine(classify);

            //Console.WriteLine("Please enter a number x and a number Y");
            //Console.Write("x = ");
            //int x = int.Parse(Console.ReadLine());
            //Console.Write("y = ");
            //int y = Convert.ToInt32(Console.ReadLine());

            //if (x > y)
            //{
            //    Console.WriteLine($"{x} > {y}");
            //}
            //else if (x == y)
            //{
            //    Console.WriteLine($"{x} = {y}");
            //}
            //else
            //{
            //    Console.WriteLine($"{x} < {y}");
            //}

            //Console.WriteLine("Please enter a number between 1 and 3");
            //int x = int.Parse(Console.ReadLine());
            //switch(x)
            //{
            //    case 1:
            //        Console.WriteLine("One");
            //        break;
            //    case 2:
            //        Console.WriteLine("Two");
            //        break;
            //    case 3:
            //        Console.WriteLine("Three");
            //        break;
            //    default:
            //        Console.WriteLine("Wrong number");
            //        break;
            //}

            Console.WriteLine("Please enter three numbers. We'll tell you which one is the greatest!");
            Console.Write("Number 1: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("NUmber 2: ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.Write("NUmber 3: ");
            int c = int.Parse(Console.ReadLine());

            // a, b, c
            if (a > b)
            {
                if (a > c)
                {
                    Console.WriteLine($"{a} is the greatest number");
                }
                else
                {
                    Console.WriteLine($"{c} is the greatest number");
                }
            }
            else if (b > c)
            {
                Console.WriteLine($"{b} is the highest number");
            }
            else
            {
                Console.WriteLine($"{c} is the highest number");
            }    

            Console.ReadLine();
        }
    }
}
