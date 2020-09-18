using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Roadie_UWP_MLB.Models
{
    [DataContract]
    public class Wrapper<T>
    {
        [DataMember]
        public List<T> Data { get; set; }
    }

    [DataContract]
    public class dto_LogEntry
    {
        [DataMember]
        public string comment { get; set; }
    }

    [DataContract]
    public class dto_Login
    {
        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string password { get; set; }
    }

    [DataContract]
    public class dto_Member
    {
        [DataMember]
        public int memberID { get; set; }

        [DataMember]
        public string firstName { get; set; }

        [DataMember]
        public string lastName { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string phoneNumber { get; set; }

        [DataMember]
        public string driverLicenseNumber { get; set; }

        [DataMember]
        //public Nullable<System.DateTime> licenseValidityFrom { get; set; }
        public string licenseValidityFrom { get; set; }

        [DataMember]
        //public Nullable<System.DateTime> licenseValidityto { get; set; }
        public string licenseValidityto { get; set; }

        [DataMember]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string AddressLine2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string gender { get; set; }

        //[DataMember]
        //public string displayName
        //{
        //    get
        //    {
        //        return string.Format("{0}, {1}",
        //        lastName,
        //        firstName);
        //    }
        //}  
    }

    [DataContract]
    public class dto_VehicleR
    {
        [DataMember]
        public int vehicleId { get; set; }
        [DataMember]
        public string vehicleRegistrationNumber { get; set; }
        [DataMember]
        public Nullable<int> numberOfSeats { get; set; }
        [DataMember]
        public string vehicleDiscription { get; set; }
        [DataMember]
        public Nullable<int> memberID { get; set; }
    }

    [DataContract]
    public class dto_Insurance
    {
        [DataMember]
        public int InsuranceID { get; set; }
        [DataMember]
        public Nullable<int> memberID { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string policy { get; set; }
        [DataMember]
        public string exprDate { get; set; }
    }

    [DataContract]
    public class dto_RideGroup
    {
        [DataMember]
        public int RideGroupID { get; set; }

        [DataMember]
        public Nullable<int> CreatedByMemberID { get; set; }

        [DataMember]
        public string RideGroupName { get; set; }

        [DataMember]
        public Nullable<decimal> BeginLAT { get; set; }

        [DataMember]
        public Nullable<decimal> BeginLONG { get; set; }

        [DataMember]
        public Nullable<decimal> EndLAT { get; set; }

        [DataMember]
        public Nullable<decimal> EndLONG { get; set; }

        [DataMember]
        public Nullable<decimal> BeginRadius { get; set; }

        [DataMember]
        public Nullable<decimal> EndRadius { get; set; }
    }


}
