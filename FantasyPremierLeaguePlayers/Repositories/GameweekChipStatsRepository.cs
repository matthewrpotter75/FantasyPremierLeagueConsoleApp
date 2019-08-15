using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;

namespace FantasyPremierLeague
{
    public class GameweekChipStatsRepository : IGameweekChipStats
    {
        public bool InsertGameweekChipStats(GameweekChipStats gameweekChipStats)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(gameweekChipStats);
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("GameweekChipStats Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdateGameweekChipStats(GameweekChipStats gameweekChipStats)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(gameweekChipStats);
                }

                if (rowUpdated == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("GameweekChipStats Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteGameweekChipStats(int gameweekChipStatsId)
        {
            try
            {
                bool rowDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowDeleted = db.Delete(new GameweekChipStats() { id = gameweekChipStatsId });
                }

                if (rowDeleted == true)
                {
                    Console.WriteLine("Gameweek Chip Stat Id:" + Convert.ToString(gameweekChipStatsId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("GameweekChipStats Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllGameweekIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.gameweeks";

                    IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GameweekChipStats Repository (GetAllGameweekIds): " + ex.Message);
                throw ex;
            }
        }

        public List<string> GetAllChipNamesForGameweekId(int gameweekId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT DISTINCT chip_name FROM dbo.GameweekChipStats WHERE gameweekid = @GameweekId";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { GameweekId = gameweekId }, commandTimeout: 300);

                    List<string> result = ReadStringList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GameweekChipStats Repository (GetAllChipNamesForGameweekId): " + ex.Message);
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

        List<string> ReadStringList(IDataReader reader)
        {
            List<string> list = new List<string>();
            int column = reader.GetOrdinal("chip_name");

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