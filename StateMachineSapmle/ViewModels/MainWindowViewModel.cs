using Prism.Mvvm;
using StateMachineSapmle.Model;

namespace StateMachineSapmle.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public Elevator Elevator { get; set; }

        public MainWindowViewModel()
        {
            Elevator = new Elevator();
        }
    }
}
