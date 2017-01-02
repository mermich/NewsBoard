/// <reference path="lib/typing/jquery.d.ts" />
//store all interfaces/common stuff here

module plop {
    export class AjaxManager {
        baseUrl: string;
        constructor(baseUrl?: string) {
            this.baseUrl = baseUrl;
        }

        Get(path: string, callback: any) {
            $.get(this.baseUrl + path, null, callback);
        }

        Post(path: string, data?: any, callback?: any) {
            $.post(this.baseUrl + path, data, callback);
        }
    }

    export class Hub {
        static controllers: { [name: string]: Controller; } = {};

        public static AddController(ctrl: Controller) {
            Hub.controllers[ctrl.GetControllerKey()] = ctrl;
        }

        public static LoadController(name: string, param?: any) {
            this.controllers[name].Load(param);
        }
    }

    export class Controller {
        ajaxManager: AjaxManager;

        constructor(ajaxManager: AjaxManager) {
            this.ajaxManager = ajaxManager;
        }

        public Load(param?: any): void {
        }

        GetControllerKey(): string {
            return (<any>this).constructor.name;
        }

        BindSimpleGetAction(selector?: string) {
            $(selector).find("[simpleGetAction]").click((b) => {
                this.ajaxManager.Get($(b.target).attr("simpleGetAction"), (c) => {
                    alert(c);
                });
            });
        }

        BindNavigateGetAction(selector?: string) {
            $(selector).find("[navigateGetAction]").click((b) => {
                //load from that js controller
                this.ajaxManager.Get($(b.target).attr("navigateGetAction"), (c) => {
                    var controllerName = $(b.target).attr("navigateGetAction");
                    var param = $(b.target).attr("param");

                    Hub.LoadController($(b.target).attr("navigateGetAction"), param);
                });
            });
        }
    }
}