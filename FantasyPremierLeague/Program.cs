using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace FantasyPremierLeague
{
    class Program
    {
        static void Main(string[] args)
        {            
            try
            {
                TextWriter tmp = Console.Out;
                Console.SetOut(tmp);
                Console.WriteLine("Starting...");

                string filePath = ReadSetting("logFilePath");

                if (filePath.Substring(filePath.Length - 1, 1) != "\\")
                {
                    filePath = filePath + "\\";
                }

                string timestamp = DateTime.Now.ToString("yyyymmdd");

                filePath = filePath + "FantasyPremierLeage" + timestamp + ".log";

                FileStream filestream = new FileStream(filePath, FileMode.Create);
                StreamWriter streamwriter = new StreamWriter(filestream);
                streamwriter.AutoFlush = true;
                Console.SetOut(streamwriter);
                Console.SetError(streamwriter);

                Console.WriteLine("Starting...");

                //Load Bootstrap data
                Console.WriteLine("Starting Bootstrap data load");
                //Logger.Out("Starting Bootstrap data load");

                FantasyPremierLeagueAPIClient.GetPlayerBootstrapDataJson();

                Console.WriteLine("Bootstrap data load complete");
                Console.WriteLine("");

                Console.WriteLine("Starting Player data load");

                string playerName;

                PlayerRepository player = new PlayerRepository();
                List<int> playerIds = player.GetAllPlayerIds();
                List<int> completedPlayerIds = player.GetCompetedPlayerIds();

                //Only process unprocessed players
                //List<int> toDoPlayerIds = playerIds.Except(completedPlayerIds).ToList();

                //Process all players
                List<int> toDoPlayerIds = playerIds;

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

                filestream.Dispose();

                Console.SetOut(tmp);
                Console.WriteLine("Finished!!!");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                string result = appSettings[key] ?? "Not Found";

                 return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                throw;
            }
        }

        public class Logger
        {
            //public static StringBuilder LogString = new StringBuilder();
            //public static void Out(string str)
            //{
            //    Console.WriteLine(str);
            //    LogString.Append(str).Append(Environment.NewLine);
            //}

            public static void Out(string filePath)
            {
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;

                try
                {
                    ostrm = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open " + filePath + " for writing");
                    Console.WriteLine(e.Message);
                    return;
                }

                Console.SetOut(writer);
                //Console.WriteLine("This is a line of text");
                //Console.WriteLine("Everything written to Console.Write() or");
                //Console.WriteLine("Console.WriteLine() will be written to a file");
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
                //Console.WriteLine("Done");
            }

        }
    }
}