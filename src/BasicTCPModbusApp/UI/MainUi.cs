using System.Collections;
using System.Collections.ObjectModel;
using Terminal.Gui;
using NStack;

namespace BasicTCPModbusApp.MainUI;

public class MainUi : Window
{
    private List<Tuple<Label, TextField>> mRegistersUi;
    private Window mControlWindow;

    public MainUi() : base("MyApp")
    {
        InitControlls();
        InitRegistersUi();
    }

    private void InitControlls()
    {
        mControlWindow = new Window($"Control")
        {
            X = Pos.Center(),
            Y = 1,
            Width = Dim.Fill(5),
            Height = 8
        };

        var displayLengthLabel = new Label($"Display Length:");
        mControlWindow.Add(displayLengthLabel);

        var displayLenghtValue = new TextField
        {
            X = Pos.Right(displayLengthLabel) + 1,
            Y = displayLengthLabel.Y,
            Width = 10,
            Height = 10,
            Text = ""
        };

        mControlWindow.Add(displayLenghtValue);

        var registerTypeLabel = new Label
        {
            X = Pos.Right(displayLenghtValue) + 10,
            Y = displayLengthLabel.Y,
            Text = "Register Type:"
        };
        mControlWindow.Add(registerTypeLabel);

        var registerTypeRadio = new RadioGroup(new ustring[]
            { "Coils Reg.", "Input Status Reg.", "Input Register Reg.", "Holding Reg." })
        {
            X = Pos.Right(registerTypeLabel) + 1,
            Y = displayLengthLabel.Y,
            SelectedItem = 0,
        };
        mControlWindow.Add(registerTypeRadio);

        var statusLabel = new Label
        {
            X = Pos.Right(registerTypeRadio) + 1,
            Y = registerTypeRadio.Y,
            Text = "Status:"
        };
        mControlWindow.Add(statusLabel);

        var statusLabelValue = new Label
        {
            X = Pos.Right(statusLabel) + 1,
            Y = registerTypeRadio.Y,
            Text = ""
        };
        mControlWindow.Add(statusLabelValue);

        var poolingButton = new Button()
        {
            X = displayLengthLabel.X,
            Y = Pos.Bottom(registerTypeRadio) + 1,
            Text = "Start Pooling"
        };
        mControlWindow.Add(poolingButton);
        
        var setRegisterButton = new Button()
        {
            X = Pos.Right(registerTypeLabel),
            Y = poolingButton.Y,
            Text = "Set Register"
        };
        mControlWindow.Add(setRegisterButton);
        
        var registerValueToSet = new TextField
        {
            X = Pos.Right(setRegisterButton),
            Y = setRegisterButton.Y,
            Width = 10,
            Height = 10,
            Text = ""
        };
        mControlWindow.Add(registerValueToSet);
        this.Add(mControlWindow);
    }

    private void InitRegistersUi()
    {
        mRegistersUi = new List<Tuple<Label, TextField>>();
        var registersFrame = new Window($"Control")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(mControlWindow),
            Width = Dim.Fill(5),
            Height = 20
        };
        this.Add(registersFrame);
    }
}