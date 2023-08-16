using System.ComponentModel;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using XFCustomRenders.Controls;
using XFCustomRenders.iOS.Renders;

[assembly: ExportRenderer(typeof(CustomHeaderView), typeof(HeaderViewRenderer))]
namespace XFCustomRenders.iOS.Renders
{
    public class HeaderViewRenderer : ViewRenderer<CustomHeaderView, UILabel>
    {

        UITapGestureRecognizer tapGestureRecognizer;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomHeaderView> args)
        {
            base.OnElementChanged(args);

            if (Control != null) return;

            UILabel uilabel = new UILabel();
            uilabel.Font = UIFont.SystemFontOfSize(25);

            tapGestureRecognizer = new UITapGestureRecognizer(() => { OnHeaderViewTapped(); });
            uilabel.AddGestureRecognizer(tapGestureRecognizer);
            uilabel.UserInteractionEnabled = true;
            SetNativeControl(uilabel);

            if (args.NewElement == null) return;

            SetText();
            SetTextColor();
        }

        private void OnHeaderViewTapped()
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
            UIColor iosColor = UIColor.Gray;

            if (Element.TextColor != Color.Default)
            {
                Color color = Element.TextColor;
                iosColor = UIColor.FromRGBA(
                    (byte)(color.R * 255),
                    (byte)(color.G * 255),
                    (byte)(color.B * 255),
                    (byte)(color.A * 255));
            }
            Control.TextColor = iosColor;
        }
    }
}