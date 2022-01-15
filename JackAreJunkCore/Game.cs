using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace JackAreJunk
{
    public class Game
    {
        public int Round { get; set; }
        public int CurrentPlayerTurn { get; set; }
        public IGameTurn CurrentTurn { get; set; }
        public bool IsGameOver { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Deck Deck { get; set; }
        public ICard LastDiscardedCard { get; set; }

        IServiceProvider serviceProvider;
        public Game(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Initialize()
        {
            var playerEntry = serviceProvider.GetService<IPlayerEntry>();
            var names = await playerEntry.GetPlayerNames();
            foreach (var name in names)
            {
                Players.Add(new Player() { Name = name, CardsPerDeal = 10 });
            }

        }
        public async Task Play()
        {
            Round++;
            Deck = new Deck();
            Deal();
            await PlayRound();
        }

        public void Deal()
        {
            ClearAllPlayerExistingCards();

            DealCardsToPlayers();

            SetInitialDiscard();
        }

        private void DealCardsToPlayers()
        {
            for (int cardIdx = 0; cardIdx < 10; cardIdx++)
            {
                for (int playerIdx = 0; playerIdx < Players.Count; playerIdx++)
                {
                    var player = Players[playerIdx];
                    if (cardIdx < player.CardsPerDeal)
                    {
                        player.Cards.Add(new CardPlayable(Deck.Cards[0]));
                        Deck.Cards.RemoveAt(0);
                    }
                }
            }
        }

        private void SetInitialDiscard()
        {
            LastDiscardedCard = Deck.Cards[0];
            Deck.Cards.RemoveAt(0);
        }

        private void ClearAllPlayerExistingCards()
        {
            for (int playerIdx = 0; playerIdx < Players.Count; playerIdx++)
            {
                var player = Players[playerIdx];
                player.Cards.Clear();
            }
        }

        public async Task PlayRound()
        {
            do
            {
                if (await PlayTurn())
                {
                    return;
                }
            }
            while (!IsGameOver && Deck.Cards.Count > 0);
        }
        public async Task<bool> PlayTurn()
        {
            var turn = serviceProvider.GetService<IGameTurn>();
            CurrentTurn = turn;
            turn.Game = this;
            turn.Player = Players[CurrentPlayerTurn];
            ICard card = await turn.AskToStartTurn();
            TurnResult result = await turn.PlayTurn(card);
            if (result.IsWinner)
            {
                var gameDisplay = serviceProvider.GetService<IGameDisplay>();
                gameDisplay.Player = result.Player;
                gameDisplay.Player.CardsPerDeal--;
                if (gameDisplay.Player.CardsPerDeal == 0)
                {
                    IsGameOver = true;
                }
                gameDisplay.DispayWinner();
                NextPlayerTurn();
                return true;
            }
            LastDiscardedCard = result.ReturnCard;
            NextPlayerTurn();
            return false;
        }
        private void NextPlayerTurn()
        {
            CurrentPlayerTurn++;
            if (CurrentPlayerTurn >= Players.Count)
            {
                CurrentPlayerTurn = 0;
            }
        }
    }
}
