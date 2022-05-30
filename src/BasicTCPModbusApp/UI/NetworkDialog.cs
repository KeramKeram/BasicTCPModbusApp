using NStack;
using Terminal.Gui;
using BasicTCPModbusApp.Modbus;

namespace BasicTCPModbusApp.MainUI;

public class NetworkDialog : Dialog
{
    private TextField? _mIpEdit;
    private TextField? _mPortEdit;
    public NetworkDialog(IInvoker modbusInvoker, ustring title, int width, int height, params Button[] buttons) : base(title, width, height,
        buttons)
    {
        buttons[0].Clicked += () => {
            var ip = _mIpEdit == null ? "" : _mIpEdit.Text.ToString();
            var port = _mPortEdit == null ? "" : _mPortEdit.Text.ToString();
            modbusInvoker.SetNetworkParameters(ip ?? "127.0.0.1", int.Parse(port ?? "502"));
            Application.RequestStop();
        };
        Init();
    }

    private void Init()
    {
        var frame = new FrameView("Network Options")
        {
            X = Pos.Center(),
            Y = 1,
            Width = Dim.Percent(75),
            Height = 10
        };

        var labelIp = CreateLabel("IP Address:", 0, 0);
        frame.Add(labelIp);

        _mIpEdit = CreateTextField("127.0.0.1", Pos.Right(labelIp) + 1, Pos.Top(labelIp));
        frame.Add(_mIpEdit);

        var labelPort = CreateLabel("Port:", 0, Pos.Bottom(labelIp));
        frame.Add(labelPort);

        _mPortEdit = CreateTextField("502", Pos.Right(labelPort) + 1, Pos.Top(labelPort));
        frame.Add(_mPortEdit);

        var labelSlaveId = CreateLabel("Slave-id:", 0, Pos.Bottom(labelPort));
        frame.Add(labelSlaveId);

        var slaveidEdit = CreateTextField("0", Pos.Right(labelSlaveId) + 1, Pos.Top(labelSlaveId));
        frame.Add(slaveidEdit);
        Add(frame);
    }

    private Label CreateLabel(string title, Pos x, Pos y)
    {
        return new Label(title)
        {
            X = x,
            Y = y,
            Width = 15,
            Height = 1,
            TextAlignment = Terminal.Gui.TextAlignment.Right,
        };
    }

    private TextField CreateTextField(string title, Pos x, Pos y)
    {
        return new TextField(title)
        {
            X = x,
            Y = y,
            Width = 20,
            Height = 1,
            TextAlignment = Terminal.Gui.TextAlignment.Right,
        };
    }
}