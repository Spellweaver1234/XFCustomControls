using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFCustomRenders.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TotalsGridControlNew : Grid
    {
        //public TotalsGridControlNew() это надо если делать дизайн вместе с XAML разметкой
        //{
        //    InitializeComponent();
        //}
        public static readonly BindableProperty TotalsProperty =
            BindableProperty.Create(nameof(Totals), typeof(List<TotalItem>), typeof(TotalsGridControlNew), null, BindingMode.OneWay, null, OnTotalsChanged);
        private static void OnTotalsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (TotalsGridControlNew)bindable;
            if (control == null) return;

            if (!(newvalue is List<TotalItem> totals)) return;

            var rowNumber = -1;
            double grandTotal = 0;

            foreach (var totalItem in totals)
            {
                grandTotal += totalItem.Value;

                var descLabel = new Label { Text = totalItem.Description };
                var valueLabel = new Label { Text = totalItem.Value.ToString("c") };

                rowNumber++;
                control.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                control.Children.Add(descLabel, 0, rowNumber);
                control.Children.Add(valueLabel, 1, rowNumber);
            }

            var grandTotalDescLabel = new Label { Text = "Total" };
            var grandTotalValueLabel = new Label { Text = grandTotal.ToString("c") };

            rowNumber++;
            control.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            control.Children.Add(grandTotalDescLabel, 0, rowNumber);
            control.Children.Add(grandTotalValueLabel, 1, rowNumber);
        }

        public List<TotalItem> Totals
        {
            get => (List<TotalItem>)GetValue(TotalsProperty);
            set => SetValue(TotalsProperty, value);
        }

        public static readonly BindableProperty DataProperty =
            BindableProperty.Create(nameof(Data), typeof(List<string>), typeof(TotalsGridControlNew), null, BindingMode.OneWay, null, OnDataChanged);
        private static void OnDataChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (TotalsGridControlNew)bindable;
            if (control == null) return;

            if (!(newvalue is List<string> list)) return;

            for (int i = 0; i < 3; i++)
            {
                control.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                for (int j = 0; j < 3; j++)
                {
                    control.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var label = new Label { Text = "123" };
                    control.Children.Add(label, i, j);
                }
            }
        }

        public List<string> Data
        {
            get => (List<string>)GetValue(TotalsProperty);
            set => SetValue(TotalsProperty, value);
        }
    }
}
