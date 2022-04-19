using Terminal.Gui;
namespace BasicTCPModbusApp;

public class MainApp
{
    static void Main()
    {
        Application.Init();
        var top = Application.Top;
        MainUI.MainUi mainUiApp = new MainUI.MainUi()
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        top.Add(mainUiApp);
        Application.Run();
    }
}