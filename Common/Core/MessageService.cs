using System.Windows;


namespace Common.Core
{
    public class MessageService
    {
        public MessageService()
        {

        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public void ShowInfoMessage(string infoMessage)
        {
            MessageBox.Show(infoMessage, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void ShowWarningMessage(string warningMessage)
        {
            MessageBox.Show(warningMessage, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void ShowErrorMessage(string warningMessage)
        {
            MessageBox.Show(warningMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
