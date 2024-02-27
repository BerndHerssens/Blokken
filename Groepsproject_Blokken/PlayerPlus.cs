using System;
//Todo nog niks :)
namespace Groepsproject_Blokken
{
    public partial class Player
    {
        //Als je beide values tegelijk wilt met text erbij
        public string CalculateWinRates() //Geeft beide winrates
        {
            return "Winrate in SP: " + SPGamesWon / SPGamesPlayed * 100 + " %" + Environment.NewLine + "Winrate in VS: " + VSGamesWon / VSGamesPlayed + " %";
        }
        // Als je het in een txtvak wilt gooien en je het wilt opsplitsen :)
        public string CalculateSPWinRate()
        {
            var WinrateSP = Convert.ToString(SPGamesWon / SPGamesPlayed * 100) + "%";
            return WinrateSP;
        }
        public string CalculateVSWinRate()
        {
            var WinrateVS = Convert.ToString(VSGamesWon / VSGamesPlayed * 100) + "%";
            return WinrateVS;
        }
    }
}
}
