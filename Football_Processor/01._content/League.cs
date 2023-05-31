namespace Football_Processor
{
    public class League
    {
        /* nop = number of positions */
        /* nosp = number of subsequent positions */
        /* nofp = number of final positions */
        public string leagueName { get; set; }
        public string nopChampions { get; set; }
        public string nopUpper { get; set; }
        public string nospEurope { get; set; }
        public string nospConference { get; set; }
        public string nolpLower { get; set; }

        public League(
            string leagueName,
            string nopChampions,
            string nopUpper,
            string nospEurope,
            string nospConference,
            string nolpLower
        )
        {
            this.leagueName = leagueName;
            this.nopChampions = nopChampions;
            this.nopUpper = nopUpper;
            this.nospEurope = nospEurope;
            this.nospConference = nospConference;
            this.nolpLower = nolpLower;
        }
    }
}
