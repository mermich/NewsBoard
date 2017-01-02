namespace NewsBoard.Tools.JsonResult
{
    public class ReplaceHtmlResult : CustomJsonResult
    {
        public ReplaceHtmlResult(string selector, string action) : base(new { ReplaceHtml = new { selector = selector, action = action } })
        {

        }
    }
}
