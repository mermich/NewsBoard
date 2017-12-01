namespace ServerSideSpaTools.JsonResult
{
    /// <summary>
    /// Displays a success message on the page.
    /// </summary>
    public class SuccessMessageResult : CustomJsonResult
    {
        public SuccessMessageResult(string message) : base(new { SuccessMessage = message })
        {

        }
    }
}
