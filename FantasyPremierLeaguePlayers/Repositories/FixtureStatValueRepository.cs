using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using DapperExtensions;

namespace FantasyPremierLeague
{
    public class FixtureStatsValuesRepository : IFixtureStatsValue
    {
        public bool InsertFixtureStatsValue(FixtureStatsValue fixtureStatsValue)
        {
            try
            {
                long rowsInserted = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsInserted = db.Insert(fixtureStatsValue);
                }

                if (rowsInserted > 0)
                {
                    //Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - inserted");
                    Logger.Out("Fixture: " + Convert.ToString(fixtureStatsValue.fixtureid) + " Identifier (PlayerStatId): " + Convert.ToString(fixtureStatsValue.playerStatid) + " Element: " + fixtureStatsValue.element + " - inserted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStatsValues Repository (insert): " + ex.Message);
                throw ex;
            }
        }

        public bool UpdateFixtureStatsValue(FixtureStatsValue fixtureStatsValue)
        {
            try
            {
                bool rowsUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsUpdated = db.Update(fixtureStatsValue);
                }

                if (rowsUpdated == true)
                {
                    //Console.WriteLine("Fixture Gameweek " + Convert.ToString(fixture.@event) + " - updated");
                    Logger.Out("Fixture: " + Convert.ToString(fixtureStatsValue.fixtureid) + " Identifier (PlayerStatId): " + Convert.ToString(fixtureStatsValue.playerStatid) + " Element: " + fixtureStatsValue.element + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStatsValues Repository (update): " + ex.Message);
                throw ex;
            }
        }

        public bool DeleteFixtureStatsValue(int fixtureStatsValueId)
        {
            try
            {
                bool rowsDeleted = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsDeleted = db.Delete(new FixtureStatsValue() { id = fixtureStatsValueId });
                }

                if (rowsDeleted == true)
                {
                    //Console.WriteLine("Fixture " + Convert.ToString(fixtureId) + " - deleted");
                    //Logger.Out("Fixture: " + Convert.ToString(fixtureStat.fixtureid) + " Identifier: " + fixtureStat.identifier + " - deleted");
                    Logger.Out("FixtureStatsValueId: " + Convert.ToString(fixtureStatsValueId) + " - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStatsValues Repository (delete): " + ex.Message);
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
                Logger.Error("FixtureStatsValues Repository (GetAllFixtureIds): " + ex.Message);
                throw ex;
            }
        }

        public List<int> GetAllPlayerIdsForFixtureIdAndIdentifier(int fixtureId, int playerStatId, bool isHome)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT playerid AS id FROM dbo.FixtureStatsValues fsv INNER JOIN dbo.FixtureStats fs ON fsv.fixtureStatid = fs.id WHERE fsv.fixtureid = @FixtureId AND fsv.playerStatid = @PlayerStatId AND fsv.isHome = @IsHome";

                    IDataReader reader = db.ExecuteReader(selectQuery, new { FixtureId = fixtureId, PlayerStatId = playerStatId, IsHome = isHome }, commandTimeout: 300);

                    List<int> result = ReadList(reader);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStatsValues Repository (GetAllPlayerIdsForFixtureIdAndIdentifier): " + ex.Message);
                throw ex;
            }
        }

        public int GetPlayerStatIdForCurrentIdentifier(string identifier)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.PlayerStats WHERE playerStatName = @Identifier";

                    using (var dbCommand = new SqlCommand(selectQuery, db))
                    {
                        db.Open();

                        //string selectQuery = @"SELECT id FROM dbo.PlayerStats WHERE playerStatName = '" + identifier + "'";

                        //var result = db.Query(selectQuery, new { Identifier = identifier }, commandTimeout: 300);

                        //var predicate = Predicates.Field<PlayerStats>(f => f.name, Operator.Eq, identifier);
                        //PlayerStats playerStat = db.Get<PlayerStats>(predicate);
                        //int result = playerStat.id;
                        //int result = db.Get<PlayerStats>(predicate).id;

                        //var playerStat = db.Get<PlayerStats>(selectQuery, commandTimeout: 300);

                        SqlParameter parameter = new SqlParameter("@Identifier", SqlDbType.VarChar, 50);
                        parameter.Value = identifier;
                        dbCommand.Parameters.Add(parameter);

                        int result = (int)dbCommand.ExecuteScalar();
                        //int result = (int)db.ExecuteScalar(selectQuery);

                        db.Close();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStatsValues Repository (GetPlayerStatIdForCurrentIdentifier): " + ex.Message);
                throw ex;
            }
        }

        public int GetFixtureStatIdForCurrentIdentifierAndFixture(int fixtureid, string identifier)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    string selectQuery = @"SELECT id FROM dbo.FixtureStats WHERE fixtureid = @FixtureId AND identifier = @Identifier";

                    using (var dbCommand = new SqlCommand(selectQuery, db))
                    {
                        db.Open();

                        SqlParameter parameter0 = new SqlParameter("@FixtureId", SqlDbType.Int);
                        SqlParameter parameter1 = new SqlParameter("@Identifier", SqlDbType.VarChar, 50);
                        parameter0.Value = fixtureid;
                        parameter1.Value = identifier;
                        dbCommand.Parameters.Add(parameter0);
                        dbCommand.Parameters.Add(parameter1);

                        int result = (int)dbCommand.ExecuteScalar();

                        db.Close();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FixtureStatsValues Repository (GetPlayerStatIdForCurrentIdentifier): " + ex.Message);
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