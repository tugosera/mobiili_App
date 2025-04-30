using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

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
    public List<Telefon> Telefonid { get; set; }
    Label lbl_list;
    ListView list;
    public NewPage6()
    {
        Telefonid = new List<Telefon>
        {
        new Telefon {Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349 },
        new Telefon {Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399 }, 
        new Telefon {Nimetus="Xiaomi Mi 11 Lite 5G", Tootja="Xiaomi", Hind=339 },
        new Telefon {Nimetus="iPhone 13", Tootja="Apple", Hind=1179 }
        };
        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = Telefonid,
            ItemTemplate = new DataTemplate(() =>
            {
                Label nimetus = new Label { FontSize = 20 };
                nimetus.SetBinding(Label.TextProperty, "Nimetus");

                Label tootja = new Label();
                tootja.SetBinding(Label.TextProperty, "Tootja");

                Label hind = new Label();
                hind.SetBinding(Label.TextProperty, "Hind");

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(0, 5),
                        Orientation = StackOrientation.Vertical,
                        Children = { nimetus, tootja, hind }
                    }
                };
            })
        };

}
}
