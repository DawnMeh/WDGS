using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDGS.Database;
using Xamarin.Forms;

namespace WDGS {
    public class App : Application {

        //app-wide shared variables, on launch
        //these variables are set by each shared
        //project
        static public int screenWidth;
        static public int screenHeight;
        static public int currentActivity = 0;
        static public bool cameraAccessGranted;
        static public Color QUTBlue = Color.FromRgb(0, 64, 122);
        static public Color linkTextColour = Color.FromRgb(30, 144, 255);
        static public Color appContentColour = Color.FromRgb(211, 211, 211);
        static public WDGSDatabase WDGSDatabase;
        static public bool activitiesLastScreen = true;

        public App() {

            if (WDGSDatabase == null)
            {
                WDGSDatabase = new WDGSDatabase();
            }

            //iOS devices have their own launch screen
            //and thus do not need a loading screen like
            //android devices
            if (Device.OS == TargetPlatform.iOS)
            {
                MainPage = new InstructionsScreen();
            }
            else
            {
				MainPage = new LoadingScreen ();
            }
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
