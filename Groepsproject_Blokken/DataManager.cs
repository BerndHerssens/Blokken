using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Groepsproject_Blokken
{
    internal static class DataManager
    {
        //TODO: Speler met de beste winrate en aantal games tonen(minstens 5 games)Je kan sorten op winrate en aantal games
        //TODO: Extra functie query toevoegen: zoeken op username en wachtwoord

        //Haalt de ingelogde gebruiker op
        public static Player GetLoggedInPlayer(string naam, string wachtwoord)
        {
            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    var query = from Player in BenKrabbeDBEntities.Players
            //                where Player.Name == naam && Player.Password == wachtwoord
            //                select Player;
            //    return query.FirstOrDefault();
            //}
            //Json versie
            List<Player> list = GetAllPlayers();
            Player returnGebruiker = new Player();
            foreach (Player gevondenGebruiker in list)
            {
                if (gevondenGebruiker.Name == naam && gevondenGebruiker.Password == wachtwoord)
                {
                    returnGebruiker = gevondenGebruiker;
                }
            }
            return returnGebruiker;
        }
        public static Admin GetLoggedInAdmin(string naam, string wachtwoord)
        {
            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    var query = from Admin in BenKrabbeDBEntities.Admins
            //                where Admin.Name == naam && Admin.Password == wachtwoord
            //                select Admin;
            //    return query.FirstOrDefault();
            //}

            //JSON Versie

            List<Admin> list = GetAllAdmins();
            Admin returnGebruiker = new Admin();
            foreach (Admin gevondenGebruiker in list)
            {
                if (gevondenGebruiker.Name == naam && gevondenGebruiker.Password == wachtwoord)
                {
                    returnGebruiker = gevondenGebruiker;


                }
            }
            return returnGebruiker;

        }
        public static Manager GetLoggedInManager(string naam, string wachtwoord)
        {
            //Database versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    var query = from Manager in BenKrabbeDBEntities.Managers
            //                where Manager.Name == naam && Manager.Password == wachtwoord
            //                select Manager;
            //    return query.FirstOrDefault();
            //}
            //Json Versie
            List<Manager> list = GetAllManagers();
            Manager returnGebruiker = new Manager();
            foreach (Manager gevondenGebruiker in list)
            {
                if (gevondenGebruiker.Name == naam && gevondenGebruiker.Password == wachtwoord)
                {
                    returnGebruiker = gevondenGebruiker;

                }
            }
            return returnGebruiker;
        }

        //1 record ophalen: Speler, Admin of Manager
        public static Player GetOnePlayer(int pkPlayer)
        {
            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    var query = from Player in BenKrabbeDBEntities.Players
            //                where Player.SpelerID == pkPlayer
            //                select Player;
            //    return query.FirstOrDefault();
            //}
            //JSON Versie
            List<Player> list = GetAllPlayers();
            Player gevondenPlayer = new Player();
            foreach (Player player in list)
            {
                if (player.SpelerID == pkPlayer)
                {
                    gevondenPlayer = player;
                }
            }
            return gevondenPlayer;
        }
        public static Admin GetOneAdmin(int pkAdmin)
        {
            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    var query = from Admin in BenKrabbeDBEntities.Admins
            //                where Admin.AdminID == pkAdmin
            //                select Admin;
            //    return query.FirstOrDefault();
            //}
            //JSON Versie
            List<Admin> list = GetAllAdmins();
            Admin returnGebruiker = new Admin();
            foreach (Admin gevondenGebruiker in list)
            {
                if (gevondenGebruiker.AdminID == pkAdmin)
                {
                    //TODO: Niet blij mee, wou de returnwaarde niet op 52, maar mag niet alleen hier
                    returnGebruiker = gevondenGebruiker;
                }
            }
            return returnGebruiker;
        }
        public static Manager GetOneManager(int pkManager)
        {
            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    var query = from Manager in BenKrabbeDBEntities.Managers
            //                where Manager.ManagerID == pkManager
            //                select Manager;
            //    return query.FirstOrDefault();
            //}
            //Json Versie
            List<Manager> list = GetAllManagers();
            Manager returnGebruiker = new Manager();
            foreach (Manager gevondenGebruiker in list)
            {
                if (gevondenGebruiker.ManagerID == pkManager)
                {
                    returnGebruiker = gevondenGebruiker;
                }
            }
            return returnGebruiker;
        }
        //alle records ophalen: Gamelogs, Players, Admins, Managers
        public static List<GameLogVS> GetAllGameLogVS()
        {            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.GamesLogVS.ToList();
            //}

            //Json Versie

            using (StreamReader r = new StreamReader("../../GameLogVS/GamelogsVS"))
            {
                List<GameLogVS> lijstVS = new List<GameLogVS>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd(); // Tekst inlezen in een string
                lijstVS = JsonSerializer.Deserialize<List<GameLogVS>>(json);
                return lijstVS;
            }
        }
        public static List<GameLogSP> GetAllGameLogSP()
        {            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.GamesLogSP.ToList();
            //}
            //Json Versie
            using (StreamReader r = new StreamReader("../../GameLogVS/GamelogsSP"))
            {
                List<GameLogSP> lijstSP = new List<GameLogSP>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd();
                lijstSP = JsonSerializer.Deserialize<List<GameLogSP>>(json);
                return lijstSP;
            }
        }
        public static List<Player> GetAllPlayersSorted() //haalt spelers op en displayt voorlopig gewonnen games, later aanpassen naar winrate%? of verder filteren?
        {   //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.Players.OrderBy(x => x.VSGamesWon).ToList();
            //}

            //Json Versie
            using (StreamReader r = new StreamReader("../../Players/CurrentPlayers"))
            {
                List<Player> lijstOrderPlayers = new List<Player>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd();
                lijstOrderPlayers = JsonSerializer.Deserialize<List<Player>>(json);
                lijstOrderPlayers.OrderBy(x => x.VSGamesPlayed).ToList();
                return lijstOrderPlayers;
            }
        }
        public static List<GameLogSP> GetAllGameLogSPSorted()
        {   //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.GamesLogSP.OrderBy(x => x.Score).ToList();
            //}
            //JSON Versie
            using (StreamReader r = new StreamReader("../../GameLogSP/GamelogsSP"))
            {
                List<GameLogSP> lijstGameSP = new List<GameLogSP>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd();
                lijstGameSP = JsonSerializer.Deserialize<List<GameLogSP>>(json);
                lijstGameSP.OrderBy(x => x.Score).ToList();
                return lijstGameSP;
            }

        }
        public static List<Player> GetAllPlayers()
        {            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.Players.ToList();
            //}
            //JSON Versie
            using (StreamReader r = new StreamReader("../../Players/CurrentPlayers"))
            {
                List<Player> lijstPlayers = new List<Player>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd();
                lijstPlayers = JsonSerializer.Deserialize<List<Player>>(json);
                return lijstPlayers;
            }

        }
        public static List<Admin> GetAllAdmins()
        {            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.Admins.ToList();
            //}
            //JSON Versie
            using (StreamReader r = new StreamReader("../../Admins/CurrentAdmins"))
            {
                List<Admin> lijstAdmins = new List<Admin>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd();
                lijstAdmins = JsonSerializer.Deserialize<List<Admin>>(json);
                return lijstAdmins;
            }
        }
        public static List<Manager> GetAllManagers()
        {            //Database Versie

            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    return BenKrabbeDBEntities.Managers.ToList();
            //}
            //Json Versie
            using (StreamReader r = new StreamReader("../../Managers/CurrentManagers"))
            {
                List<Manager> lijstManagers = new List<Manager>();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                string json = r.ReadToEnd();
                lijstManagers = JsonSerializer.Deserialize<List<Manager>>(json);
                return lijstManagers;
            }
        }
        //1 record toevoegen: Gamelogs, Players, Manager
        public static bool InsertGameLogVS(GameLogVS aGameLogVS)
        {            //Database Versie

            bool insertSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.GamesLogVS.Add(aGameLogVS);
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        insertSucceeded = true;
            //    }
            //}
            //return insertSucceeded;

            //Json Versie

            List<GameLogVS> listGameLogVS = GetAllGameLogVS();
            listGameLogVS.Add(aGameLogVS);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(listGameLogVS, options);
            File.WriteAllText("../../GamesLogVS/GamelogsVS", json);
            insertSucceeded = true;
            return insertSucceeded;
        }
        public static bool InsertGameLogSP(GameLogSP aGameLogSP)
        {            //Database Versie

            bool insertSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.GamesLogSP.Add(aGameLogSP);
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        insertSucceeded = true;
            //    }
            //}
            //return insertSucceeded;

            //JSon Versie
            List<GameLogSP> listGameLogSP = GetAllGameLogSP();
            listGameLogSP.Add(aGameLogSP);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(listGameLogSP, options);
            File.WriteAllText("../../GameLogSP/GamelogsSP", json);
            insertSucceeded = true;
            return insertSucceeded;

        }
        public static bool InsertPlayer(Player aPlayer)
        {            //Database Versie

            bool insertSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Players.Add(aPlayer);
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        insertSucceeded = true;
            //    }
            //}
            //return insertSucceeded;

            //Json Versie

            List<Player> players = GetAllPlayers();
            players.Add(aPlayer);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(players, options);
            File.WriteAllText("../../Players/CurrentPlayers", json);
            insertSucceeded = true;
            return insertSucceeded;
        }
        public static bool InsertManager(Manager aManager)
        {            //Database Versie

            bool insertSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Managers.Add(aManager);
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        insertSucceeded = true;
            //    }
            //}
            //return insertSucceeded;

            ////Json Versie

            List<Manager> managers = GetAllManagers();

            managers.Add(aManager);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(managers, options);
            File.WriteAllText("../../Managers/CurrentManagers", json);
            insertSucceeded = true;
            return insertSucceeded;
        }
        //1 record wijzigen: Players, Managers
        public static bool UpdatePlayer(Player aPlayer) //TOdo: kweet ff niet hoe het te schrijven
        {            //Database Versie

            bool updateSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Entry(aPlayer).State = System.Data.Entity.EntityState.Modified;
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        updateSucceeded = true;
            //    }
            //}
            //return updateSucceeded;

            //Json Versie

            List<Player> players = GetAllPlayers();
            foreach (Player player in players)
            {
                if (player.Name == aPlayer.Name)
                {
                    player.Password = aPlayer.Password;
                    player.ProfilePicture = aPlayer.ProfilePicture; // TODO: Uitdaging, pfp is een string
                    player.VSGamesPlayed = aPlayer.VSGamesPlayed;
                    player.VSGamesWon = aPlayer.VSGamesWon;
                    player.VSHighscore = aPlayer.VSHighscore;
                    player.SPGamesPlayed = aPlayer.SPGamesPlayed;
                    player.SPGamesWon = aPlayer.SPGamesWon;
                    player.SPHighscore = aPlayer.SPHighscore;
                    updateSucceeded = true;
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(players, options);
            File.WriteAllText("../../Players/CurrentPlayers", json);
            return updateSucceeded;

        }
        public static bool UpdateManager(Manager aManager)
        {            //Database Versie

            bool updateSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Entry(aManager).State = System.Data.Entity.EntityState.Modified;
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        updateSucceeded = true;
            //    }
            //}
            //return updateSucceeded;

            //Json Versie
            List<Manager> managers = GetAllManagers();
            foreach (Manager manager in managers)
            {
                if (manager.Name == aManager.Name)
                {
                    manager.Password = aManager.Password;
                    updateSucceeded = true;
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(managers, options);
            File.WriteAllText("../../Managers/CurrentManagers", json);
            return updateSucceeded;


        }
        //1 record verwijderen: Gamelogs, Players, Manager
        public static bool DeleteGameLogVS(GameLogVS aGameLogVS)
        {            //Database Versie

            bool deleteSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Entry(aGameLogVS).State = System.Data.Entity.EntityState.Deleted;
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        deleteSucceeded = true;
            //    }
            //}
            //return deleteSucceeded;

            //Json

            List<GameLogVS> listGameLogVS = GetAllGameLogVS();
            foreach (GameLogVS gameLogVS in listGameLogVS)
            {
                if (gameLogVS == aGameLogVS)
                {
                    listGameLogVS.Remove(gameLogVS);
                    deleteSucceeded = true;
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(listGameLogVS, options);
            File.WriteAllText("../../GameLogVS/GamesLogVS", json);
            return deleteSucceeded;
        }
        public static bool DeleteGameLogSP(GameLogSP aGameLogSP)
        {            //Database Versie

            bool deleteSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Entry(aGameLogSP).State = System.Data.Entity.EntityState.Deleted;
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        deleteSucceeded = true;
            //    }
            //}
            //return deleteSucceeded;
            //JSON Versie
            List<GameLogSP> listGameLogSP = GetAllGameLogSP();
            foreach (GameLogSP gameLogSP in listGameLogSP)
            {
                if (gameLogSP == aGameLogSP)
                {
                    listGameLogSP.Remove(gameLogSP);
                    deleteSucceeded = true;
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(listGameLogSP, options);
            File.WriteAllText("../../GameLogSP/GamesLogSP", json);
            return deleteSucceeded;
        }
        public static bool DeletePlayer(Player aPlayer)
        {            //Database Versie

            bool deleteSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Entry(aPlayer).State = System.Data.Entity.EntityState.Deleted;
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        deleteSucceeded = true;
            //    }
            //}
            //return deleteSucceeded;
            //Json Versie

            List<Player> players = GetAllPlayers();
            foreach (Player player in players)
            {
                if (player == aPlayer)
                {
                    players.Remove(player);
                    deleteSucceeded = true;
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(players, options);
            File.WriteAllText("../../Players/CurrentPlayers", json);
            return deleteSucceeded;

        }
        public static bool DeleteManager(Manager aManager)
        {            //Database Versie

            bool deleteSucceeded = false;
            //using (var BenKrabbeDBEntities = new BenKrabbeDBEntities())
            //{
            //    BenKrabbeDBEntities.Entry(aManager).State = System.Data.Entity.EntityState.Deleted;
            //    if (0 < BenKrabbeDBEntities.SaveChanges())
            //    {
            //        deleteSucceeded = true;
            //    }
            //}
            //return deleteSucceeded;


            //Json Versie
            List<Manager> managers = GetAllManagers();
            foreach (Manager manager in managers)
            {
                if (manager == aManager)
                {
                    managers.Remove(manager);
                    deleteSucceeded = true;
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(managers, options);
            File.WriteAllText("../../Managers/CurrentManagers", json);
            return deleteSucceeded;
        }
    }
}
