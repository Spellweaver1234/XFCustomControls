using System.Windows.Input;

using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    public class ButtonExt : Frame
    {
        private readonly ContentView root;
        private readonly Label label;

        public ButtonExt()
        {
            label = new Label
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            root = new ContentView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                BackgroundColor = Color.Transparent,
                Content = label,
                Margin = 0,
                Padding = 0,
            };

            Content = root;
            Padding = 0;
            HasShadow = false;
            IsClippedToBounds = true;
        }

        #region Props
        // ColorTap
        public static BindableProperty TapColorProperty =
            BindableProperty.Create(
                nameof(ColorTap),
                typeof(Color),
                typeof(ButtonExt),
                Color.Default,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    TouchSam.Touch.SetColor(self.root, (Color)n);
                });
        public Color ColorTap
        {
            get => (Color)GetValue(TapColorProperty);
            set => SetValue(TapColorProperty, value);
        }

        // CommandTap
        public static BindableProperty CommandTapProperty =
            BindableProperty.Create(
                nameof(CommandTap),
                typeof(ICommand),
                typeof(ButtonExt),
                null,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    TouchSam.Touch.SetTap(self.root, n as ICommand);
                });
        public ICommand CommandTap
        {
            get => GetValue(CommandTapProperty) as ICommand;
            set => SetValue(CommandTapProperty, value);
        }

        // CommandTapParametr
        public static BindableProperty CommandTapParametrProperty =
            BindableProperty.Create(
                nameof(CommandTapParametr),
                typeof(object),
                typeof(ButtonExt),
                null,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    TouchSam.Touch.SetTapParameter(self.root, n);
                });
        public object CommandTapParametr
        {
            get => GetValue(CommandTapParametrProperty);
            set => SetValue(CommandTapParametrProperty, value);
        }

        // InnerPadding
        public static BindableProperty InnerPaddingProperty =
            BindableProperty.Create(
                nameof(InnerPadding),
                typeof(Thickness),
                typeof(ButtonExt),
                new Thickness(),
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    self.root.Padding = (Thickness)n;
                });
        public Thickness InnerPadding
        {
            get => (Thickness)GetValue(InnerPaddingProperty);
            set => SetValue(InnerPaddingProperty, value);
        }

        // Text 
        public static BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ButtonExt),
                null,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    self.label.Text = n as string;
                });
        public string Text
        {
            get => GetValue(TextProperty) as string;
            set => SetValue(TextProperty, value);
        }

        // TextColor
        public static BindableProperty TextColorProperty =
            BindableProperty.Create(
                nameof(TextColor),
                typeof(Color),
                typeof(ButtonExt),
                Color.Default,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    self.label.TextColor = (Color)n;
                });
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        // TextSize
        public static BindableProperty TextSizeProperty =
            BindableProperty.Create(
                nameof(TextSize),
                typeof(double),
                typeof(ButtonExt),
                14.0,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    self.label.FontSize = (double)n;
                });
        public double TextSize
        {
            get => (double)GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }

        // VerticalTextAlignment
        public static BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(
                nameof(VerticalTextAlignment),
                typeof(TextAlignment),
                typeof(ButtonExt),
                TextAlignment.Start,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    self.label.VerticalTextAlignment = (TextAlignment)n;
                });
        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        // HorizontalTextAlignment
        public static BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(
                nameof(HorizontalTextAlignment),
                typeof(TextAlignment),
                typeof(ButtonExt),
                TextAlignment.Start,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ButtonExt;
                    self.label.HorizontalTextAlignment = (TextAlignment)n;
                });
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }
        #endregion Props
    }
}
