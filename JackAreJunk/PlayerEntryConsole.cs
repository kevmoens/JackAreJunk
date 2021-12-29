using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JackAreJunk
{
    public class PlayerEntryConsole : IPlayerEntry
    {
        public PlayerEntryConsole()
        {
        }
        public Task<List<string>> GetPlayerNames()
        {
            var names = new List<string>();
            var count = GetPlayerCount();
            for (int i = 0; i < count; i++)
            {
                names.Add(GetPlayerName(i));
            }
            return Task.FromResult(names);
        }
        private int GetPlayerCount()
        {
            int playerCount = 0;
            Console.WriteLine("How Many Players will play?");
            do
            {
                var inputCount = Console.ReadLine();
                if (int.TryParse(inputCount, out playerCount) && playerCount > 1 && playerCount <= 4)
                {
                    break;
                }
                Console.WriteLine("Enter a valid integer greater than or equal to 2 and less than or equal to 4");
            } while (true);
            return playerCount;
        }
        private string GetPlayerName(int playerNum)
        {
            string name = string.Empty;
            Console.WriteLine($"Enter Player {playerNum + 1}'s Name.");
            do
            {
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
            } while (true);
            return name;
        }
    }
}
