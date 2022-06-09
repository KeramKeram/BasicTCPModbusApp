using Terminal.Gui;
using BasicTCPModbusApp.MainUI;
using BasicTCPModbusApp.Modbus;

namespace BasicTCPModbusApp;

public class MainApp
{
    static void Main()
    {
        ModbusPoller modbusPollr = new();
        IInvoker modbusInvoker = new ModbusInvoker()
        {
            _mCmdSetIpAddress = new SetIpAddressCommand(modbusPollr),
            _mCmdSetIpPort = new SetIpPortCommand(modbusPollr),
            _mCmdSetAmountToPollCommand = new SetDispalyElementsAmountCommand(modbusPollr),
            _mCmdStartPolling = new StartPoolingCommand(modbusPollr),
            _mCmdStopPolling = new StopPoolingCommand(modbusPollr),
            _mCmdSetRegisterType = new ChangeRegisterTypeCommand(modbusPollr),
            _mCmdSetRegister = new SetRegisterCommand(modbusPollr)
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

        Action showDialogDelegate = CreateNetworkDialogDelegateAction(modbusInvoker);

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

    static Action CreateNetworkDialogDelegateAction(IInvoker modbusInvoker)
    {
        return new Action(() =>
        {
            var buttons = new List<Button>();
            var button = new Button("OK");
            buttons.Add(button);
            var dialog = new NetworkDialog(modbusInvoker, "Network Dialog", 40, 20,
                buttons.ToArray());
            Application.Run(dialog);
        });
    }
}