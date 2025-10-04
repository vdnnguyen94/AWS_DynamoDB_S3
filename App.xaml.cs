using Syncfusion.Licensing;
using System.Configuration;
using System.Data;
using System.Windows;
using DotNetEnv;
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
            // Load .env file
            Env.Load();

            var licenseKey = Environment.GetEnvironmentVariable("SYNCFUSION_LICENSE_KEY");

            if (!string.IsNullOrEmpty(licenseKey))
                SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            else
                MessageBox.Show("Syncfusion license key is missing in .env file.", "Warning");
        }
   
    }

}
