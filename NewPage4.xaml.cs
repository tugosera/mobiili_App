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
            await DisplayAlert("Ошибка", "Сначала начните игру", "OK");
            return;
        }

        var frame = (Frame)sender;
        var row = Grid.GetRow(frame) / 2; // Получаем строку
        var col = Grid.GetColumn(frame);   // Получаем столбец

        if (board[row, col] != "") // Если клетка уже занята
        {
            await DisplayAlert("Ошибка", "Клетка уже занята", "OK");
            return;
        }

        // Ход игрока
        board[row, col] = isXTurn ? "X" : "O";
        frame.Content = new Image
        {
            Source = isXTurn ? "krokodiro.png" : "tralala.png", // Указываем путь к изображению
            Aspect = Aspect.AspectFill, // Растягиваем изображение с сохранением пропорций
            Margin = 0, // Убираем отступы
            HorizontalOptions = LayoutOptions.FillAndExpand, // Заполняем по горизонтали
            VerticalOptions = LayoutOptions.FillAndExpand // Заполняем по вертикали
        };

        // Проверяем, есть ли победитель
        if (CheckWinner())
        {
            await DisplayAlert("Победа!", $"Игрок {(isXTurn ? "X" : "O")} выиграл!", "OK");
            ResetGame();
            return;
        }

        // Проверяем на ничью
        if (IsBoardFull())
        {
            await DisplayAlert("Ничья!", "Игра окончена", "OK");
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
                    Source = "krokodiro.png", // Указываем путь к изображению для нолика
                    Aspect = Aspect.AspectFit
                };
            }

            // Проверяем, есть ли победитель
            if (CheckWinner())
            {
                DisplayAlert("Победа!", "ИИ выиграл!", "OK");
                ResetGame();
                return;
            }

            // Проверяем на ничью
            if (IsBoardFull())
            {
                DisplayAlert("Ничья!", "Игра окончена", "OK");
                ResetGame();
                return;
            }

            // Меняем ход
            isXTurn = !isXTurn;
        }
    }

    private bool CheckWinner()
    {
        // Проверка строк и столбцов
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return true;
            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return true;
        }

        // Проверка диагоналей
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
        isAgainstAI = false; // Игра против друга
        await DisplayAlert("Игра", "Игра началась", "OK");
    }

    private async void OnClickedBtnAi(object sender, EventArgs e)
    {
        gameStarted = true;
        isAgainstAI = true; // Игра против ИИ
        await DisplayAlert("Игра", "Игра началась", "OK");

        // Если ИИ ходит первым
        if (!isXTurn)
        {
            MakeAIMove();
        }
    }

    private async void OnClickedBtnRules(object sender, EventArgs e)
    {
        await DisplayAlert("Правила",
            "1. Игроки по очереди ставят X или O на свободные клетки.\n" +
            "2. Первый, кто выстроит три своих символа в ряд (по горизонтали, вертикали или диагонали), выигрывает.\n" +
            "3. Если все клетки заполнены, но победителя нет, игра заканчивается ничьей.", "OK");
    }
}