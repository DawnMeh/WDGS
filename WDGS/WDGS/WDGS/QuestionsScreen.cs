using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XPA_PickMedia_XLabs_XFP;

namespace WDGS
{
    /*
     * A public class extending the editor Xamarin.Forms element
     */ 
    public class MyEditor : Editor
    {
        public int answerID;

        private int charLimit = 1000;

        /*
         * Class constructer
         * 
         * Instantiates an Editor object using
         * the parent constructor and some default
         * attributes
         */ 
        public MyEditor(int answerID) : base() {
            this.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Editor));
            this.TextColor = App.appContentColour;
            this.Keyboard = Keyboard.Default;
            this.BackgroundColor = Color.Black;
            this.answerID = answerID;

            this.TextChanged += (s, e) => {
                string text = this.Text;

                if (text.Length > charLimit)
                {
                    text = text.Remove(text.Length - 1);
                    this.Text = text;
                    this.Unfocus();
                }
                this.InvalidateMeasure();
            };

            this.Focused += (s, e) => {
                QuestionsScreen.answerFocused(this);
            };

            this.Unfocused += (s, e) => {
                QuestionsScreen.insertAnswerToDB(this);
            };
        } 
    }

    public class QuestionLabel : Label
    {
        /*
         * Class constructer
         * 
         * Instantiates a Label object using
         * the parent constructor and some default
         * attributes
         */ 
        public QuestionLabel() : base() {
            this.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
            this.TextColor = Color.White;
            this.BackgroundColor = Color.Black;
        }
    }

    public class QuestionsScreen : ContentPage
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

        //Question Sets are:
        // 1 - Questions
        // 2 - Trivia
        // 3 - Tasks
        static private int currentQuestionSet = 3;
        private Camera cameraOps = null;
        private Label questionTypeLabel;

        //stacklayout to host the scrollview with the activity content
        //this is necessary because without it the scrollview
        //will override the grid layout and flow out of the grid
        private StackLayout questionContent = new StackLayout {
            Padding = new Thickness(3, 0, 4, 0),
            BackgroundColor = Color.Black
        };

        //scrollview to host each stack layout for each activity
        //i.e. activity 1 questions/answers, activity 2 questions/answers
        private ScrollView activitySetContent = new ScrollView
        {
            IsClippedToBounds = true
        };
        
        //activity layout to host all the question content and to then
        //be placed inside the acitivity set content scrollview
        private StackLayout activityLayout = new StackLayout 
        {
            BackgroundColor = Color.Black
        };

        //labels containing the questions, trivia, and tasks
        //for each of the activities
        #region activity questions

        //activity 1 question set
        private QuestionLabel act1quest1 = new QuestionLabel
        {
            Text = "1. Eleven Queensland governors have lived and worked at Old Government House. What are the duties and responsibilities of the Governor?"
        };
        private QuestionLabel act1quest2 = new QuestionLabel
        {
            Text = "2. The land now referred to as Gardens Point is part of the traditional country of which people?"
        };
        private QuestionLabel act1quest3 = new QuestionLabel
        {
            Text = "3. What was the traditional name for Gardens Point? What was the official Government policy with respect to Indigenous Australians in the late nineteenth century?"
        };

        //activity 2 question set
        private QuestionLabel act2quest1 = new QuestionLabel
        {
            Text = "Scroll down to see all questions\n\n1. In what year was the Queensland Parliament founded?"
        };
        private QuestionLabel act2quest2 = new QuestionLabel
        {
            Text = "2. What Coat of Arms is displayed on the plaque at the entrance? What do the various components of the Coat of Arms represent?"
        };
        private QuestionLabel act2quest3 = new QuestionLabel
        {
            Text = "3. The Queensland Parliament passed legislation in 1897 that significantly restricted the rights of Indigenous Australians. What was the name of this Act of Parliament?"
        };
        private QuestionLabel act2quest4 = new QuestionLabel
        {
            Text = "4. What is a referendum?"
        };
        private QuestionLabel act2quest5 = new QuestionLabel
        {
            Text = "5. What used to be located at the site (close to the Parliament House entrance) on which a statue of Queen Elizabeth II now stands? What are two interesting facts about the former building?"
        };

        //activity 3 question set
        private QuestionLabel act3quest1 = new QuestionLabel
        {
            Text = "1. What is the Executive Council? Who are its members?"
        };
        private QuestionLabel act3quest2 = new QuestionLabel
        {
            Text = "2. Who opened the Executive Building in 1971? What is an interesting fact about this person?"
        };
        private QuestionLabel act3quest3 = new QuestionLabel
        {
            Text = "3. Who are the current tenants of the former Executive building?"
        };
        private QuestionLabel act3quest4 = new QuestionLabel
        {
            Text = "4. Locate a marble tablet set into the George Street entrance of the former Executive Building. Who inscribed the message on this tablet? For what purpose was the message inscribed?"
        };

        //activity 4 question set
        private QuestionLabel act4quest1 = new QuestionLabel
        {
            Text = "1. Who officially opened the current Inns of Court building in 1986?"
        };
        private QuestionLabel act4quest2 = new QuestionLabel
        {
            Text = "2. What is the title of the sculpture in the Inns of Court foyer?"
        };
        private QuestionLabel act4quest3 = new QuestionLabel
        {
            Text = "3. What is the sculptor’s connection to the Queensland Bar?"
        };
        private QuestionLabel act4quest4 = new QuestionLabel
        {
            Text = "4. What is a barrister?"
        };
        private QuestionLabel act4quest5 = new QuestionLabel
        {
            Text = "5. What is a solicitor?"
        };

        //activity 5 question set
        private QuestionLabel act5quest1 = new QuestionLabel
        {
            Text = "1. Where does the High Court sit when in Brisbane?"
        };
        private QuestionLabel act5quest2 = new QuestionLabel
        {
            Text = "2. Who are the current High Court justices?"
        };
        private QuestionLabel act5quest3 = new QuestionLabel
        {
            Text = "3. Are any of the current justices of the High Court from Queensland?"
        };
        private QuestionLabel act5quest4 = new QuestionLabel
        {
            Text = "4. What are the qualifications required of a High Court justice?"
        };
        private QuestionLabel act5quest5 = new QuestionLabel
        {
            Text = "5. There are two plaques outside the building entrance: what does each of the plaques commemorate?"
        };

        //activity 6 question set
        private QuestionLabel act6quest1 = new QuestionLabel
        {
            Text = "1. What types of matters may be heard in the Magistrates Court? Provide 3 examples of a matter that may come before the Magistrates Court"
        };
        private QuestionLabel act6quest2 = new QuestionLabel
        {
            Text = "2. Where was the Brisbane Magistrates Court previously located?"
        };

        //activity 7 question set
        private QuestionLabel act7quest1 = new QuestionLabel
        {
            Text = "Scroll down to see all questions\n\n1. You’ll find an extract of legislation on display near Themis. What is the citation to the Act?"
        };
        private QuestionLabel act7quest2 = new QuestionLabel
        {
            Text = "2. Where was the sculpture of Themis made?"
        };
        private QuestionLabel act7quest3 = new QuestionLabel
        {
            Text = "3. Read through the linked speech by the Honourable Paul De Jersey, former Queensland Chief Justice and current Queensland Governor. According to the former Chief Justice, what are the ‘themes’ of Themis and what values must Australians strive to preserve?"
        };
        private QuestionLabel act7quest4 = new QuestionLabel
        {
            Text = "4. What types of matters may be heard in the District Court?"
        };
        private QuestionLabel act7quest5 = new QuestionLabel
        {
            Text = "5. What types of matters may be heard in the Supreme Court?"
        };

        #endregion

        #region activity trivia questions

        //activity 1 trivia set
        private QuestionLabel act1Trivia1 = new QuestionLabel
        {
            Text = "1. Why is the statue of Lady Diamantina positioned on the western side of Old Government House?"
        };
        private QuestionLabel act1Trivia2 = new QuestionLabel
        {
            Text = "2. What long-standing feud between New Zealand and Australia may be settled by looking at the history of Old Government House and the influence of one of its famous residents?\nHint: See Old Government House website (link provided in activities relevant links)"
        };

        //activity 2 trivia set
        private QuestionLabel act2Trivia1 = new QuestionLabel
        {
            Text = "1. Can you find another building of historical note nearby and what is the significance of this building? Take a photo of the building or a sign identifying the building"
        };

        //activity 3 trivia set
        private QuestionLabel act3Trivia1 = new QuestionLabel
        {
            Text = "1. The Queensland Government Printing Office (GoPrint) was established in 1862 to coordinate and regulate information concerning the Queensland Parliament. GoPrint’s functions included supporting the workings of Parliament and ensuring there was a permanent record of Parliament, legislation and government declarations. The GoPrint office was formerly located on George Street. Today, where can you find the Government Printing Office?"
        };
        private QuestionLabel act3Trivia2 = new QuestionLabel
        {
            Text = "2. Making your way down George Street, where was the District Court and the Supreme Court located prior to construction of the QEII Courts complex?"
        };

        //activity 4 trivia set
        private QuestionLabel act4Trivia1 = new QuestionLabel
        {
            Text = "1. What does it mean for a barrister to ‘take silk’?"
        };

        //activity 5 trivia set
        private QuestionLabel act5Trivia1 = new QuestionLabel
        {
            Text = "1. Who was the first Chief Justice of Australia? What other important roles did he play in Australia’s political history?"
        };

        //activity 6 trivia set
        private QuestionLabel act6Trivia1 = new QuestionLabel
        {
            Text = "1. What do the waves at the right-hand side of the Magistrates’ Court entrance represent/symbolise?"
        };

        //activity 7 trivia set
        private QuestionLabel act7Trivia1 = new QuestionLabel
        {
            Text = "1. Why isn’t Themis blindfolded?"
        };

        #endregion

        #region activity task questions

        //activity 1 task set
        private QuestionLabel act1Task1 = new QuestionLabel
        {
            Text = "Take a picture of, or with:\n\n1. The statue of Lady Diamantina Bowen.\n"
        };
        private QuestionLabel act1Task2 = new QuestionLabel
        {
            Text = "2. The shield on the wall at the entrance to OGH and make a note of who opened the building after its most recent renovations."
        };

        //activity 2 task set
        private QuestionLabel act2Task1 = new QuestionLabel
        {
            Text = "1. Take a picture of the Parliament House Foundation plaque\n"
        };
        private QuestionLabel act2Task2 = new QuestionLabel
        {
            Text = "2. Locate a statue of Queen Elizabeth II across the road along George Street and take a picture/selfie of, or with the statue"
        };

        //activity 3 task set
        private QuestionLabel act3Task1 = new QuestionLabel
        {
            Text = "1. At the current Executive Building, take a picture of the plaque at the front of the building\n"
        };
        private QuestionLabel act3Task2 = new QuestionLabel
        {
            Text = "2. Take a photo of the statue of a Queen and a judge on/or in the vicinity of the old building and make note of who the statues represent"
        };
        private QuestionLabel act3Task3 = new QuestionLabel
        {
            Text = "3. Take a picture of the old courtyard entrance"
        };

        //activity 4 task set
        private QuestionLabel act4Task1 = new QuestionLabel
        {
            Text = "1. Take a picture of the statue inside the Inns of Court foyer or a picture/selfie of, or with the plaque at the entrance of Inns of Court\n"
        };
        private QuestionLabel act4Task2 = new QuestionLabel
        {
            Text = "2. Find a building in the Central Business District (other than Inns of Court), that houses barristers’ chambers and make note of where it is."
        };

        //activity 5 task set
        private QuestionLabel act5Task1 = new QuestionLabel
        {
            Text = "1. Take a picture of one of the plaques outside the Commonwealth Law Courts\n"
        };
        private QuestionLabel act5Task2 = new QuestionLabel
        {
            Text = "2. Take a picture at the Harry Gibbs Commonwealth Law Courts sign"
        };

        //activity 6 task set
        private QuestionLabel act6Task1 = new QuestionLabel
        {
            Text = "1. Take a photo at the entrance to the Courts complex"
        };

        //activity 7 task set
        private QuestionLabel act7Task1 = new QuestionLabel
        {
            Text = "Scroll down to see all questions\n\n1. Locate Themis and take a picture/selfie of, or with her\n"
        };
        private QuestionLabel act7Task2 = new QuestionLabel
        {
            Text = "2. Find an extract of legislation on display on George Street (near Themis) and take a picture\n"
        };
        private QuestionLabel act7Task3 = new QuestionLabel
        {
            Text = "3. Provide a quote from Sir Harry Gibbs inscribed near the statue of Themis"
        };
        private QuestionLabel act7Task4 = new QuestionLabel
        {
            Text = "4. You might like to enter the Courts Complex through security, provided you are suitably dressed. Walk towards the entrance of the QEII Courts Complex. After you pass through security, walk to the right and enter the 'Sir Harry Gibbs Legal Heritage Centre', or sit in on a case if you have time."
        };

        #endregion

        //editors for getting answers from the user
        //for each question asked
        #region activity questions answers

        //activity 1 question set answers
        private MyEditor act1quest1ans = new MyEditor(1) { };
        private MyEditor act1quest2ans = new MyEditor(2) { };
        private MyEditor act1quest3ans = new MyEditor(3) { };
        
        //activity 2 question set answers     
        private MyEditor act2quest1ans = new MyEditor(1) { };
        private MyEditor act2quest2ans = new MyEditor(2) { };
        private MyEditor act2quest3ans = new MyEditor(3) { };
        private MyEditor act2quest4ans = new MyEditor(4) { };
        private MyEditor act2quest5ans = new MyEditor(5) { };
        
        //activity 3 question set answers    
        private MyEditor act3quest1ans = new MyEditor(1) { };
        private MyEditor act3quest2ans = new MyEditor(2) { };
        private MyEditor act3quest3ans = new MyEditor(3) { };
        private MyEditor act3quest4ans = new MyEditor(4) { };
        
        //activity 4 question set answers     
        private MyEditor act4quest1ans = new MyEditor(1) { };
        private MyEditor act4quest2ans = new MyEditor(2) { };
        private MyEditor act4quest3ans = new MyEditor(3) { };
        private MyEditor act4quest4ans = new MyEditor(4) { };
        private MyEditor act4quest5ans = new MyEditor(5) { };
        
        //activity 5 question set answers     
        private MyEditor act5quest1ans = new MyEditor(1) { };
        private MyEditor act5quest2ans = new MyEditor(2) { };
        private MyEditor act5quest3ans = new MyEditor(3) { };
        private MyEditor act5quest4ans = new MyEditor(4) { };
        private MyEditor act5quest5ans = new MyEditor(5) { };
        
        //activity 6 question set answers     
        private MyEditor act6quest1ans = new MyEditor(1) { };
        private MyEditor act6quest2ans = new MyEditor(2) { };
        
        //activity 7 question set answers     
        private MyEditor act7quest1ans = new MyEditor(1) { };
        private MyEditor act7quest2ans = new MyEditor(2) { };
        private MyEditor act7quest3ans = new MyEditor(3) { };
        private MyEditor act7quest4ans = new MyEditor(4) { };
        private MyEditor act7quest5ans = new MyEditor(5) { };

        #endregion

        #region activity trivia answers

        //activity 1 trivia set answers   
        private MyEditor act1Trivia1ans = new MyEditor(1) { };
        private MyEditor act1Trivia2ans = new MyEditor(2) { };

        //activity 2 trivia set answers    
        private MyEditor act2Trivia1ans = new MyEditor(1) { };

        //activity 3 trivia set answers        
        private MyEditor act3Trivia1ans = new MyEditor(1) { };
        private MyEditor act3Trivia2ans = new MyEditor(2) { };

        //activity 4 trivia set answers    
        private MyEditor act4Trivia1ans = new MyEditor(1) { };

        //activity 5 trivia set answers        
        private MyEditor act5Trivia1ans = new MyEditor(1) { };

        //activity 6 trivia set answers   
        private MyEditor act6Trivia1ans = new MyEditor(1) { };

        //activity 7 trivia set answers        
        private MyEditor act7Trivia1ans = new MyEditor(1) { };

        #endregion

        #region activity task answers

        //activity 1 task set answers
        private MyEditor act1Task2ans = new MyEditor(2) { };
        
        //activity 3 task set answers
        private MyEditor act3Task2ans = new MyEditor(2) { };
        
        //activity 4 task set answers
        private MyEditor act4Task2ans = new MyEditor(2) { };
        
        //activity 7 task set answers
        private MyEditor act7Task3ans = new MyEditor(3) { };

        #endregion

        public QuestionsScreen()
        {
            //instantiates the camera class for use
            //to take images
            cameraOps = new Camera();

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

            StackLayout questionIconLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 2, 0, 0)
            };

            StackLayout triviaIconLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 2, 0, 0)
            };

            StackLayout taskIconLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 2, 0, 0)
            };

            StackLayout cameraIconLayout = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 2, 0, 0)
            };

            #endregion

            //creating each of the image icons for
            //the grid with their tap commands to load
            //correct file
            #region imageIcons and gesture commands

            Image logo = new Image
            {
                Source = "QutLogoHeader.png",
                BackgroundColor = App.QUTBlue,
                HeightRequest = (App.screenHeight / 10) - 4,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Image cameraIcon = new Image
            {
                Source = "cameraIcon.png",
                HeightRequest = (App.screenHeight / 12) - 4,
                WidthRequest = 30
            };

            Image questionIcon = new Image
            {
                Source = "questionIcon.png",
                HeightRequest = (App.screenHeight / 12) - 4,
                WidthRequest = 30
            };

            Image triviaIcon = new Image
            {
                Source = "triviaIcon.png",
                HeightRequest = (App.screenHeight / 12) - 4,
                WidthRequest = 30
            };

            Image taskIcon = new Image
            {
                Source = "taskIcon.png",
                HeightRequest = (App.screenHeight / 12) - 4,
                WidthRequest = 30
            };

            questionIcon.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => changeQuestions(1))
            });

            triviaIcon.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => changeQuestions(2))
            });

            taskIcon.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => changeQuestions(3)),
            });

            cameraIcon.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => takePicture()),
            });

            #endregion

            questionTypeLabel = new Label
            {
                Text = "Activity Tasks",
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            questionContent.Children.Add(questionTypeLabel);

            //adding each of the image icons to the relevant layout
            questionIconLayout.Children.Add(questionIcon);
            triviaIconLayout.Children.Add(triviaIcon);
            taskIconLayout.Children.Add(taskIcon);
            cameraIconLayout.Children.Add(cameraIcon);
            logoLayout.Children.Add(logo);

            //creating a new 5x8 grid
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
                    new RowDefinition {Height = App.screenHeight / 1.35},
                    new RowDefinition {Height = App.screenHeight / 12},
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

            Label nextLbl = new Label
            {
                Text = ">",
                BackgroundColor = Color.Black,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 4,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            nextLbl.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => goToNextActivity()),
            });

            loadContentIntoStackLayout();
            questionContent.Children.Add(activitySetContent);

            //adding each element to the grid
            pageGrid.Children.Add(backLbl, 1, 1); //back button

            pageGrid.Children.Add(logoLayout, 2, 7, 1, 2); //qut logo

            pageGrid.Children.Add(questionContent, 1, 7, 2, 3); //stacklayout hosting the content scrollview

            pageGrid.Children.Add(questionIconLayout, 1, 3, 3, 4); //questions button

            pageGrid.Children.Add(triviaIconLayout, 3, 3); //trivia button

            pageGrid.Children.Add(taskIconLayout, 4, 3); //tasks button

            pageGrid.Children.Add(cameraIconLayout, 5, 3); //camera button

            pageGrid.Children.Add(nextLbl, 6, 3); //next activity button

            //creating a stacklayout and adding the grid to it
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

        /* 
         * simple function to move to the next activity
         * when the next button is clicked
         */
        private void goToNextActivity()
        {
            currentQuestionSet = 3;

            if (App.currentActivity == 7)
            {
                App.Current.MainPage = new CongratulationsScreen();
                return;
            }

            App.currentActivity += 1;
            App.Current.MainPage = new MainActivityScreen();	
        }

        /* simple function to go back to the activity
         * home screen when back button is clicked
         */ 
        private void goBack()
        {
            currentQuestionSet = 3;
            App.Current.MainPage = new MainActivityScreen();
        }

        private void loadContentIntoStackLayout()
        {
            if (activityLayout.Children.Count > 0)
            {
                activityLayout.Children.Clear();
            }

            loadAnswers();

            #region if and switch statements to load content into stacklayout

            if (App.currentActivity == 1)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act1quest1);
                        activityLayout.Children.Add(act1quest1ans);
                        activityLayout.Children.Add(act1quest2);
                        activityLayout.Children.Add(act1quest2ans);
                        activityLayout.Children.Add(act1quest3);
                        activityLayout.Children.Add(act1quest3ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act1Trivia1);
                        activityLayout.Children.Add(act1Trivia1ans);
                        activityLayout.Children.Add(act1Trivia2);
                        activityLayout.Children.Add(act1Trivia2ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act1Task1);
                        activityLayout.Children.Add(act1Task2);
                        activityLayout.Children.Add(act1Task2ans);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 2)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act2quest1);
                        activityLayout.Children.Add(act2quest1ans);
                        activityLayout.Children.Add(act2quest2);
                        activityLayout.Children.Add(act2quest2ans);
                        activityLayout.Children.Add(act2quest3);
                        activityLayout.Children.Add(act2quest3ans);
                        activityLayout.Children.Add(act2quest4);
                        activityLayout.Children.Add(act2quest4ans);
                        activityLayout.Children.Add(act2quest5);
                        activityLayout.Children.Add(act2quest5ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act2Trivia1);
                        activityLayout.Children.Add(act2Trivia1ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act2Task1);
                        activityLayout.Children.Add(act2Task2);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 3)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act3quest1);
                        activityLayout.Children.Add(act3quest1ans);
                        activityLayout.Children.Add(act3quest2);
                        activityLayout.Children.Add(act3quest2ans);
                        activityLayout.Children.Add(act3quest3);
                        activityLayout.Children.Add(act3quest3ans);
                        activityLayout.Children.Add(act3quest4);
                        activityLayout.Children.Add(act3quest4ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act3Trivia1);
                        activityLayout.Children.Add(act3Trivia1ans);
                        activityLayout.Children.Add(act3Trivia2);
                        activityLayout.Children.Add(act3Trivia2ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act3Task1);
                        activityLayout.Children.Add(act3Task2);
                        activityLayout.Children.Add(act3Task2ans);
                        activityLayout.Children.Add(act3Task3);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 4)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act4quest1);
                        activityLayout.Children.Add(act4quest1ans);
                        activityLayout.Children.Add(act4quest2);
                        activityLayout.Children.Add(act4quest2ans);
                        activityLayout.Children.Add(act4quest3);
                        activityLayout.Children.Add(act4quest3ans);
                        activityLayout.Children.Add(act4quest4);
                        activityLayout.Children.Add(act4quest4ans);
                        activityLayout.Children.Add(act4quest5);
                        activityLayout.Children.Add(act4quest5ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act4Trivia1);
                        activityLayout.Children.Add(act4Trivia1ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act4Task1);
                        activityLayout.Children.Add(act4Task2);
                        activityLayout.Children.Add(act4Task2ans);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 5)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act5quest1);
                        activityLayout.Children.Add(act5quest1ans);
                        activityLayout.Children.Add(act5quest2);
                        activityLayout.Children.Add(act5quest2ans);
                        activityLayout.Children.Add(act5quest3);
                        activityLayout.Children.Add(act5quest3ans);
                        activityLayout.Children.Add(act5quest4);
                        activityLayout.Children.Add(act5quest4ans);
                        activityLayout.Children.Add(act5quest5);
                        activityLayout.Children.Add(act5quest5ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act5Trivia1);
                        activityLayout.Children.Add(act5Trivia1ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act5Task1);
                        activityLayout.Children.Add(act5Task2);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 6)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act6quest1);
                        activityLayout.Children.Add(act6quest1ans);
                        activityLayout.Children.Add(act6quest2);
                        activityLayout.Children.Add(act6quest2ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act6Trivia1);
                        activityLayout.Children.Add(act6Trivia1ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act6Task1);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 7)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        activityLayout.Children.Add(act7quest1);
                        activityLayout.Children.Add(act7quest1ans);
                        activityLayout.Children.Add(act7quest2);
                        activityLayout.Children.Add(act7quest2ans);
                        activityLayout.Children.Add(act7quest3);
                        activityLayout.Children.Add(act7quest3ans);
                        activityLayout.Children.Add(act7quest4);
                        activityLayout.Children.Add(act7quest4ans);
                        activityLayout.Children.Add(act7quest5);
                        activityLayout.Children.Add(act7quest5ans);
                        break;
                    case 2:
                        activityLayout.Children.Add(act7Trivia1);
                        activityLayout.Children.Add(act7Trivia1ans);
                        break;
                    case 3:
                        activityLayout.Children.Add(act7Task1);
                        activityLayout.Children.Add(act7Task2);
                        activityLayout.Children.Add(act7Task3);
                        activityLayout.Children.Add(act7Task3ans);
                        activityLayout.Children.Add(act7Task4);
                        break;
                    default:
                        break;
                }
            }

            #endregion

            activitySetContent.Content = activityLayout;
        }

        private void changeQuestions(int questionSet)
        {
            currentQuestionSet = questionSet;

            if (currentQuestionSet == 1)
            {
                questionTypeLabel.Text = "Activity Questions";
            }
            else if (currentQuestionSet == 2)
            {
                questionTypeLabel.Text = "Activity Trivia";
            }
            else if (currentQuestionSet == 3)
            {
                questionTypeLabel.Text = "Activity Tasks";
            }

            loadContentIntoStackLayout();
        }

        private void loadAnswers()
        {
            #region load answers from database into correct editor for each activity

            if (App.currentActivity == 1)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act1quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(1, 1, 1);
                        act1quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(1, 1, 2);
                        act1quest3ans.Text = App.WDGSDatabase.getAnswerForActivity(1, 1, 3);
                        break;
                    case 2:
                        act1Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(1, 2, 1);
                        act1Trivia2ans.Text = App.WDGSDatabase.getAnswerForActivity(1, 2, 2);
                        break;
                    case 3:
                        act1Task2ans.Text = App.WDGSDatabase.getAnswerForActivity(1, 3, 2);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 2)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act2quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(2, 1, 1);
                        act2quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(2, 1, 2);
                        act2quest3ans.Text = App.WDGSDatabase.getAnswerForActivity(2, 1, 3);
                        act2quest4ans.Text = App.WDGSDatabase.getAnswerForActivity(2, 1, 4);
                        act2quest5ans.Text = App.WDGSDatabase.getAnswerForActivity(2, 1, 5);
                        break;
                    case 2:
                        act2Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(2, 2, 1);
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 3)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act3quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 1, 1);
                        act3quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 1, 2);
                        act3quest3ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 1, 3);
                        act3quest4ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 1, 4);
                        break;                                                     
                    case 2:                                                        
                        act3Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 2, 1);
                        act3Trivia2ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 2, 2);
                        break;                                                     
                    case 3:
                        act3Task2ans.Text = App.WDGSDatabase.getAnswerForActivity(3, 3, 2);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 4)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act4quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 1, 1);
                        act4quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 1, 2);
                        act4quest3ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 1, 3);
                        act4quest4ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 1, 4);
                        act4quest5ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 1, 5);
                        break;
                    case 2:
                        act4Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 2, 1);
                        break;
                    case 3:
                        act4Task2ans.Text = App.WDGSDatabase.getAnswerForActivity(4, 3, 2);
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 5)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act5quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(5, 1, 1);
                        act5quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(5, 1, 2);
                        act5quest3ans.Text = App.WDGSDatabase.getAnswerForActivity(5, 1, 3);
                        act5quest4ans.Text = App.WDGSDatabase.getAnswerForActivity(5, 1, 4);
                        act5quest5ans.Text = App.WDGSDatabase.getAnswerForActivity(5, 1, 5);
                        break;
                    case 2:
                        act5Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(5, 2, 1);
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
            else if (App.currentActivity == 6)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act6quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(6, 1, 1);
                        act6quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(6, 1, 2);
                        break;
                    case 2:
                        act6Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(6, 2, 1);
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }

            }
            else if (App.currentActivity == 7)
            {
                switch (currentQuestionSet)
                {
                    case 1:
                        act7quest1ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 1, 1);
                        act7quest2ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 1, 2);
                        act7quest3ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 1, 3);
                        act7quest4ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 1, 4);
                        act7quest5ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 1, 5);
                        break;
                    case 2:
                        act7Trivia1ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 2, 1);
                        break;
                    case 3:
                        act7Task3ans.Text = App.WDGSDatabase.getAnswerForActivity(7, 3, 3);
                        break;
                    default:
                        break;
                }
            }

            #endregion
        }

        public static void insertAnswerToDB(MyEditor answer)
        {
            if (answer.Text != "")
            {
                App.WDGSDatabase.insertAnswerForActivity(App.currentActivity, currentQuestionSet, answer.answerID, answer.Text);
                return;
            }

            answer.Text = "Click to Answer";
        }

        public static void answerFocused(MyEditor answerBox)
        {
            if (answerBox.Text == "Click to Answer")
            {
                answerBox.Text = "";
            }     
        }

        /*
         * Take a picture using the Xamarin.Forms.XLabs camera class
         */
        private async void takePicture()
        {
            if (App.cameraAccessGranted) {
                //call the take picture function from the XLabs camera class
                var mediaFile = await cameraOps.TakePicture();

                //if image failed for any reason
                if (mediaFile == null)
                {
                    //check if image failed because user cancelled it,
                    //or an error occured when capturing
                    if (cameraOps.Status != "Canceled")
                    {
                        await DisplayAlert("Error", "There was an error taking your photo, please try again", "OK");
                    }              
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        //converting to byte array for saving process
                        mediaFile.Source.CopyTo(memoryStream);

                        //saving the image to the devices gallery
                        if (DependencyService.Get<SaveAndLoadFiles>().saveImageToGallery("image", memoryStream.ToArray()) == "error")
                        {
                            await DisplayAlert("Error", "Your photo failed to save, please take it and try again", "OK");
                        }
                        else
                        {
                            await DisplayAlert("Image Saved", "The photo has been saved to your gallery", "OK");
                        }
                    } 
                }
            }
            else
            { 
                await DisplayAlert("Error", "Camera access denied, please allow it in your settings and try again", "OK");
            }
        }
    }
}
