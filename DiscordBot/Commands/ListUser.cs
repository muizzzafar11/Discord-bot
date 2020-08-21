using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Commands {
    
    [Group("ls")]
    public class ListUser : ModuleBase<SocketCommandContext> {
        
        private readonly string [,] origNames = {
            {"Muhammad Muizz","Ali"},
            {"Taylor","Whatley"},
            {"Dev","Narula"},
            {"Timur","Khayrullin"},
            {"Zaynah","nolastname"},
            {"Yifan","Wang"},
            {"Tahmeed","Naser"},
            {"Shrena","Sribalan"},
            {"Rick","(O20)"},
            {"Pranav","Vyas"},
            {"Karen","Truong"},
            {"Muhammad Muizz","nolastname"},
            {"Vince","Li"},
            {"Alex","Li"},
            {"nofirstname","Nolan"},
            {"Borna","Sadeghi"},
            {"Hargun","nolastname"},
            {"Markos","Georghiades"},
            {"Muhammad Muizz","Zafar"},
            {"Taylor desgroup","Whatley"},
            {"Viktor fasest","Korolyuk"},
            {"Denys","Linkov"},
            {"Muhammad Ali","Syed"},
            {"Muhammad Muizz","Zafar"},
            {"John Unknown","Doe"},
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
            string discordMessage = "";
            for (int i = 0; i < origNames.GetLength(0); i++) {
                foreach (var user in users) {
                        
                    string fullName = user.Nickname;
                    if (fullName == null) {
                        fullName = user.ToString();
                        fullName = fullName.Substring(0, fullName.LastIndexOf('#'));
                    }

                    int spaceIndex = fullName.LastIndexOf(' ');

                    Console.WriteLine("index: " + i);
                    if (spaceIndex <= 3) {
                        if (compare((origNames[i, 0] + origNames[i, 1]),fullName)) {
                            discordMessage = discordMessage + $" User `{origNames[i, 0]} {origNames[i, 1]}` has either " +
                                             $"first or last name which matches with `{fullName}`\n";
                            // await ReplyAsync($" User `{origNames[i, 0]} {origNames[i, 1]}` has either " +
                                // $"first or last name which matches with `{fullName}`");
                            Console.WriteLine($" User `{origNames[i, 0]} {origNames[i, 1]}` has either " +
                                $"first or last name which matches with `{fullName}`");
                        }
                    }

                    /**
                     * 
                     * If the person has both first and last names in the server
                     * If last name matches then proceed to see if the first name matches
                     * If both of them match then give the user the role passed in the command
                     * If first name doesn't match then notify in the console that the last name 
                     * match was sucessful
                     */

                    else
                    {
                        string firstName = fullName.Substring(0, spaceIndex);
                        string lastName = fullName.Substring(spaceIndex + 1);

                        // Checking if the last names match
                        if (compare(origNames[i, 1], lastName) ||
                            compare(lastName, origNames[i, 1]))
                        {
                            // Checking if the first names match
                            if (compare(origNames[i, 0], firstName) ||
                                compare(firstName, origNames[i, 0]))
                            {

                                // Assigning user a role specified in the command
                                var role = Context.Guild.GetRole(roleId);
                                // await (user as IGuildUser).AddRoleAsync(role);
                                // await ReplyAsync($"`{firstName} {lastName}` has been granted role `{role}`");
                                discordMessage = discordMessage +
                                                 $"`{firstName} {lastName}` has been granted role `{role}`\n";
                                break;
                            }

                            // If first name doesn't match
                            // await ReplyAsync($"`{lastName}` of `{firstName} {lastName}` matches " +
                            // $"with `{origNames[i, 1]}` of `{origNames[i, 0]} {origNames[i, 1]}`");
                            discordMessage = discordMessage + $"`{lastName}` of `{firstName} {lastName}` matches " +
                                             $"with `{origNames[i, 1]}` of `{origNames[i, 0]} {origNames[i, 1]}`\n";
                            Console.WriteLine($"`{lastName}` of `{firstName} {lastName}` matches " +
                                              $"with `{origNames[i, 1]}` of `{origNames[i, 0]} {origNames[i, 1]}`");
                        }
                        // If last name doesn't match
                        else {
                            discordMessage = discordMessage + $"{firstName} {lastName} could not be found in the server\n";
                        }
                    }
                }   
            }

            // await ReplyAsync(discordMessage);
            await sendSplitMessage(discordMessage);
            // 0 - 12, 13 -  36, 37 - 52
            // await sendSplitMessage("0123456789 10\n0123456789 11\n0123456789 12\n0123456789 13\n0123456789 14\n0123456789 15");
        }

        private bool compare(string name, string matchName) {
            return name.Replace(" ", "").Contains(matchName.Replace(" ", ""));
        }

        private async Task sendSplitMessage(string mainMessage)
        {
            // The most messgaes we can send in the discord chat
            int messageLimit = 2000;
            int startIndex = 0;
            if (mainMessage == null) {
                return;
            }
            if (mainMessage.Length <= messageLimit) {
                await ReplyAsync(mainMessage);
                return;
            }
            do {
                // Get the substring of the specified length
                string subString = mainMessage.Substring(startIndex, messageLimit+1);
                // Get the last index of new line in the substring of specified length
                int endIndex = subString.LastIndexOf("\n");
                // Send the message from the start index to last index of new line character
                await ReplyAsync(mainMessage.Substring(startIndex, endIndex));
                // set the start index to end index, +1 to skip over \n character
                startIndex += endIndex+1;
            } while (startIndex + messageLimit <= mainMessage.Length);
            await ReplyAsync(mainMessage.Substring(startIndex));
        }

        [Command("give-roles")]
        [Summary("gives roles to all the users in the server after matching them from a list")]
        public async Task assignRole() {
            await ReplyAsync("Assign role  function triggered");
        }

    }
}
