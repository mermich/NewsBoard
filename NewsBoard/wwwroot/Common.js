/// <reference path="lib/typing/jquery.d.ts" />
//store all interfaces/common stuff here
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var plop;
(function (plop) {
    var AjaxManager = (function () {
        function AjaxManager(baseUrl) {
            this.baseUrl = baseUrl;
        }
        AjaxManager.prototype.Get = function (path, callback) {
            $.get(this.baseUrl + path, null, callback);
        };

        AjaxManager.prototype.Post = function (path, data, callback) {
            $.post(this.baseUrl + path, data, callback);
        };
        return AjaxManager;
    })();
    plop.AjaxManager = AjaxManager;

    var Hub = (function () {
        function Hub() {
        }
        Hub.AddController = function (ctrl) {
            Hub.controllers[ctrl.GetControllerKey()] = ctrl;
        };

        Hub.LoadController = function (name, param) {
            this.controllers[name].Load(param);
        };
        Hub.controllers = {};
        return Hub;
    })();
    plop.Hub = Hub;

    var Controller = (function () {
        function Controller(ajaxManager) {
            this.ajaxManager = ajaxManager;
        }
        Controller.prototype.Load = function (param) {
        };

        Controller.prototype.GetControllerKey = function () {
            return this.constructor.name;
        };

        Controller.prototype.BindSimpleGetAction = function (selector) {
            var _this = this;
            $(selector).find("[simpleGetAction]").click(function (b) {
                _this.ajaxManager.Get($(b.target).attr("simpleGetAction"), function (c) {
                    alert(c);
                });
            });
        };

        Controller.prototype.BindNavigateGetAction = function (selector) {
            var _this = this;
            $(selector).find("[navigateGetAction]").click(function (b) {
                //load from that js controller
                _this.ajaxManager.Get($(b.target).attr("navigateGetAction"), function (c) {
                    var controllerName = $(b.target).attr("navigateGetAction");
                    var param = $(b.target).attr("param");

                    Hub.LoadController($(b.target).attr("navigateGetAction"), param);
                });
            });
        };
        return Controller;
    })();
    plop.Controller = Controller;

    var UserController = (function (_super) {
        __extends(UserController, _super);
        function UserController(ajaxManager) {
            _super.call(this, ajaxManager);
        }
        UserController.prototype.Load = function (userId) {
            var _this = this;
            this.ajaxManager.Get("/User/Index?id=" + userId, function (c) {
                _super.prototype.BindSimpleGetAction.call(_this);
                _super.prototype.BindNavigateGetAction.call(_this);
            });
        };
        return UserController;
    })(plop.Controller);
    plop.UserController = UserController;

    var FeedListController = (function (_super) {
        __extends(FeedListController, _super);
        function FeedListController(ajaxManager) {
            _super.call(this, ajaxManager);
        }
        FeedListController.prototype.Load = function (userId) {
            var _this = this;
            this.ajaxManager.Get("/FeedList/Index", function (c) {
                _super.prototype.BindSimpleGetAction.call(_this);
                _super.prototype.BindNavigateGetAction.call(_this);
            });
        };
        return FeedListController;
    })(plop.Controller);
    plop.FeedListController = FeedListController;
})(plop || (plop = {}));
