using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.GroupApi;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("Group")]
    public class GroupEditController : BaseController
    {

        public IActionResult Index(int groupId)
        {
            var api = new GroupApi(UserId);
            var model = api.GetEditGroup(groupId);
            
            return ReturnView("GroupEditView", model);
        }

        public ActionResult Update(GroupEditVM model)
        {
            var api = new GroupApi(UserId);
            api.SaveGroup(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupList", "Index")),
                new SuccessMessageResult("Group Updated")
                );
        }
    }
}