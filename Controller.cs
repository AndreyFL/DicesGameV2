using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace DicesGameV2
{
    class Controller
    {
        public static void InitializeGame()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            Random r = new Random();
            DiceGame game;

            string fileName = "DiceSav.dat";

            if (File.Exists(fileName))
            {
                Console.WriteLine("Существует сохраненная игра, прдолжить - y, начать новую - n");
                key = Console.ReadKey(true);
                if (key.KeyChar.ToString().ToLower() == "y")
                {
                    game = SaveRestoreGame.RestoreGame(fileName);
                    if (game != null)
                        Console.WriteLine(game.PreviousSteps);
                    else game = new DiceGame(r);// в случае ошибки восстановления сохраненной игры, создается новая.
                }
                else
                    game = new DiceGame(r);
            }
            else
                game = new DiceGame(r);
            do
            {
                game.NextStep();
                Console.WriteLine("Хотите продолжить y/n?\n");
                key = Console.ReadKey(true);
            } while (key.KeyChar.ToString().ToLower() == "y");
            if (!game.Finished)
                SaveRestoreGame.SaveGame(game, fileName);
            else
                File.Delete(fileName);
        }
    }
}
