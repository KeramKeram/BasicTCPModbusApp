using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTCPModbusApp.Modbus
{
    public enum RegiterType
    {
        Coils,
        Status,
        InputRegister,
        HoldingRegister
    }

    public interface ICommand<T>
    {
        void setParameter(T parameter);
        void Execute();
    }

    class ChangeRegisterTypeCommand<T> : ICommand<T>
    {
        private RegiterType _mType { get; set; }

        public ChangeRegisterTypeCommand(RegiterType type)
        {
            _mType = type;
        }
        public void setParameter(T parameter)
        {

        }

        public void Execute()
        {

        }
    }

    class SetRegisterCommand<T> : ICommand<T>
    {
        private int _mAddress { get; set; }
        private int _mValue { get; set; }

        public SetRegisterCommand(int address, int value)
        {
            _mAddress = address;
            _mValue = value;
        }
        public void setParameter(T parameter)
        {

        }
        public void Execute()
        {

        }
    }

    class DisplayLengthCommand<T> : ICommand<T>
    {
        private int _mLength { get; set; }

        DisplayLengthCommand(int mLength)
        {
            _mLength = mLength;
        }
        public void setParameter(T parameter)
        {

        }
        public void Execute()
        {

        }
    }

    class StartPoolingCommand<T> : ICommand<T>
    {
        public void setParameter(T parameter)
        {

        }
        public void Execute()
        {

        }
    }

    class SetIpAddressCommand : ICommand<string>
    {
        private ModbuClient _mModbusClient;
        private string _mAddress = "127.0.0.1";

        SetIpAddressCommand(ModbuClient client)
        {
            _mModbusClient = client;
        }
        public void setParameter(string parameter)
        {
            _mAddress = parameter;
        }
        public void Execute()
        {
            _mModbusClient._mAddress = _mAddress;
        }
    }

    class SetIpPortCommand : ICommand<int>
    {
        private ModbuClient _mModbusClient;
        private int _mPort = 502;

        public void setParameter(int parameter)
        {
            _mPort = parameter;
        }
        SetIpPortCommand(ModbuClient client)
        {
            _mModbusClient = client;
        }
        public void Execute()
        {
            _mModbusClient._mPort = _mPort;

        }
    }

}
