namespace BullsAndCows.GameLogic
{
    public interface IGameResultValidator
    {
        GameResult GetResult(string board);
    }
}
