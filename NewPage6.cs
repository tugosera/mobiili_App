using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace mobiili_App;

public class Telefon
{
    public string Nimetus { get; set; }
    public string Tootja { get; set; }
    public int Hind { get; set; }
    public string Pilt { get; set; }
}


public class NewPage6 : ContentPage
{
    public ObservableCollection<Telefon> telefons { get; set; }

	VerticalStackLayout vsl = new VerticalStackLayout();

	public NewPage6()
	{
        telefons = new ObservableCollection<Telefon>
        {
            new Telefon {Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="GalaxyS22.png"},
            new Telefon {Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399 , Pilt="XiaomiMi11Lite.png"},
            new Telefon {Nimetus="Xiaomi Mi 11 Lite 5G", Tootja="Xiaomi", Hind=339 , Pilt="Xiaomi5G.png"},
            new Telefon {Nimetus="iPhone 13 mini", Tootja="Apple", Hind=1179 , Pilt="iPhone13.png" }
        };

        ListView ListView = new ListView() { ItemsSource = telefons };

        vsl.Children.Add(ListView);

		Content = vsl;

	}
}