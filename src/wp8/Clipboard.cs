using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;

namespace Cordova.Extension.Commands
{
    public class Clipboard : BaseCommand
    {
        public void copy(string options)
        {
            string text = "";

            try
            {
                text = JsonHelper.Deserialize<string[]>(options)[0];
            }
            catch
            {
                DispatchCommandResult(new PluginResult(PluginResult.Status.JSON_EXCEPTION));

                return;
            }

            try
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    System.Windows.Clipboard.SetText(text);
                });
                DispatchCommandResult(new PluginResult(PluginResult.Status.OK, text));
            }
            catch
            {
                DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR));
            }

        }

        public void copyString(string options)
        {
            copy(options);
        }

        public void paste(string options)
        {
            string text = "";

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    text = System.Windows.Clipboard.GetText();

                    DispatchCommandResult(new PluginResult(PluginResult.Status.OK, text));
                }
                catch
                {
                    DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR));
                }
            });
        }

        public void pasteString(string options)
        {
            paste(options);
        }

        public void clear()
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    System.Windows.Clipboard.Clear();
                    DispatchCommandResult(new PluginResult(PluginResult.Status.OK, true));
                }
                catch
                {
                    DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR));
                }
            });
        }
    }
}