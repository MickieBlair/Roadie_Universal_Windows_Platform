using Newtonsoft.Json;
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
    public sealed partial class UpdateProfile : Page
    {
        public UpdateProfile()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            FirstName.Text = Store.StoreInstance.member.firstName;
            LastName.Text = Store.StoreInstance.member.lastName;
            Email.Text = Store.StoreInstance.member.email;

            if (Store.StoreInstance.member.gender != null)
            {
                Gender.SelectedValue = Store.StoreInstance.member.gender;
            }

            if (Store.StoreInstance.member.phoneNumber != null)
            {
                CellPhone.Text = Store.StoreInstance.member.phoneNumber;
            }

            if (Store.StoreInstance.member.AddressLine1 != null)
            {
                StreetAddress1.Text = Store.StoreInstance.member.AddressLine1;
            }

            if (Store.StoreInstance.member.AddressLine2 != null)
            {
                StreetAddress2.Text = Store.StoreInstance.member.AddressLine2;
            }

            if (Store.StoreInstance.member.City != null)
            {
                City.Text = Store.StoreInstance.member.City;
            }

            if (Store.StoreInstance.member.State != null)
            {
                State.SelectedValue = Store.StoreInstance.member.State;
            }

            if (Store.StoreInstance.member.ZipCode != null)
            {
                Zip.Text = Store.StoreInstance.member.ZipCode;
            }

            if (Store.StoreInstance.member.driverLicenseNumber != null)
            {
                DriversLicenseNumber.Text = Store.StoreInstance.member.driverLicenseNumber;
            }


            if (Store.StoreInstance.member.licenseValidityFrom != null)
            {

                string str_return_issued = Store.StoreInstance.member.licenseValidityFrom;

                long returnTicks = Convert.ToInt64(str_return_issued.Substring(6, 13));

                DateTimeOffset returnedIssued = DateTimeOffset.FromUnixTimeMilliseconds(returnTicks);                              

                IssueDatePicker.Date = returnedIssued;

            }

            if (Store.StoreInstance.member.licenseValidityto != null)
            {
                string str_return_exp = Store.StoreInstance.member.licenseValidityto;
                long returnTicksExp = Convert.ToInt64(str_return_exp.Substring(6, 13));
                DateTimeOffset returnedExp = DateTimeOffset.FromUnixTimeMilliseconds(returnTicksExp);
                ExpDatePicker.Date = returnedExp;
            }
            base.OnNavigatedTo(e);
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

        private void Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status.Text = "";
        }

        private void CellPhone_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void StreetAddress1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void StreetAddress2_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void City_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }


        private void Zip_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void DriversLicenseNumber_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Status.Text = "";
        }

        private void IssueDate_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            Status.Text = "";
        }

        private void ExpDate_FocusEngaged(Control sender, FocusEngagedEventArgs args)
        {
            Status.Text = "";
        }

        private void State_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Status.Text = "";
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "" || LastName.Text == "" ||
                Email.Text == "")
            {
                FirstName.PlaceholderText = "First Name - Required";
                LastName.PlaceholderText = "Last Name - Required";
                Email.PlaceholderText = "Email - Required";
                Status.Text = "First Name, Last Name, and Email Required";

            }

            else
            {

                string selectedState;

                if (State.SelectedValue == null)
                {
                    selectedState = "";
                    Store.StoreInstance.member.State = null;
                }

                else
                {
                    selectedState = State.SelectedValue.ToString().Trim();
                    Store.StoreInstance.member.State = selectedState;
                }

                string selectedGender;

                if (Gender.SelectedValue == null)
                {
                    selectedGender = "";
                    Store.StoreInstance.member.gender = null;
                }

                else
                {
                    selectedGender = Gender.SelectedValue.ToString().Trim();
                    Store.StoreInstance.member.gender = selectedGender;
                }
                
                if (CellPhone.Text != "")
                {
                    Store.StoreInstance.member.phoneNumber = CellPhone.Text;
                }
                else
                {
                    Store.StoreInstance.member.phoneNumber = null;
                }

                if (StreetAddress1.Text != "")
                {
                    Store.StoreInstance.member.AddressLine1 = StreetAddress1.Text;
                }
                else
                {
                    Store.StoreInstance.member.AddressLine1 = null;
                }

                if (StreetAddress2.Text != "")
                {
                    Store.StoreInstance.member.AddressLine2 = StreetAddress2.Text;
                }
                else
                {
                    Store.StoreInstance.member.AddressLine2 = null;
                }

                if (City.Text != "")
                {
                    Store.StoreInstance.member.City = City.Text;
                }
                else
                {
                    Store.StoreInstance.member.City = null;
                }

                if (Zip.Text != "")
                {
                    Store.StoreInstance.member.ZipCode = Zip.Text;
                }
                else
                {
                    Store.StoreInstance.member.ZipCode = null;
                }

                if (DriversLicenseNumber.Text != "")
                {
                    Store.StoreInstance.member.driverLicenseNumber = DriversLicenseNumber.Text;
                }
                else
                {
                    Store.StoreInstance.member.driverLicenseNumber = null;
                }


                Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat
                };

                

                if (IssueDatePicker.Date != null)
                {
                    string issueDate = Newtonsoft.Json.JsonConvert.SerializeObject(IssueDatePicker.Date, settings);
                    string newissueDate = issueDate.Substring(8, 18);
                    Store.StoreInstance.member.licenseValidityFrom = "/Date(" + newissueDate + ")/"; 
                }

                if (ExpDatePicker.Date != null)
                {
                    string expDatestr = Newtonsoft.Json.JsonConvert.SerializeObject(ExpDatePicker.Date, settings);
                    string newexpDate = expDatestr.Substring(8, 18);
                    Store.StoreInstance.member.licenseValidityto = "/Date(" + newexpDate + ")/";
                }            

                Save_Member();

                Status.Text = "Profile Updated";
            }
        }

        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void Password_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            Password_Status.Text = "";
        }

        private void PasswordConfirm_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            Password_Status.Text = "";
        }

        private void Home_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
        //    Frame.Navigate(typeof(UpdateProfile), null,
        //        new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void Vehicles_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_Edit_Vehicles), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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

        private void SaveNewPassword_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "" || PasswordConfirm.Password == "")
            {
                Password_Status.Text = "Please Complete Both Fields";
            }
            else if (Password.Password == PasswordConfirm.Password)
            {
                Store.StoreInstance.member.password = Password.Password;
                Save_New_Password();
            }

            else
            {
                Password_Status.Text = "Confirm Password Does Not Match";
            }
        }

        private async void Save_New_Password()
        {
            BLL bll = new BLL();

            var list = await bll.SaveMember(Store.StoreInstance.member);
            Password.Password = "";
            PasswordConfirm.Password = "";
            Password_Status.Text = "Password Successfully Changed";
        }

        private async void Save_Member()
        {
            BLL bll = new BLL();

            var list = await bll.SaveMember(Store.StoreInstance.member);
            Password.Password = "";
            PasswordConfirm.Password = "";
        }

        private void Logout_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null,
                   new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }


        //Not an option in the short time left
        //private async void DeleteAccount_Btn_Click(object sender, RoutedEventArgs e)
        //{
        //    // Too Complicated have to delete all other references to memberID.  first.
        //    Frame.Navigate(typeof(Login), null,
        //           new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        //}
    }
}
