using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SerialPortTool.AttachedProperties
{
    public static class TextBoxExtensions
    {
        // 定义一个依赖属性来绑定数据集合
        public static readonly DependencyProperty ReceiveTextCollectionProperty = DependencyProperty.RegisterAttached(
            "ReceiveTextCollection",
            typeof(ObservableCollection<string>),
            typeof(TextBoxExtensions),
            new PropertyMetadata(null, OnReceiveTextCollectionChanged));

        public static ObservableCollection<string> GetReceiveTextCollection(TextBox textBox)
        {
            return (ObservableCollection<string>)textBox.GetValue(ReceiveTextCollectionProperty);
        }

        public static void SetReceiveTextCollection(TextBox textBox, ObservableCollection<string> value)
        {
            textBox.SetValue(ReceiveTextCollectionProperty, value);
        }

        private static void OnReceiveTextCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if (e.OldValue is ObservableCollection<string> oldCollection)
                {
                    oldCollection.CollectionChanged -= (sender, args) => UpdateTextBox(textBox, args);
                }

                if (e.NewValue is ObservableCollection<string> newCollection)
                {
                    newCollection.CollectionChanged += (sender, args) => UpdateTextBox(textBox, args);
                }
            }
        }

        private static void UpdateTextBox(TextBox textBox, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                textBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (string newItem in args.NewItems)
                    {
                        textBox.AppendText(newItem);
                        textBox.ScrollToEnd();
                    }
                }), DispatcherPriority.Background);
            }
        }
    }
}