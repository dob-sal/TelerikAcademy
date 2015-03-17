namespace BullsAndCows.GameLogic
{
    public interface ILogic
    {
        int BullsCount(string firstGuess, string secondGuess);

        int CowsCount(string firstGuess, string secondGuess);
    }
}
