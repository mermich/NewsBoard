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


        [ResponseCache(Duration = 300)]
        public virtual IActionResult Index()
        {
            var model = groupApi.GetGroups();

            return ReturnView("GroupListView", model);
        }

        
        public virtual ActionResult GetEdit(int groupId)
        {
            return ReturnReplaceMainView(new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupEdit", "Index", new { groupId })));
        }
        
        public virtual ActionResult GetCreate()
        {
            return ReturnReplaceMainView(new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupCreate", "Index")));
        }
    }
}