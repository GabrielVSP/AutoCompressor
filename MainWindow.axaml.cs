using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AutoCompressor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnChangeFoldersClicked(object? sender, RoutedEventArgs e)
        {
            // Obtenha os valores dos TextBoxes
            var inputFolder = this.FindControl<TextBox>("EntryInputFolder").Text;
            var outputFolder = this.FindControl<TextBox>("EntryOutputFolder").Text;

            // Validação ou processamento (exemplo)
            if (string.IsNullOrWhiteSpace(inputFolder) || string.IsNullOrWhiteSpace(outputFolder))
            {
                this.FindControl<TextBlock>("lblStatus").Text = "Please fill in both folders.";
                this.FindControl<TextBlock>("lblStatus").Foreground = Avalonia.Media.Brushes.Red;
            }
            else
            {
                this.FindControl<TextBlock>("lblStatus").Text = "Folders saved successfully!";
                this.FindControl<TextBlock>("lblStatus").Foreground = Avalonia.Media.Brushes.Green;

                // Aqui você pode salvar as pastas na configuração
                var config = new AppConfig
                {
                    InputFolder = inputFolder,
                    OutputFolder = outputFolder
                };
                config.Save();
            }
        }
    }
}
