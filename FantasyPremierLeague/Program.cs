using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting...");

                ////Load Bootstrap data
                Console.WriteLine("Starting Bootstrap data load");
                FantasyPremierLeagueAPIClient.GetPlayerBootstrapDataJson();

                //Console.WriteLine("Starting Player data load");

                //int playerID = 176;
                string playerName;

                PlayerRepository player = new PlayerRepository();
                List<int> playerIds = player.GetAllPlayerIds();

                ////Load player fixture and history data
                foreach (int playerID in playerIds)
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
