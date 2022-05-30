﻿using Terminal.Gui;
using BasicTCPModbusApp.MainUI;
using BasicTCPModbusApp.Modbus;

namespace BasicTCPModbusApp;

public class MainApp
{
    static void Main()
    {
        ModbuClient modbusClient = new ModbuClient();
        var cmdSetIp = new SetIpAddressCommand(modbusClient);
        var cmdSetPort = new SetIpPortCommand(modbusClient);
        IInvoker modbusInvoker = new ModbusInvoker()
        {
            _mCmdSetIpAddress = cmdSetIp,
            _mCmdSetIpPort = cmdSetPort
        };
        Application.Init();
        var top = Application.Top;
        MainUI.MainUi mainUiApp = new MainUI.MainUi(modbusInvoker)
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
            var dialog = new NetworkDialog("Network Dialog", 40, 20,
                buttons.ToArray());
            Application.Run(dialog);
        });
    }
}