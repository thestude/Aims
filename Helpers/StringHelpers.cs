using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Helpers
{
    public static class StringHelpers
    {
        public static string ToStringOrEmpty(this object strObj)
        {
            return (strObj ?? string.Empty).ToString();
        }

    }
}