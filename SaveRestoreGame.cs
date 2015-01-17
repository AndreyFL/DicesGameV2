using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DicesGameV2
{
    static class SaveRestoreGame
    {
        public static bool SaveGame(DiceGame game, string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, game);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось записать в файл \"{0}\" по причине: {1}",fileName,ex.Message);
                return false;
            }
        }

        public static DiceGame RestoreGame(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                DiceGame game = (DiceGame)bf.Deserialize(fs);
                fs.Close();
                return game;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось восстновить игру из файла \"{0}\" по причине: {1}", fileName, ex.Message);
                return null;
            }
        }
    }
}
