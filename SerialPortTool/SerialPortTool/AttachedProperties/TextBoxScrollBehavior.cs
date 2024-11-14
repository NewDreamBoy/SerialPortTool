using System.Windows;
using System.Windows.Controls;

namespace SerialPortTool.AttachedProperties
{
    public static class TextBoxScrollBehavior
    {
        public static readonly DependencyProperty AutoScrollToEndProperty = DependencyProperty.RegisterAttached(
            "AutoScrollToEnd", typeof(bool), typeof(TextBoxScrollBehavior), new PropertyMetadata(false, OnAutoScrollToEndChanged));

        public static bool GetAutoScrollToEnd(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToEndProperty);
        }

        public static void SetAutoScrollToEnd(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToEndProperty, value);
        }

        private static void OnAutoScrollToEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox && e.NewValue is bool isAutoScroll && isAutoScroll)
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
            else if (d is TextBox textBox2)
            {
                textBox2.TextChanged -= TextBox_TextChanged;
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.ScrollToEnd();
            }
        }
    }
}