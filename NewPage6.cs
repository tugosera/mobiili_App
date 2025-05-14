using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using SQLite;
using System.IO;
using System.Threading.Tasks;

namespace mobiili_App
{
    public class EuroopaRiik
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Elanikud { get; set; }
        public string LippUrl { get; set; }
    }

    public class RiigidDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public RiigidDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "riigid.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<EuroopaRiik>().Wait();
        }

        public Task<List<EuroopaRiik>> GetRiigidAsync()
        {
            return _database.Table<EuroopaRiik>().ToListAsync();
        }

        public Task<int> AddRiikAsync(EuroopaRiik riik)
        {
            return _database.InsertAsync(riik);
        }

        public Task<int> DeleteRiikAsync(EuroopaRiik riik)
        {
            return _database.DeleteAsync(riik);
        }
    }

    public class NewPage6 : ContentPage
    {
        private ObservableCollection<EuroopaRiik> riigid;
        private ListView listView;
        private RiigidDatabase db = new RiigidDatabase();

        public NewPage6()
        {
            Title = "Euroopa Riigid";

            riigid = new ObservableCollection<EuroopaRiik>();

            listView = new ListView
            {
                ItemsSource = riigid,
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(() =>
                {
                    var nimi = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
                    nimi.SetBinding(Label.TextProperty, "Nimi");

                    var lipp = new Image { HeightRequest = 40, WidthRequest = 60 };
                    lipp.SetBinding(Image.SourceProperty, "LippUrl");

                    var deleteBtn = new Button { Text = "Kustuta", BackgroundColor = Colors.Red, TextColor = Colors.White, WidthRequest = 80 };
                    deleteBtn.SetBinding(Button.CommandParameterProperty, new Binding("."));
                    deleteBtn.Clicked += async (s, e) =>
                    {
                        var btn = s as Button;
                        var riik = btn?.CommandParameter as EuroopaRiik;
                        if (riik != null)
                        {
                            riigid.Remove(riik);
                            await db.DeleteRiikAsync(riik);
                        }
                    };

                    var layout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = { lipp, nimi, deleteBtn },
                        Padding = 5,
                        Spacing = 10
                    };

                    var viewCell = new ViewCell { View = layout };

                    layout.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(async () =>
                        {
                            var selected = viewCell.BindingContext as EuroopaRiik;
                            if (selected != null)
                                await Navigation.PushAsync(new RiigiInfoPage(selected));
                        })
                    });

                    return viewCell;
                })
            };

            var nimiEntry = new Entry { Placeholder = "Riigi nimi" };
            var pealinnEntry = new Entry { Placeholder = "Pealinn" };
            var elanikudEntry = new Entry { Placeholder = "Elanikke", Keyboard = Keyboard.Numeric };
            var lippEntry = new Entry { Placeholder = "Lipu URL" };

            var lisaBtn = new Button { Text = "Lisa riik", BackgroundColor = Colors.Green, TextColor = Colors.White };
            lisaBtn.Clicked += async (s, e) =>
            {
                if (!int.TryParse(elanikudEntry.Text, out int elanikud)) return;
                if (string.IsNullOrWhiteSpace(nimiEntry.Text)) return;

                string nimi = nimiEntry.Text.Trim();

                if (!riigid.Any(r => r.Nimi.Equals(nimi, System.StringComparison.OrdinalIgnoreCase)))
                {
                    var uusRiik = new EuroopaRiik
                    {
                        Nimi = nimi,
                        Pealinn = pealinnEntry.Text?.Trim(),
                        Elanikud = elanikud,
                        LippUrl = lippEntry.Text?.Trim()
                    };

                    await db.AddRiikAsync(uusRiik);
                    riigid.Add(uusRiik);

                    nimiEntry.Text = pealinnEntry.Text = elanikudEntry.Text = lippEntry.Text = "";
                }
            };

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = 15,
                    Children =
                    {
                        new Label { Text = "Euroopa riikide nimekiri", FontSize = 24, HorizontalOptions = LayoutOptions.Center },
                        nimiEntry,
                        pealinnEntry,
                        elanikudEntry,
                        lippEntry,
                        lisaBtn,
                        listView
                    }
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = await db.GetRiigidAsync();
            riigid.Clear();
            foreach (var r in list)
                riigid.Add(r);
        }
    }

    public class RiigiInfoPage : ContentPage
    {
        public RiigiInfoPage(EuroopaRiik riik)
        {
            Title = riik.Nimi;

            Content = new StackLayout
            {
                Padding = 20,
                Children =
                {
                    new Label { Text = $"Riik: {riik.Nimi}", FontSize = 22 },
                    new Label { Text = $"Pealinn: {riik.Pealinn}", FontSize = 18 },
                    new Label { Text = $"Elanikke: {riik.Elanikud}", FontSize = 18 },
                    new Image { Source = riik.LippUrl, HeightRequest = 150, HorizontalOptions = LayoutOptions.Center }
                }
            };
        }
    }
}
