using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBot {

    public class Program {
        private readonly string token;

        private DiscordSocketClient client;
        private Handler handler;

        private Program(string token) {
            this.token = token;
        }

        public async Task start() {
            client = new DiscordSocketClient();
            handler = new Handler(client);
            await handler.init();

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // The task will continue forever
            await Task.Delay(-1);
        }

        // Entry point of the program
        static void Main(string[] args) {
            // Init command with token.
            if (args.Length >= 2 && args[0] == "init") {
                File.WriteAllText("token.txt", args[1]);
                Console.WriteLine("Writing token to 'token.txt'");
            }

            // Start bot with token from "token.txt" in working folder.
            try {
                var token = File.ReadAllText("token.txt").Trim();
                Console.WriteLine("Read token from 'token.txt' sucessfully");
                new Program(token).start().GetAwaiter().GetResult();
            } catch (IOException) {
                Console.WriteLine("Could not read from token.txt. Did you run `init <token>`?");
            }
        }
    }
}