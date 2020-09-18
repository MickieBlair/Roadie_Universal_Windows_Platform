using Roadie_UWP_MLB.Models;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Roadie_UWP_MLB.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
            


            //if (Store.StoreInstance.member.firstName != null)
            //{
            //    FirstName.Text = Store.StoreInstance.member.firstName;
            //}

            //if (Store.StoreInstance.member.lastName != null)
            //{
            //    LastName.Text = Store.StoreInstance.member.lastName;
            //}

            ////if (Store.StoreInstance.member.gender != null)
            ////{
            ////    Gender.Text = Store.StoreInstance.member.gender;
            ////}

            //if (Store.StoreInstance.member.email != null)
            //{
            //    Email.Text = Store.StoreInstance.member.email;
            //}
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });

        }

        private void ReadyToRide_Btn_Click(object sender, RoutedEventArgs e)
        {
            //var selectedGender = "";
            //try
            //{
            //    selectedGender = Gender.SelectedValue.ToString();
            //}

            //catch (Exception ex)
            //{
            //    Gender.PlaceholderText = "Gender - Required";
            //    Status.Text = "Please Complete All Fields";
            //}

            //|| selectedGender == ""



            if (FirstName.Text == ""  || LastName.Text == "" || Email.Text == "" 
                || Password.Password == "" || PasswordConfirm.Password == "" )
            {

                FirstName.PlaceholderText = "First Name - Required";
                LastName.PlaceholderText = "Last Name - Required";
                Email.PlaceholderText = "Email - Required";
                Password.PlaceholderText = "Password - Required";
                PasswordConfirm.PlaceholderText = "Confirm Password-Required";
                Status.Text = "Please Complete All Fields";
            }
            else
            {
                if (Password.Password == PasswordConfirm.Password)
                {
                    Store.StoreInstance.member.firstName = FirstName.Text;
                    Store.StoreInstance.member.lastName = LastName.Text;
                    //Store.StoreInstance.member.gender = selectedGender;
                    Store.StoreInstance.member.email = Email.Text;
                    Store.StoreInstance.member.password = Password.Password;

                    Register_New_Member();

                    //Frame.Navigate(typeof(Home), null,
                    //    new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    //Frame.Navigate(typeof(Personal_Information), null,
                    //    new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                }

                else
                {
                    Status.Text = "Confirm Password Does Not Match";
                }
            }            
        }

        private async void Register_New_Member()
        {
            BLL bll = new BLL();

            var list = await bll.Register_Member(Store.StoreInstance.member);
            
            Frame.Navigate(typeof(Home), "Register",
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }



        private void FirstName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void LastName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void Email_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        //private void Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Status.Text = "";
        //}

        private void Password_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void PasswordConfirm_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            Status.Text = "";
        }
    }
}
