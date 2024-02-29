using System;
//Todo nog niks :)
namespace Groepsproject_Blokken
{
    public partial class Player
    {
        //Als je beide values tegelijk wilt met text erbij
        public string CalculateWinRates() //Geeft beide winrates
        {
            var WinrateSPVS = "Winrate in SP: " + SPGamesWon / SPGamesPlayed * 100 + " %" + Environment.NewLine + "Winrate in VS: " + VSGamesWon / VSGamesPlayed + " %";
            if (this.SPGamesPlayed == null || this.VSGamesPlayed == null)  //Als een van deze null zijn dan geven we N/A mee met deze functie
            {
                WinrateSPVS = "N/A";
            }

            return WinrateSPVS; //Gooit regel 10 indien het niet null is, regel 13 als een van de twee null is.
        }
        // Als je het in een txtvak wilt gooien en je het wilt opsplitsen :)
        public string CalculateSPWinRate() //Zelfde logica dan bij functie CalculateWinRates();
        {
            var WinrateSP = Convert.ToString(SPGamesWon / SPGamesPlayed * 100) + "%";
            if (this.SPGamesPlayed == null)
            {
                WinrateSP = "N/A";
            }
            return WinrateSP;
        }
        public string CalculateVSWinRate()
        {
            var WinrateVS = Convert.ToString(VSGamesWon / VSGamesPlayed * 100) + "%";
            if (this.VSGamesPlayed == null)
            {
                WinrateVS = "N/A";
            }
            return WinrateVS;
        }
    }
}

