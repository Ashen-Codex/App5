using Windows.Devices.Enumeration;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
        }
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {

            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "HomePage":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "Page1":
                        ContentFrame.Navigate(typeof(Page1));
                        break;
                    case "Page2":
                        ContentFrame.Navigate(typeof(Page2));
                        break;
                    case "Page3":
                        ContentFrame.Navigate(typeof(Page3));
                        break;
                    case "Page4":
                        ContentFrame.Navigate(typeof(Page4));
                        break;
                    case "Page5":
                        ContentFrame.Navigate(typeof(Page5));
                        break;
                    case "Page6":
                        ContentFrame.Navigate(typeof(Page6));
                        break;
                    case "Page7":
                        ContentFrame.Navigate(typeof(Page7));
                        break;
                    case "Page8":
                        ContentFrame.Navigate(typeof(Page8));
                        break;
                    case "Page9":
                        ContentFrame.Navigate(typeof(Page9));
                        break;
                    case "Page10":
                        ContentFrame.Navigate(typeof(Page10));
                        break;
                    case "Page11":
                        ContentFrame.Navigate(typeof(Page11));
                        break;
                    case "Page12":
                        ContentFrame.Navigate(typeof(Page12));
                        break;
                    case "Page13":
                        ContentFrame.Navigate(typeof(Page13));
                        break;
                    case "Page14":
                        ContentFrame.Navigate(typeof(Page14));
                        break;
                    case "Page15":
                        ContentFrame.Navigate(typeof(Page15));
                        break;
                    case "Page16":
                        ContentFrame.Navigate(typeof(Page16));
                        break;
                    case "Page17":
                        ContentFrame.Navigate(typeof(Page17));
                        break;
                    case "Page18":
                        ContentFrame.Navigate(typeof(Page18));
                        break;
                    case "Page19":
                        ContentFrame.Navigate(typeof(Page19));
                        break;
                    case "Page20":
                        ContentFrame.Navigate(typeof(Page20));
                        break;
                    case "Page21":
                        ContentFrame.Navigate(typeof(Page21));
                        break;
                    case "Page22":
                        ContentFrame.Navigate(typeof(Page22));
                        break;
                    case "Page23":
                        ContentFrame.Navigate(typeof(Page23));
                        break;
                    case "Page24":
                        ContentFrame.Navigate(typeof(Page24));
                        break;
                    case "Page25":
                        ContentFrame.Navigate(typeof(Page25));
                        break;
                    case "Page26":
                        ContentFrame.Navigate(typeof(Page26));
                        break;
                    case "Page27":
                        ContentFrame.Navigate(typeof(Page27));
                        break;
                    case "Page28":
                        ContentFrame.Navigate(typeof(Page28));
                        break;
                    case "Page29":
                        ContentFrame.Navigate(typeof(Page29));
                        break;
                    case "Page30":
                        ContentFrame.Navigate(typeof(Page30));
                        break;
                    case "Page31":
                        ContentFrame.Navigate(typeof(Page31));
                        break;
                    case "Page32":
                        ContentFrame.Navigate(typeof(Page32));
                        break;
                    case "Page33":
                        ContentFrame.Navigate(typeof(Page33));
                        break;
                    case "Page34":
                        ContentFrame.Navigate(typeof(Page34));
                        break;
                    case "Page35":
                        ContentFrame.Navigate(typeof(Page35));
                        break;
                    case "Page36":
                        ContentFrame.Navigate(typeof(Page36));
                        break;
                    case "Page37":
                        ContentFrame.Navigate(typeof(Page37));
                        break;
                    case "Page38":
                        ContentFrame.Navigate(typeof(Page38));
                        break;
                    case "Page39":
                        ContentFrame.Navigate(typeof(Page39));
                        break;
                    case "Page40":
                        ContentFrame.Navigate(typeof(Page40));
                        break;
                }
            }
        }
    }
}
