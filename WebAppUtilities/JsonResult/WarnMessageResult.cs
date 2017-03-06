namespace WebAppUtilities.JsonResult
{
    public class WarnMessageResult : CustomJsonResult
    {
        public WarnMessageResult(string message) : base(new { WarnMessage = message })
        {

        }
    }
}
