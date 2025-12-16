using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace App5
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }



        private void ShowNotification(string type)
        {
            try
            {
                Notification notification = NotificationFactory.CreateNotification(type);
                string message = notification.Display(); // вызываем Display(), как в задании!
                string colorName = notification.GetColor();

                OutputTextBlock.Text = message;

                // Устанавливаем цвет текста
                var color = colorName switch
                {
                    "Red" => Colors.Red,
                    "Orange" => Colors.Orange,
                    "Blue" => Colors.Blue,
                    _ => Colors.Black
                };

                OutputTextBlock.Foreground = new SolidColorBrush(color);
            }
            catch (System.ArgumentException ex)
            {
                OutputTextBlock.Text = "Ошибка: " + ex.Message;
                OutputTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        
        private void OnErrorClick(object sender, RoutedEventArgs e) => ShowNotification("error");
        private void OnWarningClick(object sender, RoutedEventArgs e) => ShowNotification("warning");
        private void OnInfoClick(object sender, RoutedEventArgs e) => ShowNotification("info");
    }
}


