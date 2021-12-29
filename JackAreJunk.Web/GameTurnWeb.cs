using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JackAreJunk.Web
{
    public class GameTurnWeb : GameTurnBase
    {
        public GameTurnWeb()
        {
        }
        int triedPos = -1;
        public override async Task<ICard> AskToStartTurn()
        {
            await InitializeDisplayForTurn(null);
            WaitForCard = new TaskCompletionSource<ICard>();
            var player = ((IGameTurn)this).Player;
            //Show Last Card
            if (Game.Deck.Cards.Count == 0)
            {
                return Game.LastDiscardedCard;
            }
            do
            {
                return await WaitForCard.Task;
            } while (true);
        }

        public override void DisplayPlayerCards()
        {
        }

        public override async Task<int> GetNonSovlvedPosition(ICard wildCard)
        {
            WaitForCard = new TaskCompletionSource<ICard>();
            var card = await WaitForCard.Task;
            if (card == null) return -1;
            for (int i = 0; i < ((IGameTurn)this).Player.Cards.Count; i++)
            {
                if (((IGameTurn)this).Player.Cards[i] == card)
                {
                    return i;
                }
            }
            return -1;
        }

        public override Task InitializeDisplayForTurn(ICard card)
        {
            PubSub.Hub.Default.Publish<GameTurnCardPubSubMessage>(new GameTurnCardPubSubMessage() { Card = card });
            return Task.CompletedTask;
        }
    }
}
