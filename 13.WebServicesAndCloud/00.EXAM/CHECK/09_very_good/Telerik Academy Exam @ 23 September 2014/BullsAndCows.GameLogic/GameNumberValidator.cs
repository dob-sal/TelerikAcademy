namespace BullsAndCows.GameLogic
{
    public class GameNumberValidator : IGameNumberValidator
    {
        public bool IsValidGameNumber(string number)
        {
            if (number.Length != 4)
            {
                return false;
            }

            // Not sure about that - that's how I play it
            if (number[0] == '0')
            {
                return false;
            }

            for (int i = 0; i < number.Length - 1; i++)
            {
                for (int j = i + 1; j < number.Length; j++)
                {
                    if (number[i] == number[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
