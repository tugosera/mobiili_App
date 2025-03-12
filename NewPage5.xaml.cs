namespace mobiili_App;

public partial class NewPage5 : ContentPage
{
	public NewPage5()
	{
		InitializeComponent();
	}

	private async void Saada_sms_Clicked(object? sender, EventArgs e)
	{
		string phone = email_phone.Text;
		var message = "Tere tulemast! Saadan sõnumi";
		SmsMessage sms = new SmsMessage(message, phone);
		if (phone != null && Sms.Default.IsComposeSupported)
		{
			await Sms.Default.ComposeAsync(sms);
		}
	}
    private async void Saada_email_Clicked(object? sender, EventArgs e)
    {

    }

}