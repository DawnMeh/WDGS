using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace WDGS
{
    public class RelevantLinksScreen : ContentPage
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

        public RelevantLinksScreen()
        {
            //creating layouts to store each of the images within the grid
            //without this the background of the grid is white when we want
            //it to be black
            #region imageIconLayouts
            StackLayout logoLayout = new StackLayout
            {
                BackgroundColor = App.QUTBlue,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(4, 0, 0, 0)
            };
            #endregion

            //creating each of the image icons for
            //the grid with their tap commands to load
            //correct file
            #region imageIcons

            Image logo = new Image
            {
                Source = "QutLogoHeader.png",
                HeightRequest = (App.screenHeight / 10) - 4,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            #endregion

            logoLayout.Children.Add(logo);

            //The Labels for all the Links within the Links section
            # region Links
            //Activity 1 text and links
            Label Activity1 = new Label
            {
                Text = "1. Old Government House",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link1a = new ExtendedLabel
            {
                Text = "• http://www.ogh.qut.edu.au/",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link1b = new ExtendedLabel
            {
                Text = "• http://www.govhouse.qld.gov.au/the-governor-of-queensland/about-the-governor.aspx",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link1c = new ExtendedLabel
            {
                Text = "• http://www.ogh.qut.edu.au/history/the_house/",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            Label link1d= new Label
            {
                Text = "• Michelle Sanson and Thalia Anthony, Connecting with Law (Oxford University Press, 3rd ed, 2014 382.",
                BackgroundColor = Color.Black,
                TextColor = App.appContentColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 1 text and links

            //Activity 2 Text and Links
            Label Activity2 = new Label
            {
                Text = "2. Parliament House",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link2a = new ExtendedLabel
            {
                Text = "• http://www.parliament.qld.gov.au/",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link2b = new ExtendedLabel
            {
                Text = "• http://www.ogh.qut.edu.au/about/history/political/keyevents.jsp",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link2c= new ExtendedLabel
            {
                Text = "• http://www.parliament.qld.gov.au/explore/education/factsheets/6",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link2d = new ExtendedLabel
            {
                Text = "• https://www.youtube.com/watch?v=IVUItgaQALM",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            Label link2e = new Label
            {
                Text = "• Michelle Sanson and Thalia Anthony, Connecting with Law (Oxford University Press 3rd ed, 2014 126.",
                BackgroundColor = Color.Black,
                TextColor = App.appContentColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 2 text and links

            //Activity 3 Text and Links
            Label Activity3 = new Label
            {
                Text = "3. Executive Building",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            Label link3a = new Label
            {
                Text = "• Michelle Sanson and Thalia Anthony, Connecting with Law (Oxford University Press 3rd ed, 2014 129-130.",
                BackgroundColor = Color.Black,
                TextColor = App.appContentColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 3 text and links

            //Activity 4 Text and Links
            Label Activity4 = new Label
            {
                Text = "4. Inns of Court",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link4a = new ExtendedLabel
            {
                Text = "• http://www.innsofcourt.com.au/",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link4b = new ExtendedLabel
            {
                Text = "• https://www.qldbar.asn.au/index.php/about-the-bar/what-is-the-bar3",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 4 text and links

            //Activity 5 Text and Links
            Label Activity5 = new Label
            {
                Text = "5. Commonwealth Law Courts",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link5a = new ExtendedLabel
            {
                Text = "• http://www.hca.gov.au",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link5b = new ExtendedLabel
            {
                Text = "• http://www.fedcourt.gov.au",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link5c = new ExtendedLabel
            {
                Text = "• http://www.familycourt.gov.au",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link5d = new ExtendedLabel
            {
                Text = "• http://www.hcourt.gov.au/registry/court-calendars",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link5e = new ExtendedLabel
            {
                Text = "• http://www.hcourt.gov.au/justices/about-the-justices",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link5f = new ExtendedLabel
            {
                Text = "• http://www5.austlii.edu.au/au/legis/cth/consol_act/hcoaa1979233/s7.html",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 5 text and links

            //Activity 6 Text and Links
            Label Activity6 = new Label
            {
                Text = "6. Magistrates Court",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link6a = new ExtendedLabel
            {
                Text = "• http://www.courts.qld.gov.au/courts/magistrates-court",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link6b = new ExtendedLabel
            {
                Text = "• http://www.qld.gov.au/law/court/going-to-court/courtroom-rules/",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            Label link6c = new Label
            {
                Text = "• Michelle Sanson and Thalia Anthony, Connecting with Law (Oxford University Press, 3rd ed, 2014 132-139.",
                BackgroundColor = Color.Black,
                TextColor = App.appContentColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 6 text and links

            //Activity 7 Text and Links
            Label Activity7 = new Label
            {
                Text = "7. Queen Elizabeth II Courts Complex",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link7a = new ExtendedLabel
            {
                Text = "• http://archive.sclqld.org.au/judgepub/dj260401.pdf",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link7b = new ExtendedLabel
            {
                Text = "• http://www.courts.qld.gov.au/courts/district-court",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            ExtendedLabel link7c = new ExtendedLabel
            {
                Text = "• http://www.courts.qld.gov.au/courts/supreme-court",
                BackgroundColor = Color.Black,
                TextColor = App.linkTextColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                IsUnderline = true,
                HorizontalTextAlignment = TextAlignment.Start
            };
            Label link7d = new Label
            {
                Text = "• Michelle Sanson and Thalia Anthony, Connecting with Law (Oxford University Press, 3rd ed, 2014 131-133.",
                BackgroundColor = Color.Black,
                TextColor = App.appContentColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            //End activity 7 text and links

            //Error label
            Label error = new Label
            {
                Text = "Something went wrong while loading. Please try again by clicking back and reloading the page",
                BackgroundColor = Color.Black,
                TextColor = App.appContentColour,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start
            };
            #endregion

            //Tap registers for all the Link labels
            # region LinkGestures
            link1a.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>  Device.OpenUri(new Uri("http://www.ogh.qut.edu.au/")))
            });
            link1b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.govhouse.qld.gov.au/the-governor-of-queensland/about-the-governor.aspx")))
            });
            link1c.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.ogh.qut.edu.au/history/the_house/")))
            });
            link2a.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.parliament.qld.gov.au/")))
            });
            link2b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.ogh.qut.edu.au/about/history/political/keyevents.jsp")))
            });
        
            link2c.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.parliament.qld.gov.au/explore/education/factsheets/6")))
            });
            
            link2d.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("https://www.youtube.com/watch?v=IVUItgaQALM")))
            });
            link4a.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.innsofcourt.com.au/")))
            });
            link4b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("https://www.qldbar.asn.au/index.php/about-the-bar/what-is-the-bar3")))
            });
            link5a.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("www.hca.gov.au")))
            });
            link5b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("www.fedcourt.gov.au")))
            });
            link5c.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("www.familycourt.gov.au")))
            });
            link5d.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.hcourt.gov.au/registry/court-calendars")))
            });
            link5e.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.hcourt.gov.au/justices/about-the-justices")))
            });
            link5f.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www5.austlii.edu.au/au/legis/cth/consol_act/hcoaa1979233/s7.html")))
            });
            link6a.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.courts.qld.gov.au/courts/magistrates-court")))
            });
            link6b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.qld.gov.au/law/court/going-to-court/courtroom-rules/")))
            });
            link7a.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://archive.sclqld.org.au/judgepub/dj260401.pdf")))
            });
            link7b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.courts.qld.gov.au/courts/district-court")))
            });
            link7c.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Device.OpenUri(new Uri("http://www.courts.qld.gov.au/courts/supreme-court")))
            });
            #endregion

            //StackLayout for all the text of the links
            StackLayout LinksContent = new StackLayout
            {
                Padding = new Thickness(3, 0, 4, 0),
                BackgroundColor = Color.Black
            };

            //Adds links depending on current task being conducted
            if (App.currentActivity == 1)
            {
                LinksContent.Children.Add(Activity1);
                LinksContent.Children.Add(link1a);
                LinksContent.Children.Add(link1b);
                LinksContent.Children.Add(link1c);
                LinksContent.Children.Add(link1d);
            }
            else if (App.currentActivity == 2)
            {
                LinksContent.Children.Add(Activity2);
                LinksContent.Children.Add(link2a);
                LinksContent.Children.Add(link2b);
                LinksContent.Children.Add(link2c);
                LinksContent.Children.Add(link2d);
                LinksContent.Children.Add(link2e);
            }
            else if (App.currentActivity == 3)
            {
                LinksContent.Children.Add(Activity3);
                LinksContent.Children.Add(link3a);
            }
            else if (App.currentActivity == 4)
            {
                LinksContent.Children.Add(Activity4);
                LinksContent.Children.Add(link4a);
                LinksContent.Children.Add(link4b);
            }
            else if (App.currentActivity == 5)
            {
                LinksContent.Children.Add(Activity5);
                LinksContent.Children.Add(link5a);
                LinksContent.Children.Add(link5b);
                LinksContent.Children.Add(link5c);
                LinksContent.Children.Add(link5d);
                LinksContent.Children.Add(link5e);
                LinksContent.Children.Add(link5f);
            }
            else if (App.currentActivity == 6)
            {
                LinksContent.Children.Add(Activity6);
                LinksContent.Children.Add(link6a);
                LinksContent.Children.Add(link6b);
                LinksContent.Children.Add(link6c);
            }
            else if (App.currentActivity == 7)
            {
                LinksContent.Children.Add(Activity7);
                LinksContent.Children.Add(link7a);
                LinksContent.Children.Add(link7b);
                LinksContent.Children.Add(link7c);
                LinksContent.Children.Add(link7d);
            }
            else { 
                LinksContent.Children.Add(error); 
            }

            Label backLbl = new Label
            {
                Text = "<",
                BackgroundColor = App.QUTBlue,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 4,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            backLbl.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => goBack())
            });

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
                    new RowDefinition {Height = App.screenHeight / 1.213},
                    new RowDefinition {Height = 0}
                },
                ColumnDefinitions = 
                {
                    new ColumnDefinition {Width = 0},
                    new ColumnDefinition {Width = App.screenWidth / 10},
                    new ColumnDefinition {Width = App.screenWidth / 5 - 32},
                    new ColumnDefinition {Width = App.screenWidth / 5},
                    new ColumnDefinition {Width = App.screenWidth / 5},
                    new ColumnDefinition {Width = App.screenWidth / 5},
                    new ColumnDefinition {Width = App.screenWidth / 10},
                    new ColumnDefinition {Width = 0}
                }
            };

            //scrollview for the question editor
            ScrollView LinksText = new ScrollView
            {
                IsClippedToBounds = true,
                Content = LinksContent
            };

            //adding each element to the grid
            pageGrid.Children.Add(backLbl, 1, 1); //back button
            pageGrid.Children.Add(logoLayout, 2, 7, 1, 2); //qut logo
            pageGrid.Children.Add(LinksText, 1, 7, 2, 3);//scrolling text

            StackLayout innerContent = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            innerContent.Children.Add(pageGrid);
          
            //making the pages content the stacklayout with the grid
            this.Content = innerContent;
            //add padding to account for the iOS status bar
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.BackgroundImage = "background.png";
            }

            /* simple function to go back to the activity
             * home screen when back button is clicked
             */ 
            private void goBack()
            {
                App.Current.MainPage = new MainActivityScreen();
            }
      
        
    }
}
