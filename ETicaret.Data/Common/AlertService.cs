using ETicaret.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Common
{
    public class AlertService
    {
//        <div class="alert alert-warning alert-dismissible fade show" role="alert">
//  <button type = "button" class="close" data-dismiss="alert" aria-label="Close">
//    <span aria-hidden="true">&times;</span>
//  </button>
//  <strong>Holy guacamole!</strong> You should check in on some of those fields below.
//</div>
        public static string ShowAlert(AlertStates state, string message)
        {
            string alertDiv = null;
            switch (state)
            {
                case AlertStates.Success:
                    alertDiv = "<div class='alert alert-success alert-dismissable' role='alert'><a href='#' class='close' data-dismiss='alert'>&times;</a>" + message + "</div>";
                    break;
                case AlertStates.Danger:
                    alertDiv = "<div class='alert alert-danger alert-dismissable' role='alert'><a href='#' class='close' data-dismiss='alert'>&times;</a>" + message + "</div>";
                    break;
                case AlertStates.Info:
                    alertDiv = "<div class='alert alert-info alert-dismissable' role='alert'><button type='button' class='btn-close' data-dismiss='alert'></button> " + message + "</a></div>";
                    break;
                case AlertStates.Warning:
                    alertDiv = "<div class='alert alert-warning alert-dismissable' role='alert'><button type='button' class='btn-close' data-dismiss='alert'></button> " + message + "</a></div>";
                    break;
            }
            return alertDiv;

        }
    }
}
