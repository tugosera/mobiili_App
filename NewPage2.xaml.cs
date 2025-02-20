namespace mobiili_App;

public partial class NewPage2 : ContentPage
{
    public NewPage2()
    {
        InitializeComponent();
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        box.BackgroundColor = Color.FromRgb((int)redSlide.Value, (int)greenSlide.Value, (int)blueSlide.Value);   
    }
}