using System;

using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    public class CustomHeaderView : View
    {
        public event EventHandler TappedOrClickEvent;
        public void SingleClick(EventArgs e)
        {
            TappedOrClickEvent?.Invoke(this, e);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CustomHeaderView), string.Empty);
        public string Text
        {
            set => SetValue(TextProperty, value);
            get => (string)GetValue(TextProperty);
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(CustomHeaderView), Color.Default);
        public Color TextColor
        {
            set => SetValue(TextColorProperty, value);
            get => (Color)GetValue(TextColorProperty);
        }
    }
}