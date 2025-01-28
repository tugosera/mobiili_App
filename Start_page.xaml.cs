
namespace mobiili_App;

public partial class Start_page : ContentPage
{
	public List <ContentPage> lehed= new List <ContentPage> () { new Text_page(), new Figure_page()};
	public List<string> Tekstid=new List<string> {"Tee lahti TekstiPage", "Tee lahti FigurePage"};
	ScrollView sv;
	VerticalStackLayout vsl;

	public Start_page()
	{
		Title = "Avaleht";
		vsl = new VerticalStackLayout { BackgroundColor=Color.FromRgb(76, 0, 153) };
		for (int i = 0; i < Tekstid.Count; i++)
		{
			Button nupp = new Button
			{
				Text = Tekstid[i],
				BackgroundColor = Color.FromRgb(51, 153, 255),
				TextColor = Color.FromRgb(0, 0, 0),
				BorderWidth = 10,
				ZIndex = i,
				FontFamily = "LowRider BB Italic 400"

			};
			vsl.Add(nupp);
			nupp.Clicked += Lehte_avamine;
		}
		sv=new ScrollView { Content=vsl};
		Content = sv;
	}

    private async void Lehte_avamine(object? sender, EventArgs e)
    {
        Button btn = (Button)sender;
		await Navigation.PushAsync(lehed[btn.ZIndex]);
    }
}