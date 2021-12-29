using System;
namespace JackAreJunk
{
    public class GameDispalyConsole : IGameDisplay
    {
        public GameDispalyConsole()
        {
        }

        public Player Player { get; set; }

        public void DispayWinner()
        {
            Console.WriteLine($"The Round Winner is {Player.Name}");
            Console.ReadLine();
        }

        public void DisplayGameWinner()
        {
            Console.WriteLine($"The Winner is {Player.Name}");
            Console.ReadLine();
        }
    }
}
