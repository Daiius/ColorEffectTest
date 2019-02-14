using Prism.Mvvm;

using DCAMAPI_Test.Models;

namespace DCAMAPI_Test.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainModel MainModel { get; set; }

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            MainModel = new MainModel();

            MainModel.Init();

            MainModel.Uninit();
        }
    }
}
