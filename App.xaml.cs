using Syncfusion.Licensing;
using System.Configuration;
using System.Data;
using System.Windows;

namespace _301289600Nguyen_Lab2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Register the Syncfusion license key
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1JpRnxbf1x1ZFRMZVpbR39PIiBoS35Rc0RjWXtfc3FTQmJdV0FzVEFc");
        }
    }

}
