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
    public class HistoryRepository : IHistory
    {
        public bool InsertHistory(History history)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(history);
                }

                if (rowsAffected > 0)
                {
                    //Console.WriteLine("History Gameweek " + Convert.ToString(history.round) + " - inserted");
                    Logger.Out("History Gameweek " + Convert.ToString(history.round) + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdateHistory(History history)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(history);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("History Gameweek " + Convert.ToString(history.round) + " - updated");
                    Logger.Out("History Gameweek " + Convert.ToString(history.round) + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteHistory(int historyId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new History() { id = historyId });
                }

                if (rowsDeleted == true)
                {
                    //Console.WriteLine("History " + Convert.ToString(historyId) + " - deleted");
                    Logger.Out("History Gameweek " + Convert.ToString(historyId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteAllPlayerHistory(int playerId)
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
                    Console.WriteLine("Player's history " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Past Repository (DeleteAllHistoryPast): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllHistoryIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.PlayerHistory";

                    IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("History Past Repository (GetAllHistoryIds): " + ex.Message);
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