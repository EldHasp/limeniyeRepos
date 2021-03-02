using System.Windows;

namespace TempDemostratedProject
{
    public partial class App : Application
    {
        private Model model = new Model();
        private MainViewModel testVM ;

        public void Start_Application(object sender, StartupEventArgs e)
        {
            testVM = new MainViewModel(model); 
        }
    }
}
