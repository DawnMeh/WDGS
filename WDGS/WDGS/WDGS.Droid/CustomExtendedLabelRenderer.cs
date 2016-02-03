using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms.Controls;
using System.ComponentModel;
using Android.Graphics;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRender))]

namespace WDGS.Droid
{
    public class CustomExtendedLabelRenderer : ExtendedLabelRender
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
                Control.PaintFlags = view.IsUnderline ? Control.PaintFlags | PaintFlags.UnderlineText : Control.PaintFlags &= ~PaintFlags.UnderlineText;
            }
        }

        static void UpdateUi(ExtendedLabel view, TextView control)
        {
            if (view.FontSize > 0)
            {
                control.TextSize = (float)view.FontSize;
            }

            if (view.IsUnderline)
            {
                control.PaintFlags = control.PaintFlags | PaintFlags.UnderlineText;
            }
        }
    }
}