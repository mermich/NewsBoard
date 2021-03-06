﻿using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace ServerSideSpaTools
{
    /// <summary>
    /// Replaces ASP.MVC default search places for views. IE: views shoud be on same directory as the controllers.
    /// </summary>
    public class LocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // Swap /Shared/ for /_Shared/
            var copy = viewLocations.ToList();
            copy.Add("~/{2}/{1}/{0}.cshtml");
            copy.Add("~/{1}/{0}.cshtml");
            copy.Add("~/{0}.cshtml");
            return copy;

        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // do nothing.. not entirely needed for this 
            //context.ViewName = context.ViewName.Replace("EditorTemplates/", "");
        }
    }
}
