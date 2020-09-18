using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Roadie_UWP_MLB.Models;


namespace Roadie_UWP_MLB.Models
{
    public class BLL
    {

        #region Init 
        public string URL { get; set; }
        public HttpClient client { get; set; }


        public BLL()
        {
            URL = @"http://www.jerrybhill.com/roadie/Service1.svc/";

            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); client.Timeout = new TimeSpan(0, 5, 0);

        }
        #endregion
        public async Task<List<dto_Member>> GetAllMembers()
        {
            List<dto_Member> retlist = new List<dto_Member>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "GetMembers"), "");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Member>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Member>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.members = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.memberID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;


        }

        public async Task<List<dto_RideGroup>> GetAllRideGroups()
        {
            List<dto_RideGroup> retlist = new List<dto_RideGroup>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "GetRideGroups"), "");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_RideGroup>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_RideGroup>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.rideGroups = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.RideGroupID);
                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;


        }

        public async Task<List<dto_Member>> Login(string email, string password)
        {
            List<dto_Member> retlist = new List<dto_Member>();

            dto_Login x = new dto_Login();

            x.email = email;
            x.password = password;
                
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "Login"), x);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Member>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Member>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.members = retlist;
                    Store.StoreInstance.member = Store.StoreInstance.members.First();

                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.memberID);
                        
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_Member>> Register_Member(dto_Member member)
        {

            List<dto_Member> retlist = new List<dto_Member>();

            
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "Register_Member"), member);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Member>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Member>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.members = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.memberID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_VehicleR>> GetAllMemberVehicles(dto_Member member)
        {
            List<dto_VehicleR> retlist = new List<dto_VehicleR>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "GetVehicles"), member);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_VehicleR>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_VehicleR>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.vehicles = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.vehicleId);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;


        }

        public async Task<List<dto_Member>> SaveMember(dto_Member member)
        {
            
            //Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings
            //{
            //    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat
            //};

            //string json_member= Newtonsoft.Json.JsonConvert.SerializeObject(member, settings);
            

            List<dto_Member> retlist = new List<dto_Member>();

            try
            {                
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "SaveMember"), member);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Member>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Member>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.members = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.memberID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }


        public async Task<List<dto_VehicleR>> SaveVehicle(dto_VehicleR vehicle)
        {
            List<dto_VehicleR> retlist = new List<dto_VehicleR>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "SaveVehicle"), vehicle);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_VehicleR>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_VehicleR>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.vehicles = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.vehicleId);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_VehicleR>> DeleteVehicle(dto_VehicleR vehicle)
        {
            List<dto_VehicleR> retlist = new List<dto_VehicleR>();


            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "DeleteVehicle"), vehicle);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_VehicleR>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_VehicleR>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.vehicles = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.memberID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_Insurance>> SaveInsurance(dto_Insurance insurance)
        {
            List<dto_Insurance> retlist = new List<dto_Insurance>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "SaveInsurance"), insurance);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Insurance>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Insurance>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.insurances = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.InsuranceID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_Insurance>> ReadInsurance(dto_Member member)
        {
            List<dto_Insurance> retlist = new List<dto_Insurance>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "ReadInsurance"), member);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Insurance>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Insurance>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.insurances = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.InsuranceID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_Insurance>> DeleteInsurance(dto_Insurance insurance)
        {
            List<dto_Insurance> retlist = new List<dto_Insurance>();


            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "DeleteInsurance"), insurance);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_Insurance>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_Insurance>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.insurances = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.InsuranceID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }

        public async Task<List<dto_RideGroup>> SaveRideGroup(dto_RideGroup rideGroup)
        {
            List<dto_RideGroup> retlist = new List<dto_RideGroup>();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, "SaveRideGroup"), rideGroup);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var des = (Wrapper<dto_RideGroup>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Wrapper<dto_RideGroup>));

                retlist = des.Data.ToList();

                if (retlist.Count > 0)
                {
                    Store.StoreInstance.rideGroups = retlist;
                    foreach (var d in retlist)
                        Debug.WriteLine("{0}", d.RideGroupID);
                }


            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retlist;
        }



    }
}
