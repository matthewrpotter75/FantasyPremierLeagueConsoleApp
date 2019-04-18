using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FantasyPremierLeague
{
    public class PlayerPositionRepository : IPlayerPosition
    {
        public bool InsertPlayerPosition(PlayerPosition playerPosition)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(playerPosition);
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Player Position Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdatePlayerPosition(PlayerPosition playerPosition)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(playerPosition);
                }

                if (rowUpdated == true)
                {
                    Console.WriteLine(playerPosition.singular_name + " (" + playerPosition.singular_name_short + ") - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Player Position Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeletePlayerPosition(int playerPositionId)
        {
            try
            {
                bool rowDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowDeleted = db.Delete(new PlayerPosition() { id = playerPositionId });
                }

                if (rowDeleted == true)
                {
                    Console.WriteLine(Convert.ToString(playerPositionId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Player Position Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllPlayerPositionIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.PlayerPositions";

                    IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Player Position Repository (GetAllPlayerPositionIds): " + ex.Message);
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