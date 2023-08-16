using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

using XFCustomRenders.Controls;

namespace XFCustomRenders.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        int countClicks = 0;

        private void ActionCommand(object obj)
        {
            countClicks++;
            Name = countClicks.ToString();
        }



        public List<string> TypeItems { get; set; } = typeItems.Select(x => x.Item1).ToList();
        private static List<(string, string)> typeItems = new List<(string, string)>
        {
            ("Вызов", "call"),
            ("Позвать", "visit"),
            ("Текст", "text"),
            ("Выход", "exit"),
        };

        private int selectedType;
        public int SelectedType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        public MainViewModel()
        {
            Name = countClicks.ToString();

            TotalsGridInitialize();
        }

        private void TotalsGridInitialize()
        {
            Totals = new List<TotalItem>
            {
                new TotalItem {Description = "SubTotal", Value = 99.91},
                new TotalItem {Description = "GST", Value = 5.0},
                new TotalItem {Description = "PST", Value = 4.9}
            };

            Data = new List<string> { "0.0", "0.1" };
        }
        public List<TotalItem> Totals { get; set; }
        public List<string> Data { get; set; }


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand CommandTap => new Command(ActionCommand);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}