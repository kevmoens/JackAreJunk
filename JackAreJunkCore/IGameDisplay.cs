using System;
namespace JackAreJunk
{
    public interface IGameDisplay
    {
        Player Player { get; set; }
        void DispayWinner();
        void DisplayGameWinner();
    }
}
