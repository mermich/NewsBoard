using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.GroupApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("Group")]
    public partial class GroupEditController : BaseController
    {
        GroupApi groupApi;

        public GroupEditController(GroupApi groupApi)
        {
            this.groupApi = groupApi;
        }


        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual IActionResult Index(int groupId)
        {
            var model = groupApi.GetEditGroup(groupId);
            
            return ReturnView("GroupEditView", model);
        }

        public virtual ActionResult Update(GroupEditVM model)
        {
            groupApi.SaveGroup(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupList", "Index")),
                new SuccessMessageResult("Group Updated")
                );
        }
    }
}