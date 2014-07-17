using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPBackgroundAgentApp.Resources;
using Microsoft.Phone.Scheduler;
using Quotes.DataAccess;

namespace WPBackgroundAgentApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            RegisterAgent();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void RegisterAgent()
        {
            string taskName = "RandomQuoteTask";
            try
            {

                if (ScheduledActionService.Find(taskName) != null)
                {
                    //if the agent exists, remove and then add it to ensure
                    //the agent's schedule is updated to avoid expiration
                    ScheduledActionService.Remove(taskName);
                }

                PeriodicTask periodicTask = new PeriodicTask(taskName);
                periodicTask.Description = "Random Quote Update On Tile";
                ScheduledActionService.Add(periodicTask);

#if DEBUG
                ScheduledActionService.LaunchForTest(taskName, TimeSpan.FromSeconds(30));
#endif
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (SchedulerServiceException schedulerException)
            {
                MessageBox.Show(schedulerException.Message);
            }
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            QuotesRepository repository = new QuotesRepository();
            txtQuote.Text = repository.GetRandomQuote();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}