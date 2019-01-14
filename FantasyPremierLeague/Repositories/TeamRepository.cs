using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FantasyPremierLeague
{
    public class TeamRepository : ITeam
    {
        public bool InsertTeam(Team team)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(team);
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Team Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdateTeam(Team team)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(team);
                }

                if (rowUpdated == true)
                {
                    Console.WriteLine(team.name + " (" + Convert.ToString(team.code) + ") - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Team Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteTeam(int teamcode)
        {
            try
            {
                bool rowDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowDeleted = db.Delete(new Team() { code = teamcode});
                }

                    if (rowDeleted == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Team Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllTeamIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.Teams";

                    IDataReader reader = db.ExecuteReader(selectQuery);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (SqlException ex)
            {
                Logger.Error("Team Repository (GetAllTeamIds): " + ex.Message);
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