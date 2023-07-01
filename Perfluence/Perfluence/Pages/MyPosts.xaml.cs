using Google.Protobuf.Collections;
using Perfluence.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Perfluence.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPosts : ContentPage
    {
        public MyPosts()
        {
            InitializeComponent();
            LoadData();
            DataTable table = SQL.GetTable("SELECT * FROM requests JOIN projects ON Project = projects.ID WHERE Blogger = " + UserData.ID + " AND `Status` = 'Одобрено'");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Project.Items.Add(table.Rows[i]["Title"].ToString());
            }
        }
        public void LoadData()
        {
            DataTable table = SQL.GetTable("SELECT * FROM payouts JOIN posts ON posts.ID = Post JOIN projects ON projects.ID = Project JOIN advertisers ON advertisers.ID = Company WHERE Blogger = " + UserData.ID + " AND projects.Title LIKE '%" + Search.Text + "%'");
            Posts.Children.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Posts.Children.Add(UserControl(table.Rows[i]["ID"].ToString(), table.Rows[i]["Title"].ToString(), (byte[])table.Rows[i]["Logo"], table.Rows[i]["Title1"].ToString(), table.Rows[i]["Specification"].ToString(), table.Rows[i]["WhenDate"].ToString()));
            }
        }
        private StackLayout UserControl(string ID, string Title, byte[] Photo, string Company, string Link, string Status)
        {
            StackLayout sl = new StackLayout();
            StackLayout slimg = new StackLayout();
            StackLayout slinfo = new StackLayout();
            sl.Orientation = StackOrientation.Horizontal;
            sl.HorizontalOptions = LayoutOptions.Center;
            sl.VerticalOptions = LayoutOptions.Center;
            sl.WidthRequest = 250;
            sl.HeightRequest = 150;

            Image img = new Image();
            Label title = new Label();
            Label company = new Label();
            Label specification = new Label();
            Label tags = new Label();

            img.WidthRequest = 80;
            img.HeightRequest = 80;
            title.WidthRequest = 150;
            company.WidthRequest = 50;
            specification.WidthRequest = 150;
            tags.WidthRequest = 150;
            

            title.Text = Title;
            company.Text = Company;
            if (Status == "01.01.0001 0:00:00")
                specification.Text = "Неоплачено";
            else
                specification.Text = "Оплачено!";
            var stream = new MemoryStream(Photo);
            img.Source = ImageSource.FromStream(() => stream);

            slimg.Children.Add(img);
            slimg.Children.Add(company);
            slinfo.Children.Add(title);
            slinfo.Children.Add(specification);
            slinfo.Children.Add(tags);

            sl.Children.Add(slimg);
            sl.Children.Add(slinfo);

            return sl;
        }
        private void Refresh_Pressed(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }
        private void Add_Pressed(object sender, EventArgs e)
        {
            if (Link.Text != null && Project.SelectedIndex != null)
            {
                DataTable table = SQL.GetTable("SELECT * FROM projects WHERE Title = '" + Project.Items[Project.SelectedIndex] + "'");
                SQL.DoQuery("INSERT INTO posts (Blogger, Link, Project, WhenDate, `Status`) VALUES ('" + UserData.ID + "', '" + Link.Text + "', '" + table.Rows[0][0].ToString() + "', now(), 'Новый')");
                DataTable table1 = SQL.GetTable("SELECT * FROM posts ORDER BY ID DESC");
                SQL.DoQuery("INSERT INTO payouts (Post, WhenDate) VALUES ('" + table1.Rows[0][0].ToString() + "', '0001-01-01')");
                DisplayAlert("Успешно!", "Вы добавили новый пост!", "Ок");
            }
            else
                DisplayAlert("Заполните все данные!", "Вы должны заполнить все поля!", "Ок");
        }
    }
}