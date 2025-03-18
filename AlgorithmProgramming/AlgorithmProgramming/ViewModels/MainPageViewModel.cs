using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using AlgorithmProgramming.Models;
using CommunityToolkit.Mvvm.Input;

namespace AlgorithmProgramming.ViewModels
{
    public partial class MainPageViewModel : INotifyPropertyChanged
    {
        public Datastructures.ArrayList Stocks { get; set; } = new Datastructures.ArrayList();
        public Datastructures.DoublyLinkedList<Stock> StocksLinkedList { get; set; } = new Datastructures.DoublyLinkedList<Stock>();

        public event PropertyChangedEventHandler? PropertyChanged;

        [RelayCommand]
        public async Task<FileResult> PickFile(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                    {
                        string jsonString = File.ReadAllText(result.FullPath);
                        Console.WriteLine(jsonString);
                        StockList stockList = JsonSerializer.Deserialize<StockList>(jsonString);
                        Stocks.Clear();
                        foreach (var stock in stockList.Stocks)
                        {
                            Stocks.Add(stock);
                            StocksLinkedList.Add(stock);
                        }
                        OnPropertyChanged(nameof(Stocks));
                        OnPropertyChanged(nameof(StocksLinkedList));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
