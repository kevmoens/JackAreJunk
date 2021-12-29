using System;
using System.Threading.Tasks;

namespace JackAreJunk
{
    public interface IGameTurn
    {
        Game Game { get; set; }
        Player Player { get; set; }
        ICard CurrentCard { get; set; }
        TaskCompletionSource<ICard> WaitForCard { get; set; }
        Task<ICard> AskToStartTurn();
        Task<TurnResult> PlayTurn(ICard card);
        void DisplayPlayerCards();
        Task InitializeDisplayForTurn(ICard card);

    }
}
