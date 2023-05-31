using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Football_Processor
{
    public class FileHandler
    {
        private readonly FileInfo _file;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        private string _splitVar = ",";
        public List<Team> teams { get; set; }
        public List<League> leagues { get; set; }

        public FileHandler() { }

        public FileHandler(string filePath)
        {
            _file = new FileInfo(filePath);
            leagues = new List<League>(); // Initialize the leagues list
            teams = new List<Team>(); // Initialize the teams list

            try
            {
                _reader = new StreamReader(_file.FullName);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StartReading()
        {
            List<string> linesOfFile = ReadFile();

            int length = linesOfFile.Count;

            for (int i = 0; i < length; i++)
            {
                List<string> splitLine = ReadFile2(linesOfFile[i]);

                if (splitLine.Count < 4)
                {
                    Team team = new Team(splitLine[0], splitLine[1], splitLine[2]);

                    try
                    {
                        teams.Add(team);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine("a" + e.Message);
                    }
                }
                else
                {
                    League league = new League(
                        splitLine[0],
                        splitLine[1],
                        splitLine[2],
                        splitLine[3],
                        splitLine[4],
                        splitLine[5]
                    );

                    try
                    {
                        leagues.Add(league);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine("b" + e.Message);
                    }
                }
            }
        }

        public List<string> ReadFile()
        {
            List<string> returnValue = new List<string>();

            try
            {
                string line;
                string header = _reader.ReadLine();
                while ((line = _reader.ReadLine()) != null)
                {
                    returnValue.Add(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _reader.Close();
            }

            return returnValue;
        }

        public List<string> ReadFile2(string lineOfFile)
        {
            try
            {
                string[] splitLine = lineOfFile.Split(_splitVar);
                return splitLine.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void PrintList()
        {
            try
            {
                List<string> stringList = ReadFile();
                foreach (string item in stringList)
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _reader.Close();
            }
        }

        public List<string> getTeamAbbreviations()
        {
            List<string> foundAbb = new List<string>();

            try
            {
                teams.ForEach(team => foundAbb.Add(team.abbreviation));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return foundAbb;
        }

        public void PrintSimpleStandings(int position)
        {
            try
            {
                Team team = teams[position - 1];

                /* Position in the table, Special marking in parentheses, Full club name */
                Console.WriteLine($"Position in the table           : {position}");
                Console.WriteLine($"Special marking in parentheses  : {team.abbreviation}");
                Console.WriteLine($"Full club name                  : {team.clubname}");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _reader.Close();
            }
        }

        public void PrintExpandedStandings(int position)
        {
            try
            {
                Team team = teams[position - 1];

                /* Position in the table, Special marking in parentheses, Full club name */
                Console.WriteLine($"Position in the table           : {position}");
                Console.WriteLine($"Special marking in parentheses  : {team.abbreviation}");
                Console.WriteLine($"Full club name                  : {team.clubname}");
                Console.WriteLine($"Games played                    : {team.gamesPlayed}");
                Console.WriteLine($"Number of games won             : {team.nogWon}");
                Console.WriteLine($"Number of games drawn           : {team.nogDrawn}");
                Console.WriteLine($"Number of games lost            : {team.nogLost}");
                Console.WriteLine($"Goals for                       : {team.goalsFor}");
                Console.WriteLine($"Goals against                   : {team.goalsAgainst}");
                Console.WriteLine($"Goal Difference                 : {team.goalDifference}");
                Console.WriteLine($"Points achieved                 : {team.pointsAchieved}");
                Console.WriteLine($"Current winning streak          : {team.winningStreak}");

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _reader.Close();
            }
        }

        public void WriteFile(Round round)
        {
            try
            {
                string[] files = Directory.GetFiles("02._csv\\rounds");
                int numberOfFiles = files.Length + 1;

                Console.WriteLine(numberOfFiles);
                _writer = new StreamWriter($"02._csv\\rounds\\rounds-{numberOfFiles}.csv");

                _writer.WriteLine($"{round.homeTeam},{round.awayTeam},{round.score}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _writer.Close();
            }
        }

        // "Pos  Team          M W D L GF GA GD P Streak"


        public void WriteResults()
        {
            try
            {
                string[] files = Directory.GetFiles("02._csv\\01._rounds\\");
                int numberOfFiles = files.Length + 1;
                int idx = 1;
                UI_Elements ui = new UI_Elements();
                string stringToAppend = "";


                //Debug.WriteLine(numberOfFiles);
                //_writer = new StreamWriter($"02._csv\\03._results.txt");
                using (StreamWriter writer = new StreamWriter($"02._csv\\03._results.txt"))
                {
                    foreach (string file in files)
                    {
                        List<Match> resultMatches = FindMatches(idx);
                        CalcResults(resultMatches);
                        SortTeams();
                        stringToAppend = "";
                        //if (idx == 1) { continue; }
                        //else
                        //{
                        if (idx <= 22)
                        {
                            Console.WriteLine("\n");
                            ui.GetDivider(TextDividerType.Wavy, $"Round: {idx}");
                            writer.WriteLine($"~~~~~~ ROUND: {idx} ~~~~~~");
                            foreach (Team team in teams)
                            {
                                Console.WriteLine(team);
                                writer.WriteLine(team);
                            }
                        }


                        //}
                        idx++;
                    }
                }


                //_writer.WriteLine($"{round.homeTeam},{round.awayTeam},{round.score}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //_writer.Close();
            }
        }

        private void SortTeams()
        {

            teams = teams.OrderByDescending(t => t.pointsAchieved)
            .ThenByDescending(t => t.goalDifference)
            .ThenByDescending(t => t.goalsFor)
            .ThenBy(t => t.abbreviation)
            .ToList();


        }

        private void CalcResults(List<Match> matches)
        {

            foreach (Match match in matches)
            {

                Team hTeam = DetermineTeam(match.hTeam);
                Team aTeam = DetermineTeam(match.aTeam);

                hTeam.gamesPlayed += 1;
                hTeam.goalsFor += match.hGoals;
                hTeam.goalsAgainst += match.aGoals;
                hTeam.goalDifference = hTeam.goalsFor - hTeam.goalsAgainst;
                aTeam.gamesPlayed += 1;
                aTeam.goalsFor += match.aGoals;
                aTeam.goalsAgainst += match.hGoals;
                aTeam.goalDifference = aTeam.goalsFor - aTeam.goalsAgainst;



                //Draw
                if (match.hGoals == match.aGoals)
                {

                    hTeam.nogDrawn++;
                    hTeam.pointsAchieved++;
                    hTeam.SetStreak("D");

                    aTeam.nogDrawn++;
                    aTeam.pointsAchieved++;
                    aTeam.SetStreak("D");
                }
                //Home team loses
                else if (match.hGoals < match.aGoals)
                {

                    hTeam.nogLost++;
                    hTeam.SetStreak("L");
                    aTeam.nogWon++;
                    aTeam.pointsAchieved += 3;
                    aTeam.SetStreak("W");
                }
                //Home team wins
                else
                {

                    hTeam.nogWon++;
                    hTeam.pointsAchieved += 3;
                    hTeam.SetStreak("W");
                    aTeam.nogLost++;
                    aTeam.SetStreak("L");
                }

            }
        }

        private List<Match> FindMatches(int round)
        {
            List<Team> calcedTeams = new List<Team>();
            List<Match> matches = new List<Match>();


            try
            {
                StreamReader rdr = new StreamReader($"02._csv\\01._rounds\\round-{round}.csv");
                string firstLine = rdr.ReadLine();
                while (!rdr.EndOfStream)
                {
                    string line = rdr.ReadLine();
                    string[] lines = line.Split(',');
                    Match match = new Match(lines[0], lines[1], Int32.Parse(lines[2]), Int32.Parse(lines[3]));
                    matches.Add(match);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return matches;
        }

        private Team DetermineTeam(string abbreviation)
        {
            Team team = new Team(null, null, null, null);

            foreach (Team item in teams)
            {
                if (item.abbreviation == abbreviation)
                    team = item;

                //Console.WriteLine("In DetermineTeam(): " + item.abbreviation);
            }

            return team;
        }

    }
}
