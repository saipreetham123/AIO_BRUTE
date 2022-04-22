using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHQKEK.Start;

namespace UHQKEK.Discordb
{
    class Settings
    {
        public static Settings.ConfigObject config = new Settings.ConfigObject();


        public static ConfigObject settings(bool AskToSave, bool displ)
        {
            Logo.logo();
            if (AskToSave)
            {
                Console.WriteLine("Please Enter Your Discord Tag [Then You can do [/s] for Stats on Discord]");
                config.DiscordID = Console.ReadLine();
            }
            if (displ)
            {
                Console.WriteLine("Display Hits / 2FA etc on Console? [ Y / N ]");
                config.keylo = Console.ReadLine().ToUpper();
            }
            File.WriteAllText("config.json", JsonConvert.SerializeObject((object)Settings.config));
            Console.WriteLine("Config saved!", Color.Lime);
            return config;
        }
        public class ConfigObject
        {

            public string DiscordID { get; set; }

            public string keylo { get; set; } = "Y";
        }
    }
}
