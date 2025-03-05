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
            await DisplayAlert("Viga", "enne alusta mängi", "ok");
        }
    }

    private async void OnClickedBtnSõbraga(object sender, EventArgs e)
    {
        gameStarted = true;
        await DisplayAlert("Mäng", "Mäng algas", "ok");
    }
    private void OnClickedBtnAi(object sender, EventArgs e)
    {
        
    }
    private async void OnClickedBtnRules(object sender, EventArgs e)
    {
        await DisplayAlert("reeglid", "Mängijad: 2 mängijat – üks mängib ristidega (X) ja teine ringidega (O).\r\n Mängulaud: 3×3 ruudustik.\r\n Eesmärk: Esimene mängija, kes saab kolm oma märki järjestikku – horisontaalselt, vertikaalselt või diagonaalselt – võidab mängu.\r\n Mängu käik:\r\n\r\n    Mängijad teevad käike kordamööda.\r\nEsimene mängija asetab oma märgi (X või O) vabale ruudule.\r\n    Mäng jätkub, kuni üks mängija võidab või laud saab täis.\r\n    Viik: Kui kõik ruudud on täidetud ja võitjat pole, siis mäng lõpeb viigiga.", "OK");
    }
}