using System.Windows.Input;

using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    public class MyCustomControl : ContentView
    {
        public MyCustomControl()
        {
            var layout = new StackLayout();

            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(nameof(MyProperty), source: this));

            var button = new Button();
            button.SetBinding(Button.CommandProperty, new Binding(nameof(MyCommand), source: this));

            layout.Children.Add(label);
            layout.Children.Add(button);

            Content = layout;
        }

        public static readonly BindableProperty MyPropertyProperty =
        BindableProperty.Create(nameof(MyProperty), typeof(string), typeof(MyCustomControl), default(string));

        public string MyProperty
        {
            get { return (string)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly BindableProperty MyCommandProperty =
                BindableProperty.Create(nameof(MyCommand), typeof(ICommand), typeof(MyCustomControl), null);

        public ICommand MyCommand
        {
            get { return (ICommand)GetValue(MyCommandProperty); }
            set { SetValue(MyCommandProperty, value); }
        }
    }
}
