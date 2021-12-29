using System;
namespace JackAreJunk.Web
{
    public class GameDisplayWeb : IGameDisplay
    {
        public GameDisplayWeb()
        {
        }

        public Player Player { get; set; }

        public void DispayWinner()
        {
            PubSub.Hub.Default.Publish<GameDisplayWinnerPubSubMessage>(new GameDisplayWinnerPubSubMessage() { Player = Player, Round = true });
        }

        public void DisplayGameWinner()
        {
            PubSub.Hub.Default.Publish<GameDisplayWinnerPubSubMessage>(new GameDisplayWinnerPubSubMessage() { Player = Player, Game = true });
        }
    }
    public class GameDisplayWinnerPubSubMessage
    {
        public bool Round { get; set; }
        public bool Game { get; set; }
        public Player Player { get; set; }
    }
}
