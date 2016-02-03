
using Xamarin.Forms;
using WDGS.Droid;

[assembly: Dependency(typeof(AndroidMethodsDroid))]

namespace WDGS.Droid
{
    public class AndroidMethodsDroid : AndroidMethods
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}