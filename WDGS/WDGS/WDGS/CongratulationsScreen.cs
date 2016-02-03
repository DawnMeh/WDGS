using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace WDGS
{
    public class CongratulationsScreen : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                App.Current.MainPage = new MainActivityScreen();
                return true;
            }
            return base.OnBackButtonPressed();
        }
        public CongratulationsScreen()
        {
            #region imageIcons
            Image logo = new Image
            {
                Source = "QutLogoHeader.png",
                HeightRequest = (App.screenHeight / 10) - 4,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Image WDGS_logo = new Image
            {
                Source = "logo.png",
                HeightRequest = App.screenHeight / 2.5,
                WidthRequest = App.screenWidth / 1.8,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            #endregion

            #region contentLayouts
            StackLayout logoLayout = new StackLayout
            {
                BackgroundColor = App.QUTBlue,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(4, 0, 0, 0)
            };
            StackLayout WDGS_logoLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(4, (App.screenHeight / 10) / 3, 0, 0)
            };
            StackLayout homeTextLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(4, (App.screenHeight / 10) / 6, 0, 0)
            };
            #endregion

            Grid pageGrid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Opacity = 0.8,
                //row and column spacing creates a "bordered"
                //effect around each element
                RowSpacing = 2,
                ColumnSpacing = 2,
                IsClippedToBounds = true,
                Padding = new Thickness(.5, 1, .5, 0),
                //all row heights, and column widths are relative to
                //the devices screen size/resolution so they should
                //be an appropriate size on each type of device
                RowDefinitions = {
                    new RowDefinition {Height = 0},
                    new RowDefinition {Height = App.screenHeight / 10},
                    new RowDefinition {Height = App.screenHeight / 1.4},
                    new RowDefinition {Height = App.screenHeight / 10},
                    new RowDefinition {Height = 0}
                },
                ColumnDefinitions = 
                {
                    new ColumnDefinition {Width = 0},
                    new ColumnDefinition {Width = App.screenWidth / 2 - 16},
                    new ColumnDefinition {Width = App.screenWidth / 2 - 16},
                    new ColumnDefinition {Width = 0}
                }
            };
            Label congratulationsLbl = new Label
            {
                Text = "WELL DONE ON COMPLETING",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) + 10,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            Label homeLbl = new Label
            {
                Text = "BACK TO HOME >",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) + 10,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            homeTextLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => goHome()),
            });  

            logoLayout.Children.Add(logo);
            pageGrid.Children.Add(logoLayout, 1, 3, 1, 2);
            WDGS_logoLayout.Children.Add(congratulationsLbl);
            WDGS_logoLayout.Children.Add(WDGS_logo);
            pageGrid.Children.Add(WDGS_logoLayout, 1, 3, 2, 3);
            homeTextLayout.Children.Add(homeLbl);
            pageGrid.Children.Add(homeTextLayout, 1, 3, 3, 4);

            StackLayout innerContent = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };
            innerContent.Children.Add(pageGrid);

            this.Content = innerContent;
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.BackgroundImage = "background.png";
        }

        private void goHome()
        {
            App.Current.MainPage = new HomeScreen();
        }
    }
}
