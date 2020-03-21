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

namespace ReadingNotepad
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            // Create your application here
            //login_btn
            EditText mail = FindViewById<EditText>(Resource.Id.login_mail);
            EditText pass = FindViewById<EditText>(Resource.Id.login_pass);
            Button loginButton = FindViewById<Button>(Resource.Id.login_btn);
            Button backButton = FindViewById<Button>(Resource.Id.login_backBtn);

            loginButton.Click += delegate
            {
                string mt = mail.Text;
                string pt = pass.Text;
                //Httpで送信処理
                Task.Run(() =>
                {
                    Task<Dictionary<string, string>> t = API.HttpLapper.Login(mt, pt);
                    t.Wait();
                    //他スレッドからコントールにアクセスできない→Invokeメソッドアクセスできる


                    Dictionary<string, string> d = t.Result;
                    this.RunOnUiThread(new Action(()=> 
                    {
                        Toast.MakeText(Android.App.Application.Context, d["message"], ToastLength.Long).Show();
                        if (d["errCode"] == null)
                        {
                            //遷移処理
                            var intent = new Intent(this, typeof(MainActivity));
                            StartActivity(intent);
                        }
                    }));
                    



                });
            };

            backButton.Click += delegate
            {
                var intent = new Intent(this, typeof(TopActivity));
                StartActivity(intent);
            };
        }
    }
}