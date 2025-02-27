using Android.Content.Res;
using Kotlin.Jvm.Internal;
using Microsoft.Maui.Controls;

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

    void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        meme.Opacity = 1 - e.NewValue;
        Snowman.Opacity = e.NewValue;
    }

    private async void OnClickedBtnSalto(object sender, EventArgs e)
    {
        uint duration = 1000; 

        await Task.WhenAll(
            Snowman.RotateTo(90, duration / 4)
        );
        await Task.WhenAll(
            Snowman.RotateTo(180, duration / 4)
        );
        await Task.WhenAll(
            Snowman.RotateTo(270, duration / 4)
        );
        await Task.WhenAll(
            Snowman.RotateTo(360, duration / 4)
        );

        Snowman.Rotation = 0;
        Snowman.TranslationX = 0;
        Snowman.TranslationY = 0;
    }
}