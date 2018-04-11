namespace NewBoardRestApi.FeedApi
{
    public class FeedVMListOptions
    {
        public string Heading { get; set; } = "";

        public string NoDataToDisplay { get; set; } = "Il n'y a aucuns resultats a afficher.";

        public FeedVMListOptions()
        {
        }

        public FeedVMListOptions(string heading)
        {
            Heading = heading;
        }

        public FeedVMListOptions(string heading, string noDataToDisplay)
        {
            Heading = heading;
            NoDataToDisplay = noDataToDisplay;
        }
    }
}
