using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Roadie_UWP_MLB.Models;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Roadie_UWP_MLB.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();                 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Membername.Text = "Hello, " + Store.StoreInstance.members.First().firstName + "!";
            Store.StoreInstance.member = Store.StoreInstance.members.First();                    
            
            
            ////Navigated_Status.Text = e.Parameter.ToString();
            //if (e.Parameter != null)
            //{
            //    if (e.Parameter.ToString() == "Register")
            //    {
            //        First_Time_User.Visibility = Visibility.Visible;
            //    }

            //    else
            //    {
            //        First_Time_User.Visibility = Visibility.Collapsed;
            //    }
            //}          
            base.OnNavigatedTo(e);
        }
        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateProfile), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void Vehicles_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_Edit_Vehicles), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void New_User_Profile_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateProfile), null,
                   new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void Logout_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null,
                   new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void SearchForCarpool_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllRideGroups), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void CreateCarpool_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RideGroupCreation), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
