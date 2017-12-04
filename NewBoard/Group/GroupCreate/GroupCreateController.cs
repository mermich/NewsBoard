using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.GroupApi;
using ServerSideSpaTools.JsonResult;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("Group")]
    public class GroupCreateController : BaseController
    {
        GroupApi groupApi;

        public GroupCreateController(GroupApi groupApi)
        {
            this.groupApi = groupApi;
        }


        [ResponseCache(Duration = 300)]
        public IActionResult Index()
        {
            var model = groupApi.GetNewGroupEditVM();
            
            return ReturnView("GroupCreateView", model);
        }

        public ActionResult Create(GroupEditVM model)
        {
            groupApi.CreateGroup(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupList", "Index")),
                new SuccessMessageResult("Group Created")
                );
        }
    }
}