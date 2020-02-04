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
    public class PlayerHistoryRepository : IPlayerHistory
    {
        public bool InsertPlayerHistory(PlayerHistory playerHistory)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(playerHistory);
                }

                if (rowsAffected > 0)
                {
                    //Console.WriteLine("History Gameweek " + Convert.ToString(history.round) + " - inserted");
                    Logger.Out("PlayerHistory Gameweek " + Convert.ToString(playerHistory.round) + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdatePlayerHistory(PlayerHistory playerHistory)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(playerHistory);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("History Gameweek " + Convert.ToString(history.round) + " - updated");
                    Logger.Out("PlayerHistory Gameweek " + Convert.ToString(playerHistory.round) + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeletePlayerHistory(int playerHistoryId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new PlayerHistory() { id = playerHistoryId });
                }

                if (rowsDeleted == true)
                {
                    //Console.WriteLine("History " + Convert.ToString(historyId) + " - deleted");
                    Logger.Out("PlayerHistory Gameweek " + Convert.ToString(playerHistoryId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteAllPlayerHistoryForPlayerId(int playerId)
        {
            try
            {
                string playerName;
                int rowsDeleted;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    //rowsDeleted = db.Delete(new History() { element = playerId });
                    string deleteQuery = "DELETE FROM dbo.PlayerHistory WHERE playerId = @PlayerId;";
                    rowsDeleted = db.Execute(deleteQuery, new { PlayerId = playerId });

                    var player = db.Get<Player>(playerId);
                    playerName = player.first_name + " " + player.second_name;
                }

                if (rowsDeleted > 0)
                {
                    Console.WriteLine("PlayerHistory: " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (DeleteAllPlayerHistoryForPlayerId): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllPlayerHistoryPlayerIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT playerid AS id FROM dbo.PlayerHistory";

                    IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (GetAllPlayerHistoryPlayerIds): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetPlayerHistoryGameweekIdsForPlayerId(int playerId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT gameweekid AS id FROM dbo.PlayerHistory WHERE playerid = @PlayerId;";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { PlayerId = playerId }, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (GetPlayerHistoryGameweekIdsForPlayerId): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetPlayerHistoryFixtureIdsForPlayerId(int playerId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT fixtureid AS id FROM dbo.PlayerHistory WHERE playerid = @PlayerId;";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { PlayerId = playerId }, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistory Repository (GetPlayerHistoryFixtureIdsForPlayerId): " + ex.Message);
                throw ex;
            }
        }


        List<int> ReadList(IDataReader reader)
        {
            List<int> list = new List<int>();
            int column = reader.GetOrdinal("id");

            while (reader.Read())
            {
                //check for the null value and than add 
                if (!reader.IsDBNull(column))
                    list.Add(reader.GetInt32(column));
            }

            return list;
        }
    }
}