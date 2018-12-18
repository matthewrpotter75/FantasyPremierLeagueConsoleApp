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
        public bool InsertFixture(Fixture2 fixture)
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
                    Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateFixture(Fixture2 fixture)
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
                    Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
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
                    rowsDeleted = db.Delete(new Fixture2() { id = fixtureId });
                }

                if (rowsDeleted == true)
                {
                    Console.WriteLine("Fixture " + Convert.ToString(fixtureId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetAllFixtureIds()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT id FROM dbo.fixtures";

                IDataReader reader = db.ExecuteReader(selectQuery);

                List<int> result = ReadList(reader);

                return result;
            }
        }

        public List<string> GetAllFixtureOpponentShortName(int fixtureId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT opponent_short_name FROM dbo.Fixtures WHERE id = " + fixtureId.ToString();

                IDataReader reader = db.ExecuteReader(selectQuery);

                List<string> result = ReadStringList(reader);

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

        List<string> ReadStringList(IDataReader reader)
        {
            List<string> list = new List<string>();
            int column = reader.GetOrdinal("opponent_short_name");

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