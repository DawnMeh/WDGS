using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WDGS
{
	public class MainActivityScreen : ContentPage
	{
        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                goBack();
                return true;
            }
            return base.OnBackButtonPressed();
        }

		public static int countActivity = 0;
        public String activityVideoURL = "";
        public String locationDescription = "";

		public MainActivityScreen ()
		{
            getVideoURLAndLocationDescriptionForActivity();

			#region imageIconLayout
			StackLayout logoLayout = new StackLayout {
				BackgroundColor = App.QUTBlue,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (4, 0, 0, 0)
			};

			StackLayout homeIconLayout = new StackLayout {
				BackgroundColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (0, 2, 0, 0)
			};

			StackLayout mapIconLayout = new StackLayout {
				BackgroundColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (0, 2, 0, 0)
			};

			StackLayout questionIconLayout = new StackLayout {
				BackgroundColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (0, 2, 0, 0)
			};

			StackLayout linksIconLayout = new StackLayout {
				BackgroundColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (0, 2, 0, 0)
			};
			#endregion

			#region videoEmbed
			StackLayout videoContent = new StackLayout {
				//Padding = new Thickness (0, 2, 0, 0),
				BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = App.screenWidth / 1.1,
                HeightRequest = App.screenHeight / 2.3
			};

            double playerWidth = App.screenWidth / 1.1;
            double playerHeight = App.screenHeight / 2.3;

			// To do - YouTube plugin
            WebView youtubePlayer = new WebView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Black,
                WidthRequest = playerWidth,
                HeightRequest = playerHeight
            };

            String youtubeSourceHtml = @"<html><head><style type='text/css'>body { background-color: black; }</style></head><body><iframe width='" + playerWidth + "' height='" + playerHeight + "' src='" + activityVideoURL + "' frameborder='0' allowfullscreen></iframe></body></html>";

            HtmlWebViewSource youtubeSource = new HtmlWebViewSource()
            {
                Html = youtubeSourceHtml
            };

			youtubePlayer.Source = youtubeSource;

			videoContent.Children.Add(youtubePlayer);
			#endregion

			StackLayout descripContent = new StackLayout {
				Padding = new Thickness (5, 10, 0, 0),
				BackgroundColor = Color.Black
			};

            StackLayout locationDescripContent = new StackLayout
            {
                Padding = new Thickness(5, 5, 5, 0),
                BackgroundColor = Color.Black,
                Children = {new Label {
                    Text = locationDescription,
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    TextColor = App.appContentColour,
                    BackgroundColor = Color.Black,
                    HorizontalTextAlignment = TextAlignment.Start,
                    LineBreakMode = LineBreakMode.WordWrap
                }}
            };
					
			#region imageIcons
			Image logo = new Image {
				Source = "QutLogoHeader.png",
				HeightRequest = (App.screenHeight / 10) - 4,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			Image homeIcon = new Image {
				Source = "homeIcon.png",
				HeightRequest = (App.screenHeight / 12) - 4,
				WidthRequest = 30
			};

			Image mapIcon = new Image {
				Source = "mapIcon.png",
				HeightRequest = (App.screenHeight / 12) - 4,
				WidthRequest = 30
			};

			Image questionIcon = new Image {
				Source = "questionIcon.png",
				HeightRequest = (App.screenHeight / 12) - 4,
				WidthRequest = 30
			};

			Image linksIcon = new Image {
				Source = "relevantLinksIcon.png",
				HeightRequest = (App.screenHeight / 12) - 4,
				WidthRequest = 30
			};

			homeIcon.GestureRecognizers.Add(new TapGestureRecognizer {
				Command = new Command (() => goHome ())
			});

			mapIcon.GestureRecognizers.Add(new TapGestureRecognizer {
				Command = new Command (() => goToMap ())
			});

			questionIcon.GestureRecognizers.Add(new TapGestureRecognizer {
				Command = new Command (() => goToQuestions ())
			});

			linksIcon.GestureRecognizers.Add(new TapGestureRecognizer {
				Command = new Command (() => goToLinks ())
			});
			#endregion

			logoLayout.Children.Add(logo);
			homeIconLayout.Children.Add(homeIcon);
			mapIconLayout.Children.Add(mapIcon);
			questionIconLayout.Children.Add(questionIcon);
			linksIconLayout.Children.Add(linksIcon);

			Grid pageGrid = new Grid {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				BackgroundColor = Color.White,
				Opacity = 0.8,
				RowSpacing = 2,
				ColumnSpacing = 2,
				IsClippedToBounds = true,
				Padding = new Thickness (.5, 1, .5, 0),
				RowDefinitions = {
					new RowDefinition { Height = 0 },
					new RowDefinition { Height = App.screenHeight / 10 },
                    new RowDefinition { Height = App.screenHeight / 12 },
					new RowDefinition { Height = App.screenHeight / 2.35 },
					new RowDefinition { Height = App.screenHeight / 5 },
					new RowDefinition { Height = App.screenHeight / 12 },
					new RowDefinition { Height = 0 }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = 0 },
					new ColumnDefinition { Width = App.screenWidth / 10 },
					new ColumnDefinition { Width = App.screenWidth / 5 - 32 },
					new ColumnDefinition { Width = App.screenWidth / 5 },
					new ColumnDefinition { Width = App.screenWidth / 5 },
					new ColumnDefinition { Width = App.screenWidth / 5 },
					new ColumnDefinition { Width = App.screenWidth / 10 },
					new ColumnDefinition { Width = 0 }
				}
			};

			#region descriptions
			Label govHouse_des = new Label {
				Text = "1. Old Government House",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
			};

			Label parHouse_des = new Label {
				Text = "2. Parliament House",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};

			Label execBuild_des = new Label {
				Text = "3. Executive Building",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};

			Label innsCourt_des = new Label {
				Text = "4. Inns of Court",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};

			Label comCourt_des = new Label {
				Text = "5. Commonwealth Law Courts",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};

			Label magCourt_des = new Label {
				Text = "6. Magistrates Court",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};

			Label qeIICourt_des = new Label {
				Text = "7. Queen Elizabeth II Court Complex",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};
			#endregion

			Label backLbl = new Label {
				Text = "<",
				BackgroundColor = App.QUTBlue,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 4,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center
			};

			backLbl.GestureRecognizers.Add (new TapGestureRecognizer {
				Command = new Command (() => goBack ())
			});
				
			Label nextLbl = new Label {
				Text = ">",
				BackgroundColor = Color.Black,
				TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 4,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center
			};

			nextLbl.GestureRecognizers.Add (new TapGestureRecognizer {
				Command = new Command (() => goToNextActivity ())
			});

			#region descLoad
            if (App.currentActivity == 1)
            {
				descripContent.Children.Add(govHouse_des);
            }
            else if (App.currentActivity == 2)
            {
				descripContent.Children.Add(parHouse_des);
            }
            else if (App.currentActivity == 3)
            {
				descripContent.Children.Add(execBuild_des);
            }
            else if (App.currentActivity == 4)
            {
				descripContent.Children.Add(innsCourt_des);
            }
            else if (App.currentActivity == 5)
            {
				descripContent.Children.Add(comCourt_des);
            }
            else if (App.currentActivity == 6)
            {
				descripContent.Children.Add(magCourt_des);
            }
            else if (App.currentActivity == 7)
            {
				descripContent.Children.Add(qeIICourt_des);
			}
			#endregion

			pageGrid.Children.Add(backLbl, 1, 1);
			pageGrid.Children.Add(logoLayout, 2, 7, 1, 2);
            pageGrid.Children.Add(descripContent, 1, 7, 2, 3);
			pageGrid.Children.Add(videoContent, 1, 7, 3, 4);
            pageGrid.Children.Add(locationDescripContent, 1, 7, 4, 5);
			pageGrid.Children.Add(homeIconLayout, 1, 3, 5, 6);
			pageGrid.Children.Add(mapIconLayout, 3, 5);
			pageGrid.Children.Add(questionIconLayout, 4, 5);
			pageGrid.Children.Add(linksIconLayout, 5, 5);
			pageGrid.Children.Add(nextLbl, 6, 5);

			StackLayout innerContent = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			innerContent.Children.Add(pageGrid);

			this.Content = innerContent;

			this.Padding = new Thickness(0, Device.OnPlatform (20, 0, 0), 0, 0);
			this.BackgroundImage = "background.png";
		}

        private void getVideoURLAndLocationDescriptionForActivity()
        {
            switch (App.currentActivity)
            {
                case 1:
                    locationDescription = "Old Government House is located along the path between the QUT Library and P block. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/uZP1ZtHsfeo";
                    break;
                case 2:
                    locationDescription = "Parliament House is located on the left as you leave QUT along George Street. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/PUizT-hqDbw";
                    break;
                case 3:
                    locationDescription = "The Executive Building is located on the left hand side of George Street, called the 'Lands Administration Building'. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/0cmFKf7jmMc";
                    break;
                case 4:
                    locationDescription = "The Inns of Court entrance is located along Turbot Street. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/yxth-rbxDwI";
                    break;
                case 5:
                    locationDescription = "The Commonwealth Law Courts is on the right hand side of North Quay. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/d-5Ls_sjcxk";
                    break;
                case 6:
                    locationDescription = "The Magistrates Court is located on the right hand side of George Street. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/R8mhWfWjSuA";
                    break;
                case 7:
                    locationDescription = "The Queen Elizabeth II Courts Complex is located on the right hand side of George Street. For further directions click on the globe icon below.";
                    activityVideoURL = "https://www.youtube.com/embed/bwyByp_bmLM";
                    break;
                default:
                    break;
            }
        }

		private void goBack ()
		{
            if (App.currentActivity == 1 || App.activitiesLastScreen)
            {
                App.Current.MainPage = new ActivitiesListScreen();
                return;
            }

            App.currentActivity -= 1;
            App.Current.MainPage = new MainActivityScreen();		
		}

		private void goHome ()
		{
			App.Current.MainPage = new HomeScreen ();
		}

		private void goToMap ()
		{
			App.Current.MainPage = new MapScreen ();
		}

		private void goToQuestions ()
		{
			App.Current.MainPage = new QuestionsScreen ();
		}

		private void goToLinks ()
		{
			App.Current.MainPage = new RelevantLinksScreen ();
		}

		private void goToNextActivity ()
		{
            App.activitiesLastScreen = false;

            if (App.currentActivity == 7)
            {
                App.Current.MainPage = new CongratulationsScreen();
                return;
            }

			App.currentActivity += 1;
			App.Current.MainPage = new MainActivityScreen ();
		}
	}
}


