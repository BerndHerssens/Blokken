using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Groepsproject_Blokken
{
    internal static class DataManager
    {
        //1 record ophalen: Speler, Admin of Manager
        public static Player GetOnePlayer(int pkPlayer)
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                var query = from Player in BenKrabbeDBEntities.Players
                            where Player.SpelerID == pkPlayer
                            select Player;
                return query.FirstOrDefault();
            }
        }
        public static Admin GetOneAdmin(int pkAdmin)
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                var query = from Admin in BenKrabbeDBEntities.Admins
                            where Admin.AdminID == pkAdmin
                            select Admin;
                return query.FirstOrDefault();
            }
        }
        public static Manager GetOneManager(int pkManager)
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                var query = from Manager in BenKrabbeDBEntities.Managers
                            where Manager.ManagerID == pkManager
                            select Manager;
                return query.FirstOrDefault();
            }
        }
        //alle records ophalen: Gamelogs, Players, Admins, Managers
        public static List<GameLogVS> GetAllGameLogVS()
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.GamesLogVS.ToList();
            }
        }
        public static List<GameLogSP> GetAllGameLogSP()
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.GamesLogSP.ToList();
            }
        }
        public static List<Player> GetAllPlayersSorted() //haalt spelers op en displayt voorlopig gewonnen games, later aanpassen naar winrate%? of verder filteren?
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.Players.OrderBy(x => x.VSGamesWon).ToList();
            }
        }
        public static List<GameLogSP> GetAllGameLogSPSorted()
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.GamesLogSP.OrderBy(x => x.Score).ToList();
            }
        }
        public static List<Player> GetAllPlayers()
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.Players.ToList();
            }
        }
        public static List<Admin> GetAllAdmins()
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.Admins.ToList();
            }
        }
        public static List<Manager> GetAllManagers()
        {
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                return BenKrabbeDBEntities.Managers.ToList();
            }
        }
        //1 record toevoegen: Gamelogs, Players, Manager
        public static bool InsertGameLogVS(GameLogVS aGameLogVS)
        {
            bool insertSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.GamesLogVS.Add(aGameLogVS);
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    insertSucceeded = true;
                }
            }
            return insertSucceeded;
        }
        public static bool InsertGameLogSP(GameLogSP aGameLogSP)
        {
            bool insertSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.GamesLogSP.Add(aGameLogSP);
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    insertSucceeded = true;
                }
            }
            return insertSucceeded;
        }
        public static bool InsertPlayer(Player aPlayer)
        {
            bool insertSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Players.Add(aPlayer);
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    insertSucceeded = true;
                }
            }
            return insertSucceeded;
        }
        public static bool InsertManager(Manager aManager)
        {
            bool insertSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Managers.Add(aManager);
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    insertSucceeded = true;
                }
            }
            return insertSucceeded;
        }
        //1 record wijzigen: Players, Managers
        public static bool UpdatePlayer(Player aPlayer)
        {
            bool updateSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Entry(aPlayer).State = System.Data.Entity.EntityState.Modified;
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    updateSucceeded = true;
                }
            }
            return updateSucceeded;
        }
        public static bool UpdateManager(Manager aManager)
        {
            bool updateSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Entry(aManager).State = System.Data.Entity.EntityState.Modified;
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    updateSucceeded = true;
                }
            }
            return updateSucceeded;
        }
        //1 record verwijderen: Gamelogs, Players, Manager
        public static bool DeleteGameLogVS(GameLogVS aGameLogVS)
        {
            bool deleteSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Entry(aGameLogVS).State = System.Data.Entity.EntityState.Deleted;
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    deleteSucceeded = true;
                }
            }
            return deleteSucceeded;
        }
        public static bool DeleteGameLogSP(GameLogSP aGameLogSP)
        {
            bool deleteSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Entry(aGameLogSP).State = System.Data.Entity.EntityState.Deleted;
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    deleteSucceeded = true;
                }
            }
            return deleteSucceeded;
        }
        public static bool DeletePlayer(Player aPlayer)
        {
            bool deleteSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Entry(aPlayer).State = System.Data.Entity.EntityState.Deleted;
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    deleteSucceeded = true;
                }
            }
            return deleteSucceeded;
        }
        public static bool DeleteManager(Manager aManager)
        {
            bool deleteSucceeded = false;
            using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            {
                BenKrabbeDBEntities.Entry(aManager).State = System.Data.Entity.EntityState.Deleted;
                if (0 < BenKrabbeDBEntities.SaveChanges())
                {
                    deleteSucceeded = true;
                }
            }
            return deleteSucceeded;
        }
    }
}
