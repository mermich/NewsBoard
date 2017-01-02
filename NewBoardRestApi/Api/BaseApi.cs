using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Api
{
    public class BaseApi : Controller
    {
        private NewsBoardContext newsBoardContext;

        public NewsBoardContext NewsBoardContext
        {
            get
            {
                if (newsBoardContext == null)
                    newsBoardContext = new NewsBoardContext();
                return newsBoardContext;
            }
            set
            {
                newsBoardContext = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (newsBoardContext != null)
            {
                newsBoardContext.Dispose();
            }
        }
    }
}
