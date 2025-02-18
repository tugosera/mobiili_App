namespace mobiili_App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count != 10)
            {
                CounterBtn.Text = $"Clicked {count} time";
            }
            else if (count == 10)
            {
                Lbl.Text = "NEGRO NEGRO NEGRO";
                CounterBtn.Text = $"NEGRO NEGRO NEGRO";
                Img.Source = "floyd.png";
            }
            else
            {
                CounterBtn.Text = $"Clicked {count} times";
            }

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnClickedBtn2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage1());
        }
        private async void OnClickedBtn5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage2());
        }
    }

}
