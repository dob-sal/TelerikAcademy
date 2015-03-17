using System;

namespace BullsAndCows.GameLogic
{
    public class Logic : ILogic
    {
        public int BullsCount(string firstGuess, string secondGuess)
        {
            int counter = 0;

            for (int i = 0; i < firstGuess.Length; i++)
            {
                if (firstGuess[i] == secondGuess[i])
                {
                    counter++;
                }
            }

            return counter;
        }

        public int CowsCount(string firstGuess, string secondGuess)
        {
            int counter = 0;

            for (int i = 0; i < firstGuess.Length - 1; i++)
            {
                for (int j = i; j < secondGuess.Length; j++)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
