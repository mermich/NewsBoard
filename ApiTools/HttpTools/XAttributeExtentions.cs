﻿using System;
using System.Xml.Linq;

namespace ApiTools.HttpTools
{
    public static class XAttributeExtentions
    {
        internal static string GetValueOrEmpty(this XAttribute element)
        {
            try
            {
                return element.Value;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
