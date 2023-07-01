using Perfluence.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySqlConnector;
using System.Data;
using Perfluence.Classes;

namespace Perfluence
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            Classes.SQL.con.Open();
        }
        private void ShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (ShowPassword.IsChecked)
                Passcode.IsPassword = false;
            else
                Passcode.IsPassword = true;
        }

        private void Enter_Pressed(object sender, EventArgs e)
        {
            if (Login.Text != null && Passcode.Text != null)
            {
                DataTable table = SQL.GetTable("SELECT * FROM bloggers WHERE Login = '" + Login.Text + "' AND Passcode = '" + Passcode.Text + "'");
                if (table.Rows.Count != 0)
                {
                    UserData.ID = table.Rows[0]["ID"].ToString();

                    Navigation.PushAsync(new MainMenu());
                }
                else
                    DisplayAlert("Ошибка!", "Вы ввели неверные данные!", "ОК");
            }
            else
                DisplayAlert("Ошибка!", "Вы должны заполнить все поля!", "ОК");
        }

        private void Registr_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registration());
        }
    }
}
