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
    public class PlayerHistoryPastRepository : IPlayerHistoryPast
    {
        public bool InsertPlayerHistoryPast(PlayerHistoryPast playerHistoryPast)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(playerHistoryPast);
                }

                if (rowsAffected > 0)
                {
                    //Console.WriteLine("History Past " + historyPast.season_name + " - inserted");
                    Logger.Out("PlayerHistoryPast: " + playerHistoryPast.season_name + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistoryPast Repository (insert): " + ex.Message);
                throw new Exception("Insert PlayerHistoryPast exception", ex);
            }
        }

        public bool UpdatePlayerHistoryPast(PlayerHistoryPast playerHistoryPast)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(playerHistoryPast);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("History Past " + historyPast.season_name + " - updated");
                    Logger.Out("PlayerHistoryPast: " + playerHistoryPast.season_name + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistoryPast Repository (update): " + ex.Message);
                throw new Exception("Update PlayerHistoryPast exception", ex);
            }
        }

        public bool DeletePlayerHistoryPast(int playerHistoryPastId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new PlayerHistoryPast() { id = playerHistoryPastId });
                }

                if (rowsDeleted == true)
                {
                    Console.WriteLine("PlayerHistoryPast: " + playerHistoryPastId + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistoryPast Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteAllHistoryPast(int playerId)
        {
            try
            {
                string playerName;
                int rowsDeleted;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    //rowsDeleted = db.Delete(new History() { element = playerId });
                    string deleteQuery = "DELETE FROM dbo.PlayerHistoryPast WHERE playerId = @PlayerId;";
                    rowsDeleted = db.Execute(deleteQuery, new { PlayerId = playerId });

                    var player = db.Get<Player>(playerId);
                    playerName = player.first_name + " " + player.second_name;
                }

                if (rowsDeleted > 0)
                {
                    //Console.WriteLine("History Past - " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    Logger.Out("PlayerHistoryPast: " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistoryPast Repository (DeleteAllHistoryPast): " + ex.Message);
                throw ex;
            }
        }

        public List<string> GetAllPlayerHistoryPastSeasons(int playerId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT season_name FROM dbo.PlayerHistoryPast WHERE playerid = @PlayerId";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { PlayerId = playerId }, commandTimeout: 300);

                    List<string> result = ReadStringList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerHistoryPast Repository (GetAllPlayerHistoryPastSeasons): " + ex.Message);
                throw ex;
            }
        }

        List<string> ReadStringList(IDataReader reader)
        {
            List<string> list = new List<string>();
            int column = reader.GetOrdinal("season_name");

            while (reader.Read())
            {
                //check for the null value and than add 
                if (!reader.IsDBNull(column))
                    list.Add(reader.GetString(column));
            }

            return list;
        }
    }
}