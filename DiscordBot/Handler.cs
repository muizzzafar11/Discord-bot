using Discord;
using Discord.WebSocket;
using System;
using System.Reflection;
using Discord.Commands;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot {
    
    public class Handler {
        private readonly char prefix = '-';
        
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly ServiceProvider services;

        private static Task log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task messageReceived(SocketMessage msg) {
            // Don't process the command if it was a system message
            var message = msg as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix(prefix, ref argPos) || 
                  message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await commands.ExecuteAsync(
                context: context, 
                argPos: argPos,
                services: services);
        }

        public async Task init() {
            client.Log += log;
            client.MessageReceived += messageReceived;
            
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        public Handler(DiscordSocketClient client) {
            this.client = client;
            
            commands = new CommandService();
            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();
        }
        
    }
}
