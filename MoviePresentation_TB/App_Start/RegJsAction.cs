using System;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(MoviePresentation_TB.App_Start.RegJsAction), "PreStart")]

namespace MoviePresentation_TB.App_Start {
    public static class RegJsAction {
        public static void PreStart() {
            System.Web.Routing.RouteTable.Routes.Add("JsActionRoute", JsAction.JsActionRouteHandlerInstance.JsActionRoute);
        }
    }
}
