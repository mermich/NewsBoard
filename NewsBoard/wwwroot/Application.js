/// <reference path="Common.ts" />
var manager = new plop.AjaxManager();
var feedlistController = new plop.FeedListController(manager);
plop.Hub.AddController(feedlistController);

var userController = new plop.UserController(manager);
plop.Hub.AddController(userController);
