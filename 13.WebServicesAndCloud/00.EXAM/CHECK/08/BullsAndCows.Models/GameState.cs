namespace BullsAndCows.Models
{
    public enum GameState
    {
        WaitingForOpponent = 0,
        TurnBluePlayer = 1,
        TurnRedPlayer = 2,
        WonByBluePlayer = 3,
        WonByRedPlayer = 4,
    }
}
