using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullAndCows.Models
{
    public enum GameState
    {
        WaitingForOpponent = 0,
        RedInTurn = 1,
        BlueInTurn = 2,
        Finished = 3
    }
}
