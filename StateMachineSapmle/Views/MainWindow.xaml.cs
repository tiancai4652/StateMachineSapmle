using StateMachineSapmle.Model;
using System.Windows;

namespace StateMachineSapmle.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Elevator Elevator { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Elevator = new Elevator();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Elevator.GoToUpperLevel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Elevator.GoToLowerLevel();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Elevator.Stop();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Elevator.Error();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Elevator.Reset();
        }
    }
}
