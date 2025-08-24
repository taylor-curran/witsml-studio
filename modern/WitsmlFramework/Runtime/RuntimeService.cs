using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace PDS.WITSMLstudio.Desktop.Core.Runtime
{
    public class RuntimeService : IRuntimeService
    {
        public string DataFolderPath { get; private set; }
        public string OutputFolderPath { get; set; } = string.Empty;
        public object Container { get; } = new object();
        public object Shell { get; } = new object();

        public RuntimeService()
        {
            DataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WitsmlStudio");
            OutputFolderPath = Path.Combine(DataFolderPath, "Output");
            EnsureDataFolder();
        }

        public void Invoke(Action action)
        {
            if (Application.Current?.Dispatcher != null)
            {
                Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public async Task InvokeAsync(Func<Task> action)
        {
            if (Application.Current?.Dispatcher != null)
            {
                await Application.Current.Dispatcher.InvokeAsync(async () => await action());
            }
            else
            {
                await action();
            }
        }

        public bool ShowDialog(object viewModel)
        {
            return false;
        }

        public bool ShowConfirm(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowBusy(bool isBusy = true)
        {
        }

        public void EnsureDataFolder()
        {
            if (!Directory.Exists(DataFolderPath))
            {
                Directory.CreateDirectory(DataFolderPath);
            }
        }
    }
}
