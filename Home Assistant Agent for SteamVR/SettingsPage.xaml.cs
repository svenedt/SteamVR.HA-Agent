using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Home_Assistant_Agent_for_SteamVR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            PortBox.Text = Settings.Default.Port.ToString();
            LaunchMinimizedToggle.IsOn = Settings.Default.LaunchMinimized;
            EnableTrayToggle.IsOn = Settings.Default.EnableTray;
            ExitWithSteamVRToggle.IsOn = Settings.Default.ExitWithSteamVR;
            AlwaysOnTopToggle.IsOn = Settings.Default.AlwaysOnTop;
        }

        private void PortBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(SavePortButton);
                if (peer.GetPattern(PatternInterface.Invoke) is IInvokeProvider invokeProv) invokeProv.Invoke();
                VisualStateManager.GoToState(SavePortButton, "Pressed", true);
            } else
            {
                VisualStateManager.GoToState(SavePortButton, "Normal", true);
                SavePortButton.Content = "Save";
            }
        }

        private void LaunchMinimized_Toggled(object sender, RoutedEventArgs e)
        {
            Settings.Default.LaunchMinimized = LaunchMinimizedToggle.IsOn;
            Settings.Default.Save();
        }

        private void EnableTray_Toggled(object sender, RoutedEventArgs e)
        {
            Settings.Default.EnableTray = EnableTrayToggle.IsOn;
            Settings.Default.Save();
            (Application.Current as App)?.MWindow.SetTrayIconVisibility(Settings.Default.EnableTray);
        }
        
        private void AlwaysOnTop_Toggled(object sender, RoutedEventArgs e)
        {
            Settings.Default.AlwaysOnTop = AlwaysOnTopToggle.IsOn;
            Settings.Default.Save();
            (Application.Current as App)?.MWindow.SetIsAlwaysOnTop(Settings.Default.AlwaysOnTop);
        }

        private void ExitWithSteamVR_Toggled(object sender, RoutedEventArgs e)
        {
            Settings.Default.ExitWithSteamVR = ExitWithSteamVRToggle.IsOn;
            Settings.Default.Save();
        }

        private void SavePortButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Port = int.Parse(PortBox.Text);
            Settings.Default.Save();
            (Application.Current as App)?.MWindow.SetWebsocketPort(Settings.Default.Port);
            SavePortButton.Content = "Saved!";
        }

    }
}
