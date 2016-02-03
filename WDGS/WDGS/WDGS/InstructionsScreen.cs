using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WDGS
{
    public class InstructionsScreen : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                askUserToExit();
                return true;
            }
            return base.OnBackButtonPressed();
        }

        private async void askUserToExit()
        {
            var exit = await this.DisplayAlert("Exit", "Do you wish to exit the app completely?", "Exit", "Cancel");
            if (exit)
            {
                DependencyService.Get<AndroidMethods>().CloseApp();
                return;
            }
        }

        public InstructionsScreen()
        {
            //labels and images for the instructions
            //labels and images are created in the order
            //they are displayed on the instruction screen
            //for simplicity
            #region instructions
            Label instruction1Lbl = new Label
            {
                Text = "\nScroll down to read all instructions\n\n" +
                       "Welcome to the Walk Down George Street app. This will guide you to seven key sites along George Street that are pivotal to the operation of law and government in Queensland. If you are a QUT Law student, completing this activity is part of your assessment for LLB101. If you aren't a QUT Law student, this app is a great way to learn about the legal system in Queensland and have fun walking around our beautiful city.\n\nNow you have successfully downloaded the app, how do you get the most from it?  At each location you are required to complete tasks and answer questions.\n\n1. Meet your fellow team members at the QUT Law Library when ready to begin\n2. Click on the first location and play the video. Take note of what the building looks like\n3. Click on the map icon and follow the directions to locate the building",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black,
            };

            Image mapIcon = new Image
            {
                Source = "mapIcon.png",
                HeightRequest = App.screenWidth / 4,
            };

            Label instruction2Lbl = new Label
            {
                Text = "4. When you arrive, click on the questions icon to bring up the questions to complete for the site. Type your answers in the text boxes below each question and the app will save them for you to share later",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black
            };

            Image questionIcon = new Image
            {
                Source = "questionIcon.png",
                HeightRequest = App.screenWidth / 4,
            };

            Label instruction3Lbl = new Label
            {
                Text = "5. Next, click on the tasks icon and complete the tasks",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black
            };

            Image taskIcon = new Image
            {
                Source = "taskIcon.png",
                HeightRequest = App.screenWidth / 4,
            };

            Label instruction4Lbl = new Label
            {
                Text = "If you are required to take a picture, use the camera icon to bring up the camera",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black
            };

            Image cameraIcon = new Image
            {
                Source = "cameraIcon.png",
                HeightRequest = App.screenWidth / 4,
            };

            Label instruction5Lbl = new Label
            {
                Text = "6. Next, click on the trivia icon to complete the extra trivia questions about the site",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black
            };

            Image triviaIcon = new Image
            {
                Source = "triviaIcon.png",
                BackgroundColor = Color.Black,
                HeightRequest = App.screenWidth / 4,
            };

            Label instruction6Lbl = new Label
            {
                Text = "7. Finally, you can post pictures to Twitter using the following hash tags, #QUTWalkDownGeorgeStreet and #QUTSeeingMyselfInTheLaw\n8. There are 7 locations to visit and complete.\n\nKey reference material can also be found by clicking on the relevant links icon.",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black
            };

            Image relevantLinksIcon = new Image
            {
                Source = "relevantLinksIcon.png",
                BackgroundColor = Color.Black,
                HeightRequest = App.screenWidth / 4,
            };

            Label instruction7Lbl = new Label
            {
                Text = "\nHave fun and enjoy your Walk Down George Street!\n\n",
                TextColor = App.appContentColour, 
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                BackgroundColor = Color.Black
            };
            #endregion

            StackLayout instructionContent = new StackLayout();

            //adding each instruction in the correct order
            //stacklayout works like a stack with the first
            //element being added being at the top and
            //each element after being added below
            instructionContent.Children.Add(instruction1Lbl);
            instructionContent.Children.Add(mapIcon);
            instructionContent.Children.Add(instruction2Lbl);
            instructionContent.Children.Add(questionIcon);
            instructionContent.Children.Add(instruction3Lbl);
            instructionContent.Children.Add(taskIcon);
            instructionContent.Children.Add(instruction4Lbl);
            instructionContent.Children.Add(cameraIcon);
            instructionContent.Children.Add(instruction5Lbl);
            instructionContent.Children.Add(triviaIcon);
            instructionContent.Children.Add(instruction6Lbl);
            instructionContent.Children.Add(relevantLinksIcon);
            instructionContent.Children.Add(instruction7Lbl);

            ScrollView instructions = new ScrollView
            {
                IsClippedToBounds = true,
                Content = instructionContent
            };

            /*
             * this stacklayout is needed for the scrollview
             * if you do the padding and background colour in
             * the scrollview, it makes the background colour
             * of the grid disappear, but placing it in a stacklayout
             * and having the same padding/background colour
             * makes the scrollview not affect the grid.
             * see this thread for more info:
             * forums.xamarin.com/discussion/53974/grid-background-colour-disappears-on-load-any-way-to-fix
            */
            StackLayout instructionScrollViewWrapper = new StackLayout
            {
                Padding = new Thickness(3, 0, 2, 0),
                BackgroundColor = Color.Black
            };

            //Adding the instruction text to the scrollview
            instructionScrollViewWrapper.Children.Add(instructions);

            //creating a 5x6 grid 
            //background set to white with row/column spacing
            //which enables a border effect on the grid
            Grid pageGrid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Opacity = 0.8,
                RowSpacing = 2,
                ColumnSpacing = 2,
                Padding = new Thickness(.5, 1, .5, 0),
                RowDefinitions = {
                    new RowDefinition {Height = 0},
                    new RowDefinition {Height = App.screenHeight / 10},
                    new RowDefinition {Height = App.screenHeight / 1.35},
                    new RowDefinition {Height = App.screenHeight / 12},
                    new RowDefinition {Height = 0}
                },
                ColumnDefinitions = 
                {
                    new ColumnDefinition {Width = 0},
                    new ColumnDefinition {Width = App.screenWidth / 4},
                    new ColumnDefinition {Width = App.screenWidth / 4},
                    new ColumnDefinition {Width = App.screenWidth / 4 - 28},
                    new ColumnDefinition {Width = App.screenWidth / 4},
                    new ColumnDefinition {Width = 0}
                }
            };

            //Adding each page element into the pages grid layout
            pageGrid.Children.Add(instructionScrollViewWrapper, 1, 5, 2, 3);

            pageGrid.Children.Add(new Label
            {
                Text = " Instructions:",
                BackgroundColor = App.QUTBlue,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 4,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 1, 5, 1, 2);

            Label skipLbl = new Label
            {
                Text = "Skip",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 4,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            skipLbl.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => goToHomeScreen()),
            });

            //blank label to fill blank spaces on grid
            pageGrid.Children.Add(new Label
            {
                Text = "",
                BackgroundColor = Color.Black,
            }, 1, 4, 3, 4);

            pageGrid.Children.Add(skipLbl, 4, 3);

            //creating layout to host all the pages content
            StackLayout innerContent = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            //adding the grid to the stacklayout to display the content
            innerContent.Children.Add(pageGrid);

            //set the pages content to be the stacklayout
            this.Content = innerContent;

            //add padding to account for iOS status bar
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.BackgroundImage = "background.png";
        }

        private void goToHomeScreen()
        {
            App.Current.MainPage = new HomeScreen();
        }
    }
}
