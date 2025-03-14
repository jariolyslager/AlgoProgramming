using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AlgorithmProgramming.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

namespace AlgorithmProgramming.ViewModels
{
    public partial class MainPageViewModel
    {
        public Datastructures.ArrayList Stocks { get; set; } = new Datastructures.ArrayList();

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
                        }
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
    }
}
