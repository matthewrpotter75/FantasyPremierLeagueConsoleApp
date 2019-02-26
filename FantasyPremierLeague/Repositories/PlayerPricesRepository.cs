using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Linq;

namespace FantasyPremierLeague
{
    public class PlayerPricesRepository : IPlayerPrices
    {
        public bool InsertPlayerPrices(Player player)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(player);
                }

                if (rowsAffected > 0)
                {
                    Logger.Out(player.first_name + " " + player.second_name + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePlayerPrices(Player player)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(player);
                }

                if (rowUpdated == true)
                {
                    Logger.Out(player.first_name + " " + player.second_name + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePlayerPrices(int playerId)
        {
            try
            {
                string playerName;
                bool rowDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    Player player = new Player();
                    player = db.Get<Player>(playerId);
                    playerName = player.first_name + " " + player.second_name;

                    //string querySQL = "SELECT first_name + ' ' + second_name FROM dbo.Players WHERE id = @PlayerId";
                    //playerName = db.Query(querySQL, new { PlayerId = playerId }).SingleOrDefault().ToString();

                    rowDeleted = db.Delete(new Player() { id = playerId });
                }

                if (rowDeleted == true)
                {
                    Logger.Out("Player - " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetAllPlayerPricesIds()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT playerid FROM dbo.PlayerPrices";

                IDataReader reader = db.ExecuteReader(selectQuery);

                List<int> result = ReadList(reader);

                return result;
            }
        }

        public List<int> GetCompletedPlayerIds()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT p.playerid FROM dbo.PlayerPrices p INNER JOIN dbo.PlayerHistory ph ON p.id = ph.playerId INNER JOIN dbo.Gameweeks g ON ph.gameweekId = g.id WHERE g.id = (SELECT TOP 1 id FROM dbo.Gameweeks WHERE deadline_time < GETDATE() ORDER BY deadline_time DESC)";

                IDataReader reader = db.ExecuteReader(selectQuery);

                List<int> result = ReadList(reader);

                return result;
            }
        }

        List<int> ReadList(IDataReader reader)
        {
            List<int> list = new List<int>();
            int column = reader.GetOrdinal("playerid");

            while (reader.Read())
            {
                //check for the null value and than add 
                if (!reader.IsDBNull(column))
                    list.Add(reader.GetInt32(column));
            }

            return list;
        }

        public string GetPlayerName(int playerId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                //string selectQuery = @"SELECT first_name + ' ' + second_name FROM dbo.Players WHERE id = " + playerId.ToString();

                //Player player = new Player();
                var player = db.Get<Player>(playerId);

                string playerName = player.first_name + " " + player.second_name;

                return playerName;
            }
        }
    }
}