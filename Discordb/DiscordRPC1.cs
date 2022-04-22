using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UHQKEK.Discordb
{
    class DiscordRPC1
    {
        public static DiscordRpcClient client;

        public static void Initialize()
        {
            client = new DiscordRpcClient("927794762608705538");
            client.Initialize();
            client.SetPresence(new RichPresence
            {
                Details = "Best AIO!",
                State = "By CrackerShadow & Gam3rr & Lover",
                Timestamps = Timestamps.Now,
                Assets = new Assets
                {

                    LargeImageKey = "logo",
                    LargeImageText = "DIVINE Checker",
                    SmallImageKey = "fire-modified",
                    SmallImageText = ".gg/nUybwsnR"

                }
            }); ;

        }

    }
}
