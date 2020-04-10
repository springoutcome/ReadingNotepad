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
        public static async Task<Dictionary<string, string>> GetAllUsers()
        {
            return await HttpRequester.NoQueryGet<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/users");
        }
        //レスポンスはerrCode,message
        public static async Task<Dictionary<string, string>> Login(string email, string password)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("email", email);
            d.Add("password", password);
            return await HttpRequester.Post<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/users/login");
        }

        public static async Task<Dictionary<string, string>> Register(string email, string name, string password)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("email", email);
            d.Add("name", name);
            d.Add("password", password);
            return await HttpRequester.Post<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/users/register");
        }

        public static async Task<Dictionary<string,string>> GetUser(string userID)
        {
            return await HttpRequester.NoQueryGet<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/users/" + userID);
        }

        public static async Task<Dictionary<string, string>> UpdateUser(string userID,string email, string name, string password)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("email", email);
            d.Add("name", name);
            d.Add("password", password);
            return await HttpRequester.Put<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/users/"+userID);
        }

        public static async Task<Dictionary<string, string>> DeleteUser(string userID)
        {
            return await HttpRequester.Delete<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/users/" + userID);
        }

        public static async Task<Dictionary<string, string>> GetAllBooks(string userID)
        {
            return await HttpRequester.NoQueryGet<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/books");
        }

        public static async Task<Dictionary<string, string>> BookRegister(string user_id, string title, string auther, string category,string photo,string impression)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("user_id",user_id);
            d.Add("title", title);
            d.Add("auther", auther);
            d.Add("category", category);
            d.Add("photo", photo);
            d.Add("impression", impression);
            return await HttpRequester.Post<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/books/register");
        }

        public static async Task<Dictionary<string, string>> GetBook(string bookID)
        {
            return await HttpRequester.NoQueryGet<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/books/book_id/"+bookID);
        }

        public static async Task<Dictionary<string, string>> GetUsersBook(string userID)
        {
            return await HttpRequester.NoQueryGet<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/books/user_id/" + userID);
        }

        public static async Task<Dictionary<string, string>> UpdateBook(string bookID, string user_id, string title, string auther, string category, string photo, string impression)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("user_id", user_id);
            d.Add("title", title);
            d.Add("auther", auther);
            d.Add("category", category);
            d.Add("photo", photo);
            d.Add("impression", impression);
            return await HttpRequester.Post<Dictionary<string, string>>(d, "https://rnserver2020.herokuapp.com/"+bookID);
        }

        public static async Task<Dictionary<string, string>> DeleteBook(string bookID)
        {
            return await HttpRequester.Delete<Dictionary<string, string>>("https://rnserver2020.herokuapp.com/" + bookID);
        }
    }
}
