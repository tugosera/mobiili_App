namespace mobiili_App;

public partial class NewPage4 : ContentPage
{
    bool gameStarted = false;
    bool krest = false;
	public NewPage4()
	{
		InitializeComponent();
	}

    private async void FrameTapped(object sender, TappedEventArgs e)
    {
        if (gameStarted == false)
        {
            await DisplayAlert("Viga", "enne alusta m�ngi", "ok");
        }
    }

    private async void OnClickedBtnS�braga(object sender, EventArgs e)
    {
        gameStarted = true;
        await DisplayAlert("M�ng", "M�ng algas", "ok");
    }
    private void OnClickedBtnAi(object sender, EventArgs e)
    {
        
    }
    private async void OnClickedBtnRules(object sender, EventArgs e)
    {
        await DisplayAlert("reeglid", "M�ngijad: 2 m�ngijat � �ks m�ngib ristidega (X) ja teine ringidega (O).\r\n M�ngulaud: 3�3 ruudustik.\r\n Eesm�rk: Esimene m�ngija, kes saab kolm oma m�rki j�rjestikku � horisontaalselt, vertikaalselt v�i diagonaalselt � v�idab m�ngu.\r\n M�ngu k�ik:\r\n\r\n    M�ngijad teevad k�ike kordam��da.\r\nEsimene m�ngija asetab oma m�rgi (X v�i O) vabale ruudule.\r\n    M�ng j�tkub, kuni �ks m�ngija v�idab v�i laud saab t�is.\r\n    Viik: Kui k�ik ruudud on t�idetud ja v�itjat pole, siis m�ng l�peb viigiga.", "OK");
    }
}