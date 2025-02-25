using Android.Content.Res;
using Kotlin.Jvm.Internal;

namespace mobiili_App;

public partial class NewPage3 : ContentPage
{
    Random random = new Random();
    bool i = false;
	public NewPage3()
	{
		InitializeComponent();
	}

    private async void OnClickedBtnHide(object sender, EventArgs e)
    {
        if (i == false)
        {
            i = true;
            Snowman.IsVisible = false;
        }
        else
        {
            i = false;
            Snowman.IsVisible = true;
        }
    }

    private async void OnClickedBtnColor(object sender, EventArgs e)
    {

        head.BackgroundColor = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        body.BackgroundColor = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        legs.BackgroundColor = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}