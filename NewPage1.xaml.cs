namespace mobiili_App;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}
    int g = 0;
    bool i = false;
    private async void OnClickedBtn3(object sender, EventArgs e)
    {
        i = true;
        while (i == true)
        {
            green.BackgroundColor = Color.FromRgb(0, 255, 0);
            red.BackgroundColor = Color.FromRgb(128, 128, 128);
            await Task.Delay(1500);
            yellow.BackgroundColor = Color.FromRgb(255, 255, 0);
            green.BackgroundColor = Color.FromRgb(128, 128, 128);
            await Task.Delay(1500);
            red.BackgroundColor = Color.FromRgb(255, 0, 0);
            yellow.BackgroundColor = Color.FromRgb(128, 128, 128);
            await Task.Delay(1500);
        }
    }

    private async void OnClickedBtn4(object sender, EventArgs e)
    {
        i = false;

        red.BackgroundColor = Color.FromRgb(128, 128, 128);
        green.BackgroundColor = Color.FromRgb(128, 128, 128);
        yellow.BackgroundColor = Color.FromRgb(128, 128, 128);
    }

    private void redTapped(object sender, TappedEventArgs e)
    {
        if (i == false)
        {
            lblR.Text = "lülita valgusfoor";
        }
        else
        {
            lblR.Text = "stop";
        }

    }

    private void greenTapped(object sender, TappedEventArgs e)
    {
        if (i == false)
        {
            lblG.Text = "lülita valgusfoor";
        }
        else
        {
            lblG.Text = "mine";
        }

    }

    private void yellowTapped(object sender, TappedEventArgs e)
    {
        if (i == false)
        {
            lblY.Text = "lülita valgusfoor";
        }
        else
        {
            lblY.Text = "oota";
        }

    }

    private async void OnClickedBtn6(object sender, EventArgs e)
    {
        if (g == 0)
        {
            g++;
            green.BackgroundColor = Color.FromRgb(0, 255, 0);
            red.BackgroundColor = Color.FromRgb(128, 128, 128);
            yellow.BackgroundColor = Color.FromRgb(128, 128, 128);
        }
        else if (g == 1)
        {
            g++;
            yellow.BackgroundColor = Color.FromRgb(255, 255, 0);
            green.BackgroundColor = Color.FromRgb(128, 128, 128);
            red.BackgroundColor = Color.FromRgb(128, 128, 128);
        }
        else {
            g = 0;
            red.BackgroundColor = Color.FromRgb(255, 0, 0);
            yellow.BackgroundColor = Color.FromRgb(128, 128, 128);
            green.BackgroundColor = Color.FromRgb(128, 128, 128);
        }
    }
}  