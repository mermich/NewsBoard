namespace NewsBoard.Tools.JsonResult
{
    public class ShowHtmlResult : CustomJsonResult
    {
        public ShowHtmlResult(string selector) : base(new { ShowHtml = new { selector = selector } })
        {

        }
    }
}
