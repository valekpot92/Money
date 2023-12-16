using System;

namespace Money
{
    class Program
    {
        static void Main(string[] args)
        {
            int balance = 0;
            int[] coinsQuantity = { 0, 0, 0, 0 };
            int[] coinsValues = {1, 2, 5, 10 };
            string[] names = { "Шоколадка", "Газировка" };
            int[] price = { 70, 60 };
            int[] availableQuantity = { 5, 2};
            PaymentType payment = PaymentType.Card;

            string command = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Баланс {balance}");
                Console.WriteLine("Введите команду");
                
                command = Console.ReadLine();
                if (command == "AddMoney")
                {
                    switch (payment)
                    {
                        case PaymentType.Coins:
                            for (int i = 0; i < coinsValues.Length; i++)
                            {
                                Console.WriteLine($"Сколько монет номиналом {coinsValues[i]} вы хотите внести?");
                                int count = 0;
                                while (!int.TryParse(Console.ReadLine(), out count))
                                {
                                    Console.WriteLine("Вы ввели не число");
                                }
                                coinsQuantity[i] += count;
                                balance += count * coinsValues[i];

                            }
                            break;
                        case PaymentType.Card:
                            Console.WriteLine("Сколько снять с вашей карты?");
                            int balanceDelta = 0;
                            while (!int.TryParse(Console.ReadLine(), out balanceDelta))
                            {
                                Console.WriteLine("Вы ввели не число");

                            }
                            balance += balanceDelta;
                            Console.WriteLine("Баланс успешно пополнен");
                            break;
                        default: break;
                    }
                }
                else if (command == "GetChange")
                {
                    balance = 0;

                }
                else if (command.StartsWith("BuyGood"))
                {
                    string[] rawData = command.Substring("BuyGood ".Length).Split(' ');
                    if (rawData.Length != 2)
                    {
                        Console.WriteLine("Неправильный аргумент команды");
                        break;
                    }
                    int id = Convert.ToInt32(rawData[0]);
                    int count = Convert.ToInt32(rawData[1]);

                    if (id < 0 || id >= names.Length)
                    {
                        Console.WriteLine("Такого товара нет");
                        break;
                    }
                    if (count < 0 || count > availableQuantity[id])
                    {
                        Console.WriteLine("Нет такого количества");
                        break;
                    }
                    if (balance >= price[id] * count)
                    {
                        balance -= price[id] * count;
                        availableQuantity[id] -= count;
                    }
                    else
                    {
                        Console.WriteLine("Команда не определена");
                    }
                } Console.ReadLine();

            } 

        }

        enum PaymentType
        {
            Coins,
            Card 
        }
    }


}
