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
        private const string baseUrl = "https://fantasy.premierleague.com/drf/element-summary/{0}";
        private const string baseBootstrapUrl = "https://fantasy.premierleague.com/drf/bootstrap-static";

        #endregion

        #region methods

        public static void GetPlayerBootstrapDataJson()
        {
            try
            {
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
                    Console.WriteLine("Starting Teams load");
                    List<int> teamIds = teamRepository.GetAllTeamIds();

                    foreach (Team team in fantasyPremierLeagueBootstrapData.teams)
                    {
                        if (!teamIds.Contains(team.id))
                        {
                            Console.WriteLine(team.name + " (" + Convert.ToString(team.code) + ") - inserted");

                            teamRepository.InsertTeam(team);
                        }
                        else
                        {
                            teamRepository.UpdateTeam(team);
                        }
                    }

                    Console.WriteLine("Teams load complete");
                    Console.WriteLine("");

                    //Load player position data
                    Console.WriteLine("Starting Player Positions load");

                    PlayerPositionRepository playerPositionRepository = new PlayerPositionRepository();

                    List<int> playerPositionIds = playerPositionRepository.GetAllPlayerPositionIds();

                    foreach (PlayerPosition playerPosition in fantasyPremierLeagueBootstrapData.element_types)
                    {
                        if (!playerPositionIds.Contains(playerPosition.id))
                        {
                            Console.WriteLine(playerPosition.singular_name + " (" + playerPosition.singular_name_short + ") - inserted");

                            playerPositionRepository.InsertPlayerPosition(playerPosition);
                        }
                        else
                        {
                            playerPositionRepository.UpdatePlayerPosition(playerPosition);
                        }
                    }

                    Console.WriteLine("Player Positions load complete");
                    Console.WriteLine("");

                    //Load player data
                    Console.WriteLine("Starting Players load");

                    PlayerRepository playerRepository = new PlayerRepository();

                    List<int> playerIds = playerRepository.GetAllPlayerIds();

                    //Delete player history and player records for players no longer in the Premier League
                    List<int> inputPlayerIds = fantasyPremierLeagueBootstrapData.elements.Select(x => x.id).ToList();
                    List<int> playersToDelete = playerIds.Except(inputPlayerIds).ToList();
                    HistoryRepository historyRepository = new HistoryRepository();

                    foreach (int playerId in playersToDelete)
                    {
                        historyRepository.DeleteAllPlayerHistory(playerId);
                        playerRepository.DeletePlayer(playerId);
                    }

                    //Insert and update players data
                    foreach (Player player in fantasyPremierLeagueBootstrapData.elements)
                    {
                        if (!playerIds.Contains(player.id))
                        {
                            playerRepository.InsertPlayer(player);

                            Console.WriteLine(player.first_name + " " + player.second_name + " - inserted");
                        }
                        else
                        {
                            playerRepository.UpdatePlayer(player);
                        }
                    }

                    Console.WriteLine("Players load complete");
                    Console.WriteLine("");

                    //Load gameweek data
                    Console.WriteLine("Starting Gameweeks load");

                    GameweekRepository gameweekRepository = new GameweekRepository();

                    List<int> gameweekIds = gameweekRepository.GetAllGameweekIds();

                    foreach (Gameweek gameweek in fantasyPremierLeagueBootstrapData.events)
                    {
                        if (!gameweekIds.Contains(gameweek.id))
                        {
                            gameweekRepository.InsertGameweek(gameweek);

                            Console.WriteLine(gameweek.name + " - inserted");
                        }
                        else
                        {
                            gameweekRepository.UpdateGameweek(gameweek);
                        }
                    }

                    Console.WriteLine("Gameweeks load complete");
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Bootstrap data exception: " + ex.Message);
            }
        }

        public static void GetPlayerDataJson(int playerID)
        {
            try
            {
                //playerID = 188;
                // Customize URL according to playerID parameter
                var url = string.Format(baseUrl, playerID);

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

                    //Load player fixtures data
                    FixtureRepository fixtureRepository = new FixtureRepository();

                    List<int> fixtureIds = fixtureRepository.GetAllFixtureIds();
                    List<string> opponentShortNames;

                    foreach (Fixture2 fixture in fantasyPremierLeaguePlayerData.fixtures)
                    {
                        //fixture.is_homeINT = Convert.ToInt32(fixture.is_home);
                        opponentShortNames = fixtureRepository.GetAllFixtureOpponentShortName(fixture.id);
                        if (!fixtureIds.Contains(fixture.id)) //|| !opponentShortNames.Contains(fixture.opponent_short_name))
                        {
                            fixtureRepository.InsertFixture(fixture);
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                        }
                        else
                        {
                            fixtureRepository.UpdateFixture(fixture);
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        }
                    }

                    //Load player history data
                    HistoryRepository historyRepository = new HistoryRepository();

                    List<int> historyIds = historyRepository.GetAllHistoryIds();

                    foreach (History history in fantasyPremierLeaguePlayerData.history)
                    {
                        if (!historyIds.Contains(history.id))
                        {
                            historyRepository.InsertHistory(history);
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                        }
                        else
                        {
                            historyRepository.UpdateHistory(history);
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        }
                    }

                    //Load history past data
                    HistoryPastRepository historyPastRepository = new HistoryPastRepository();

                    List<int> historyPastIds = historyPastRepository.GetAllHistoryPastIds();

                    foreach (HistoryPast historyPast in fantasyPremierLeaguePlayerData.history_past)
                    {
                        historyPast.playerId = playerID;
                        if (!historyPastIds.Contains(historyPast.id))
                        {
                            historyPastRepository.InsertHistoryPast(historyPast);
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - inserted");
                        }
                        else
                        {
                            historyPastRepository.UpdateHistoryPast(historyPast);
                            //Console.WriteLine("PlayerId (" + Convert.ToString(playerID) + ") - updated");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Player data exception (PlayerId: " + playerID.ToString() + "): " + ex.Message);
            }
        }

        #endregion
    }
}