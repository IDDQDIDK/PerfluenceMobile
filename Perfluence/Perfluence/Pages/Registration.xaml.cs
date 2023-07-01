using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Perfluence.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Perfluence.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            if (FirstName.Text != null && SecondName.Text != null && Patronymic.Text != null && BirthDate.Date.ToString() != null && Blog.Text != null && Requisits.Text != null && Email.Text != null && Login.Text != null && Passcode.Text != null)
            {
                DataTable check = SQL.GetTable("SELECT * FROM bloggers WHERE Email = '" + Email.Text + "' OR Login = '" + Login.Text + "'");
                if (check.Rows.Count == 0)
                {
                    SQL.DoQuery("INSERT INTO `bloggers` (`FirstName`, `SecondName`, `Patronymic`, `BirthDate`, `Blog`, `Requisits`, `Email`, `Login`, `Passcode`, `Status`) VALUES ('" +
                        FirstName.Text + "', '" + SecondName.Text + "', '" + Patronymic.Text + "', '" + BirthDate.Date.ToString() + "', '" + Blog.Text + "', '" + Requisits.Text + "', '" + Email.Text + "" +
                        "', '" + Login.Text + "', '" + Passcode.Text + "', 'Работает');");
                    DataTable user = SQL.GetTable("SELECT ID FROM bloggers ORDER BY ID DESC");
                    UserData.ID = user.Rows[0][0].ToString();
                    DisplayAlert("Успешно!", "Добро пожаловать в Perfluence!", "ОК");
                    Navigation.PushAsync(new MainMenu());
                }
                else
                    DisplayAlert("Ошибка!", "Такой Email или логин уже содержится в базе данных!", "ОК");
            }
            else
                DisplayAlert("Ошибка!", "Вы должны заполнить все поля!", "ОК");
        }
    }
}