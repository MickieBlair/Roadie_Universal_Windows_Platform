using Roadie_UWP_MLB.Models;
using Roadie_UWP_MLB.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Roadie_UWP_MLB
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RideGroupCreation : Page
    {
        private dto_RideGroup newPool;
        private bool ValidStart;
        private bool ValidEnd;
        private bool ValidHome;       
        
        private BLL bll { get; set; }

        public RideGroupCreation()
        {
            this.InitializeComponent();
            bll = new BLL();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            newPool = new dto_RideGroup();
            if (Store.StoreInstance.member.AddressLine1 == null ||
                Store.StoreInstance.member.City == null ||
                Store.StoreInstance.member.State == null ||
                Store.StoreInstance.member.ZipCode == null)
            {
                ValidHome = false;
            }
            else
            {
                ValidHome = true;
            }

            base.OnNavigatedTo(e);
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
            //Frame.Navigate(typeof(RideGroupCreation), null,
            //    new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }


        private void Logout_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null,
                   new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void StartLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Start_Status_Red.Text = "";
            Start_Status_Red.Visibility = Visibility.Collapsed;
            End_Status_Red.Text = "";
            End_Status_Red.Visibility = Visibility.Collapsed;
            Creation_Status_Red.Text = "";
            Creation_Status_Red.Visibility = Visibility.Collapsed;

            if (StartLocation.SelectedValue.ToString() == "Home")
            {
                if (ValidHome)
                {
                    // Call Home Coordiates - set new pool START to home coordiates
                    CheckHomeLocation("Begin");
                }
                else
                {
                    //Display message no valid address.  Update Profile.
                    Start_Status_Red.Text = "Update Your Profile. No Home Address.";
                    Start_Status_Red.Visibility = Visibility.Visible;
                }
            }
            else
            {
                //Display the address box
                StartAddressAll.Visibility = Visibility.Visible;

            }            
        }

        private void EndLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Start_Status_Red.Text = "";
            Start_Status_Red.Visibility = Visibility.Collapsed;
            End_Status_Red.Text = "";
            End_Status_Red.Visibility = Visibility.Collapsed;
            Creation_Status_Red.Text = "";
            Creation_Status_Red.Visibility = Visibility.Collapsed;

            if (EndLocation.SelectedValue.ToString() == "Home")
            {
                if (ValidHome)
                {
                    // Call Home Coordiates - set new pool END to home coordiates
                    CheckHomeLocation("End");
                }
                else
                {
                    //Display message no valid address.  Update Profile.
                    End_Status_Red.Text = "Update Your Profile. No Home Address.";
                    End_Status_Red.Visibility = Visibility.Visible;
                }
            }
            else
            {
                //Display the address box
                EndAddressAll.Visibility = Visibility.Visible;
            }            
        }

        private async void CreateCarpool()
        {
            var list = await bll.SaveRideGroup(newPool);
            Creation_Status_Green.Text = "Carpool Created";
            Creation_Status_Green.Visibility = Visibility.Visible;
        }

        private void Check_Addresses_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(StartLocation.SelectedValue==null || EndLocation.SelectedValue==null)
            {
                Creation_Status_Red.Text = "Start and End are Required.";
                Creation_Status_Red.Visibility = Visibility.Visible;
            }            

            else
            {
                if (StartLocation.SelectedValue.ToString() != "Home")
                {
                    SetStartLocation();
                }

                if (EndLocation.SelectedValue.ToString() != "Home")
                {
                    SetEndLocation();
                }


                if (StartRadius.SelectedValue != null)
                {
                    newPool.BeginRadius = Convert.ToInt32(StartRadius.SelectedValue.ToString());
                }
                else
                {
                    newPool.BeginRadius = null;
                }

                if (EndRadius.SelectedValue != null)
                {
                    newPool.EndRadius = Convert.ToInt32(EndRadius.SelectedValue.ToString());
                }
                else
                {
                    newPool.EndRadius = null;
                }

                if (newPool.EndLAT !=null && newPool.EndLONG != null)
                {
                    newPool.CreatedByMemberID = Store.StoreInstance.member.memberID;
                    ResetMessages();
                    CreateCarpool();
                }
                
            }           
        }

        private async void CheckHomeLocation(string coordinatesTo)
        {
            string member_address = "";

            if (Store.StoreInstance.member.AddressLine1 != null
                && Store.StoreInstance.member.City != null
                && Store.StoreInstance.member.State != null
                && Store.StoreInstance.member.ZipCode != null)
            {
                member_address += Store.StoreInstance.member.AddressLine1.Trim() + ", ";

                if (Store.StoreInstance.member.AddressLine2 != null)
                {
                    member_address += Store.StoreInstance.member.AddressLine2.Trim() + ", ";
                }

                member_address += Store.StoreInstance.member.City.Trim() + ", ";

                member_address += Store.StoreInstance.member.State.Trim() + ", ";

                member_address += Store.StoreInstance.member.ZipCode.Trim();

            }
            

            if (member_address != "")
            {
                // The address or business to geocode.
                
                string addressToGeocode = member_address;

                // The nearby location to use as a query hint.
                BasicGeoposition queryHint = new BasicGeoposition();

                queryHint.Latitude = 47.643;
                queryHint.Longitude = -122.131;

                Geopoint hintPoint = new Geopoint(queryHint);

                // Geocode the specified address, using the specified reference point
                // as a query hint. Return no more than 1 results.
                MapLocationFinderResult result =
                        await MapLocationFinder.FindLocationsAsync(
                                        addressToGeocode,
                                        hintPoint,
                                        1);

                // If the query returns results, display the coordinates
                // of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {                   
                   if (coordinatesTo == "Begin")
                    {
                        ValidStart = true;
                        newPool.BeginLAT = (decimal)result.Locations[0].Point.Position.Latitude;
                        newPool.BeginLONG = (decimal)result.Locations[0].Point.Position.Longitude;
                        
                    }
                    else if (coordinatesTo == "End")
                    {
                        ValidEnd = true;
                        newPool.EndLAT = (decimal)result.Locations[0].Point.Position.Latitude;
                        newPool.EndLONG = (decimal)result.Locations[0].Point.Position.Longitude;
                        newPool.RideGroupName = member_address;
                    }                    
                }
                else
                {
                    if (coordinatesTo == "Begin")
                    {
                        ValidStart = false;                        
                    }
                    else if (coordinatesTo == "End")
                    {
                        ValidEnd = false;                        
                    }

                }

                //BasicGeoposition home = new BasicGeoposition();
                //home.Latitude = result.Locations[0].Point.Position.Latitude;
                //home.Longitude = result.Locations[0].Point.Position.Longitude;

                //Geopoint myhomePoint = new Geopoint(home);
                //MapIcon myhome = new MapIcon
                //{
                //    Location = myhomePoint,
                //    Title = "Home Address",
                //    NormalizedAnchorPoint = new Point(0.5, 1.0)
                //};

                //MapControl1.MapElements.Add(myhome);
                //MapControl1.Center = myhomePoint;
                //MapControl1.ZoomLevel = 11;
                
            }
        }

        private async void SetStartLocation()
        {
            string start_address = "";

            if (StartAddress1.Text != "")
            {
                start_address += StartAddress1.Text.Trim() + ", ";
            }

            if (StartAddress2.Text != "")
            {
                start_address += StartAddress2.Text.Trim() + ", ";
            }

            if (StartCity.Text != "")
            {
                start_address += StartCity.Text.Trim() + ", ";
            }

            string selectedState;

            if (StartState.SelectedValue == null)
            {
                selectedState = "";
            }

            else
            {
                selectedState = "";
                start_address += StartState.SelectedValue.ToString().Trim();
            }

            if (StartZip.Text != "")
            {
                start_address += StartZip.Text.Trim();
            }

            if (start_address != "")
            {
                // The address or business to geocode.
                //string addressToGeocode = "Microsoft";

                string addressToGeocode = start_address;

                // The nearby location to use as a query hint.
                BasicGeoposition queryHint = new BasicGeoposition();

                queryHint.Latitude = 47.643;
                queryHint.Longitude = -122.131;

                Geopoint hintPoint = new Geopoint(queryHint);

                // Geocode the specified address, using the specified reference point
                // as a query hint. Return no more than 1 results.
                MapLocationFinderResult result =
                      await MapLocationFinder.FindLocationsAsync(
                                        addressToGeocode,
                                        hintPoint,
                                        1);

                // If the query returns results, display the coordinates
                // of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {
                    try
                    {
                        newPool.BeginLAT = (decimal)result.Locations[0].Point.Position.Latitude;
                        newPool.BeginLONG = (decimal)result.Locations[0].Point.Position.Longitude;
                        ValidStart = true;
                    }
                    catch(Exception ex)
                    {
                        ValidStart = false;
                        Start_Status_Red.Text = "Address Not Found";
                        Start_Status_Green.Visibility = Visibility.Collapsed;
                        Start_Status_Red.Visibility = Visibility.Visible;
                    }                  
                }
                else
                {
                    ValidStart = false;
                    Start_Status_Red.Text = "Address Not Found";
                    Start_Status_Green.Visibility = Visibility.Collapsed;
                    Start_Status_Red.Visibility = Visibility.Visible;
                }

                //BasicGeoposition endAdd = new BasicGeoposition();
                //endAdd.Latitude = result.Locations[0].Point.Position.Latitude;
                //endAdd.Longitude = result.Locations[0].Point.Position.Longitude;

                //Geopoint myEndPoint = new Geopoint(endAdd);
                //MapIcon myEnd = new MapIcon
                //{
                //    Location = myEndPoint,
                //    Title = "End Address",
                //    NormalizedAnchorPoint = new Point(0.5, 1.0)
                //};

                //MapControl1.MapElements.Add(myEnd);
                //MapControl1.Center = myEndPoint;
                //MapControl1.ZoomLevel = 11;
            }

            else
            {
                ValidStart = false;
                Start_Status_Red.Text = "No Start Address Entered";
                Start_Status_Green.Visibility = Visibility.Collapsed;
                Start_Status_Red.Visibility = Visibility.Visible;
            }
        }

        private async void SetEndLocation()
        {
            string end_address = "";

            if (EndStreetAddress1.Text != "")
            {
                end_address += EndStreetAddress1.Text.Trim() + ", ";
            }

            if (EndStreetAddress2.Text != "")
            {
                end_address += EndStreetAddress2.Text.Trim() + ", ";
            }

            if (EndCity.Text != "")
            {
                end_address += EndCity.Text.Trim() + ", ";
            }

            string selectedState;

            if (EndState.SelectedValue == null)
            {
                selectedState = "";
            }

            else
            {
                selectedState = "";
                end_address += EndState.SelectedValue.ToString().Trim() + ", ";
            }

            if (EndZip.Text != "")
            {
                end_address += EndZip.Text.Trim();
            }

            if (end_address != "")
            {
                // The address or business to geocode.
                //string addressToGeocode = "Microsoft";

                string addressToGeocode = end_address;

                // The nearby location to use as a query hint.
                BasicGeoposition queryHint = new BasicGeoposition();

                queryHint.Latitude = 47.643;
                queryHint.Longitude = -122.131;

                Geopoint hintPoint = new Geopoint(queryHint);

                // Geocode the specified address, using the specified reference point
                // as a query hint. Return no more than 1 results.
                MapLocationFinderResult result =
                      await MapLocationFinder.FindLocationsAsync(
                                        addressToGeocode,
                                        hintPoint,
                                        1);



                // If the query returns results, display the coordinates
                // of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {
                    try
                    {
                        newPool.EndLAT = (decimal)result.Locations[0].Point.Position.Latitude;
                        newPool.EndLONG = (decimal)result.Locations[0].Point.Position.Longitude;
                        newPool.RideGroupName = end_address;
                        ValidEnd = true;
                    }
                    catch(Exception ex)
                    {
                        ValidEnd = false;
                        End_Status_Red.Text = "Address Not Found";
                        End_Status_Green.Visibility = Visibility.Collapsed;
                        End_Status_Red.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    End_Status_Red.Text = "End Address Not Found";
                    End_Status_Green.Visibility = Visibility.Collapsed;
                    End_Status_Red.Visibility = Visibility.Visible;
                }

                //BasicGeoposition endAdd = new BasicGeoposition();
                //endAdd.Latitude = result.Locations[0].Point.Position.Latitude;
                //endAdd.Longitude = result.Locations[0].Point.Position.Longitude;

                //Geopoint myEndPoint = new Geopoint(endAdd);
                //MapIcon myEnd = new MapIcon
                //{
                //    Location = myEndPoint,
                //    Title = "End Address",
                //    NormalizedAnchorPoint = new Point(0.5, 1.0)
                //};

                //MapControl1.MapElements.Add(myEnd);
                //MapControl1.Center = myEndPoint;
                //MapControl1.ZoomLevel = 11;
            }

            else
            {
                ValidEnd = false;
                End_Status_Red.Text = "No End Address Entered";
                End_Status_Green.Visibility = Visibility.Collapsed;
                End_Status_Red.Visibility = Visibility.Visible;
            }
        }

        private void ResetMessages()
        {
            Start_Status_Red.Text = "";
            Start_Status_Red.Visibility = Visibility.Collapsed;
            Start_Status_Green.Text = "";
            Start_Status_Green.Visibility = Visibility.Collapsed;
            End_Status_Red.Text = "";
            End_Status_Red.Visibility = Visibility.Collapsed;
            End_Status_Green.Text = "";
            End_Status_Green.Visibility = Visibility.Collapsed;
            Creation_Status_Red.Text = "";
            Creation_Status_Red.Visibility = Visibility.Collapsed;

        }

        private void StartAddress1_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void StartAddress2_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void StartCity_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void StartState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetMessages();
        }

        private void StartZip_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void EndStreetAddress1_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void EndStreetAddress2_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void EndCity_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }

        private void EndState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetMessages();
        }

        private void EndZip_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ResetMessages();
        }
    }
}
