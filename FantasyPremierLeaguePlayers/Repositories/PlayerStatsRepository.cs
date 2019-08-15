using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;

namespace FantasyPremierLeague
{
    public class PlayerStatsRepository : IPlayerStat
    {
        public bool InsertPlayerStat(PlayerStats playerStats)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(playerStats);
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerStats Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdatePlayerStat(PlayerStats playerStats)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(playerStats);
                }

                if (rowUpdated == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerStats Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeletePlayerStat(int playerStatId)
        {
            try
            {
                bool rowDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowDeleted = db.Delete(new PlayerStats() { id = playerStatId });
                }

                if (rowDeleted == true)
                {
                    Logger.Out(Convert.ToString(playerStatId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerStat Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public List<string> GetAllPlayerStatNames()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT playerStatName AS name FROM dbo.PlayerStats";

                    IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

                    List<string> result = ReadStringList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PlayerStats Repository (GetAllPlayerStatNames): " + ex.Message);
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
            int column = reader.GetOrdinal("name");

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
