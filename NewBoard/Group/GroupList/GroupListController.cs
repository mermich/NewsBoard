using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.GroupApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Group.GroupList
{
    [Area("Group")]
    public partial class GroupListController : BaseController
    {
        GroupApi groupApi;

        public GroupListController(GroupApi groupApi)
        {
            this.groupApi = groupApi;
        }


        
        public virtual IActionResult Index()
        {
            var model = groupApi.GetGroups();

            return ReturnView("GroupListView", model);
        }


        
        public virtual ActionResult GetEdit(int groupId)
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupEdit", "Index", new { groupId })).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }

        
        public virtual ActionResult GetCreate()
        {
            return new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupCreate", "Index")).ReplaceResultOrRedirectResult(IsAjaxRequest);
        }
    }
}