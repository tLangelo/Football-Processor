namespace Football_Processor
{
    public class Team
    {
        public string abbreviation { get; set; }
        public string clubname { get; set; }
        public string ranking { get; set; }
        public League league { get; set; }
        public int position { get; set; }

        // Special attribute (not in constructor)
        // nog = number of games
        public int gamesPlayed { get; set; }
        public int nogWon { get; set; }
        public int nogDrawn { get; set; }
        public int nogLost { get; set; }

        // ????
        public int goalsFor { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
        public int pointsAchieved { get; set; }
        public string winningStreak { get; set; } = "";

        public Team(string abbreviation, string clubname, string ranking)
        {
            this.abbreviation = abbreviation;
            this.clubname = clubname;
            this.ranking = ranking;
            this.league = new League("test", "test", "test", "test", "test", "test");
        }

        public Team(string abbreviation, string clubname, string ranking, League league)
        {
            this.abbreviation = abbreviation;
            this.clubname = clubname;
            this.ranking = ranking;
            this.league = league;
        }

        public void SetStreak(string matchResult = "")
        {
            if (matchResult.Length != 1)
            {
                return;
            }

            if (this.winningStreak.Length >= 4)
            {
                this.winningStreak = winningStreak.Substring(1);
                this.winningStreak += matchResult;
            }
            else
            {
                this.winningStreak += matchResult;
            }
            this.winningStreak = winningStreak;
        }

        // "Pos  Team          M W D L GF GA GD P Streak"


        public override string ToString()
        {
            string clubInfo = $"{this.clubname} ({this.abbreviation})";
            int desiredLength = 40; // Adjust this value to control the padding

            // Pad the clubInfo with spaces to the desired length
            string paddedClubInfo = clubInfo.PadRight(desiredLength, ' ');

            int shortLength = 3;

            string aa = $"{this.gamesPlayed}".PadRight(shortLength, ' ');
            string bb = $"{this.nogWon}".PadRight(shortLength, ' ');
            string cc = $"{this.nogDrawn}".PadRight(shortLength, ' ');
            string dd = $"{this.nogLost}".PadRight(shortLength, ' ');
            string ee = $"{this.goalsFor}".PadRight(shortLength, ' ');
            string ff = $"{this.goalsAgainst}".PadRight(shortLength, ' ');
            string gg = $"{this.goalDifference}".PadRight(shortLength, ' ');
            string hh = $"{this.pointsAchieved}".PadRight(shortLength, ' ');
            string ii = $"{this.winningStreak}".PadRight(shortLength, ' ');

            string formattedString =
                $"{paddedClubInfo} {aa} {bb} {cc} {dd} {ee} {ff} {gg} {hh} {ii}";

            return formattedString;
        }
    }
}
