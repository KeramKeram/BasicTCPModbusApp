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

        Action showDialogDelegate = CreateNetworkDialogDelegate();

        top.Add(mainUiApp);
        top.Add(CreateMenuBar(top, showDialogDelegate));

        Application.Run();
    }

    private static MenuBar CreateMenuBar(Toplevel top, Action ac)
    {
        var menu = new MenuBar(new MenuBarItem[]
        {
            new MenuBarItem("_File", new MenuItem[]
            {
                new MenuItem("_Quit", "", () =>
                {
                    if (Quit()) top.Running = false;
                })
            }),
            new MenuBarItem("Preference", new MenuItem[]
            {
                new MenuItem("Network", "", ac),
                new MenuItem("View", "", null)
            })
        });
        return menu;
    }

    static bool Quit()
    {
        var n = MessageBox.Query(50, 7, "Quit Program", "Are you sure you want to quit this demo?", "Yes", "No");
        return n == 0;
    }

    static Action CreateNetworkDialogDelegate()
    {
        return new Action(() =>
        {
            int width = int.Parse("10");
            int height = int.Parse("10");
            int numButtons = int.Parse("1");

            var buttons = new List<Button>();
            var clicked = -1;
            for (int i = 0; i < numButtons; i++)
            {
                var buttonId = i;
                //var button = new Button (btnText [buttonId % 10],
                //	is_default: buttonId == 0);
                var button = new Button("OK");
                button.Clicked += () =>
                {
                    clicked = buttonId;
                    Application.RequestStop();
                };
                buttons.Add(button);
            }

            var dialog = new Dialog("Test Title", width, height,
                buttons.ToArray());
            Application.Run(dialog);
        });
    }
}