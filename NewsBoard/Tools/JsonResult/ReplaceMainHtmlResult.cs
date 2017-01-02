namespace NewsBoard.Tools.JsonResult
{
    public class ReplaceMainHtmlResult : ReplaceHtmlResult
    {
        public ReplaceMainHtmlResult(string action) : base("#page", action)
        {

        }
    }
}
