namespace NewsBoard.Tools.JsonResult
{
    public class AppendHtmlResult : CustomJsonResult
    {
        public AppendHtmlResult(string selector, string action) : base(new { AppendHtml = new { selector = selector, action = action } })
        {

        }
    }
}
