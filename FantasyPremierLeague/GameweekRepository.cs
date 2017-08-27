using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;

namespace FantasyPremierLeague
{
    public class GameweekRepository : IGameweek
    {
        public bool InsertGameweek(Gameweek gameweek)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(gameweek);
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateGameweek(Gameweek gameweek)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(gameweek);
                }

                if (rowUpdated == true)
                {
                    Console.WriteLine(gameweek.name + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGameweek(int gameweekId)
        {
            try
            {
                bool rowDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowDeleted = db.Delete(new Gameweek() { id = gameweekId });
                }

                if (rowDeleted == true)
                {
                    Console.WriteLine("Gameweek" + Convert.ToString(gameweekId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetAllGameweekIds()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT id FROM dbo.gameweeks";

                IDataReader reader = db.ExecuteReader(selectQuery);

                List<int> result = ReadList(reader);

                return result;
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