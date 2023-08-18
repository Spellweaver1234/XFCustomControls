using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    public class ImageButton : ContentView
    {
        public ImageButton()
        {
            var layout = new StackLayout();
            layout.VerticalOptions = LayoutOptions.Center;
            layout.HorizontalOptions = LayoutOptions.Center;
            layout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Command?.Execute(null))
            });

            var label = new Label();
            label.HorizontalOptions = LayoutOptions.Center;
            label.FontSize = 18;
            label.TextColor = Color.Black;
            label.FontAttributes = FontAttributes.Bold;
            label.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));

            var image = new Image();
            //image.Aspect = Aspect.AspectFit;
            image.SetBinding(Image.SourceProperty, new Binding(nameof(ImageSource), source: this));

            layout.Children.Add(image);
            layout.Children.Add(label);

            Content = layout;
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(
                nameof(ImageSource),
                typeof(string),
                typeof(ImageButton));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ImageButton),
                default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
                BindableProperty.Create(
                    nameof(Command),
                    typeof(Command),
                    typeof(ImageButton),
                    null);

        public Command Command
        {
            get { return (Command)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}