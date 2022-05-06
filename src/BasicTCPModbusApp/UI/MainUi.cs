using System.Collections;
using System.Collections.ObjectModel;
using Terminal.Gui;
using NStack;

namespace BasicTCPModbusApp.MainUI;

public class MainUi : Window
{
    private List<Tuple<Label, TextField>>? _mRegistersUi;
    private Window? _mControlWindow;
    private TextField _mDisplayLenghtTextField = new TextField();

    public MainUi() : base("MyApp")
    {
        InitControlls();
        InitRegistersUi();
    }

    private void InitControlls()
    {
        _mControlWindow = new Window($"Control")
        {
            X = Pos.Center(),
            Y = 1,
            Width = Dim.Fill(5),
            Height = 8
        };

        var displayLengthLabel = new Label($"Display Length:");
        _mControlWindow.Add(displayLengthLabel);

        _mDisplayLenghtTextField.X = Pos.Right(displayLengthLabel) + 1;
        _mDisplayLenghtTextField.Y = displayLengthLabel.Y;
        _mDisplayLenghtTextField.Width = 10;
        _mDisplayLenghtTextField.Height = 10;
        _mDisplayLenghtTextField.Text = "90";

        _mControlWindow.Add(_mDisplayLenghtTextField);

        var registerTypeLabel = new Label
        {
            X = Pos.Right(_mDisplayLenghtTextField) + 10,
            Y = displayLengthLabel.Y,
            Text = "Register Type:"
        };
        _mControlWindow.Add(registerTypeLabel);

        var registerTypeRadio = new RadioGroup(new ustring[]
            { "Coils Reg.", "Input Status Reg.", "Input Register Reg.", "Holding Reg." })
        {
            X = Pos.Right(registerTypeLabel) + 1,
            Y = displayLengthLabel.Y,
            SelectedItem = 0,
        };
        _mControlWindow.Add(registerTypeRadio);

        var statusLabel = new Label
        {
            X = Pos.Right(registerTypeRadio) + 1,
            Y = registerTypeRadio.Y,
            Text = "Status:"
        };
        _mControlWindow.Add(statusLabel);

        var statusLabelValue = new Label
        {
            X = Pos.Right(statusLabel) + 1,
            Y = registerTypeRadio.Y,
            Text = ""
        };
        _mControlWindow.Add(statusLabelValue);

        var poolingButton = new Button()
        {
            X = displayLengthLabel.X,
            Y = Pos.Bottom(registerTypeRadio) + 1,
            Text = "Start Pooling"
        };
        _mControlWindow.Add(poolingButton);
        
        var setRegisterButton = new Button()
        {
            X = Pos.Right(registerTypeLabel),
            Y = poolingButton.Y,
            Text = "Set Register"
        };
        _mControlWindow.Add(setRegisterButton);
        
        var registerValueToSet = new TextField
        {
            X = Pos.Right(setRegisterButton),
            Y = setRegisterButton.Y,
            Width = 10,
            Height = 10,
            Text = ""
        };
        _mControlWindow.Add(registerValueToSet);
        this.Add(_mControlWindow);
    }

    private void InitRegistersUi()
    {
        string registerAmountStr = _mDisplayLenghtTextField.Text.ToString() ?? "1";
        _mRegistersUi = new List<Tuple<Label, TextField>>(int.Parse(registerAmountStr));
        var registersFrame = new Window($"Control")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(_mControlWindow),
            Width = Dim.Fill(5),
            Height = 20
        };
        this.Add(registersFrame);
    }
}