using Discord;
using Discord.WebSocket;
using System;
using Discord.Commands;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot {
    
    public class Handler {
        private DiscordSocketClient client; 

        private static Task log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task init() {
            client.Log += log;
        }

        public Handler(DiscordSocketClient client) {
            this.client = client;
        }
    }
}
