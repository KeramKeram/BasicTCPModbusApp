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

        Action showDialogDelegate = CreateNetworkDialogDelegateAction();

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

    static Action CreateNetworkDialogDelegateAction()
    {
        return new Action(() =>
        {
            var buttons = new List<Button>();
            var button = new Button("OK");
            button.Clicked += () => { Application.RequestStop(); };
            buttons.Add(button);
            var dialog = new Dialog("Test Title", 40, 20,
                buttons.ToArray());
            var frame = new FrameView("Network Options")
            {
                X = Pos.Center(),
                Y = 1,
                Width = Dim.Percent(75),
                Height = 10
            };

            var labelIp = new Label("IP Address:")
            {
                X = 0,
                Y = 0,
                Width = 15,
                Height = 1,
                TextAlignment = Terminal.Gui.TextAlignment.Right,
            };
            frame.Add(labelIp);
            var ipEdit = new TextField("127.0.0.1")
            {
                X = Pos.Right(labelIp) + 1,
                Y = Pos.Top(labelIp),
                Width = 20,
                Height = 1
            };
            frame.Add(ipEdit);

            var labelPort = new Label("Port:")
            {
                X = 0,
                Y = Pos.Bottom (labelIp),
                Width = 15,
                Height = 1,
                TextAlignment = Terminal.Gui.TextAlignment.Right,
            };
            frame.Add(labelPort);
            var portEdit = new TextField("502")
            {
                X = Pos.Right(labelPort) + 1,
                Y = Pos.Top(labelPort),
                Width = 20,
                Height = 1
            };
            frame.Add(portEdit);

            var labelSlaveId = new Label("Slave-id:")
            {
                X = 0,
                Y = Pos.Bottom (labelPort),
                Width = 15,
                Height = 1,
                TextAlignment = Terminal.Gui.TextAlignment.Right,
            };
            frame.Add(labelSlaveId);
            var slaveidEdit = new TextField("0")
            {
                X = Pos.Right(labelSlaveId) + 1,
                Y = Pos.Top(labelSlaveId),
                Width = 20,
                Height = 1
            };
            frame.Add(slaveidEdit);

            dialog.Add(frame);
            Application.Run(dialog);
        });
    }
}