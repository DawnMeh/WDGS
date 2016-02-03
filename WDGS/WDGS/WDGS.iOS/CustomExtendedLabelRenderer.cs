using Foundation;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Forms.Controls;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]

namespace WDGS.iOS
{
    public class CustomExtendedLabelRenderer : ExtendedLabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var view = (ExtendedLabel)Element;

            UpdateUi(view, Control);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (ExtendedLabel)Element;

            if (e.PropertyName == ExtendedLabel.IsUnderlineProperty.PropertyName)
            {
                UpdateUi(view, Control);
            }
        }

        private static void UpdateUi(ExtendedLabel view, UILabel control)
        {
            var font = UIFont.SystemFontOfSize((view.FontSize > 0) ? (float)view.FontSize : 12.0f);    // regular

            control.Font = font;

            var attrString = new NSMutableAttributedString(control.Text);

            if (view.IsUnderline)
            {
                attrString.AddAttribute(UIStringAttributeKey.UnderlineStyle,
                                        NSNumber.FromInt32((int)NSUnderlineStyle.Single),
                                        new NSRange(0, attrString.Length));
            }

            control.AttributedText = attrString;
        }
    }
}
