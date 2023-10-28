using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SaveEditor.Structs;

namespace SaveEditor
{
    public class SaveFile
    {
        private const string SaveName = "game00.bin";
        private const string SystemName = "system.bin";

        public Game game;
        public SystemSave system;
        public bool IsSwitch;

        public void Load(string sPath)
        {
            game = new Game();
            game.Read(sPath);
        }

        public void LoadSystem(string sPath)
        {
            if (sPath != null)
            {
                system = Util.ReadStructure<SystemSave>(File.ReadAllBytes(sPath));
            }
        }

        public void Save(string sPathGame, int GameVersion, SaveGameType gameType)
        {
            game?.Write(sPathGame, GameVersion, gameType);
        }

        public void SaveSystem(string sPath)
        {
            if (sPath != null)
            {
                //CalculateSystemHash(ref system);
            }
        }

        private void CalculateSystemHash(ref byte[] data)
        {
            //fill hash with zeroes
            for (int i = 0; i < 0x14; i++) data[i] = 0;

            //calc hash
            var hash = new SHA1Managed().ComputeHash(data);

            //write hash
            //hash.CopyTo(data, 0);
            Console.WriteLine(BitConverter.ToString(hash));
        }




    }
}
