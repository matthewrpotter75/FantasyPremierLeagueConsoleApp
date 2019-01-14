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
    public class HistoryPastRepository : IHistoryPast
    {
        public bool InsertHistoryPast(HistoryPast historyPast)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(historyPast);
                }

                if (rowsAffected > 0)
                {
                    //Console.WriteLine("History Past " + historyPast.season_name + " - inserted");
                    Logger.Out("History Past " + historyPast.season_name + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Past Repository (insert): " + ex.Message);
                throw new Exception("Insert History Past exception", ex);
            }
        }

        public bool UpdateHistoryPast(HistoryPast historyPast)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(historyPast);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("History Past " + historyPast.season_name + " - updated");
                    Logger.Out("History Past " + historyPast.season_name + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Past Repository (update): " + ex.Message);
                throw new Exception("Update History Past exception", ex);
            }
        }

        public bool DeleteHistoryPast(int historyPastId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new HistoryPast() { id = historyPastId });
                }

                if (rowsDeleted == true)
                {
                    Console.WriteLine("History " + historyPastId + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("History Past Repository (delete): " + ex.Message);
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
                    string deleteQuery = "DELETE FROM dbo.HistoryPast WHERE playerId = @PlayerId;";
                    rowsDeleted = db.Execute(deleteQuery, new { PlayerId = playerId });

                    var player = db.Get<Player>(playerId);
                    playerName = player.first_name + " " + player.second_name;
                }

                if (rowsDeleted > 0)
                {
                    //Console.WriteLine("History Past - " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    Logger.Out("History Past - " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
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

        public List<int> GetAllHistoryPastIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.HistoryPast";

                    IDataReader reader = db.ExecuteReader(selectQuery);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("History Past Repository (GetAllHistoryPastIds): " + ex.Message);
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