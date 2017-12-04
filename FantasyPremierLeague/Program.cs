using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyPremierLeague
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting...");

                //Load Bootstrap data
                Console.WriteLine("Starting Bootstrap data load");
                FantasyPremierLeagueAPIClient.GetPlayerBootstrapDataJson();
                Console.WriteLine("Bootstrap data load complete");
                Console.WriteLine("");

                Console.WriteLine("Starting Player data load");

                string playerName;

                PlayerRepository player = new PlayerRepository();
                List<int> playerIds = player.GetAllPlayerIds();
                List<int> completedPlayerIds = player.GetCompetedPlayerIds();

                List<int> toDoPlayerIds = playerIds.Except(completedPlayerIds).ToList();

                //Remove when finished debugging
                //int playerID = 176;
                //playerName = player.GetPlayerName(playerID);
                //Console.WriteLine(playerName);
                //FantasyPremierLeagueAPIClient.GetPlayerDataJson(playerID);

                //Load player fixture and history data
                foreach (int playerID in toDoPlayerIds)
                {
                    playerName = player.GetPlayerName(playerID);

                    Console.WriteLine(playerName);

                    // Get the fantasyPremierLeaguePl1ayerData using JSON.NET
                    FantasyPremierLeagueAPIClient.GetPlayerDataJson(playerID);

                    Console.WriteLine("");
                }

                Console.WriteLine("Player data load complete");
                Console.WriteLine("");

                //// Wait for user input - keep the program running
                Console.WriteLine("Finished!!!");

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
