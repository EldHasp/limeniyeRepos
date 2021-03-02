using System.Windows;
using TempDemostratedProject.Models;
using TempDemostratedProject.ViewModels;

namespace TempDemostratedProject
{
    public partial class App : Application
    {
        private Model model ;
        private MyTestListViewModel testVM ;

        public void Start_Application(object sender, StartupEventArgs e)
        {
            model = new Model();
            testVM = new MyTestListViewModel(model, 3, 3);

            MainWindow = new MainWindow() { DataContext = testVM };

            MainWindow.Show();
        }
    }
}
