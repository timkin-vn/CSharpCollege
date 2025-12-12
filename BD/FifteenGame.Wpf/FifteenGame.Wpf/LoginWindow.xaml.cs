using System.Windows;
using FifteenGame.Data;
using FifteenGame.Data.Entities;
using System.Linq;
using System;
namespace FifteenGame.Wpf
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
            txtUsername.SelectAll();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtUsername.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                    name = "Капитан";

                using (var db = new GameDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == name);
                    if (user == null)
                    {
                        user = new User
                        {
                            Username = name,
                            BestTimeSeconds = null  // Новый игрок — рекорда пока нет
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                    }

                    var mainWindow = new MainWindow(user.Username);
                    mainWindow.Show();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nInner: " + (ex.InnerException?.Message ?? "нет"), "Ошибка БД");
            }
        }


    }
}