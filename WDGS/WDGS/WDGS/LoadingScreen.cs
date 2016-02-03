using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WDGS
{
    public class LoadingScreen : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                DependencyService.Get<AndroidMethods>().CloseApp();
                return true;
            }
            return base.OnBackButtonPressed();
        }
        public LoadingScreen()
        {
            Image logoImage = new Image()
            {
                Source = "logo.png",
                HeightRequest = App.screenHeight / 2.5,
                WidthRequest = App.screenWidth / 1.8
            };

            //Loading indicator native for each platform
            ActivityIndicator loadActivity = new ActivityIndicator
            {
                Color = Color.Default,
                IsRunning = true,
                IsVisible = true,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            //absolute layout to absolute position logo and loading indicator
            AbsoluteLayout innerContent = new AbsoluteLayout();

            //adding and positioning logo to the absolute layout
            innerContent.Children.Add(logoImage);
            AbsoluteLayout.SetLayoutFlags(logoImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(logoImage, new Rectangle(0.5, 0.4, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            //adding and positioning loading indicator
            innerContent.Children.Add(loadActivity);
            AbsoluteLayout.SetLayoutFlags(loadActivity, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loadActivity, new Rectangle(0.5, 0.8, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            this.Content = innerContent;

            //account for iOS status bar
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.BackgroundImage = "background.png";

            //load the instructions page
            this.loadInstructions();
        }

        //function to load the instructions page
        private async void loadInstructions()
        {
            //delay the load to create a loading experience
            await Task.Delay(3000);
            App.Current.MainPage = new InstructionsScreen();
        }
    }
}
