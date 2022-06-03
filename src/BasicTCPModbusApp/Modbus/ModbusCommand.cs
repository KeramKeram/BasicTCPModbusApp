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

    class SetDispalyElementsAmountCommand : ICommand<int>
    {
        private ModbusPoller _mPoller;
        private int _mLength { get; set; } = 0;

        public SetDispalyElementsAmountCommand(ModbusPoller poller) => _mPoller = poller;
        public void setParameter(int parameter) => _mLength = parameter;
        public void Execute() => _mPoller._mAmountToPool = _mLength;
    }

    class StartPoolingCommand : ICommand<Boolean>
    {
        private ModbusPoller _mPoller;
        public StartPoolingCommand(ModbusPoller poller) => _mPoller = poller;
        public void setParameter(Boolean parameter)
        {}
        public void Execute()
        {
            _mPoller.StartPolling();
        }
    }

    class StopPoolingCommand : ICommand<Boolean>
    {
        private ModbusPoller _mPoller;
        public StopPoolingCommand(ModbusPoller poller) => _mPoller = poller;
        public void setParameter(Boolean parameter)
        { }
        public void Execute()
        {
            _mPoller.StopPolling();
        }
    }

    class SetIpAddressCommand : ICommand<string>
    {
        private ModbuClient _mModbusClient;
        private string _mAddress = "127.0.0.1";

        public SetIpAddressCommand(ModbuClient client)
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
        public SetIpPortCommand(ModbuClient client)
        {
            _mModbusClient = client;
        }
        public void Execute()
        {
            _mModbusClient._mPort = _mPort;

        }
    }

}
