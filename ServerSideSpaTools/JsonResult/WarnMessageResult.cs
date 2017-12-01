namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// display a warning to the page.
    /// </summary>
    public class WarnMessageResult : CustomJsonResult
    {
        public WarnMessageResult(string message) : base(new { WarnMessage = message })
        {

        }
    }
}
