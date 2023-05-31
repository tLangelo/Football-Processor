namespace Football_Processor
{
    public class Round
    {
        public string homeTeam { get; set; }
        public string awayTeam { get; set; }
        public string score { get; set; }

        public Round(string homeTeam, string awayTeam, string score)
        {
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.score = score;
        }
    }
}
