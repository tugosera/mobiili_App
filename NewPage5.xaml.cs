namespace mobiili_App;

public partial class NewPage5 : ContentPage
{
	public NewPage5()
	{
		InitializeComponent();
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