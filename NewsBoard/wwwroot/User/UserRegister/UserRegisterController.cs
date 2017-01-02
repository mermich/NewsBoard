using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;

namespace NewsBoard.wwwroot.User.UserRegister
{
    [Area("User")]
    public class UserRegisterController : BaseController
    {

        public IActionResult Index()
        {
            var model = new UserRegisterVM();
            return ReturnView("UserRegisterView", model);
        }

        public ActionResult Register(UserRegisterVM model)
        {
            var api = new UserRegisterApi();
            var user = api.Register(model.Email, model.Password);

            return new SuccessMessageResult("Registered");
        }

        public ActionResult Hide(int articleId)
        {
            var articleRepo = new ArticleApi(HttpContext.Session.Id);
            articleRepo.HideArticle(articleId);

            return new HideHtmlResult("#article-" + articleId);
        }
    }
}