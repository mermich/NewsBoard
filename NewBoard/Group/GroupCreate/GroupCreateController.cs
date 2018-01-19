using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.GroupApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("Group")]
    public partial class GroupCreateController : BaseController
    {
        GroupApi groupApi;

        public GroupCreateController(GroupApi groupApi)
        {
            this.groupApi = groupApi;
        }


        [ResponseCache(Duration = 300, VaryByHeader = "X-Requested-With")]
        public virtual IActionResult Index()
        {
            var model = groupApi.GetNewGroupEditVM();
            
            return ReturnView("GroupCreateView", model);
        }

        public virtual ActionResult Create(GroupEditVM model)
        {
            groupApi.CreateGroup(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupList", "Index")),
                new SuccessMessageResult("Group Created")
                );
        }
    }
}