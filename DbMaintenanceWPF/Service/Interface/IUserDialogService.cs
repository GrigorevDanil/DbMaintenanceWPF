using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;

namespace DbMaintenanceWPF.Service.Interface
{
    public interface IUserDialogService
    {
        bool ShowPrint(string namePrint);
        bool Edit(object item, string titleWindow);
        void ShowWarning(string title, string text);
        void ShowQuestion(string title, string text);
        bool ShowConfirm(string title, string text);
        bool ShowSettingConnection();
        bool ShowAuthentication();
        bool ShowMain(object user);
        string ShowInput(string title);
        string ShowBrowserCatalog();
        (string, string) ShowResetKey();
        string ShowChangeKey(string resultHash, string generatedSalt);

    }
}
