using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFCustomRenders.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CustomGrid : Grid
    {
        public static readonly BindableProperty DataProperty =
            BindableProperty.Create(nameof(Data), typeof(List<string>), typeof(CustomGrid), null, BindingMode.OneWay, null, OnDataChanged);
        private static void OnDataChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (CustomGrid)bindable;
            if (control == null) return;

            if (!(newvalue is List<string> list)) return;

            for (int top = 0; top < 3; top++)
            {
                var rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                control.RowDefinitions.Add(rowDefinition);

                for (int left = 0; left < 3; left++)
                {
                    var columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = GridLength.Auto;
                    control.ColumnDefinitions.Add(columnDefinition);

                    var label = new Label { Text = $"x={left}; y={top}" };
                    control.Children.Add(label, left, top);
                }
            }
        }

        public List<string> Data
        {
            get => (List<string>)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}




