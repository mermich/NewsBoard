namespace WebAppUtilities.JsonResult
{
    public class FatalResult : CustomJsonResult
    {
        public FatalResult(string message) : base(new { FatalMessage = message })
        {

        }
    }
}
