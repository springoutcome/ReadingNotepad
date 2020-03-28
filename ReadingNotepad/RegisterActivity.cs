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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);


            EditText name = FindViewById<EditText>(Resource.Id.edt_register_name);
            EditText mail = FindViewById<EditText>(Resource.Id.edt_register_email);
            EditText pass = FindViewById<EditText>(Resource.Id.edt_register_password);
            Button loginButton = FindViewById<Button>(Resource.Id.btn_register);
            TextView backButton = FindViewById<TextView>(Resource.Id.txt_register_back);

            loginButton.Click += delegate
            {
                string nt = name.Text;
                string mt = mail.Text;
                string pt = pass.Text;
                //Httpで送信処理
                Task.Run(() =>
                {
                    Task<Dictionary<string, string>> t = API.HttpLapper.Register(mt, nt, pt);
                    t.Wait();


                    Dictionary<string, string> d = t.Result;
                    this.RunOnUiThread(new Action(() =>
                    {
                        Toast.MakeText(this, d["message"], ToastLength.Long).Show();
                        if (d["error"] == "false")
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
            // Create your application here
        }
    }
}