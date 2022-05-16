using System;
using System.Collections.Generic;

namespace CarService
{
    internal class CarService
    {        
        private List<Details> _details = new List<Details>();
        private int _money;
        private int _priceForWork = 30000;
        private string _putUser;
        private bool _check = true;

        public CarService(int money)
        {
            _money = money;
        }

        public void AddDetails(string name, int price)
        {
            _details.Add(new Details(name, price));
        }

        public void ShowInfo()
        {
            Console.WriteLine($"В кассе: {_money}\n\nНа складе:");

            foreach (var detail in _details)
            {
                Console.WriteLine($"{detail.Name} {detail.CountOfPices} шт.");
            }
        }

        public void BuyDetails()
        {
            foreach (var detail in _details)
            {
                Console.Write($"{detail.Name}: ");
                while (_check)
                {
                    _putUser = Console.ReadLine().Replace(" ", "");
                    if (!String.IsNullOrEmpty(_putUser))
                    {
                        if (Int32.TryParse(_putUser, out int countOfPices) && countOfPices >= 0 )
                        {
                            if (_money >= 0)
                            {
                                detail.CountOfPices = countOfPices;
                                _money -= detail.Price * countOfPices;
                                _check = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("У вас недостаточно денег");
                                Console.ResetColor();
                                _check = false;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели что-то не то.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вы ничего не ввели.");
                        Console.ResetColor();
                    }
                }
                _check = true;
            }
        }

        public string ShowClient()
        {
            var _i = new Random().Next(_details.Count);
            Console.WriteLine($"Клиенту необходимо заменить {_details[_i].Name.ToLower()}: {_details[_i].Price} + {_priceForWork} за работу.");
            return _details[_i].Name;
        }

        public void Match(string detail)
        {
            foreach (var item in _details)
            {
                if (item.Name.ToLower() == detail.ToLower())
                {
                    if (item.CountOfPices > 0)
                    {
                        _money += item.Price + _priceForWork;
                        --item.CountOfPices;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Успех!");
                        Console.ResetColor();
                    }

                    else if (item.CountOfPices == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("На складе не осталось деталей, вам придётся заплатить штраф.");
                        Console.ResetColor();
                        _money -= 50000;
                    }
                }
            }
        }

        public bool Mismatch(string detail, string putUser)
        {
            var isFound = false;

            foreach (var item in _details)
            {
                if (item.Name.ToLower() == putUser.ToLower())
                {
                    if (item.CountOfPices == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("На складе не осталось деталей, которые вы запросили. Проверьте правильность ввода. Возможно, ");
                        Console.ResetColor();
                    }

                    else
                    {
                        --item.CountOfPices;
                        isFound = true;

                        foreach (var item2 in _details)
                        {
                            if (item2.Name.ToLower() == detail.ToLower())
                            {
                                _money -= item2.Price * 2;
                            }
                        }
                    }
                }
            }
            return isFound;
        }
    }
}
