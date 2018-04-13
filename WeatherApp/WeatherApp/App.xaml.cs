using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
   
    sealed partial class App : Application
    {

       public class WeatherMap
        {
            //Get Weather Function
            public async static Task<RootObject> GetWeather(double lat, double lon)
            {
                var http = new HttpClient();
                var url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=14e0c28ab11464931cdac7c73248d23e", lat, lon);
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(RootObject));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (RootObject)serializer.ReadObject(ms);
                return data;
            }


            [DataContract]
            public class Coord
            {
                [DataMember]
                public double lon { get; set; }

                [DataMember]
                public double lat { get; set; }
            }

            [DataContract]
            public class Weather
            {

                [DataMember]
                public int id { get; set; }


                [DataMember]
                public string main { get; set; }


                [DataMember]
                public string description { get; set; }


                [DataMember]
                public string icon { get; set; }
            }

            [DataContract]
            public class Main
            {

                [DataMember]
                public double temp { get; set; }

                [DataMember]
                public int humidity { get; set; }
              
            }

            [DataContract]
            public class Wind
            {

                [DataMember]
                public double speed { get; set; }

            }

            [DataContract]
            public class Clouds
            {
                [DataMember]
                public int all { get; set; }
            }

            [DataContract]
            public class Sys
            {

                [DataMember]
                public double message { get; set; }

                [DataMember]
                public string country { get; set; }

            }

            [DataContract]
            public class RootObject
            {

                [DataMember]
                public Coord coord { get; set; }

                [DataMember]
                public List<Weather> weather { get; set; }

                [DataMember]
                public string @base { get; set; }

                [DataMember]
                public Main main { get; set; }

                [DataMember]
                public Wind wind { get; set; }

                [DataMember]
                public Clouds clouds { get; set; }

                [DataMember]
                public int dt { get; set; }

                [DataMember]
                public Sys sys { get; set; }

                [DataMember]
                public int id { get; set; }

                [DataMember]
                public string name { get; set; }

                [DataMember]
                public int cod { get; set; }
            }

        }
       

        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
