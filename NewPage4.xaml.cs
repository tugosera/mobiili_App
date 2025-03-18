using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace mobiili_App;

public partial class NewPage4 : ContentPage
{
    private bool gameStarted = false;
    private bool isXTurn = true; // Ход крестика (true) или нолика (false)
    private bool isAgainstAI = false; // Игра против ИИ
    private string[,] board = new string[3, 3]; // Игровое поле

    public NewPage4()
    {
        InitializeComponent();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ""; // Очищаем поле
            }
        }
    }

    private async void FrameTapped(object sender, TappedEventArgs e)
    {
        if (!gameStarted)
        {
            await DisplayAlert("Viga", "Kõigepealt alusta mängu", "OK");
            return;
        }

        var frame = (Frame)sender;
        var row = Grid.GetRow(frame) / 2; // Получаем строку
        var col = Grid.GetColumn(frame);   // Получаем столбец

        if (board[row, col] != "") // Если клетка уже занята
        {
            await DisplayAlert("Viga", "Rakk on juba hõivatud", "OK");
            return;
        }

        // Ход игрока
        board[row, col] = isXTurn ? "X" : "O";
        frame.Content = new Image
        {
            Source = isXTurn ? "krokodiro.png" : "tralala.png", // Указываем путь к изображению
            Aspect = Aspect.AspectFill, // Растягиваем изображение с сохранением пропорций
            Margin = 0, // Убираем отступы
        };

        // Проверяем, есть ли победитель
        if (CheckWinner())
        {
            await DisplayAlert("Võit!", $"Mängija {(isXTurn ? "X" : "O")} võitis!", "OK");
            ResetGame();
            return;
        }

        // Проверяем на ничью
        if (IsBoardFull())
        {
            await DisplayAlert("Viik!", "Mäng on läbi", "OK");
            ResetGame();
            return;
        }

        // Меняем ход
        isXTurn = !isXTurn;

        // Если игра против ИИ и сейчас ход ИИ
        if (isAgainstAI && !isXTurn)
        {
            MakeAIMove();
        }
    }

    private void MakeAIMove()
    {
        // Находим все свободные клетки
        var emptyCells = new System.Collections.Generic.List<(int row, int col)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == "")
                {
                    emptyCells.Add((i, j));
                }
            }
        }

        // Если есть свободные клетки, выбираем случайную
        if (emptyCells.Any())
        {
            var random = new Random();
            var (row, col) = emptyCells[random.Next(emptyCells.Count)];

            // Ход ИИ
            board[row, col] = "O";
            var frame = GameGrid.Children
                .OfType<Frame>()
                .FirstOrDefault(f => Grid.GetRow(f) / 2 == row && Grid.GetColumn(f) == col);

            if (frame != null)
            {
                frame.Content = new Image
                {
                    Source = "tralala.png",
                    Aspect = Aspect.AspectFit
                };
            }

            if (CheckWinner())
            {
                DisplayAlert("Võit!", "robot võitis!", "OK");
                ResetGame();
                return;
            }

            if (IsBoardFull())
            {
                DisplayAlert("Viik!", "Mäng on läbi", "OK");
                ResetGame();
                return;
            }

            isXTurn = !isXTurn;
        }
    }

    private bool CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return true;
            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return true;
        }

        if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return true;
        if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return true;

        return false;
    }

    private bool IsBoardFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == "")
                    return false;
            }
        }
        return true;
    }

    private void ResetGame()
    {
        InitializeBoard();
        gameStarted = false;
        isXTurn = true;

        // Очищаем все клетки
        foreach (var child in GameGrid.Children)
        {
            if (child is Frame frame)
            {
                frame.Content = null;
            }
        }
    }

    private async void OnClickedBtnSõbraga(object sender, EventArgs e)
    {
        gameStarted = true;
        isAgainstAI = false; 
        await DisplayAlert("Mäng", "Mäng algas", "OK");
    }

    private async void OnClickedBtnAi(object sender, EventArgs e)
    {
        gameStarted = true;
        isAgainstAI = true;
        await DisplayAlert("Mäng", "Mäng lõpetas", "OK");

        if (!isXTurn)
        {
            MakeAIMove();
        }
    }

    private async void OnClickedBtnRules(object sender, EventArgs e)
    {
        await DisplayAlert("Reeglid",
            "1. Mängijad panevad kordamööda X või O vabadele puuridele.\n" +
            "2. Esimene, kes rivistab oma kolm sümbolit ritta (horisondi, vertikaali või diagonaali järgi), võidab.\n" +
            "3. Kui kõik puurid on täis, aga võitjat ei ole, lõpeb mäng viigiga.", "OK");
    }
}