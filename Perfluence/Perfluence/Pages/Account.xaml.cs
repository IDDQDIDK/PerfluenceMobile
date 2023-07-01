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
    public partial class Account : ContentPage
    {
        public Account()
        {
            InitializeComponent();
            DataTable table = SQL.GetTable("SELECT * FROM bloggers WHERE ID = " + UserData.ID);
            FirstName.Text = table.Rows[0]["FirstName"].ToString();
            SecondName.Text = table.Rows[0]["SecondName"].ToString();
            Patronymic.Text = table.Rows[0]["Patronymic"].ToString();
            BirthDate.Date = Convert.ToDateTime(table.Rows[0]["BirthDate"].ToString());
            BlogShow.Text = table.Rows[0]["Blog"].ToString();
            RequisitsShow.Text = table.Rows[0]["Requisits"].ToString();
            Blog.Text = table.Rows[0]["Blog"].ToString();
            Requisits.Text = table.Rows[0]["Requisits"].ToString();
            Email.Text = table.Rows[0]["Email"].ToString();
            Login.Text = table.Rows[0]["Login"].ToString();
            Passcode.Text = table.Rows[0]["Passcode"].ToString();
            try
            {
                var stream = new MemoryStream((byte[])table.Rows[0]["Photo"]);
                Img.Source = ImageSource.FromStream(() => stream);
            }
            catch
            {
                Img.Source = ph.Source;
            }
            
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            if (FirstName.Text != null && SecondName.Text != null && Patronymic.Text != null && BirthDate.Date.ToString() != null && Blog.Text != null && Requisits.Text != null && Email.Text != null && Login.Text != null && Passcode.Text != null)
            {
                DataTable check = SQL.GetTable("SELECT * FROM bloggers WHERE (Email = '" + Email.Text + "' OR Login = '" + Login.Text + "') AND ID != " + UserData.ID);
                if (check.Rows.Count == 0)
                {
                    SQL.DoQuery("UPDATE `bloggers` SET `FirstName` = '" + FirstName.Text + "', `SecondName` = '" + SecondName.Text + "', `Patronymic` = '" + Patronymic.Text + "', `BirthDate` = '" + BirthDate.Date.ToString() + "'," + " `Blog` = '" + Blog.Text + "', `Requisits` = '" + Requisits.Text + "', `Email` = '" + Email.Text + "', `Login` = '" + Login.Text + "', `Passcode` = '" + Passcode.Text + "' WHERE ID = " + UserData.ID);
                    DisplayAlert("Успешно!", "Данные изменены!", "ОК");
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