using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ReadingNotepad.API
{
    public static class HttpLapper
    {
        //レスポンスはerrCode,message
        public static async Task<Dictionary<string, string>> Login(string email, string password)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("email", email);
            d.Add("password", password);
            return await HttpRequester.Post<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/api/login");
        }

        public static async Task<Dictionary<string, string>> Register(string email, string name, string password)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("email", email);
            d.Add("name", name);
            d.Add("password", password);
            return await HttpRequester.Post<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/api/register");
        }
    }
}
