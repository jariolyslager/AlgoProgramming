using Microsoft.UI.Xaml.Controls;

namespace AlgorithmProgramming.Models
{
    public class Stock : IComparable<Stock>
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public Stock(string ticker, string name, DateTime date, double price)
        {
            Ticker = ticker;
            Name = name;
            Date = date;
            Price = price;
        }

        public int CompareTo(Stock? obj)
        {
            return Price.CompareTo(obj.Price);
        }

        public override string ToString()
        {
            return $"{Ticker} - {Name} - {Date} - {Price}";
        }
    }

    public class StockList
    {
        public List<Stock> Stocks { get; set; }
    }
}
