namespace BullsAndCows.GameLogic
{
    public interface IGameResultValidator
    {
        GameResult GetResult(string firstPlayerGuess, string secondPlayerGuess);
    }
}
