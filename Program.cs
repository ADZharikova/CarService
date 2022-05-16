using System;
using System.Collections.Generic;
using System.IO;

namespace CarService
{
    internal class Program
    {
        static Dictionary<string, string> AddDetails(string name, char delimiter)
        {
            Dictionary<string, string> detailPrice = new Dictionary<string, string>();

            using (StreamReader sr = new StreamReader(name))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyvalye = line.Split(delimiter);
                    if (keyvalye.Length == 2)
                    {
                        detailPrice.Add(keyvalye[0], keyvalye[1]);
                    }
                }
            }

            return detailPrice;
        }

        static void Main(string[] args)
        {

            Dictionary<string, string> readDetailsOfFile;
            string name = "Details.txt";
            char delimiter = ':';

            CarService carService = new CarService(500000);
            bool isOpen = true;
            string detail, putUser;
            bool isExist = false;


            readDetailsOfFile = AddDetails(name, delimiter);
            foreach (var item in readDetailsOfFile)
            {
                var valuePrice = Convert.ToInt32(item.Value);
                carService.AddDetails(item.Key, valuePrice);
            }

            carService.ShowInfo();
            Console.WriteLine();
            Console.WriteLine("Для начала необходимо закупить детали.");
            Console.WriteLine();
            Console.WriteLine("Введите необходимое количество");
            carService.BuyDetails();
            Console.Clear();

            while (isOpen)
            {
                carService.ShowInfo();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Если вы правильно замените деталь, то получите стоимость детали и стоимость работ (30000).\n" +
                    "Если у вас не будет нужной детали, то Вам придётся заплатить штраф в размере 50000.\n" +
                    "Если вы замените не ту деталь, то придётся заплатить двойную цену нужной детали. А также деталь, которую вы вставили, пропадёт со склада.\n\n");
                Console.ResetColor();
                detail = carService.ShowClient();
                while (!isExist) {
                    Console.Write("\nВведите название детали, которую необходимо принести со склада: ");
                    putUser = Console.ReadLine().Trim();
                    if (putUser.ToLower() == detail.ToLower())
                    {
                        carService.Match(detail);
                        isExist = true;
                    }
                    else
                    {
                        isExist = carService.Mismatch(detail, putUser);

                        if (isExist)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Вы заменили не ту деталь.");
                            Console.ResetColor();
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели что-то не то.");
                            Console.ResetColor();
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\nДля перехода к следующему клиенту нажмите любую кнопку.");
                Console.ResetColor();
                Console.ReadKey(true);
                isExist = false;
                Console.Clear();
            }
        }
    }
}
