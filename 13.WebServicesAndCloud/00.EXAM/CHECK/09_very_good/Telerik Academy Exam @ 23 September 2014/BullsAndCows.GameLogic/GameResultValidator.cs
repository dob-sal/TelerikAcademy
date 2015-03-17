using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.GameLogic
{
    public class GameResultValidator : IGameResultValidator
    {
        public GameResult GetResult(string firstPlayerGuess, string secondPlayerGuess)
        {
            if (firstPlayerGuess == secondPlayerGuess)
            {
                return GameResult.Won;
            }

            return GameResult.NotFinished;
        }

    }
}
