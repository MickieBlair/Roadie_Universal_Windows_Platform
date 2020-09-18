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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Roadie_UWP_MLB.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MemberList : Page
    {
        private dto_Member member { get; set; }
        private BLL bll { get; set; }

        public MemberList()
        {
            this.InitializeComponent();
            bll = new BLL();
            LoadData();
        }

        private async Task LoadData()
        {
            await LoadMembers();
        }

        private async Task LoadMembers()
        {

            var members = await bll.GetAllMembers();



            lb.ItemsSource = members.OrderBy(c => c.lastName).ToList();

        }

        private void Lb_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = (e.AddedItems[0] as dto_Member);

                member = item;

                grpArea.Visibility = Visibility.Collapsed;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // save code coming
            member.firstName = fn.Text;
            member.lastName = ln.Text;

            //using (DB_111206_roadieEntities db = new DB_111206_roadieEntities())
            //{
            //    var sqlmemberObject = db.Members.Where(c => c.memberID == member.memberID).FirstOrDefault();

            //    if (sqlmemberObject != null)
            //    {
            //        try
            //        {
            //            sqlmemberObject.firstName = member.firstName;
            //            sqlmemberObject.lastName = member.lastName;

            //            db.SaveChanges();
            //        }
            //        catch (Exception ex)
            //        {
            //            Debug.WriteLine(ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        // ADDING
            //        Member sqlobj = new Member();
            //        sqlobj.firstName = member.firstName;
            //        sqlobj.lastName = member.lastName;
            //        sqlobj.memberID = 0;
            //        db.Members.Add(sqlobj);

            //        db.SaveChanges();




            //    }
            //}
            LoadMembers();
            grpArea.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            grpArea.Visibility = Visibility.Collapsed;

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            member = new dto_Member();
            member.firstName = string.Empty;
            member.lastName = string.Empty;

            fn.Text = member.firstName;
            ln.Text = member.lastName;

            grpArea.Visibility = Visibility.Visible;

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (member != null)
            {
                ln.Text = member.lastName;
                fn.Text = member.firstName;


                grpArea.Visibility = Visibility.Visible;
            }
            //else
            //{
            //    MessageBox.Show("Click on a Member first");
            //}

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            //if (member != null)
            //{
            //    string v = string.Format("Are you sure you want to delete {0}", member.displayName);
            //    if (MessageBox.Show(v, "Delete Member", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            //    {
            //        using (DB_111206_roadieEntities db = new DB_111206_roadieEntities())
            //        {
            //            var sqlmemberObject = db.Members.Where(c => c.memberID == member.memberID).FirstOrDefault();
            //            db.Members.Remove(sqlmemberObject);
            //            db.SaveChanges();

            //            LoadMembers();

            //        }
            //    }
            //}
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {


            //ServiceReference1.Service1Client svc = new ServiceReference1.Service1Client();
            //string r = svc.LoginMember(member);
            //if (r != null)
            //    MessageBox.Show(r);
            //else
            //    MessageBox.Show("No");

        }
    }
}
