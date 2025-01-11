using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.IO;

namespace AutoCompressor
{
    public partial class MainWindow : Window
    {
        private AppConfig _config;

        public MainWindow()
        {
            InitializeComponent();
            Logs.onLogAdded += AddLog;
        }

       
        public void AddLog(string log)
        {
            
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                LogsText.Text += log + Environment.NewLine;
            });

        }

        public void Initialize(AppConfig config)
        {
            this.Width = 500;
            this.Height = 500;

            _config = config;

            if (!string.IsNullOrEmpty(_config.inputFolder)) EntryInputFolder.Text = _config.inputFolder;
            if (!string.IsNullOrEmpty(_config.outputFolder)) EntryOutputFolder.Text = _config.outputFolder;
        }

        private async void OnPickInputFolderClicked(object? sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog()
            {
                Title = "Select Input Folder"
            };

            var result = await dialog.ShowAsync(this);

            if (!string.IsNullOrEmpty(result))
            {
                this.FindControl<TextBox>("EntryInputFolder").Text = result;
            }
        }

        private async void OnPickOutputFolderClicked(object? sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog()
            {
                Title = "Select Output Folder"
            };

            var result = await dialog.ShowAsync(this);

            if (!string.IsNullOrEmpty(result))
            {
                this.FindControl<TextBox>("EntryOutputFolder").Text = result;
            }
        }

        private void OnChangeFoldersClicked(object? sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryInputFolder.Text) || string.IsNullOrEmpty(EntryOutputFolder.Text))
            {
                lblStatus.Text = "Error: Please fill the fields above.";
                lblStatus.Foreground = Avalonia.Media.Brushes.Red;
                lblStatus.IsVisible = true;
                return;
            }

            _config.inputFolder = EntryInputFolder.Text.Trim();   
            _config.outputFolder = EntryOutputFolder.Text.Trim(); 

            _config.Save();                

            lblStatus.Text = "Settings have been saved.";
            lblStatus.Foreground = Avalonia.Media.Brushes.Green;
            lblStatus.IsVisible = true;
        }

        private void OnChangeTab(object? sender, RoutedEventArgs e)
        {

            LogsTab.IsVisible = !LogsTab.IsVisible;
            SettingsTab.IsVisible = !SettingsTab.IsVisible;

        }
    }
}
