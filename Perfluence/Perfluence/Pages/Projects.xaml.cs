using MySqlConnector;
using MySqlX.XDevAPI.Relational;
using Perfluence.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Perfluence.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Projects : ContentPage
    {
        public Projects()
        {
            InitializeComponent();
            LoadData();
        }
        List<string> IDs = new List<string>();
        public void LoadData()
        {
            DataTable table = SQL.GetTable("SELECT * FROM projects JOIN advertisers ON advertisers.ID = Company");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                IDs.Add(table.Rows[i]["ID"].ToString());
                Projs.Children.Add(UserControl(table.Rows[i]["ID"].ToString(), table.Rows[i]["Title"].ToString(), table.Rows[i]["Logo"], table.Rows[i]["Title1"].ToString(), table.Rows[i]["Specification"].ToString()));
            }
        }
        private StackLayout UserControl(string ID, string Title, object Photo, string Company, string Specification)
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
            Button button = new Button();

            img.WidthRequest = 80;
            img.HeightRequest = 80;
            title.WidthRequest = 150;
            company.WidthRequest = 50;
            specification.WidthRequest = 150;
            tags.WidthRequest = 150;

            title.Text = Title;
            company.Text = Company;
            specification.Text = Specification;
            try
            {
                var stream = new MemoryStream((byte[])Photo);
                img.Source = ImageSource.FromStream(() => stream);
            }
            catch 
            {
                img.Source = ph.Source;
            }
            button.Text = "+";
            button.Pressed += new EventHandler(Press);

            slimg.Children.Add(img);
            slimg.Children.Add(company);
            slinfo.Children.Add(title);
            slinfo.Children.Add(specification);
            slinfo.Children.Add(tags);

            sl.Children.Add(slimg);
            sl.Children.Add(slinfo);
            sl.Children.Add(button);

            return sl;
        }
        private void Press(object sender, EventArgs e)
        {
            SQL.DoQuery("INSERT INTO requests (`Blogger`, `Project`, `WhenDate`, `Status`) VALUES ('" + UserData.ID + "', '1', now(), 'На рассмотрении')");

            DisplayAlert("Успешно!", "Ваша заявка на участие в проекте отослана!", "ОК");
        }
    }
}