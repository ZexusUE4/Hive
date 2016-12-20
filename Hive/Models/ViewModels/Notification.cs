using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hive.Models.ViewModels
{
    public enum NotificationTypes
    {
        Success,
        Error,
        Warning,
    }

    public class Notification
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationTypes Type { get; set; }

        public string ToToastrString()
        {
            string ret = "toastr.";

            switch (Type)
            {
                case NotificationTypes.Success:
                    ret += "success";
                    break;
                case NotificationTypes.Error:
                    ret += "error";
                    break;
                case NotificationTypes.Warning:
                    ret += "warning";
                    break;
            }

            string errorToastrOptions = "{ \"closeButton\": true,\"debug\": false,\"progressBar\": false,\"positionClass\": \"toast-bottom-right\",";
            errorToastrOptions += "\"showDuration\": \"0\",\"hideDuration\": \"0\",\"timeOut\": \"0\",\"extendedTimeOut\": \"0\",\"showEasing\": \"swing\",";
            errorToastrOptions += "\"hideEasing\": \"linear\",\"showMethod\": \"fadeIn\",\"hideMethod\": \"fadeOut\"}";

            if (Type != NotificationTypes.Error)
            {
                ret += "(\"" + Content + "\",\"" + Title + "\");";
            }
            else
            {
                ret += "(\"" + Content + "\",\"" + Title + "\"," + errorToastrOptions + ");";
            }


            return ret;
        }
    }
}