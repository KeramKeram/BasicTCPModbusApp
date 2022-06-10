using System.Collections;
using System.Collections.ObjectModel;
using System;
using System.Data;
using Terminal.Gui;
using NStack;
using BasicTCPModbusApp.Modbus;

namespace BasicTCPModbusApp.MainUI;


public class MainUi : Window
{
    private Dictionary<int, string>? _mRegistersUi;
    private Window? _mControlWindow;
    private TextField _mDisplayLenghtTextField = new TextField();
    private Window? _mRegisterWindow;
    TableView? _mTableView;
    private IInvoker _mModbusInvoker;

    public MainUi(IInvoker invoker) : base("MyApp")
    {
        _mModbusInvoker = invoker;
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

        var okbButtonAmountPoll = new Button()
        {
            X = Pos.Right(_mDisplayLenghtTextField) + 1,
            Y = displayLengthLabel.Y,
            Text = "OK"
        };
        okbButtonAmountPoll.Clicked += () =>
        {
            _mModbusInvoker.SetAmountElementsToPoll(int.Parse(_mDisplayLenghtTextField.Text.ToString() ?? "0"));
        };
        _mControlWindow.Add(okbButtonAmountPoll);

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
        registerTypeRadio.SelectedItemChanged += (e) =>
        {
            switch (e.SelectedItem)
            {
                case 0: _mModbusInvoker.SetRegisterType(RegiterType.Coils); break;
                case 1: _mModbusInvoker.SetRegisterType(RegiterType.Status); break;
                case 2: _mModbusInvoker.SetRegisterType(RegiterType.InputRegister); break;
                case 3: _mModbusInvoker.SetRegisterType(RegiterType.HoldingRegister); break;
            }
        };
        _mControlWindow.Add(registerTypeRadio);

        var statusLabel = new Label
        {
            X = Pos.Right(registerTypeRadio) + 1,
            Y = registerTypeRadio.Y,
            Text = "Status:"
        };
        _mControlWindow.Add(statusLabel);

        var statusTextField = new TextField
        {
            X = Pos.Right(statusLabel),
            Y = statusLabel.Y,
            Width = 29,
            Height = 10,
            Text = ""
        };
        _mControlWindow.Add(statusTextField);

        var statusLabelValue = new Label
        {
            X = Pos.Right(statusLabel) + 1,
            Y = registerTypeRadio.Y,
            Text = ""
        };
        _mControlWindow.Add(statusLabelValue);

        var startPoolingButton = new Button()
        {
            X = displayLengthLabel.X,
            Y = Pos.Bottom(registerTypeRadio) + 1,
            Text = "Start Pooling"
        };
        _mControlWindow.Add(startPoolingButton);

        var stopPoolingButton = new Button()
        {
            X = Pos.Right(startPoolingButton) + 1,
            Y = Pos.Bottom(registerTypeRadio) + 1,
            Text = "Stop Pooling"
        };
        stopPoolingButton.Clicked += () =>
        {
            _mModbusInvoker.StopPooling();
            stopPoolingButton.ColorScheme = Colors.Error;
            startPoolingButton.ColorScheme = Colors.Base;
        };
        _mControlWindow.Add(stopPoolingButton);

        startPoolingButton.Clicked += () =>
        {
            _mModbusInvoker.StartPooling();
            stopPoolingButton.ColorScheme = Colors.Base;
            startPoolingButton.ColorScheme = Colors.Error;
        };

        var setRegisterButton = new Button()
        {
            X = Pos.Right(registerTypeLabel),
            Y = startPoolingButton.Y,
            Text = "Set Register"
        };
        _mControlWindow.Add(setRegisterButton);

        var registerAddressToSet = new TextField
        {
            X = Pos.Right(setRegisterButton),
            Y = setRegisterButton.Y,
            Width = 10,
            Height = 10,
            Text = "Address"
        };

        _mControlWindow.Add(registerAddressToSet);

        var registerValueToSet = new TextField
        {
            X = Pos.Right(registerAddressToSet) + 1,
            Y = setRegisterButton.Y,
            Width = 10,
            Height = 10,
            Text = "Value"
        };
        _mControlWindow.Add(registerValueToSet);
        setRegisterButton.Clicked += () =>
        {
            var adr = registerAddressToSet.Text.ToString();
            var val = registerValueToSet.Text.ToString();
            if (adr is null || val is null)
            {
                return;
            }
            try
            {
                _mModbusInvoker.SetRegister(int.Parse(adr), ushort.Parse(val));
            }
            catch (System.FormatException)
            {
                statusTextField.Text = "Error:Wrong address/value.";
            }
            catch (System.NullReferenceException)
            {
                statusTextField.Text = "Error:Connection problem.";
            }
        };
        this.Add(_mControlWindow);
    }

    private void InitRegistersUi()
    {
        string registerAmountStr = _mDisplayLenghtTextField.Text.ToString() ?? "1";
        int registerAmount = int.Parse(registerAmountStr);
        _mRegistersUi = new Dictionary<int, string>(registerAmount);
        _mRegisterWindow = new Window($"Registers")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(_mControlWindow),
            Width = Dim.Fill(5),
            Height = 20
        };

        _mTableView = new TableView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(1),
        };
        _mRegisterWindow.Add(_mTableView);
        this.Add(_mRegisterWindow);

        int numberOfColumns = (registerAmount / 10) + 1;
        var dt = new DataTable();
        for (int x = 0; x < numberOfColumns; x++)
        {
            dt.Columns.Add("");
        }

        for (int t = 0; t < registerAmount; t++)
        {
            _mRegistersUi[t] = new string("x:" + t.ToString());
        }

        string[] regArray = new string[10];
        for (int i = 0; i < (_mRegistersUi.Count / 10) + 1; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                if (i + ((j - 1) * 10) < _mRegistersUi.Count && i + ((j - 1) * 10) >= 0) regArray[j - 1] = _mRegistersUi[i + ((j - 1) * 10)];
            }
            dt.Rows.Add(regArray);
        }

        _mTableView.Table = dt;
    }

    void updateRegisterView(Dictionary<int, string> registersValue)
    {

    }
}