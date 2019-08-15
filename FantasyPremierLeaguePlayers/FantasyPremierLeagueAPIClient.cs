using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace FantasyPremierLeague
{
    public class FantasyPremierLeagueAPIClient
    {
        #region fields

        /// <summary>
        /// Base URL for the FantasyPremierLeaguePlayerData Endpoint URL
        /// </summary>
        //private const string baseUrl = "https://fantasy.premierleague.com/api/element-summary/{0}";
        //private const string baseBootstrapUrl = "https://fantasy.premierleague.com/api/bootstrap-static";

        #endregion

        #region methods

        public static void GetPlayerBootstrapDataJson()
        {
            try
            {
                string baseBootstrapUrl = ConfigSettings.ReadSetting("bootstrapURL");

                //PlayerModelMappings.Initialize();
                //GameweekModelMappings.Initialize();

                TeamRepository teamRepository = new TeamRepository();

                HttpClient client = new HttpClient();
                JsonSerializer serializer = new JsonSerializer();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (Stream s = client.GetStreamAsync(baseBootstrapUrl).Result)
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    // read the json from a stream
                    // json size doesn't matter because only a small piece is read at a time from the HTTP request
                    var fantasyPremierLeagueBootstrapData = serializer.Deserialize<FantasyPremierLeagueBootstrapData>(reader);

                    //Load team data
                    Logger.Out("Starting Teams load");

                    List<int> teamIds = teamRepository.GetAllTeamIds();

                    foreach (Team team in fantasyPremierLeagueBootstrapData.teams)
                    {
                        if (!teamIds.Contains(team.id))
                        {
                            teamRepository.InsertTeam(team);
                            Logger.Out(team.name + " (" + Convert.ToString(team.code) + ") - inserted");
                        }
                        //else
                        //{
                        //    teamRepository.UpdateTeam(team);
                        //    Logger.Out(team.name + " (" + Convert.ToString(team.code) + ") - updated");
                        //}
                    }

                    Logger.Out("Teams load complete");
                    Logger.Out("");

                    //Load player position data
                    Logger.Out("Starting Player Positions load");

                    PlayerPositionRepository playerPositionRepository = new PlayerPositionRepository();

                    List<int> playerPositionIds = playerPositionRepository.GetAllPlayerPositionIds();

                    foreach (PlayerPosition playerPosition in fantasyPremierLeagueBootstrapData.element_types)
                    {
                        if (!playerPositionIds.Contains(playerPosition.id))
                        {
                            playerPositionRepository.InsertPlayerPosition(playerPosition);
                            Logger.Out(playerPosition.singular_name + " (" + playerPosition.singular_name_short + ") - inserted");
                        }
                        //else
                        //{
                        //    playerPositionRepository.UpdatePlayerPosition(playerPosition);
                        //    Logger.Out(playerPosition.singular_name + " (" + playerPosition.singular_name_short + ") - updated");
                        //}
                    }

                    Logger.Out("Player Positions load complete");
                    Logger.Out("");

                    //Load player data
                    Logger.Out("Starting Players load");

                    PlayerRepository playerRepository = new PlayerRepository();
                    PlayerPricesRepository playerPricesRepository = new PlayerPricesRepository();

                    List<int> playerIds = playerRepository.GetAllPlayerIds();

                    //Delete player history and player records for players no longer in the Premier League
                    List<int> inputPlayerIds = fantasyPremierLeagueBootstrapData.elements.Select(x => x.id).ToList();
                    List<int> playersToDelete = playerIds.Except(inputPlayerIds).ToList();
                    HistoryRepository historyRepository = new HistoryRepository();

                    foreach (int playerId in playersToDelete)
                    {
                        historyRepository.DeleteAllPlayerHistory(playerId);
                        playerRepository.DeletePlayer(playerId);
                        //playerPricesRepository.DeletePlayerPrices(playerId);
                    }

                    //Insert and update players data
                    foreach (Player player in fantasyPremierLeagueBootstrapData.elements)
                    {
                        if (!playerIds.Contains(player.id))
                        {
                            playerRepository.InsertPlayer(player);
                            //playerPricesRepository.InsertPlayerPrices(player);
                        }
                        else
                        {
                            playerRepository.UpdatePlayer(player);
                            //playerPricesRepository.UpdatePlayerPrices(player);
                        }
                    }

                    Logger.Out("Players load complete");
                    Logger.Out("");

                    //Load gameweek data
                    Logger.Out("Starting Gameweeks load");

                    GameweekRepository gameweekRepository = new GameweekRepository();
                    GameweekChipStatsRepository gameweekChipStatsRepository = new GameweekChipStatsRepository();

                    int gameweekid = 0;
                    List<int> gameweekIds = gameweekRepository.GetAllGameweekIds();

                    foreach (Gameweek gameweek in fantasyPremierLeagueBootstrapData.events)
                    {
                        gameweekid = gameweek.id;

                        if (!gameweekIds.Contains(gameweek.id))
                        {
                            gameweekRepository.InsertGameweek(gameweek);
                            Logger.Out(gameweek.name + " - inserted");
                        }
                        //else
                        //{
                        //    gameweekRepository.UpdateGameweek(gameweek);
                        //    Logger.Out(gameweek.name + " - updated");
                        //}

                        List<string> gameweekChipNames = gameweekChipStatsRepository.GetAllChipNamesForGameweekId(gameweekid);

                        foreach (GameweekChipStats gameweekChipStats in gameweek.chip_plays)
                        {
                            gameweekChipStats.gameweekid = gameweek.id;

                            if (!gameweekChipNames.Contains(gameweekChipStats.chip_name))
                            {
                                gameweekChipStatsRepository.InsertGameweekChipStats(gameweekChipStats);
                                Logger.Out(gameweek.name + " Chip Name:" + gameweekChipStats.chip_name + " - inserted");
                            }
                            //else
                            //{
                            //    gameweekChipStatsRepository.UpdateGameweekChipStats(gameweekChipStats);
                            //    Logger.Out(gameweek.name + " Chip Name:" + gameweekChipStats.chip_name + " - updated");
                            //}
                        }
                    }

                    Logger.Out("Gameweeks load complete");
                    Logger.Out("");

                    //Load Stat data
                    Logger.Out("Starting PlayerStats load");

                    PlayerStatsRepository playerStatRepository = new PlayerStatsRepository();
                    List<string> statnames = playerStatRepository.GetAllPlayerStatNames();

                    foreach (PlayerStats playerStats in fantasyPremierLeagueBootstrapData.element_stats)
                    {
                        if (!statnames.Contains(playerStats.name))
                        {
                            playerStatRepository.InsertPlayerStat(playerStats);
                            Logger.Out("PlayerStats:" + playerStats.name + " - inserted");
                        }
                        //else
                        //{
                        //    playerStatRepository.UpdatePlayerStat(playerStats);
                        //    Logger.Out("PlayerStats:" + playerStats.name + " - updated");
                        //}
                    }

                    Logger.Out("PlayerStats load complete");
                    Logger.Out("");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Bootstrap data exception: " + ex.Message);
                throw new Exception("Bootstrap data exception: " + ex.Message);
            }
        }

        public static void GetPlayerDataJson(int playerID)
        {
            try
            {
                string playerUrl = ConfigSettings.ReadSetting("playerURL");

                //playerID = 188;
                // Customize URL according to playerID parameter
                var url = string.Format(playerUrl, playerID);

                HttpClient client = new HttpClient();
                JsonSerializer serializer = new JsonSerializer();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (Stream s = client.GetStreamAsync(url).Result)
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    // read the json from a stream
                    // json size doesn't matter because only a small piece is read at a time from the HTTP request
                    var fantasyPremierLeaguePlayerData = serializer.Deserialize<FantasyPremierLeaguePlayerData>(reader);
                    //var fantasyPremierLeaguePlayerData = JsonConvert.DeserializeObject< FantasyPremierLeaguePlayerData>(reader);

                    //Load player history data
                    HistoryRepository historyRepository = new HistoryRepository();

                    List<int> historyIds = historyRepository.GetAllHistoryIds();

                    foreach (History history in fantasyPremierLeaguePlayerData.history)
                    {
                        if (!historyIds.Contains(history.id))
                        {
                            historyRepository.InsertHistory(history);
                            //Logger.Out("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                        }
                        //else
                        //{
                        //    historyRepository.UpdateHistory(history);
                        //    //Logger.Out("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        //    //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        //}
                    }

                    //Load player fixtures data
                    //FixtureRepository fixtureRepository = new FixtureRepository();

                    //List<int> fixtureIds = fixtureRepository.GetAllFixtureIds();
                    //List<string> opponentShortNames;

                    //foreach (Fixture2 fixture in fantasyPremierLeaguePlayerData.fixtures)
                    //{
                    //    //fixture.is_homeINT = Convert.ToInt32(fixture.is_home);
                    //    opponentShortNames = fixtureRepository.GetAllFixtureOpponentShortName(fixture.id);
                    //    if (!fixtureIds.Contains(fixture.id)) //|| !opponentShortNames.Contains(fixture.opponent_short_name))
                    //    {
                    //        fixtureRepository.InsertFixture(fixture);
                    //        //Logger.Out("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                    //        //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                    //    }
                    //    else
                    //    {
                    //        fixtureRepository.UpdateFixture(fixture);
                    //        //Logger.Out("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                    //        //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                    //    }
                    //}

                    //Logger.Out("");

                    //Load history past data
                    HistoryPastRepository historyPastRepository = new HistoryPastRepository();

                    List<int> historyPastIds = historyPastRepository.GetAllHistoryPastIds();

                    foreach (HistoryPast historyPast in fantasyPremierLeaguePlayerData.history_past)
                    {
                        historyPast.playerId = playerID;
                        if (!historyPastIds.Contains(historyPast.id))
                        {
                            historyPastRepository.InsertHistoryPast(historyPast);
                            //Logger.Out("PlayerId(" + Convert.ToString(playerID) + ") - inserted");
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                        }
                        //else
                        //{
                        //    historyPastRepository.UpdateHistoryPast(historyPast);
                        //    //Logger.Out("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        //    //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Player data exception (PlayerId: " + playerID.ToString() + "): " + ex.Message);
                //throw new Exception("Player data exception (PlayerId: " + playerID.ToString() + "): " + ex.Message);
                GetPlayerDataJson(playerID);
            }
        }

        public static void GetPlayerData()
        {
            string playerName = "";

            try
            {
                //int playerIdforDebug;

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

                    Logger.Out(playerName);

                    // Get the fantasyPremierLeaguePl1ayerData using JSON.NET
                    FantasyPremierLeagueAPIClient.GetPlayerDataJson(playerID);

                    Logger.Out("");
                }

            }
            catch (Exception ex)
            {
                //Logger.Error("Player data exception (PlayerId: " + playerID.ToString() + "): " + ex.Message);
                Logger.Error("Player data exception" + playerName + " caused error!!!");
                throw new Exception("GetPlayerData data exception: " + ex.Message);
            }
        }

        public static void GetFixtureDataJson()
        {
            try
            {
                string fixturesUrl = ConfigSettings.ReadSetting("fixturesURL");

                HttpClient client = new HttpClient();
                JsonSerializer serializer = new JsonSerializer();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (Stream s = client.GetStreamAsync(fixturesUrl).Result)
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    // read the json from a stream
                    // json size doesn't matter because only a small piece is read at a time from the HTTP request
                    var fantasyPremierLeagueFixtureData = serializer.Deserialize<List<FixtureData>>(reader);
                    //var fantasyPremierLeagueFixtureData = JsonConvert.DeserializeObject< List<FantasyPremierLeagueFixtureData>>(reader);

                    //Load Fixtures data
                    FixtureRepository fixtureRepository = new FixtureRepository();
                    FixtureStatRepository fixtureStatRepository = new FixtureStatRepository();
                    FixtureStatsValuesRepository fixtureStatsValuesRepository = new FixtureStatsValuesRepository();

                    List<int> fixtureIds = fixtureRepository.GetAllFixtureIds();

                    int fixtureId = 0;

                    foreach (FixtureData fixture in fantasyPremierLeagueFixtureData)
                    {
                        fixtureId = fixture.id;

                        if (!fixtureIds.Contains(fixture.id))
                        {
                            fixtureRepository.InsertFixture(fixture);
                        }
                        else
                        {
                            fixtureRepository.UpdateFixture(fixture);
                        }

                        //Load Fixture Stats
                        List<string> fixtureStatIdentifiers = fixtureStatRepository.GetAllFixtureStatIdentifiersForFixtureId(fixtureId);

                        foreach (FixtureStat fixtureStat in fixture.stats)
                        {
                            fixtureStat.fixtureid = fixtureId;

                            if (!fixtureStatIdentifiers.Contains(fixtureStat.identifier))
                            {
                                fixtureStatRepository.InsertFixtureStat(fixtureStat);
                            }
                            //else
                            //{
                            //    fixtureStatRepository.UpdateFixtureStat(fixtureStat);
                            //}

                            int playerStatId = fixtureStatsValuesRepository.GetPlayerStatIdForCurrentIdentifier(fixtureStat.identifier);
                            int fixtureStatId = fixtureStatsValuesRepository.GetFixtureStatIdForCurrentIdentifierAndFixture(fixtureId, fixtureStat.identifier);

                            //Load Fixture Stat values (away)
                            foreach (FixtureStatsValue fixtureStatValueAway in fixtureStat.a)
                            {
                                fixtureStatValueAway.fixtureid = fixtureId;
                                fixtureStatValueAway.fixtureStatid = fixtureStatId;
                                fixtureStatValueAway.playerStatid = playerStatId;
                                fixtureStatValueAway.isHome = false;

                                List<int> fixtureStatValuePlayerIds = fixtureStatsValuesRepository.GetAllPlayerIdsForFixtureIdAndIdentifier(fixtureId, playerStatId, false);

                                if (!fixtureStatValuePlayerIds.Contains(fixtureStatValueAway.element))
                                {
                                    fixtureStatsValuesRepository.InsertFixtureStatsValue(fixtureStatValueAway);
                                }
                                else
                                {
                                    fixtureStatsValuesRepository.UpdateFixtureStatsValue(fixtureStatValueAway);
                                }
                            }

                            //Load Fixture Stat values (home)
                            foreach (FixtureStatsValue fixtureStatValueHome in fixtureStat.h)
                            {
                                fixtureStatValueHome.fixtureid = fixtureId;
                                fixtureStatValueHome.fixtureStatid = fixtureStatId;
                                fixtureStatValueHome.playerStatid = playerStatId;
                                fixtureStatValueHome.isHome = true;

                                List<int> fixtureStatValuePlayerIds = fixtureStatsValuesRepository.GetAllPlayerIdsForFixtureIdAndIdentifier(fixtureId, playerStatId, true);

                                if (!fixtureStatValuePlayerIds.Contains(fixtureStatValueHome.element))
                                {
                                    fixtureStatsValuesRepository.InsertFixtureStatsValue(fixtureStatValueHome);
                                }
                                else
                                {
                                    fixtureStatsValuesRepository.UpdateFixtureStatsValue(fixtureStatValueHome);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Fixture data exception: " + ex.Message);
                throw new Exception("Fixture data exception: " + ex.Message);
            }

            #endregion
        }
    }
}