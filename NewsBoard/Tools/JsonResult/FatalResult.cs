namespace NewsBoard.Tools.JsonResult
{
    public class FatalResult : CustomJsonResult
    {
        public FatalResult(string message) : base(new { FatalMessage = message })
        {

        }
    }
}
