﻿using NewsBoard.Tools;
using Microsoft.AspNetCore.Mvc;
using NewBoardRestApi.GroupApi;
using WebAppUtilities.JsonResult;

namespace NewsBoard.wwwroot.Group.GroupCreate
{
    [Area("Group")]
    public class GroupCreateController : BaseController
    {

        public IActionResult Index()
        {
            var api = new GroupApi(UserId);
            var model = api.GetNewGroupEditVM();
            
            return ReturnView("GroupCreateView", model);
        }

        public ActionResult Create(GroupEditVM model)
        {
            var api = new GroupApi(UserId);
            api.CreateGroup(model);

            return new ComposeResult(
                new ReplaceMainHtmlResult(NewsBoardUrlHelper.Action("Group", "GroupList", "Index")),
                new SuccessMessageResult("Group Created")
                );
        }
    }
}