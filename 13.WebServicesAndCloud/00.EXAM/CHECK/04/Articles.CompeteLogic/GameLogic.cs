namespace Articles.CompeteLogic
{
    using System;

    public class GameLogic
    {
        /*game logic instance
         * give the red number, 
         * give the blue guess, 
         * returns a Result object 
         *          with int Bulls, int Cows properties <- if bulls = 4 victory, but thats in the 
        */

        private Random rand;

        public GameLogic()
        {
            this.rand = new Random();
        }

        public bool RedGoesFirst()
        {
            var number = rand.Next(2);

            bool redGoes = number == 0 ? true : false;

            return redGoes;
        }

        public Result Process(string realNumber, string guess)
        {
            //TODO: throw excetions if numbers are not valid
            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < 4; i++) //guess
            {
                for (int j = 0; j < 4; j++) //realNumber
                {
                    //TODO: implement the game logic
                }
            }
            return new Result(bulls, cows);
        }

        public bool IsValidNumber(string number)
        {
            //TODO: validation logic
            return true;
        }
    }
}
