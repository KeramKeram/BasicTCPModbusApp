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

        top.Add(CreateMenuBar(top));

        Application.Run();
    }

    private static MenuBar CreateMenuBar(Toplevel top)
    {
        var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
            }),
            new MenuBarItem ("Preference", new MenuItem [] {
                new MenuItem ("Network", "", null),
                new MenuItem ("View", "", null),
            })
        });
        return menu;
    }

    static bool Quit()
    {
        var n = MessageBox.Query(50, 7, "Quit Program", "Are you sure you want to quit this demo?", "Yes", "No");
        return n == 0;
    }
}