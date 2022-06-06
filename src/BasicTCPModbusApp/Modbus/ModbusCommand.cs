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

    class ChangeRegisterTypeCommand : ICommand<RegiterType>
    {
        private ModbusPoller _mPoller;
        
        private RegiterType _mRegiterType;

        public ChangeRegisterTypeCommand(ModbusPoller poll) => _mPoller = poll;

        public void setParameter(RegiterType parameter) => _mRegiterType = parameter;

        public void Execute()
        {
            _mPoller._mRegisterType = _mRegiterType;
        }
    }

    class SetRegisterCommand : ICommand<Tuple<int, int>>
    {
        private ModbusPoller _mModbusPoller;
        private int _mAddress = 0;
        private int _mValue = 0;

        public SetRegisterCommand(ModbusPoller modbusPoller)
        {
            _mModbusPoller = modbusPoller;
        }
        public void setParameter(Tuple<int, int> parameter)
        {
            _mAddress = parameter.Item1;
            _mValue = parameter.Item2;
        }
        public void Execute()
        {
            _mModbusPoller.SetRegister(_mAddress, _mValue);
        }
    }

    class SetDispalyElementsAmountCommand : ICommand<int>
    {
        private ModbusPoller _mPoller;
        public int _mLength = 0;

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
