using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Quotes.DataAccess;
using Microsoft.Phone.Shell;
using System.Linq;
using System;

namespace QuotesTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background
            if (task is PeriodicTask)
            {
                QuotesRepository repository = new QuotesRepository();
                string quotes = repository.GetRandomQuote();
                
                FlipTileData flipTileData = new FlipTileData()
                {
                    BackContent = quotes,
                    WideBackContent = quotes

                };

                ShellTile appTile = ShellTile.ActiveTiles.First();
                if (appTile != null)
                {
                    appTile.Update(flipTileData);
                }
            }

            #if DEBUG
              ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
            #endif


            NotifyComplete();
        }
    }
}