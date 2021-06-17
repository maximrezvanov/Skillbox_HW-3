using System;
using System.Collections.Generic;

namespace HW3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            List<string> players = new List<string>();
            int gameNumber = 0, userTry;
            int minNumber, minUserTry = 0;
            int maxNumber, maxUserTry = 0, maxUserCount;
            bool isFirstMove = true;
            bool isWin = false;
            bool isFirstUserTry = true;
            string playerName;
            string strToRestart;
            string warnningString = "Не корректный ввод. Повторите";
            string rules = "Загадывается число от 12 до 120, причём случайным образом. Назовём его gameNumber." +
                            "\nИгроки по очереди выбирают число от одного до четырёх. " +
                            "\nUserTry после каждого хода вычитается из gameNumber, а само gameNumber выводится на экран." +
                            "\nЕсли после хода игрока gameNumber равняется нулю, то походивший игрок оказывается победителем.";

            Random rand = new Random();

            Console.WriteLine("Правила игры: " + rules);

            Console.WriteLine("Введите кол-во игроков");
            maxUserCount = int.Parse(Console.ReadLine());
            for (int user = 0; user < maxUserCount; user++)
            {
                Console.WriteLine($"\nИгрок {user + 1} введите ваше имя");
                playerName = Console.ReadLine();
                players.Add(playerName);
            }

            while (!isWin)
            {

                if (isFirstUserTry)
                {
                    Console.WriteLine("Укажите min и max ограничения gameNumber");
                    isFirstUserTry = false;
                    while (!int.TryParse(Console.ReadLine(), out minNumber) || minNumber <= 0)
                    {
                        Console.WriteLine(warnningString);
                    }
                    while (!int.TryParse(Console.ReadLine(), out maxNumber))
                    {
                        Console.WriteLine(warnningString);
                    }
                    gameNumber = rand.Next(minNumber, maxNumber);
                    Console.WriteLine("Укажите min и max ограничения userTry");
                    while (!int.TryParse(Console.ReadLine(), out minUserTry))
                    {
                        Console.WriteLine(warnningString);
                    }
                    while (!int.TryParse(Console.ReadLine(), out maxUserTry))
                    {
                        Console.WriteLine(warnningString);
                    }
                    Console.WriteLine("начальное значение gameNumber: " + gameNumber);

                }

                while (gameNumber != 0)
                {
                    if (players.Count > 1)
                    {
                        for (int i = 0; i < players.Count; i++)
                        {
                        again: // точка возврата

                            //как решение бага с завершением цикла for при вводе min-max 
                            if (gameNumber == 0) break;
                            Console.WriteLine("ход игрока " + players[i]);
                            Console.WriteLine(players[i] + " ввидете число");
                            while (!int.TryParse(Console.ReadLine(), out userTry))
                            {
                                Console.WriteLine(warnningString);
                            }
                            if (userTry >= minUserTry && userTry <= maxUserTry && userTry <= gameNumber)
                            {
                                gameNumber -= userTry;
                                isFirstMove = false;
                                if (gameNumber != 0)
                                {
                                    Console.WriteLine("gameNumber: " + gameNumber);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ход не засчитан, " + players[i] + " повторите ввод");
                                goto again;
                            }
                            if (isFirstMove && userTry > 1 && gameNumber <= userTry)
                            {
                                Console.WriteLine("Ничья!");
                                isFirstMove = false;
                                Restart();
                            }
                            if (gameNumber == 0)
                            {
                                Console.WriteLine(players[i] + " победил!");
                                Restart();

                            }

                        }
                    }
                    else
                    {
                        bool isPCMove = false;
                        Console.WriteLine("Ваш ход " + players[0]);
                        Console.WriteLine(players[0] + " ввидете число");
                        while (!int.TryParse(Console.ReadLine(), out userTry))
                        {
                            Console.WriteLine(warnningString);
                        }
                        if (userTry >= minUserTry && userTry <= maxUserTry && userTry <= gameNumber)
                        {
                            gameNumber -= userTry;
                            isFirstMove = false;
                            if (gameNumber != 0)
                            {
                                isPCMove = true;
                                Console.WriteLine("gameNumber: " + gameNumber);
                            }
                            else
                            {
                                Console.WriteLine("Победа за вами");
                                Restart();
                            }
                        }
                        else
                        {
                            Console.WriteLine("ход не засчитан, " + players[0] + " повторите ввод");
                        }
                        if (isPCMove)
                        {
                            do
                            {
                                Console.WriteLine("Ходит терминатор");
                                int pcMoveValue = rand.Next(minUserTry, maxUserTry);
                                int prevValue = pcMoveValue;
                                bool isValidValue = true;
                                if (pcMoveValue > prevValue && !isValidValue)
                                    pcMoveValue = rand.Next(minUserTry, maxUserTry);
                                Console.WriteLine("Терминатор ввел значение " + pcMoveValue);
                                if (pcMoveValue <= gameNumber)
                                {
                                    gameNumber -= pcMoveValue;
                                    Console.WriteLine("gameNumber: " + gameNumber);
                                    isPCMove = false;
                                    break;
                                }
                                else
                                {
                                    isValidValue = false;
                                    Console.WriteLine("Терминатор промахнулся и ходит еще раз");  
                                }
                            } while (isPCMove || gameNumber != 0);
                          
                            if (gameNumber == 0)
                            {
                                Console.WriteLine("Победа за Скайнет");
                                Restart();
                            }
                        }
                    }
                }
            }
            void Restart()
            {
                Console.WriteLine("Что бы начать заново введитете \"+\", что бы выйти \"-\"");
                strToRestart = Console.ReadLine();
                if (strToRestart == "-") isWin = true;
                else if (strToRestart == "+")
                {
                    isWin = false;
                    isFirstUserTry = true;
                }
            }
        }
    }
}


