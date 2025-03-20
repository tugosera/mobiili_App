using Microsoft.Maui.Media;
using System;
using System.Data.SqlClient;
using Microsoft.Maui.Controls;
namespace mobiili_App;

public partial class NewPage5 : ContentPage
{
    private string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = app; Integrated Security = True;";
    public NewPage5()
	{
		InitializeComponent();
	}

    private void AddContact_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(phone.Text ))
        {
            DisplayAlert("Error", "Please enter a phone number", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(imageUrl.Text))
        {
            DisplayAlert("Error", "Please enter a image:(", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(name.Text))
        {
            DisplayAlert("Error", "Please enter a name", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(email.Text))
        {
            DisplayAlert("Error", "Please enter a email", "OK");
            return;
        }

        var newContact = new Frame
        {
            Padding = 10,
            Margin = 10,
            CornerRadius = 10,
            BackgroundColor = Colors.LightGray,
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Children =
                    {
                        new Image { Source = new UriImageSource { Uri = new Uri(imageUrl.Text) }, WidthRequest = 40, HeightRequest = 40 },
                        new StackLayout
                        {
                            Children =
                            {
                                new Label { Text = name.Text },
                                new Label { Text = email.Text, FontAttributes = FontAttributes.Bold },
                                new Label { Text = phone.Text }
                            }
                        }
                    }
            }
        };

        ContactsList.Children.Add(newContact);
        phoneEntry.Text = string.Empty;
        email_phone.Text = string.Empty;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO friend (name, phone, email, image) VALUES (@name, @phone, @email, @image)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@phone", phone.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@image", imageUrl.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private async void Saada_sms_Clicked(object? sender, EventArgs e)
	{
		string phone = phoneEntry.Text;
		var message = "Tere tulemast! Saadan sõnumi";
		SmsMessage sms = new SmsMessage(message, phone);
		if (phone != null && Sms.Default.IsComposeSupported)
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
			To = new List<string>(new[] { email_phone.Text })
		};
		if (Email.Default.IsComposeSupported)
		{
			await Email.Default.ComposeAsync(e_mail);
		}
    }

}