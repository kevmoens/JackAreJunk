using System;
using System.Threading.Tasks;

namespace JackAreJunk
{
    public class GameTurnConsole : GameTurnBase
    {
        public GameTurnConsole()
        {
        }


        public override async Task<ICard> AskToStartTurn()
        {
            await InitializeDisplayForTurn(null);
            var player = ((IGameTurn)this).Player;
            //Show Last Card
            Console.WriteLine();
            Console.WriteLine($"{player.Name}'s turn");
            Console.WriteLine($"Discard {Game.LastDiscardedCard}");
            if (Game.Deck.Cards.Count == 0)
            {
                return Game.LastDiscardedCard;
            }
            do
            {
                Console.WriteLine($"Do you want want to pick from the (P)ile or (D)iscard?");
                var answer = Console.ReadKey();
                if ("D".Contains(answer.KeyChar.ToString().ToUpper()))
                {
                    return Game.LastDiscardedCard;
                }
                if ("P".Contains(answer.KeyChar.ToString().ToUpper()))
                {
                    var returnCard = Game.Deck.Cards[0];
                    Game.Deck.Cards.RemoveAt(0);
                    return returnCard;
                }
            } while (true);
        }
        public override Task<int> GetNonSovlvedPosition(ICard wildCard)
        {
            DisplayPlayerCards();
            var playerCards = ((IGameTurn)this).Player.Cards;
            int pos = -1;
            do
            {
                Console.WriteLine($"Which Place would you like to place card {wildCard}? (1-{playerCards.Count})");
                var input = Console.ReadLine();
                int.TryParse(input, out pos);
                if (pos > 0 && pos <= playerCards.Count)
                {
                    pos--;
                    var card = ((ICardPlayable)playerCards[pos]);
                    if (!card.IsShown || card.Rank == Rank.Queen || card.Rank == Rank.King)
                    {
                        break;
                    }
                }
            } while (true);
            return Task.FromResult(pos);
        }

        public override Task InitializeDisplayForTurn(ICard card)
        {
            Console.Clear();
            DisplayPlayerCards();
            return Task.CompletedTask;
        }
        public override void DisplayPlayerCards()
        {

            Console.WriteLine();
            var player = ((IGameTurn)this).Player;
            Console.WriteLine($"{player.Name}'s cards");
            var playerCards = player.Cards;
            for (int i = 0; i < playerCards.Count; i++)
            {
                var card = ((IGameTurn)this).Player.Cards[i] as ICardPlayable;
                if (card.IsShown)
                {
                    Console.WriteLine($"{i + 1} {card}");
                }
                else
                {
                    Console.WriteLine($"{i + 1} Hidden");
                }
            }
        }

        public override Task DisplayNewMatchingCard(Player player, int cardIdx)
        {
            Console.WriteLine($"{player.Cards[cardIdx]} Matched");
            return Task.CompletedTask;
        }
    }
}
