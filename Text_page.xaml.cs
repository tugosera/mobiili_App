
using mobiili_App;

public partial class TextPage : ContentPage
{
    Label lbl;
    Editor editor;
    HorizontalStackLayout hsl;
    List<string> buttons = new List<string> { "Tagasi", "Avaleht", "Edasi" };

    public TextPage(int k)
    {
        lbl = new Label
        {
            Text = "Pealkiri",
            TextColor = Color.FromRgb(100, 10, 10),
            FontFamily = "Socafe 400",
            FontAttributes = FontAttributes.Bold,
            TextDecorations = TextDecorations.Underline,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 28,
        };

        editor = new Editor
        {
            Placeholder = "Vihje:Sisesta siia tekst",
            PlaceholderColor = Color.FromRgb(250, 200, 100),
            TextColor = Color.FromRgb(200, 200, 100),
            BackgroundColor = Color.FromRgb(100, 50, 200),
            FontSize = 28,
            FontAttributes = FontAttributes.Italic,
        };

        editor.TextChanged += Teksti_sisestamine; // Lisame s�ndmuse TextChanged, mis k�ivitab funktsiooni Tekste_sisestamine

        hsl = new HorizontalStackLayout { };

        for (int i = 0; i < 3; i++)
        {
            Button b = new Button
            {
                Text = buttons[i],
                ZIndex = i,
                WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / 8.3,
            };
            hsl.Add(b);
            b.Clicked += Liikumine;
        }

        VerticalStackLayout vst = new VerticalStackLayout
        {
            Children = { lbl, editor, hsl },
            VerticalOptions = LayoutOptions.End
        };

        Content = vst;
    }

    private async void Liikumine(object? sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (btn.ZIndex == 0)
        {
            await Navigation.PushAsync(new TextPage(btn.ZIndex));
        }
        else if (btn.ZIndex == 1)
        {
            await Navigation.PushAsync(new Start_page());
        }
        else
        {
            await Navigation.PushAsync(new FigurePage(btn.ZIndex));
        }
    }

    private void Teksti_sisestamine(object? sender, TextChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}