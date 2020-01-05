using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using log4net;
using log4net.Config;

namespace FantasyPremierLeague
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            try
            {
                Logger.Out("Starting...");
                Logger.Out("");

                //Load Bootstrap data
                //Console.WriteLine("Starting Bootstrap data load");
                Logger.Out("Starting Bootstrap data load");
                Logger.Out("");

                FantasyPremierLeagueAPIClient.GetPlayerBootstrapDataJson();

                Logger.Out("Bootstrap data load complete");
                Logger.Out("");

                Logger.Out("Starting Player data load");
                Logger.Out("");

                FantasyPremierLeagueAPIClient.GetPlayerData();

                Logger.Out("Player data load complete");
                Logger.Out("");

                //Logger.Out("Starting Fixtures data load");
                //Logger.Out("");

                //FantasyPremierLeagueAPIClient.GetFixtureDataJson();

                //Logger.Out("Fixtures data load complete");
                //Logger.Out("");

                //Logger.Out("Finished!!!");

                //// Wait for user input - keep the program running
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }

        //public static string ReadSetting(string key)
        //{
        //    try
        //    {
        //        var appSettings = ConfigurationManager.AppSettings;

        //        string result = appSettings[key] ?? "Not Found";

        //        return result;
        //    }
        //    catch (ConfigurationErrorsException ex)
        //    {
        //        Console.WriteLine("Error reading app settings");
        //        Logger.Error("Error reading app settings: " + ex.Message);
        //        throw;
        //    }
        //}

        public static NameValueCollection ConfigSettings(string section)
        {
            try
            {
                var configSettings = ConfigurationManager.GetSection(section) as NameValueCollection;
                String[] configStrings = new String[configSettings.Count];

                // Copies the values to a string array and displays the string array.
                configSettings.CopyTo(configStrings, 0);

                return configSettings;

                //foreach (var key in configSettings.AllKeys)
                //{
                //    Console.WriteLine(key + " = " + applicationSettings[key]);
                //}
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                throw;
            }
        }

        public static NameValueCollection AppSettings()
        {
            NameValueCollection appSettings = new NameValueCollection();

            //Read the filepath from the App.Settings
            appSettings = ConfigSettings("appSettings");
            string filePath = appSettings["logFilePath"];
            return appSettings;
        }
    }

    public static class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Out(string LogText)
        {
            try
            {
                // do the actual work
                _log.Info(LogText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Error(string LogText)
        {
            try
            {
                // do the actual work
                _log.Error(LogText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}