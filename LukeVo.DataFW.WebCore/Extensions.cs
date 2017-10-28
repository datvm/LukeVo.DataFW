using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LukeVo.DataFW.WebCore
{

    public static class Extensions
    {

        public static T Service<T>(this Controller controller)
        {
            return (T) controller.HttpContext.RequestServices.GetService(typeof(T));
        }

        public static string GetUserId(this Controller controller)
        {
            return controller.User.Claims.FirstOrDefault(q => q.Type == "sub").Value; ;
        }

    }

}
