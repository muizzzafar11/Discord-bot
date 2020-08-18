using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.Commands {
    
    [Group("ls")]
    public class ListUser : ModuleBase<SocketCommandContext> {
        
        private string [,] origNames = {
            {"John Unknown","Doe"},
            {"Muhammad Muizz","Ali"},
            {"Taylor","Whatley"},
            {"Muhammad Muizz","Zafar"},
        };
        
        [Command("names")]
        [Summary("lists the nicknames of all the members in the server")]
        public async Task nicknames() {
            var users = Context.Guild.Users;
            foreach (var user in users) {
                var nickname = user.Nickname;
                if (nickname == null) {
                    var userString = user.ToString();
                    await ReplyAsync(
                        $"{userString.Substring(0, userString.LastIndexOf('#'))}");
                } else {
                    await ReplyAsync(user.Nickname);
                }
            }
            Console.WriteLine("Sent all the list of users");
        }

        [Command("match-names")]
        [Summary("compares names of discord guild members with a given list and outputs the names that match")]
        public async Task matchNames() {
            var users = Context.Guild.Users;
            foreach (var user in users) {
                var nickname = user.Nickname;
                if (nickname == null) {
                    var userString = user.ToString();
                    await ReplyAsync(
                        $"{userString.Substring(0, userString.LastIndexOf('#'))}");
                } else {
                    await ReplyAsync(user.Nickname);
                }
            }
        }

        [Command("give-roles")]
        [Summary("gives roles to all the users in the server after matching them from a list")]
        public async Task assignRole() {
            await ReplyAsync("Assign role  function triggered");
        }

    }
}