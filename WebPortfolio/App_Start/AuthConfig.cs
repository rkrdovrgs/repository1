using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using WebPortfolio.Models;

namespace WebPortfolio
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "12",
                clientSecret: "12");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "12",
                consumerSecret: "12");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "12",
                appSecret: "12");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
