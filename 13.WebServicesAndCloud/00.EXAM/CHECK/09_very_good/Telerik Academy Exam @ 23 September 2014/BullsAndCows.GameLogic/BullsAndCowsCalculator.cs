namespace BullsAndCows.GameLogic
{
    public class BullsAndCowsCalculator : IBullsAndCowsCalculator
    {
        public int GetBulls(string playerNumber, string guess)
        {
            int result = 0;
            for (int i = 0; i < playerNumber.Length; i++)
            {
                if(playerNumber[i] == guess[i])
                {
                    ++result;
                }
            }
            return result;
        }

        public int GetCows(string playerNumber, string guess)
        {
            int cows = 0;
            for (int i = 0; i < playerNumber.Length; i++)
            {
                for (int j = 0; j < guess.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if(playerNumber[i] == guess[j])
                    {
                        ++cows;
                    }
                }
            }
            return cows;
        }
    }
}
