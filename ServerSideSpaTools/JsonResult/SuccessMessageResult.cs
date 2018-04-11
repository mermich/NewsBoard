namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Displays a success message on the page.
    /// </summary>
    public class SuccessMessageResult : CustomJsonResult
    {
        public SuccessMessageResult(string message, int duration = 5000) : base(new { SuccessMessage = message, duration })
        {

        }
    }
}
