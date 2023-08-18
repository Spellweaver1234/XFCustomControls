using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    public class CustomFrame : Frame
    {
        public CustomFrame()
        {
            CornerRadius = 10;
            Padding = 0;
            BackgroundColor = Color.Yellow;  // out

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(new BoxView
            {
                WidthRequest = 18,
                HeightRequest = 18,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Color = Color.Red,      // out
                CornerRadius = 9
            }, 2, 0);

            grid.Children.Add(new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFit,
                Source = "order"        // out
            }, 1, 1);

            grid.Children.Add(new Label
            {
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,    // out
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Text = "1231231 asdasdaasdaada"        // out
            }, 1, 2);

            Content = grid;
        }
    }
}
