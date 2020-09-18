using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roadie_UWP_MLB.Models
{
    public sealed class Store
    {
        // Singleton
        private static readonly Lazy<Store> store = new Lazy<Store>(() => new Store());
        public Store()
        {
        }
        public static Store StoreInstance
        {
            get { return store.Value; }
        }

        public List<dto_Member> members { get; set; }
        public dto_Member member { get; set; }

        public List<dto_VehicleR> vehicles { get; set; }
        public dto_VehicleR vehicle { get; set; }        

        public dto_Insurance insurance { get; set; }
        public List<dto_Insurance> insurances { get; set; }

        public dto_RideGroup rideGroup { get; set; }
        public List<dto_RideGroup> rideGroups { get; set; }
    }
    
}
