using Roadie_UWP_MLB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Roadie_UWP_MLB.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            Store.StoreInstance.member = new dto_Member();
            
            Frame.Navigate(typeof(SignUp), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            
                       
        }

        private void MemberList_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MemberList), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            BLL bll = new BLL();

            var list = await bll.Login(Email.Text, Password.Password);

            if (list.Count == 0)
            {
                if (Email.Text == "" || Password.Password == "")
                {
                    Status.Text = "Email and Password Required";
                }
                else
                {
                    Status.Text = "Login Failed";
                }
                
            }

            else if (Store.StoreInstance.members.Count > 0)
            {
                Frame.Navigate(typeof(Home), "Login",
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

            }
        }

        private void Email_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void Password_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            Status.Text = "";
        }
    }
}
