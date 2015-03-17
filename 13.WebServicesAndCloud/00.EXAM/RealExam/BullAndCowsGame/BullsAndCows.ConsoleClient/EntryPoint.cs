using BullAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.ConsoleClient
{
    class EntryPoint
    {
        static void Main(string[] args)
        {

            var data = new BullAndCowsData();

            data.Games.All().Any(); ;

        }
    }
}
