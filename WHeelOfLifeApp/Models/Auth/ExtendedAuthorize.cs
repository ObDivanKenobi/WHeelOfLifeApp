using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WHeelOfLifeApp.Models.Auth
{
    public class ExtendedAuthorizeAttribute : AuthorizeAttribute
    {
        private string redirectUrl;
        public string RedirectUrl
        {
            get { return redirectUrl; }
            set { redirectUrl = value; }
        }

        public ExtendedAuthorizeAttribute() : base() { }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                string authUrl = redirectUrl; //passed from attribute

                //if null, get it from config
                if (string.IsNullOrEmpty(authUrl))
                    authUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["RolesAuthRedirectUrl"];

                if (!string.IsNullOrEmpty(authUrl))
                    filterContext.HttpContext.Response.Redirect(authUrl);
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}