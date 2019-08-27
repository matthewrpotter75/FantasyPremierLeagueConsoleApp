using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;

namespace FantasyPremierLeague
{
    public class FixtureRepository : IFixture
    {
        public bool InsertFixture(Fixture fixture)
        {
            try
            {
                long rowsInserted = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsInserted = db.Insert(fixture);
                }

                if (rowsInserted > 0)
                {
                    //Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - inserted");
                    Logger.Out("Fixture " + Convert.ToString(fixture.id) + " Gameweek " + Convert.ToString(fixture.@event) + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Fixture Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdateFixture(Fixture fixture)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(fixture);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - updated");
                    Logger.Out("Fixture " + Convert.ToString(fixture.id) + " Gameweek " + Convert.ToString(fixture.@event) + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Fixture Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteFixture(int fixtureId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new Fixture() { id = fixtureId });
                }

                if (rowsDeleted == true)
                {
                    //Console.WriteLine("Fixture " + Convert.ToString(fixtureId) + " - deleted");
                    Logger.Out("Fixture " + Convert.ToString(fixtureId) + " Gameweek " + Convert.ToString(fixtureId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Fixture Repository (delete): " + ex.Message);
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
                Logger.Error("Fixture Repository (GetAllFixtureIds): " + ex.Message);
                throw ex;
            }
        }

        //public List<string> GetAllFixtureOpponentShortName(int fixtureId)
        //{
        //    try
        //    {
        //        using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
        //        {
        //            string selectQuery = @"SELECT opponent_short_name FROM dbo.Fixtures WHERE id = " + fixtureId.ToString();

        //            IDataReader reader = db.ExecuteReader(selectQuery, commandTimeout: 300);

        //            List<string> result = ReadStringList(reader);

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Fixture Repository (GetAllFixtureOpponentShortName): " + ex.Message);
        //        throw ex;
        //    }
        //}

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

        //List<string> ReadStringList(IDataReader reader)
        //{
        //    List<string> list = new List<string>();
        //    int column = reader.GetOrdinal("opponent_short_name");

        //    while (reader.Read())
        //    {
        //        //check for the null value and than add 
        //        if (!reader.IsDBNull(column))
        //            list.Add(reader.GetString(column));
        //    }

        //    return list;
        //}
    }
}