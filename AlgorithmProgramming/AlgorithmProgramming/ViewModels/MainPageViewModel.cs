using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using AlgorithmProgramming.Models;
using AlgorithmProgramming.Sorting;
using CommunityToolkit.Mvvm.Input;

namespace AlgorithmProgramming.ViewModels
{
    public partial class MainPageViewModel : INotifyPropertyChanged
    {
        public Datastructures.ArrayList<Stock> Stocks { get; set; } = new Datastructures.ArrayList<Stock>();
        public Datastructures.DoublyLinkedList<Stock> StocksLinkedList { get; set; } = new Datastructures.DoublyLinkedList<Stock>();
        public Datastructures.HashMap<string, Stock> StocksHashMap { get; set; } = new Datastructures.HashMap<string, Stock>();
        public ObservableCollection<Stock> StockCollection { get; set; } = new ObservableCollection<Stock>();
        public string LastAction { get; set; } = "Geen";
        public string LastActionString => LastAction + " " + stopwatch.ElapsedMilliseconds + " ms";
        public Stopwatch stopwatch = new Stopwatch();
        public string SearchText { get; set; } = "";

        private DateTime searchDate = DateTime.Today;
        public DateTime SearchDate
        {
            get => searchDate;
            set
            {
                if (searchDate != value)
                {
                    searchDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Picks a file using the FilePicker
        /// </summary>
        /// <param name="options">Options for customizing the behaviour of FilePicker</param>
        /// <returns></returns>
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
                        StocksLinkedList.Clear();
                        StockCollection.Clear();
                        StocksHashMap.Clear();
                        foreach (var stock in stockList.Stocks)
                        {
                            Stocks.Add(stock);
                            StocksLinkedList.Add(stock);
                            if (StocksHashMap.TryGetValue(stock.Ticker, out var existingStock))
                            {
                                if (stock.Date.CompareTo(existingStock.Date) > 0)
                                {
                                    StocksHashMap.Remove(stock.Ticker);
                                    StocksHashMap.Add(stock.Ticker, stock);
                                }
                            }
                            else
                            {
                                StocksHashMap.Add(stock.Ticker, stock);
                            }
                        }
                    }
                    OnPropertyChanged(nameof(Stocks));
                    OnPropertyChanged(nameof(StocksLinkedList));
                    OnPropertyChanged(nameof(StocksHashMap));
                    SetTableToArrayList();
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
                Console.WriteLine($"Error picking file: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method for performing the QuickSort
        /// </summary>
        [RelayCommand]
        public void QuickSort()
        {
            LastAction = "ArrayList quicksort";
            stopwatch.Reset();
            stopwatch.Start();
            Quicksort.Sort(Stocks);
            stopwatch.Stop();
            SetTableToArrayList();
            OnPropertyChanged(nameof(LastActionString));
        }

        /// <summary>
        /// Method for performing the BubbleSort
        /// </summary>
        [RelayCommand]
        public void BubbleSort()
        {
            LastAction = "BubbleSort HashMap";
            stopwatch.Reset();
            stopwatch.Start();

            var sortedHashMap = Sorting.BubbleSort.Sort(StocksHashMap.Values);

            stopwatch.Stop();

            StockCollection = new ObservableCollection<Stock>(sortedHashMap);
            OnPropertyChanged(nameof(StockCollection));

            OnPropertyChanged(nameof(LastActionString));
        }

        /// <summary>
        /// Method to convert the datatable to an ArrayList
        /// </summary>
        [RelayCommand]
        public void SetTableToArrayList()
        {
            StockCollection = new ObservableCollection<Stock>(Stocks);
            OnPropertyChanged(nameof(StockCollection));
        }

        /// <summary>
        /// Method to convert the datatable to a DoublyLinkedList
        /// </summary>
        [RelayCommand]
        public void SetTableToDoublyLinkedList()
        {
            StockCollection = new ObservableCollection<Stock>(StocksLinkedList);
            OnPropertyChanged(nameof(StockCollection));
        }

        /// <summary>
        /// Method to convert the datatable to a HashMap
        /// </summary>
        [RelayCommand]
        public void SetTableToHashMap()
        {
            StockCollection.Clear();

            foreach (var stock in StocksHashMap)
            {
                StockCollection.Add(stock.Value);
            }
        }

        /// <summary>
        /// Method for performing the JumpSearch
        /// </summary>
        [RelayCommand]
        public void JumpSearch()
        {
            var comparer = new StockDateComparer();

            stopwatch.Reset();
            stopwatch.Start();
            Quicksort.Sort(Stocks, comparer);
            stopwatch.Stop();
            var quickSortTime = stopwatch.ElapsedMilliseconds;

            LastAction = "ArrayList QuickSort " + quickSortTime + " ms, ArrayList JumpSearch";

            stopwatch.Reset();
            stopwatch.Start();

            Stock searchStock = new Stock("", "", SearchDate, 0);

            var results = Search.JumpSearch.Search(Stocks, comparer, searchStock);

            stopwatch.Stop();

            StockCollection = new ObservableCollection<Stock>(results);
            OnPropertyChanged(nameof(StockCollection));

            OnPropertyChanged(nameof(LastActionString));
        }

        /// <summary>
        /// Method for performing the LinearSearch
        /// </summary>
        [RelayCommand]
        public void LinearSearch()
        {
            LastAction = "Doubly Linked List LinearSearch";
            string ticker = SearchText;
            stopwatch.Reset();
            stopwatch.Start();
            Stock stockToFind = new Stock(ticker, "", DateTime.Today, 0);
            StockTickerComparer stockToFindComparer = new StockTickerComparer();
            var results = Search.LinearSearch.SearchDoublyLinkedList(StocksLinkedList, stockToFindComparer, stockToFind);

            stopwatch.Stop();
            StockCollection = new ObservableCollection<Stock>(results);
            OnPropertyChanged(nameof(StockCollection));

            OnPropertyChanged(nameof(LastActionString));
        }
    }
}
