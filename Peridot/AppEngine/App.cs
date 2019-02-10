using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peridot.AppEngine
{
    class App
    {
        private Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        List<AppUser> users = new List<AppUser>();
        public string appFolder = ""; 
        public void cback(Session s)
        {
            string token = s.GetCookie("PERIDOT_APP_TOKEN");
            foreach (AppUser user in users)
            {
                if (token == user.Token)
                {
                    //I need toadd the reader rq
                    s.GenerateHTMLOutput(System.IO.File.ReadAllText(appFolder + "/" + user.CurrentPage));
                    return;
                }
            }
            string newToken = RandomString(40);
            s.CreateCookie("PERIDOT_APP_TOKEN", newToken);
            AppUser a = new AppUser();
            a.Token = newToken;
            users.Add(a);
        }
        public void setCurrentPageForUser(string token, string page)
        {
            foreach (AppUser user in users)
            {
                if (user.Token == token)
                {
                    user.CurrentPage = page;
                    return;
                }
            }
        }
        public string getCurrentUserAppToken(Session s)
        {
            return s.GetCookie("PERIDOT_APP_TOKEN");
        }
        public List<UserData> getUserData(string token)
        {
            foreach (AppUser user in users)
            {
                if (user.Token == token)
                {
                    return user.userData;
                }
            }
            return null;
        }
        public string getUserData(string token, string name)
        {
            foreach (UserData data in getUserData(token))
            {
                if (data.Name == name)
                {
                    return data.Data;
                }
            }
            return null;
        }
        public void addUserData(UserData data, string token)
        {
            foreach (AppUser user in users)
            {
                if (user.Token == token)
                {
                    user.userData.Add(data);
                }
            }
        }
        public void editUserData(string name, string newV, string token)
        {
            List<UserData> d = getUserData(token);
            foreach (UserData dd in d)
            {
                if (dd.Name == name)
                {
                    dd.Data = newV;
                    return;
                }
            }
        }
    }
    public class AppUser
    {
        public string Token;
        public string CurrentPage = "index.html";
        public List<UserData> userData = new List<UserData>();
    }
    public class UserData
    {
        public string Name;
        public string Data;
    }
}
