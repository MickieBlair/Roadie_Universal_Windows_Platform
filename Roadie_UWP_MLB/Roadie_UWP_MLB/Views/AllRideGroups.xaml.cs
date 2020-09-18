using Roadie_UWP_MLB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
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

namespace Roadie_UWP_MLB.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllRideGroups : Page
    {
        private dto_RideGroup ridegroup { get; set; }
        private BLL bll { get; set; }
        private BasicGeoposition geoposition;
        private Geoposition position;
        private BasicGeoposition startPool;
        private BasicGeoposition endPool;
        private MapIcon myPOI;


        public AllRideGroups()
        {
            this.InitializeComponent();
            bll = new BLL();
            LoadData();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Current_Location();
            base.OnNavigatedTo(e);
        }

        private async void Current_Location()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            var geolocator = new Geolocator();

            position = await geolocator.GetGeopositionAsync();

            var mapLocation = await MapLocationFinder.FindLocationsAtAsync(position.Coordinate.Point);


            geoposition = new BasicGeoposition();
            geoposition.Latitude = Math.Round(position.Coordinate.Point.Position.Latitude, 8);
            geoposition.Longitude = Math.Round(position.Coordinate.Point.Position.Longitude, 8);

            Geopoint myPoint = new Geopoint(geoposition);
            myPOI = new MapIcon
            {
                Location = myPoint,
                //Title = "You are Here!",
                NormalizedAnchorPoint = new Point(0.5, 1.0)
            };
            myPOI.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/here.png"));
            
            MapControl1.MapElements.Add(myPOI);
            MapControl1.Center = myPoint;
            MapControl1.ZoomLevel = 11;

            //tbOutputText.Text = "Current = (" +
            //          geoposition.Latitude.ToString() + "," +
            //          geoposition.Longitude.ToString() + ")";
        }


        private async Task LoadData()
        {
            await LoadRideGroups();
        }

        private async Task LoadRideGroups()
        {

            var groups = await bll.GetAllRideGroups();

            lb.ItemsSource = groups.OrderBy(c => c.RideGroupID).ToList();

        }

        private void Lb_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                MapControl1.MapElements.Clear();
                MapControl1.MapElements.Add(myPOI);
                var item = (e.AddedItems[0] as dto_RideGroup);

                ridegroup = item;

                startPool = new BasicGeoposition();
                startPool.Latitude = (double)ridegroup.BeginLAT;
                startPool.Longitude = (double)ridegroup.BeginLONG;
                endPool = new BasicGeoposition();
                endPool.Latitude = (double)ridegroup.EndLAT;
                endPool.Longitude = (double)ridegroup.EndLONG;

                Geopoint startPoint = new Geopoint(startPool);
                MapIcon startIcon = new MapIcon
                {
                    Location = startPoint,
                    NormalizedAnchorPoint = new Point(0.5, 1.0)
                };
                startIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/start.png"));
                MapControl1.MapElements.Add(startIcon);

                Geopoint endPoint = new Geopoint(endPool);
                MapIcon endIcon = new MapIcon
                {
                    Location = endPoint,
                    NormalizedAnchorPoint = new Point(0.5, 1.0)
                };
                endIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/end.png"));
                MapControl1.MapElements.Add(endIcon);

                MapControl1.Center = endPoint;
                MapControl1.ZoomLevel = 8;
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

        private void Vehicles_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_Edit_Vehicles), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void SearchForCarpool_Btn_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(AllRideGroups), null,
            //    new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void CreateCarpool_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RideGroupCreation), null,
                new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }


        private void Logout_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login), null,
                   new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }
        
    }
}
