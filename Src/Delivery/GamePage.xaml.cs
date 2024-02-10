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
using Windows.UI.Xaml.Navigation;


namespace GameManager
{
    public sealed partial class GamePage : Page
    {
        public Game1 _game;
        //public /*static*/ Game1 game = new Game1();
       
        public GamePage()
        {
            this.InitializeComponent();

			// Create the game.
			var launchArguments = string.Empty;

            //RnD
            _game = MonoGame.Framework.XamlGame<Game1>.Create(
                launchArguments, 
                Window.Current.CoreWindow, 
                swapChainPanel);

            //RnD
            //dirt hack
            //Game1.game = this._game;
        }
    }
}
