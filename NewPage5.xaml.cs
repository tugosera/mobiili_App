using Microsoft.Maui.Media;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace mobiili_App;

public class Friend
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
}

public partial class NewPage5 : ContentPage
{
    private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
    private SQLiteConnection db;

    public NewPage5()
    {
        InitializeComponent();
        db = new SQLiteConnection(dbPath);
        db.CreateTable<Friend>();
        LoadContacts();
    }

    private void LoadContacts()
    {
        ContactsList.Children.Clear();
        var contacts = db.Table<Friend>().ToList();

        foreach (var contact in contacts)
        {
            var contactFrame = new Frame
            {
                Padding = 10,
                Margin = 10,
                CornerRadius = 10,
                BackgroundColor = Colors.LightGray,
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = new Command(() => SelectContact(contact))
                    }
                },
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Children =
                    {
                        new Image
                        {
                            Source = new UriImageSource { Uri = new Uri(contact.Image) },
                            WidthRequest = 40,
                            HeightRequest = 40
                        },
                        new StackLayout
                        {
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                new Label { Text = contact.Name },
                                new Label { Text = contact.Email, FontAttributes = FontAttributes.Bold },
                                new Label { Text = contact.Phone }
                            }
                        },
                        new ImageButton
{
                            Source = "delete.png",
                            BackgroundColor = Colors.Transparent,
                            WidthRequest = 30,
                            HeightRequest = 30,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            Command = new Command(() => DeleteContact(contact))
                        }
                    }
                }
            };

            ContactsList.Children.Add(contactFrame);
        }
    }

    private void SelectContact(Friend contact)
    {
        phoneEntry.Text = contact.Phone;
        email_phone.Text = contact.Email;
    }

    private async void DeleteContact(Friend friend)
    {
        bool confirm = await DisplayAlert("Kustuta kontakt", $"Kas soovid kindlasti kustutada {friend.Name}?", "Jah", "Ei");
        if (confirm)
        {
            db.Delete(friend);
            LoadContacts();
        }
    }

    private void AddContact_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(phone.Text) ||
            string.IsNullOrWhiteSpace(imageUrl.Text) ||
            string.IsNullOrWhiteSpace(name.Text) ||
            string.IsNullOrWhiteSpace(email.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        var friend = new Friend
        {
            Name = name.Text,
            Phone = phone.Text,
            Email = email.Text,
            Image = imageUrl.Text
        };

        db.Insert(friend);
        LoadContacts();

        // Очистка полей
        name.Text = string.Empty;
        phone.Text = string.Empty;
        email.Text = string.Empty;
        imageUrl.Text = string.Empty;
        phoneEntry.Text = string.Empty;
        email_phone.Text = string.Empty;
    }

    private async void Saada_sms_Clicked(object? sender, EventArgs e)
    {
        string phone = phoneEntry.Text;
        var message = "Tere tulemast! Saadan sõnumi";
        SmsMessage sms = new SmsMessage(message, phone);
        if (!string.IsNullOrWhiteSpace(phone) && Sms.Default.IsComposeSupported)
        {
            await Sms.Default.ComposeAsync(sms);
        }
    }

    private async void Saada_email_Clicked(object? sender, EventArgs e)
    {
        var message = "Tere tulemast! Saadan emaili";
        EmailMessage e_mail = new EmailMessage
        {
            Subject = email_phone.Text,
            Body = message,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string> { email_phone.Text }
        };
        if (Email.Default.IsComposeSupported)
        {
            await Email.Default.ComposeAsync(e_mail);
        }
    }
}