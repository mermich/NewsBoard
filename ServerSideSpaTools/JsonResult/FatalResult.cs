namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Displays a fatal message.
    /// </summary>
    public class FatalResult : CustomJsonResult
    {
        public FatalResult(string message) : base(new { FatalMessage = message })
        {

        }
    }
}
