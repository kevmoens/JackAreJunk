using System;
using System.Threading.Tasks;

namespace JackAreJunk
{
    public abstract class GameTurnBase : IGameTurn
    {
        public GameTurnBase()
        {
        }
        public Game Game { get; set; }
        Player IGameTurn.Player { get; set; }
        public ICard CurrentCard { get; set; }
        public abstract Task<ICard> AskToStartTurn();
        public abstract Task<int> GetNonSovlvedPosition(ICard card);
        public abstract void DisplayPlayerCards();
        public abstract Task InitializeDisplayForTurn(ICard card);

        public TaskCompletionSource<ICard> WaitForCard { get; set; }

        public async Task<TurnResult> PlayTurn(ICard card)
        {
            await InitializeDisplayForTurn(card);
            var result = new TurnResult();
            result.Player = ((IGameTurn)this).Player;
            if (CheckForWinner())
            {
                result.IsWinner = true;
                return result;
            }
            if (card.IsJoker || card.Rank == Rank.Jack)
            {
                result.ReturnCard = card;
                return result;
            }
            CurrentCard = card;
            int index = GetPlayerTurnIndex(card);
            if (index == -1) //Wild Card
            {
                //Ask for non-solved position 1-10
                index = await GetNonSovlvedPosition(card);
            }
            CurrentCard = null;
            if (index >= ((IGameTurn)this).Player.Cards.Count)  //DON'T HAVE THIS CARD TO (SOLVE/TURN OVER)
            {
                result.ReturnCard = card;
                return result;
            }
            var posCard = (ICardPlayable)((IGameTurn)this).Player.Cards[index];
            if (!posCard.IsShown || posCard.Rank == Rank.Queen || posCard.Rank == Rank.King)
            {
                if (!(card is ICardPlayable))
                {
                    card = new CardPlayable(card);
                }
                ((ICardPlayable)card).IsShown = true;
                ((IGameTurn)this).Player.Cards[index] = card;
                if (CheckForWinner())
                {
                    result.IsWinner = true;
                    return result;
                }
                //This is the next card to check...
                return await PlayTurn((ICard)posCard);
            }
            //Play turn with King or Queen
            if (posCard.Rank == Rank.Queen || posCard.Rank == Rank.King)
            {
                return await PlayTurn((ICard)posCard);
            }
            //Card already solved.  Turn Over
            result.ReturnCard = card;
            return result;

        }
        private int GetPlayerTurnIndex(ICard card)
        {
            switch (card.Rank)
            {
                case Rank.C01:
                    return 0;
                case Rank.C02:
                    return 1;
                case Rank.C03:
                    return 2;
                case Rank.C04:
                    return 3;
                case Rank.C05:
                    return 4;
                case Rank.C06:
                    return 5;
                case Rank.C07:
                    return 6;
                case Rank.C08:
                    return 7;
                case Rank.C09:
                    return 8;
                case Rank.C10:
                    return 9;
                case Rank.Queen:
                    break;
                case Rank.King:
                    break;
            }
            return -1;
        }
        private bool CheckForWinner()
        {
            var player = ((IGameTurn)this).Player;
            //Check to see if player won
            for (int cardPos = 0; cardPos < player.Cards.Count; cardPos++)
            {
                if (!((ICardPlayable)player.Cards[cardPos]).IsShown)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class GameTurnCardPubSubMessage
    {
        public ICard Card { get; set; }
    }
}
