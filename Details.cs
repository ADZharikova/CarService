using System;

namespace CarService
{
    internal class Details
    {
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int CountOfPices { get; set; }

        public Details(string name, int price)
        {
            Name = name;
            Price = price;
            CountOfPices = 0;
        }
    }
}
