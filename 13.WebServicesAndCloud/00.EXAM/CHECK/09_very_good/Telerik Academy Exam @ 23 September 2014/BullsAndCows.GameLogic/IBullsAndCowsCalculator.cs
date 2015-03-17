namespace BullsAndCows.GameLogic
{
    public interface IBullsAndCowsCalculator
    {
        int GetBulls(string playerNumber, string guess);

        int GetCows(string playerNumber, string guess);

    }
}
