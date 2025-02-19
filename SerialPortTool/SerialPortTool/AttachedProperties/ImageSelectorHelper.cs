using HandyControl.Controls;
using System.Windows;

namespace SerialPortTool.AttachedProperties
{
    public static class ImageSelectorHelper
    {
        public static readonly DependencyProperty BindableUriProperty =
            DependencyProperty.RegisterAttached(
                "BindableUri",
                typeof(Uri),
                typeof(ImageSelectorHelper),
                new PropertyMetadata(null, OnBindableUriChanged));

        public static void SetBindableUri(DependencyObject obj, Uri value)
            => obj.SetValue(BindableUriProperty, value);

        public static Uri GetBindableUri(DependencyObject obj)
            => (Uri)obj.GetValue(BindableUriProperty);

        private static void OnBindableUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageSelector selector)
            {
                // 当外部给我们的附加属性赋值时，我们再去给 selector.Uri 赋值
                selector.Uri = (Uri)e.NewValue;
            }
        }
    }
}