using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public class ConfigSettings
    {
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                string result = appSettings[key] ?? "Not Found";

                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("Error reading app settings");
                Logger.Error("Error reading app settings: " + ex.Message);
                throw;
            }
        }
    }
}
