using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Facebook.Login.Widget;
using System.Linq;

namespace MyMobileClientFB.Droid
{
    [Activity(Label = "MyMobileClientFB.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        MobileServiceClient client = new MobileServiceClient("https://daregame.azurewebsites.net");

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button loginBtn = FindViewById<Button>(Resource.Id.login);
            TextView IDTvw = FindViewById<TextView>(Resource.Id.iduser);
            Button logoutBtn = FindViewById<Button>(Resource.Id.logout);
            Button insertBtn = FindViewById<Button>(Resource.Id.insert);
            EditText entry = FindViewById<EditText>(Resource.Id.entry);
            ListView list = FindViewById<ListView>(Resource.Id.list);

            if (client.CurrentUser != null)
            {
                IDTvw.Text = client.CurrentUser.UserId;
                await ShowData(list);
            }


            loginBtn.Click += async delegate
            {
                var user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                var id = user?.UserId;
                var token = user?.MobileServiceAuthenticationToken;
                IDTvw.Text = id;
            };

            logoutBtn.Click += async delegate
            {
                await client.LogoutAsync();
                IDTvw.Text = "";
            };

            insertBtn.Click += async delegate
            {
                var entryText = entry.Text;
                var table = client.GetTable<Models.Dare>();
                await table.InsertAsync(new Models.Dare { Image = entryText });
                await ShowData(list);
            };
        }

        private async System.Threading.Tasks.Task ShowData(ListView list)
        {
            var table = client.GetTable<Models.Dare>();
            var items = await table.ReadAsync();
            list.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items.Select(p => p.Image).ToArray());
        }
    }
}

