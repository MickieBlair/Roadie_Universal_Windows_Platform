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
using Roadie_UWP_MLB.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Roadie_UWP_MLB.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Personal_Information : Page
    {
        public Personal_Information()
        {
            this.InitializeComponent();
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


        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {

            var selectedState = "";
            try
            {
                selectedState = State.SelectedValue.ToString();
            }

            catch(Exception ex)
            {
                State.PlaceholderText = "State - Required";
                Status.Text = "Please Complete All Fields";
            }
                       
            

            if (CellPhone.Text == "" || StreetAddress1.Text == ""
                || City.Text == "" || Zip.Text == "")
            {
                CellPhone.PlaceholderText = "Cell Phone - Required";
                StreetAddress1.PlaceholderText = "Address - Required";
                City.PlaceholderText = "City - Required";
                Zip.PlaceholderText = "Zip Code - Required";
                Status.Text = "Please Complete All Fields";
            }
            else
            {
                if (Status.Text == "")
                {
                    Store.StoreInstance.member.phoneNumber = CellPhone.Text;
                    Store.StoreInstance.member.AddressLine1 = StreetAddress1.Text;
                    Store.StoreInstance.member.AddressLine2 = StreetAddress2.Text;
                    Store.StoreInstance.member.City = City.Text;
                    Store.StoreInstance.member.State = selectedState;
                    Store.StoreInstance.member.ZipCode = Zip.Text;
                    Store.StoreInstance.member.driverLicenseNumber = DriversLicenseNumber.Text;

                    DateTime issue = new DateTime(IssueDate.Date.Value.Year, IssueDate.Date.Value.Month, IssueDate.Date.Value.Day);

                    //var issue = IssueDate.Date;
                    //DateTime issued = issue.Value.DateTime;
                    //var formattedtime = issued.ToString("yyyy-mm-dd");                    
                    Store.StoreInstance.member.licenseValidityFrom = issue;



                    //var exp = ExpDate.Date;
                    //DateTime expires = exp.Value.Date;
                    //Store.StoreInstance.member.licenseValidityto = expires;
                    //1999 - 05 - 31T11: 20:00


                    var info_string = "\nStatus" + Status.Text + "/n First "
                        + Store.StoreInstance.member.firstName + "\n Last "
                        + Store.StoreInstance.member.lastName + "\n Email "
                        + Store.StoreInstance.member.email + "\n Gender "
                        + Store.StoreInstance.member.gender + "\n Password "
                        + Store.StoreInstance.member.password + "\n Add 1 "
                        + Store.StoreInstance.member.AddressLine1 + "\n Add2 "
                        + Store.StoreInstance.member.AddressLine2 + "\n City "
                        + Store.StoreInstance.member.City + "\n State "
                        + Store.StoreInstance.member.State + "\n Zip "
                        + Store.StoreInstance.member.ZipCode + "\n DL "
                        + Store.StoreInstance.member.driverLicenseNumber + "\n"


                        + Store.StoreInstance.member.licenseValidityFrom + "\n Expires ";
                        //+ Store.StoreInstance.member.licenseValidityto;

                    

                    Info.Text = info_string;
                   // Register_New_Member();             
                }            
            }
        }

        private async void Register_New_Member()
        {
            BLL bll = new BLL();

            var list = await bll.Register_Member(Store.StoreInstance.member);

            Status.Text = list.Count.ToString();

            Frame.Navigate(typeof(Login), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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
    }
}
