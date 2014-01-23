using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit.Api;
using Fitbit.Models;

namespace FitWin
{
    class Program
    {
        const string CONSUMER_KEY = "db99dd605119432cb2deff773a4cc9ad";
        const string CONSUMER_SECRET = "533f23e4f37c437aa8a202a9a8f1e706";

        static void Main(string[] args)
        {
            var credentials = GetLoginCredentials();

            var fitbitClient = new FitbitClient(CONSUMER_KEY, CONSUMER_SECRET, credentials.AuthToken, credentials.AuthTokenSecret);
            var profile = fitbitClient.GetUserProfile();

            Activity dayActivity = fitbitClient.GetDayActivity(DateTime.Today);

            Console.ReadLine();
        }

        private static AuthCredential GetLoginCredentials()
        {
            const string requestTokenUrl = "http://api.fitbit.com/oauth/request_token";
            const string accessTokenUrl = "http://api.fitbit.com/oauth/access_token";
            const string authorizeUrl = "http://www.fitbit.com/oauth/authorize";

            var a = new Authenticator(CONSUMER_KEY, CONSUMER_SECRET, requestTokenUrl, accessTokenUrl, authorizeUrl);
            var url = a.GetAuthUrlToken();

            Process.Start(url);

            Console.WriteLine("Enter the verification code from the website");
            var pin = Console.ReadLine();

            var credentials = a.GetAuthCredentialFromPin(pin);
            return credentials;
        }
    }
}
