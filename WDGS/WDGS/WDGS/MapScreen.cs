using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Net;

namespace WDGS
{
	public class MapScreen : ContentPage
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

        private Position activity1Pos = new Position(-27.477452, 153.028987);
        private Position activity2Pos = new Position(-27.475321, 153.027305);
        private Position activity3Pos = new Position(-27.472515, 153.024895);
        private Position activity4Pos = new Position(-27.469251, 153.020753);
        private Position activity5Pos = new Position(-27.469183, 153.019917);
        private Position activity6Pos = new Position(-27.468401, 153.021246);
        private Position activity7Pos = new Position(-27.467751, 153.020609);

        private String activity1Address = "2 George Street, Brisbane, QLD 4000";
        private String activity2Address = "Corner of George and Alice Streets, Brisbane QLD 4000";
        private String activity3Address = "142 George Street, Brisbane QLD 4000";
        private String activity4Address = "107 North Quay, Brisbane QLD 4000";
        private String activity5Address = "119 North Quay, Brisbane QLD 4000";
        private String activity6Address = "363 George Street, Brisbane QLD 4000";
        private String activity7Address = "415 George St, Brisbane QLD 4000";

        private List<Position> activityPositions = new List<Position> { };

		public MapScreen ()
		{
            activityPositions.Add(activity1Pos);
            activityPositions.Add(activity2Pos);
            activityPositions.Add(activity3Pos);
            activityPositions.Add(activity4Pos);
            activityPositions.Add(activity5Pos);
            activityPositions.Add(activity6Pos);
            activityPositions.Add(activity7Pos);

			#region imageIconLayout
			StackLayout logoLayout = new StackLayout {
				BackgroundColor = App.QUTBlue,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (4, 0, 0, 0)
			};
			#endregion

			#region imageIcons
			Image logo = new Image {
				Source = "QutLogoHeader.png",
				HeightRequest = (App.screenHeight / 12) - 4,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			#endregion

			logoLayout.Children.Add(logo);

            #region mapContent
            StackLayout mapContent = new StackLayout {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Map map = new Map(MapSpan.FromCenterAndRadius(activityPositions[App.currentActivity - 1], Distance.FromKilometers(0.25))) {
                IsShowingUser = true,
                HasZoomEnabled = true,
                HasScrollEnabled = true,
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            addActivityPinToMap(map);

            Button directionsBtn = new Button
            {
                Text = "Get Directions",
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                Command = new Command(() => sendToMapsForGPS())
            };

            StackLayout btnLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                Children = { directionsBtn }
            };

            if (Device.OS == TargetPlatform.iOS)
            {
                directionsBtn.Opacity = 0.8;
                directionsBtn.BorderColor = Color.FromRgb(60, 61, 60);
                directionsBtn.BackgroundColor = Color.FromRgb(60, 61, 60);
                directionsBtn.BorderWidth = 1;
                btnLayout.Padding = new Thickness(3, 3, 3, 0);
            }

            mapContent.Children.Add(btnLayout);

            mapContent.Children.Add(map);

            #endregion

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
					new RowDefinition { Height = App.screenHeight / 12 },
					new RowDefinition { Height = App.screenHeight / 1.213 },
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

			pageGrid.Children.Add (backLbl, 1, 1);
			pageGrid.Children.Add (logoLayout, 2, 7, 1, 2);
			pageGrid.Children.Add (mapContent, 1, 7, 2, 3);

			StackLayout innerContent = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			innerContent.Children.Add (pageGrid);

			this.Content = innerContent;

			this.Padding = new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
			this.BackgroundImage = "background.png";
		}

        private void addActivityPinToMap(Map map)
        {
            //adding correct pin to map
            switch (App.currentActivity)
            {
                case 1:
                    Pin activity1Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity1Pos,
                        Label = "Old Government House",
                        Address = activity1Address
                    };
                    map.Pins.Add(activity1Pin);
                    break;
                case 2:
                    Pin activity2Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity2Pos,
                        Label = "Parliament House",
                        Address = activity2Address
                    };
                    map.Pins.Add(activity2Pin);
                    break;
                case 3:
                    Pin activity3Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity3Pos,
                        Label = "Executive Building",
                        Address = activity3Address
                    };
                    map.Pins.Add(activity3Pin);
                    break;
                case 4:
                    Pin activity4Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity4Pos,
                        Label = "Inns of Court",
                        Address = activity4Address
                    };
                    map.Pins.Add(activity4Pin);
                    break;
                case 5:
                    Pin activity5Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity5Pos,
                        Label = "Commonwealth Law Courts",
                        Address = activity5Address
                    };
                    map.Pins.Add(activity5Pin);
                    break;
                case 6:
                    Pin activity6Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity6Pos,
                        Label = "Magistrates' Court",
                        Address = activity6Address
                    };
                    map.Pins.Add(activity6Pin);
                    break;
                case 7:
                    Pin activity7Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = activity7Pos,
                        Label = "QEII Courts Complex",
                        Address = activity7Address
                    };
                    map.Pins.Add(activity7Pin);
                    break;
                default:
                    break;
            }
        }

        private void sendToMapsForGPS()
        {
            String address = "";

            switch (App.currentActivity)
            {
                case 1:
                    address = activity1Address;
                    break;
                case 2:
                    address = activity2Address;
                    break;
                case 3:
                    address = activity3Address;
                    break;
                case 4:
                    address = activity4Address;
                    break;
                case 5:
                    address = activity5Address;
                    break;
                case 6:
                    address = activity6Address;
                    break;
                case 7:
                    address = activity7Address;
                    break;
                default:
                    break;
            }

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case TargetPlatform.Android:
                    Device.OpenUri(new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                    break;
            }
        }

		private void goBack ()
		{
			App.Current.MainPage = new MainActivityScreen ();
		}
	}
}


