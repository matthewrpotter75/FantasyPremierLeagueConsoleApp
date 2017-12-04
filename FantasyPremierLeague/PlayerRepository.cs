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
    public class PlayerRepository : IPlayer
    {
        public bool InsertPlayer(Player player)
        {
            try
            {
                long rowsAffected = 0;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowsAffected = db.Insert(player);
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

        public bool UpdatePlayer(Player player)
        {
            try
            {
                bool rowUpdated = false;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    rowUpdated = db.Update(player);
                    db.Execute("UPDATE dbo.Players SET chance_of_playing_next_round = 0 WHERE chance_of_playing_next_round IS NULL;");
                }

                if (rowUpdated == true)
                {
                    Console.WriteLine(player.first_name + " " + player.second_name + " - updated");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePlayer(int playerId)
        {
            try
            {
                string playerName;
                //bool rowDeleted = false;
                int rowsDeleted;

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
                {
                    Player player = new Player();
                    player = db.Get<Player>(playerId);
                    playerName = player.first_name + " " + player.second_name;

                    //string querySQL = "SELECT first_name + ' ' + second_name FROM dbo.Players WHERE id = @PlayerId";
                    //playerName = db.Query(querySQL, new { PlayerId = playerId }).SingleOrDefault().ToString();

                    //rowDeleted = db.Delete(new Player() { id = playerId });
                    string deleteQuery = "DELETE FROM dbo.Players WHERE id = @PlayerId;";
                    rowsDeleted = db.Execute(deleteQuery, new { PlayerId = playerId });
                }

                if (rowsDeleted > 0)
                {
                    Console.WriteLine("Player - " + playerName + "(" + Convert.ToString(playerId) + ") - deleted");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetAllPlayerIds()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT id FROM dbo.Players";

                IDataReader reader = db.ExecuteReader(selectQuery);

                List<int> result = ReadList(reader);

                return result;
            }
        }

        public List<int> GetCompetedPlayerIds()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                string selectQuery = @"SELECT p.id FROM dbo.Players p INNER JOIN dbo.PlayerHistory ph ON p.id = ph.playerId INNER JOIN dbo.Gameweeks g ON ph.gameweekId = g.id WHERE g.id = (SELECT TOP 1 id FROM dbo.Gameweeks WHERE deadline_time < GETDATE() ORDER BY deadline_time DESC)";

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

        public string GetPlayerName(int playerId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["FantasyPremierLeague"].ConnectionString))
            {
                //string selectQuery = @"SELECT first_name + ' ' + second_name FROM dbo.Players WHERE id = " + playerId.ToString();

                //Player player = new Player();
                var player = db.Get<Player>(playerId);

                string playerName = player.first_name + " " + player.second_name;

                return playerName;
            }
        }
    }
}