using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.BaseApi
{
    public class BaseApi : Controller
    {
        public NewsBoardContext NewsBoardContext { get; set; }

        public BaseApi(NewsBoardContext newsBoardContext)
        {
            NewsBoardContext = newsBoardContext;
        }

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
