namespace mobiili_App;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

    bool i = false;
    private void OnClickedBtn3(object sender, EventArgs e)
    {
        i = true;
        green.BackgroundColor = Color.FromRgb(0, 255, 0);
        red.BackgroundColor = Color.FromRgb(128, 128, 128);
        Thread.Sleep(2000);
        yellow.BackgroundColor = Color.FromRgb(255, 255, 0);
        green.BackgroundColor = Color.FromRgb(128, 128, 128);
        Thread.Sleep(2000);
        red.BackgroundColor = Color.FromRgb(255, 0, 0);
        yellow.BackgroundColor = Color.FromRgb(128, 128, 128);
        Thread.Sleep(2000);

    }
}  