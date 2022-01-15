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

        public override async Task DisplayNewMatchingCard(Player player, int cardIdx)
        {            
            await PubSub.Hub.Default.PublishAsync<GameTurnCardMatchPubSubMessage>(new GameTurnCardMatchPubSubMessage(
                player.Cards[cardIdx]
            ));
        }
        public override async Task<int> GetNonSovlvedPosition(ICard wildCard)
        {
            WaitForCard = new TaskCompletionSource<ICard>();
            PubSub.Hub.Default.Publish<GameTurnRefreshUIMessage>(new GameTurnRefreshUIMessage());
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

        public override async Task InitializeDisplayForTurn(ICard card)
        {
            await PubSub.Hub.Default.PublishAsync<GameTurnCardPubSubMessage>(new GameTurnCardPubSubMessage() { Card = card });
        }
    }

    public class GameTurnCardMatchPubSubMessage
    {
        public GameTurnCardMatchPubSubMessage(ICard card)
        {
            Card = card;
        }
        public ICard Card { get; set; }
    }
}
