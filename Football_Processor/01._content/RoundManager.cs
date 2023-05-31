namespace Football_Processor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    struct Match
    {
        //hometeam
        public string hTeam { get; set; }

        //awayteam
        public string aTeam { get; set; }

        public int hGoals { get; set; }
        public int aGoals { get; set; }

        public Match(string home, string away, int homeGoals, int awayGoals)
        {
            this.hTeam = home;
            this.aTeam = away;
            this.hGoals = homeGoals;
            this.aGoals = awayGoals;
        }
    }

    public class RoundManager
    {
        public RoundManager() { }

        private static Random random = new Random();

        public void InitRounds(List<string> abbreviations)
        {
            /*
            This should emulate the rounds that are to take place between the different teams
            The filehandling should also be handled from here as well.
            */

            for (int round = 1; round <= 22; round++)
            {
                var matches = GenerateRound(abbreviations, round);
                SaveMatchesToCSV(matches, $"02._csv\\01._rounds\\round-{round}.csv");
            }
        }

        public void runRound(List<Team> teams) { }

        private static List<Match> GenerateRound(List<string> teams, int round)
        {
            int numTeams = teams.Count;
            var matches = new List<Match>();

            // Rotate the list of teams using the Circle method
            int rotationIndex = (round - 1) % (numTeams - 1);
            var rotatedTeams = new List<string> { teams[0] };
            rotatedTeams.AddRange(teams.Skip(rotationIndex + 1).Take(numTeams - rotationIndex - 1));
            rotatedTeams.AddRange(teams.Skip(1).Take(rotationIndex));

            for (int i = 0; i < numTeams / 2; i++)
            {
                var homeTeam = rotatedTeams[i];
                var awayTeam = rotatedTeams[numTeams - i - 1];

                // Swap home and away teams for half of the rounds to ensure equal home and away matches
                if (round % 2 == 0)
                {
                    var temp = homeTeam;
                    homeTeam = awayTeam;
                    awayTeam = temp;
                }

                var match = new Match
                {
                    hTeam = homeTeam,
                    aTeam = awayTeam,
                    hGoals = GenerateGoals(),
                    aGoals = GenerateGoals()
                };

                matches.Add(match);
            }

            return matches;
        }

        private static int GenerateGoals()
        {
            return random.Next(0, 6); // Random number of goals between 0 and 5
        }

        /* private static void SaveMatchesToCSV(List<Match> matches, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("home,away,homeGoals,awayGoals");

                foreach (var match in matches)
                {
                    writer.WriteLine($"{match.hTeam},{match.aTeam},{match.hGoals},{match.aGoals}");
                }
            }
        } */

        private static void SaveMatchesToCSV(List<Match> matches, string fileName)
        {
            // Create the directory if it doesn't exist
            string directoryPath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("home,away,homeGoals,awayGoals");

                foreach (var match in matches)
                {
                    writer.WriteLine($"{match.hTeam},{match.aTeam},{match.hGoals},{match.aGoals}");
                }
            }
        }
    }
}
