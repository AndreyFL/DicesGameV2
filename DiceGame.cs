using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DicesGameV2
{
    [Serializable]
    class DiceGame
    {
        Random r;
        int step;
        int humanScore;
        int compScore;
        public string PreviousSteps { get; private set; }
        public bool Finished
        {
            get
            {
                if (step == 1)
                    return true;
                return false;
            }
        }

        public DiceGame(Random rnd)
        {
            this.r = rnd;
            step = 1;
            humanScore = 0;
            compScore = 0;
            PreviousSteps = "";
        }

        static char[,] InitCubik(int number)
        {
            char[,] cubik = new char[7, 7];
            for (int i = 0; i < cubik.GetLength(0); i++)
                for (int j = 0; j < cubik.GetLength(1); j++)
                    if (i == 0 || j == 0 || i == cubik.GetLength(0) - 1 || j == cubik.GetLength(1) - 1)
                        cubik[i, j] = '*';
                    else
                        cubik[i, j] = ' ';
            switch (number)
            {
                case 1:
                    cubik[3, 3] = '*';
                    break;
                case 2:
                    cubik[3, 2] = '*';
                    cubik[3, 4] = '*';
                    break;
                case 3:
                    cubik[2, 2] = '*';
                    cubik[3, 3] = '*';
                    cubik[4, 4] = '*';
                    break;
                case 4:
                    cubik[2, 2] = '*';
                    cubik[2, 4] = '*';
                    cubik[4, 2] = '*';
                    cubik[4, 4] = '*';
                    break;
                case 5:
                    cubik[2, 2] = '*';
                    cubik[2, 4] = '*';
                    cubik[4, 2] = '*';
                    cubik[4, 4] = '*';
                    cubik[3, 3] = '*';
                    break;
                case 6:
                    cubik[2, 2] = '*';
                    cubik[2, 3] = '*';
                    cubik[2, 4] = '*';
                    cubik[4, 2] = '*';
                    cubik[4, 3] = '*';
                    cubik[4, 4] = '*';
                    break;
            }
            return cubik;
        }



        int ShowCubik()
        {

            int number = r.Next(1, 7);
            char[,] cubik = InitCubik(number);
            for (int i = 0; i < cubik.GetLength(0); i++)
            {
                for (int j = 0; j < cubik.GetLength(1); j++)
                {
                    PreviousSteps += cubik[i, j];
                    Console.Write(cubik[i, j]);
                }
                PreviousSteps += "\n";
                Console.WriteLine();
            }
            return number;
        }

        void PrintMessage(string message)
        {
            PreviousSteps += message;// Заношу текстовый вывод в переменную PreviousSteps
            Console.WriteLine(message);// Вывожу сообщение на консоль.

        }


        public void NextStep()
        {
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        PrintMessage(String.Format("Шаг № {0}\n", step));

                        PrintMessage(String.Format("Бросает человек.\n"));
                        Thread.Sleep(200);
                        humanScore += ShowCubik();
                        humanScore += ShowCubik();
                    }
                    else
                    {
                        PrintMessage(String.Format("Бросает компьютер ...\n"));
                        Thread.Sleep(200);
                        compScore += ShowCubik();
                        compScore += ShowCubik();
                    }
                }
                PrintMessage(String.Format("Человек - {0} баллов.\n", humanScore));
                PrintMessage(String.Format("Компьютер - {0} баллов.\n", compScore));

            }
            step++;
            if (step > 3)
            {
                step = 1;
                Console.WriteLine("Игра окончена.");
                Console.WriteLine("Человек - {0} баллов.", humanScore);
                Console.WriteLine("Компьютер - {0} баллов.", compScore);
                if (humanScore > compScore)
                    Console.WriteLine("Победитель Человек!!!");
                else if (humanScore < compScore)
                    Console.WriteLine("Победитель компьютер.");
                else
                    Console.WriteLine("Ничья :)");
                Console.WriteLine("\n");
                humanScore = 0;
                compScore = 0;
                PreviousSteps = "";
            }
        }
    }
}
