using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewsBoard.Tools.JsonResult;
using NewBoardRestApi.Api;
using NewBoardRestApi.Api.Model;
using Microsoft.AspNetCore.Authorization;

namespace NewsBoard.wwwroot.Group.GroupList
{
    [Area("Group")]
    public class GroupListController : BaseController
    {

        public IActionResult Index()
        {
            var api = new GroupApi(UserId);
            var model =api.GetGroups();
            
            return ReturnView("GroupListView", model);
        }

        public ActionResult GetEdit(int groupId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupEdit", "Index", new { groupId = groupId }));
        }

        public ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupCreate", "Index"));
        }
    }
}