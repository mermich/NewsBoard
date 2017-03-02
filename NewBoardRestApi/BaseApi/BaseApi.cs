using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.BaseApi
{
    public class BaseApi : Controller
    {
        public NewsBoardContext NewsBoardContext { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (NewsBoardContext != null)
            {
                NewsBoardContext.Dispose();
            }
        }
    }
}
