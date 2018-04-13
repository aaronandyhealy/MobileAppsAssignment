using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static WeatherApp.App.WeatherMap;



namespace WeatherApp
{
  
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
      
     
        //On button click to get weather
        private async void Button_Click(Object sender, RoutedEventArgs e)
        {
            //Call LocationManager Class Get Position Function
            var position = await LocationManager.GetPosition();

            //Get results depending on current latitude and longitude
            RootObject myWeather = await App.WeatherMap.GetWeather(position.Coordinate.Latitude, position.Coordinate.Longitude);
            String icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            LocationTextBlock.Text = "Location: " + myWeather.name;
            TempTextBlock.Text = "Temperature: " + ((int)(myWeather.main.temp - 273.15)).ToString() + "c";
            HumidityTextBlock.Text = "Humidity: " + ((int)(myWeather.main.humidity)).ToString();
            WindTextBlock.Text = "Wind Speed: " + ((int)(myWeather.wind.speed * 3.6)).ToString() + "km/h";
            WeatherTextBlock.Text = "Weather: " + myWeather.weather[0].description;

        }




    }
}
