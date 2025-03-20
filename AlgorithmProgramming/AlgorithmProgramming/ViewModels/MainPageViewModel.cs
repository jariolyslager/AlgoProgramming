﻿using System.Collections.ObjectModel;
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
        public ObservableCollection<Stock> StockCollection { get; set; } = new ObservableCollection<Stock>();
        public string LastAction { get; set; } = "Geen";
        public string LastActionString => LastAction + " " + stopwatch.ElapsedTicks + " ticks";
        public Stopwatch stopwatch = new Stopwatch();
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
                        StocksLinkedList.Clear();
                        StockCollection.Clear();
                        foreach (var stock in stockList.Stocks)
                        {
                            Stocks.Add(stock);
                            StocksLinkedList.Add(stock);
                        }                    
                    }
                    OnPropertyChanged(nameof(Stocks));
                    OnPropertyChanged(nameof(StocksLinkedList));
                    SetTableToArrayList();
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

        [RelayCommand]
        public void QuickSort()
        {
            LastAction = "ArrayList quicksort";
            stopwatch.Start();
            Quicksort.Sort(Stocks);
            stopwatch.Stop();
            SetTableToArrayList();
            OnPropertyChanged(nameof(LastActionString));
        }

        [RelayCommand]
        public void SetTableToArrayList()
        {
            StockCollection.Clear();
            foreach(Stock stock in Stocks)
            {
                StockCollection.Add(stock);
            }
        }

        [RelayCommand]
        public void SetTableToDoublyLinkedList()
        {
            StockCollection.Clear();
            foreach (Stock stock in StocksLinkedList)
            {
                StockCollection.Add(stock);
            }
        }
    }
}
