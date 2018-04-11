namespace NewBoardRestApi.ArticleApi
{
    public class ArticleVMListOptions
    {
        public string Heading { get; set; } = "";

        public string NoDataToDisplay { get; set; } = "Il n'y a aucuns resultats a afficher.";

        public ArticleVMListOptions() : this("")
        {
        }


        public ArticleVMListOptions(string heading)
        {
            Heading = heading;
        }

        public ArticleVMListOptions(string heading, string noDataToDisplay)
        {
            Heading = heading;
            NoDataToDisplay = noDataToDisplay;
        }
    }
}
