using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    public class CustomControl : ContentView
    {
        public CustomControl()
        {
            var label = new Label
            {
                Text = "Привет, это кастомный контрол!",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Content = new StackLayout
            {
                Children = { label }
            };
        }
    }
}
