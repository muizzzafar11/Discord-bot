using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Commands {
    
    [Group("ls")]
    public class ListUser : ModuleBase<SocketCommandContext> {
        
        private readonly string [,] origNames = {
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

        [Command("match-name")]
        [Summary("compares names of discord guild members with a given list and outputs the names that match")]
        public async Task matchNames(ulong roleId) {
            var users = Context.Guild.Users;

            for (int i = 0; i < origNames.Length; i++) {
                foreach (var user in users) {
                    
                    string fullName = user.Nickname;
                    if (fullName == null) {
                        fullName = user.ToString();
                        fullName = fullName.Substring(0, fullName.LastIndexOf('#'));
                    }

                    int spaceIndex = fullName.LastIndexOf(' ');
                    
                    // Normal case of where the person has a normal first and last name
                    if (spaceIndex >= 2) {
                        string firstName = fullName.Substring(0, spaceIndex);
                        string lastName = fullName.Substring(spaceIndex + 1);
                        // If last name matches then proceed to see if the first name is the same as well.
                        // If both of them match then give the user the role passed in by the user
                    
                        // Checking if the last names match
                        if (origNames[i, 1].Contains(lastName) || lastName.Contains(origNames[i, 1])) {
                            // Checking if the first names match
                            if(origNames[i,0].Contains(firstName.Replace(" ", "")) ||
                                firstName.Replace(" ", "").Contains(origNames[i,0])) {
                                // await ReplyAsync($"The first name: `{origNames[i,0]}` matches with `{firstName}`");
                                // await ReplyAsync($"The last name: `{origNames[i,1]}` matches with `{lastName}`");
                                // await ReplyAsync($"Full name: `{origNames[i,0]} {origNames[i,1]}` matches with `{firstName} {lastName}`");
                                
                                // Assigning user a role specified in the command
                                var role = Context.Guild.GetRole(roleId);
                                await (user as IGuildUser).AddRoleAsync(role);
                                await ReplyAsync($"`{firstName} {lastName}` has been granted role `{role}`");
                                
                                break;
                            }
                        }
                    }
                    // If the user has only one name in discord
                    else {
                        if ((origNames[i, 0] + origNames[i, 1]).Contains(fullName)) {
                            // Console.WriteLine($" name {origNames[i, 0] +" "+ origNames[i, 1]} matches with {fullName}");
                            await ReplyAsync($" name {origNames[i, 0] +" "+ origNames[i, 1]} matches with {fullName}");
                        }
                    }
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