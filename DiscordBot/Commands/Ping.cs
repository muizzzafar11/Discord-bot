using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.Commands {
    public class Ping : ModuleBase<SocketCommandContext> {
        
        [Command("ping")]
        public async Task ping() {
            Console.WriteLine("Ping!");

            await ReplyAsync("Pong!");
        }
        
    }
}
