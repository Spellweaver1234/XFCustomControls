using System.ComponentModel;

using Android.Content;
using Android.Util;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using XFCustomRenders.Controls;
using XFCustomRenders.Droid.Renders;

[assembly: ExportRenderer(typeof(CustomHeaderView), typeof(HeaderViewRenderer))]
namespace XFCustomRenders.Droid.Renders
{
    public class HeaderViewRenderer : ViewRenderer<CustomHeaderView, TextView>
    {
        public HeaderViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomHeaderView> args)
        {
            base.OnElementChanged(args);

            if (Control != null) return;

            TextView textView = new TextView(Context);
            textView.SetTextSize(ComplexUnitType.Dip, 28);
            textView.Click += OnHeaderViewTapped; // одиночный клик (Touch - двойной клик)
            SetNativeControl(textView);

            if (args.NewElement == null) return;

            SetText();
            SetTextColor();
        }

        private void OnHeaderViewTapped(object sender, System.EventArgs e)
        {
            Element?.SingleClick(System.EventArgs.Empty);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomHeaderView.TextColorProperty.PropertyName)
            {
                SetTextColor();
            }
            else if (e.PropertyName == CustomHeaderView.TextProperty.PropertyName)
            {
                SetText();
            }
        }

        private void SetText() => Control.Text = Element.Text;

        private void SetTextColor()
        {
            Android.Graphics.Color andrColor = Android.Graphics.Color.Gray;

            if (Element.TextColor != Color.Default)
            {
                Color color = Element.TextColor;
                andrColor = Android.Graphics.Color.Argb(
                    (byte)(color.A * 255),
                    (byte)(color.R * 255),
                    (byte)(color.G * 255),
                    (byte)(color.B * 255));
            }
            Control.SetTextColor(andrColor);
        }
    }
}