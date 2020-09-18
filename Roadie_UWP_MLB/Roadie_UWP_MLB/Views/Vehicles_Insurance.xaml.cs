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
    public sealed partial class Add_Edit_Vehicles : Page
    {
        private dto_VehicleR vehicle { get; set; }
        private dto_Insurance insurance { get; set; }

        private BLL bll { get; set; }

        public Add_Edit_Vehicles()
        {
            this.InitializeComponent();
            bll = new BLL();        
            LoadData();
            

        }

        private async Task LoadData()
        {
            await LoadVehicles();           
        }

        private async Task LoadVehicles()
        {
            
            var vehicles = await bll.GetAllMemberVehicles(Store.StoreInstance.member);
            var insurances = await bll.ReadInsurance(Store.StoreInstance.member);            
            VehicleCount.Text = vehicles.Count + " vehicle(s) in account";
            if (insurances.Count > 0)
            {
                InsuranceCount.Text = "Insurance Policy On File";
                Store.StoreInstance.insurance = insurances.First();
                InsuranceCompany.Text = Store.StoreInstance.insurance.company;
                PolicyNumber.Text = Store.StoreInstance.insurance.policy;

                if (Store.StoreInstance.insurance.exprDate != null)
                {
                    string str_return_exp = Store.StoreInstance.insurance.exprDate;
                    long returnTicksExp = Convert.ToInt64(str_return_exp.Substring(6, 13));
                    DateTimeOffset returnedExp = DateTimeOffset.FromUnixTimeMilliseconds(returnTicksExp);
                    ExpInsDatePicker.Date = returnedExp;
                }
            }
            else
            {
                Store.StoreInstance.insurance = new dto_Insurance();
            }
            vehicle_list.ItemsSource = vehicles.OrderBy(c => c.vehicleDiscription).ToList();
            
           
        }

        private void vehicle_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            if (e.AddedItems.Count > 0)
            {
                var item = (e.AddedItems[0] as dto_VehicleR);

                vehicle = item;

                grpArea.Visibility = Visibility.Collapsed;

                Status.Text = "";
                Status.Visibility = Visibility.Collapsed;
            }

            
        }

        private void Home_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateProfile), null,
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

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            
            vehicle = new dto_VehicleR();

            vehicle.vehicleDiscription = string.Empty;
            vehicle.vehicleRegistrationNumber = string.Empty;
            vehicle.numberOfSeats = 0;

            Description.Text = vehicle.vehicleDiscription;
            TagNumber.Text = vehicle.vehicleRegistrationNumber;
            NumberOfSeats.Text = "";
            if (vehicle.numberOfSeats != 0)
            {
                NumberOfSeats.Text = vehicle.numberOfSeats.ToString();
            }
            
            grpArea.Visibility = Visibility.Visible;
            Title.Text = "Add New Vehicle";
            AddSave_Btn.Visibility = Visibility.Visible;
            SaveEdit_Btn.Visibility = Visibility.Collapsed;
            Remove_Btn.Visibility = Visibility.Collapsed;
            Button_Panel.Visibility = Visibility.Collapsed;
            Status.Visibility = Visibility.Collapsed;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vehicle.vehicleDiscription = Description.Text;
            vehicle.vehicleRegistrationNumber = TagNumber.Text;
            vehicle.numberOfSeats = Convert.ToInt32(NumberOfSeats.Text);
            vehicle.memberID = Store.StoreInstance.member.memberID;
            SaveVehicle();                               
            LoadVehicles();
            grpArea.Visibility = Visibility.Collapsed;
            Title.Text = "";
            Status.Text = "Vehicle Added";
            AddSave_Btn.Visibility = Visibility.Collapsed;
            SaveEdit_Btn.Visibility = Visibility.Visible;
            Remove_Btn.Visibility = Visibility.Collapsed;
            Status.Visibility = Visibility.Visible;            
            Button_Panel.Visibility = Visibility.Visible;
            vehicle = null;
        }

        private async void SaveVehicle()
        {
            var list = await bll.SaveVehicle(vehicle);
            LoadData();
        }


        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (vehicle != null)
            {
                if (vehicle.vehicleId == 0)
                {
                    Status.Text = "No Vehicle Selected";
                    Status.Visibility = Visibility.Visible;
                }

                else
                {
                    Description.Text = vehicle.vehicleDiscription;
                    TagNumber.Text = vehicle.vehicleRegistrationNumber;
                    NumberOfSeats.Text = vehicle.numberOfSeats.ToString();
                    grpArea.Visibility = Visibility.Visible;
                    Title.Text = "Edit Vehicle";
                    Button_Panel.Visibility = Visibility.Collapsed;
                    AddSave_Btn.Visibility = Visibility.Collapsed;
                    SaveEdit_Btn.Visibility = Visibility.Visible;
                    Remove_Btn.Visibility = Visibility.Collapsed;
                }


            }
            else
            {
                Status.Text = "No Vehicle Selected";
                Status.Visibility = Visibility.Visible;
            }

            
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            grpArea.Visibility = Visibility.Collapsed;
            Title.Text = "";
            Button_Panel.Visibility = Visibility.Visible;
            AddSave_Btn.Visibility = Visibility.Collapsed;
            SaveEdit_Btn.Visibility = Visibility.Collapsed;
            Remove_Btn.Visibility = Visibility.Collapsed;

        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (vehicle != null)
            {
                if (vehicle.vehicleId == 0)
                {
                    Status.Text = "No Vehicle Selected";
                    Status.Visibility = Visibility.Visible;
                }

                else
                {
                    Description.Text = vehicle.vehicleDiscription;
                    TagNumber.Text = vehicle.vehicleRegistrationNumber;
                    NumberOfSeats.Text = vehicle.numberOfSeats.ToString();
                    grpArea.Visibility = Visibility.Visible;
                    Title.Text = "Delete Vehicle";
                    AddSave_Btn.Visibility = Visibility.Collapsed;
                    SaveEdit_Btn.Visibility = Visibility.Collapsed;
                    Remove_Btn.Visibility = Visibility.Visible;
                    Button_Panel.Visibility = Visibility.Collapsed;
                    
                }
            }
            else
            {
                Status.Text = "No Vehicle Selected";
                Status.Visibility = Visibility.Visible;
            }
        }

        private void Logout_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null,
                   new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void SaveEdit_Btn_Click(object sender, RoutedEventArgs e)
        {
            vehicle.vehicleDiscription = Description.Text;
            vehicle.vehicleRegistrationNumber = TagNumber.Text;
            vehicle.numberOfSeats = Convert.ToInt32(NumberOfSeats.Text);
            vehicle.memberID = Store.StoreInstance.member.memberID;
            SaveVehicle();
            grpArea.Visibility = Visibility.Collapsed;
            Title.Text = "";
            Status.Text = "Vehicle Updated";
            AddSave_Btn.Visibility = Visibility.Collapsed;
            SaveEdit_Btn.Visibility = Visibility.Collapsed;
            Remove_Btn.Visibility = Visibility.Collapsed;
            Status.Visibility = Visibility.Visible;
            Button_Panel.Visibility = Visibility.Visible;
            vehicle = null;
            LoadVehicles();
            
        }

        private async void Remove_Btn_Click(object sender, RoutedEventArgs e)
        {
            var list = await bll.DeleteVehicle(vehicle);
            LoadVehicles();
            if (Store.StoreInstance.vehicles.Count == 0)
            {
                Status.Text = "Vehicle Deleted - 0 Vehicles in account";
            }
            else
            {
                Status.Text = "Vehicle Deleted";
            }
            
            
            Status.Visibility = Visibility.Visible;
            grpArea.Visibility = Visibility.Collapsed;
            Title.Text = "";
            Button_Panel.Visibility = Visibility.Visible;
            vehicle = null;
        }

        
        private void UpdateIns_Btn_Click(object sender, RoutedEventArgs e)
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat
            };


            if (Store.StoreInstance.insurance.InsuranceID == 0)
            {
                Store.StoreInstance.insurance.company = InsuranceCompany.Text;
                Store.StoreInstance.insurance.policy = PolicyNumber.Text;
                Store.StoreInstance.insurance.memberID = Store.StoreInstance.member.memberID;
                if (ExpInsDatePicker.Date.Year == 1600)
                {
                    Store.StoreInstance.insurance.exprDate = null;
                }
                else /*if (ExpInsDatePicker.Date != null)*/
                {
                    string expDatestr = Newtonsoft.Json.JsonConvert.SerializeObject(ExpInsDatePicker.Date, settings);
                    string newexpDate = expDatestr.Substring(8, 18);
                    Store.StoreInstance.insurance.exprDate = "/Date(" + newexpDate + ")/";
                }
                SaveInsurance();
            }
                
            else
            {
                Store.StoreInstance.insurance.InsuranceID = Store.StoreInstance.insurances.First().InsuranceID;
                Store.StoreInstance.insurance.company = InsuranceCompany.Text;
                Store.StoreInstance.insurance.policy = PolicyNumber.Text;
                Store.StoreInstance.insurance.memberID = Store.StoreInstance.member.memberID;
                if (ExpInsDatePicker.Date != null)
                {
                    string expDatestr = Newtonsoft.Json.JsonConvert.SerializeObject(ExpInsDatePicker.Date, settings);
                    string newexpDate = expDatestr.Substring(8, 18);
                    Store.StoreInstance.insurance.exprDate = "/Date(" + newexpDate + ")/";
                }
                SaveInsurance();
            }
            
            
        }

        private void Ins_Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            DeleteInsurance();
        }

        private async void SaveInsurance()
        {
            var list = await bll.SaveInsurance(Store.StoreInstance.insurance);
            Ins_Status.Text = "Insurance Updated";
            LoadData();


        }

        private async void DeleteInsurance()
        {
            var list = await bll.DeleteInsurance(Store.StoreInstance.insurance);
            Ins_Status.Text = "Insurance Deleted";
            InsuranceCount.Text = "No insurance associated with account.";
            Store.StoreInstance.insurance = null;
            InsuranceCompany.Text = "";
            PolicyNumber.Text = "";
            var timeString = "12/31/1600 7:00:00 PM -05:00";
            ExpInsDatePicker.Date = DateTimeOffset.Parse(timeString);
            LoadData();


        }
    }
}
