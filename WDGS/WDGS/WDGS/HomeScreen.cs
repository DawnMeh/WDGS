using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace WDGS
{
    public class HomeScreen : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                goToInstructions();
                return true;
            }
            return base.OnBackButtonPressed();
        }
        public HomeScreen()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

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
            Image Activities = new Image
            {
                Source = "activitiesIcon.png",
                HeightRequest = (App.screenHeight / 6) - 4,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Image Instruction = new Image
            {
                Source = "instructionIcon.png",
                HeightRequest = (App.screenHeight / 6) - 4,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            
            Activities.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => goToActivityListScreen())
            });
            Instruction.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => goToInstructions())
            });
            #endregion

            #region imageIconLayouts
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
            };
            StackLayout ActivitiesLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 2, 0, 2)
            };
            StackLayout InstructionLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 2, 0, 2)
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
                    new RowDefinition {Height = App.screenHeight / 1.7},
                    new RowDefinition {Height = App.screenHeight / 6 + 20},
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
            Label Act = new Label
            {
                Text = "ACTIVITIES",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Label Ins = new Label
            {
                Text = "INSTRUCTIONS",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            logoLayout.Children.Add(logo);
            pageGrid.Children.Add(logoLayout, 1, 3, 1, 2);
            WDGS_logoLayout.Children.Add(WDGS_logo);
            pageGrid.Children.Add(WDGS_logoLayout, 1, 3, 2, 3);
            ActivitiesLayout.Children.Add(Activities);
            ActivitiesLayout.Children.Add(Act);
            pageGrid.Children.Add(ActivitiesLayout, 1, 2, 3, 4);
            InstructionLayout.Children.Add(Instruction);
            InstructionLayout.Children.Add(Ins);
            pageGrid.Children.Add(InstructionLayout, 2, 3, 3, 4);

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

		private void goToActivityListScreen()
		{
            App.Current.MainPage = new ActivitiesListScreen();
		}
        private void goToInstructions()
        {
            App.Current.MainPage = new InstructionsScreen();
        }
    }
}
