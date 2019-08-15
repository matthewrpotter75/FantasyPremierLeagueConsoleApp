using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;

namespace FantasyPremierLeague
{
    public class FixtureStatRepository : IFixtureStat
    {
        public bool InsertFixtureStat(FixtureStat fixtureStat)
        {
            try
            {
                long rowsInserted = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsInserted = db.Insert(fixtureStat);
                }

                if (rowsInserted > 0)
                {
                    //Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - inserted");
                    Logger.Out("Fixture: " + Convert.ToString(fixtureStat.fixtureid) + " Identifier: " + fixtureStat.identifier + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdateFixtureStat(FixtureStat fixtureStat)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(fixtureStat);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - updated");
                    Logger.Out("Fixture: " + Convert.ToString(fixtureStat.fixtureid) + " Identifier: " + fixtureStat.identifier + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteFixtureStat(int fixtureStatId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new FixtureStat() { id = fixtureStatId });
                }

                if (rowsDeleted == true)
                {
                    //Console.WriteLine("Fixture " + Convert.ToString(fixtureId) + " - deleted");
                    //Logger.Out("Fixture: " + Convert.ToString(fixtureStat.fixtureid) + " Identifier: " + fixtureStat.identifier + " - deleted");
                    Logger.Out("FixtureStatId: " + Convert.ToString(fixtureStatId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (delete): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllFixtureIds()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.Fixture";

                    IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (GetAllFixtureIds): " + ex.Message);
                throw ex;
            }
        }

        public bool CheckFixtureStatForFixtureId(int fixtureId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT COUNT(*) FROM dbo.FixtureStats WHERE fixtureId = @FixtureId";

                    int fixtureStatCount = db.Execute(selectQuery, new { FixtureId = fixtureId }, commandTimeout: 300);

                    bool result = false;

                    if (fixtureStatCount > 0)
                    {
                        result = true;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (GetAllFixtureStatIdsForFixtureId): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllFixtureStatIdsForFixtureId(int fixtureId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.FixtureStats WHERE fixtureId = @FixtureId";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { FixtureId = fixtureId }, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (GetAllFixtureStatIdsForFixtureId): " + ex.Message);
                throw ex;
            }
        }

        public List<string> GetAllFixtureStatIdentifiersForFixtureId(int fixtureId)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT DISTINCT identifier FROM dbo.FixtureStats WHERE fixtureId = @FixtureId";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { FixtureId = fixtureId }, commandTimeout: 300);

                    List<string> result = ReadStringList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStat Repository (GetAllFixtureStatIdentifiersForFixtureId): " + ex.Message);
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
            int column = reader.GetOrdinal("identifier");

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