using System;

namespace BasicTCPModbusApp.Modbus
{
    public interface IInvoker
    {
        public void SetNetworkParameters(string ipAddress, int ipPort);

        public void SetAmountElementsToPoll(int number);

        public void StartPooling(Action<LinkedList<string>> callback);

        public void StopPooling();

        public void SetRegisterType(RegiterType type);
        public void SetRegister(int address, ushort value);
    };

    public class ModbusInvoker : IInvoker
    {
        public ModbusInvoker() { }
        public ICommand<string>? _mCmdSetIpAddress { private get; set; }
        public ICommand<int>? _mCmdSetIpPort { private get; set; }
        public ICommand<int>? _mCmdSetAmountToPollCommand { private get; set; }
        public ICommand<Action<LinkedList<string>>>? _mCmdStartPolling { private get; set; }
        public ICommand<Boolean>? _mCmdStopPolling { private get; set; }
        public ICommand<RegiterType>? _mCmdSetRegisterType { private get; set; }
        public ICommand<Tuple<int, ushort>>? _mCmdSetRegister { private get; set; }

        public void SetNetworkParameters(string ipAddress, int ipPort)
        {
            _mCmdSetIpAddress?.setParameter(ipAddress);
            _mCmdSetIpAddress?.Execute();
            _mCmdSetIpPort?.setParameter(ipPort);
            _mCmdSetIpPort?.Execute();
        }

        public void SetAmountElementsToPoll(int number)
        {
            _mCmdSetAmountToPollCommand?.setParameter(number);
            _mCmdSetAmountToPollCommand?.Execute();
        }

        public void StartPooling(Action<LinkedList<string>> callback)
        {
            _mCmdStartPolling?.setParameter(callback);
            _mCmdStartPolling?.Execute();
        }

        public void StopPooling()
        {
            _mCmdStopPolling?.Execute();
        }

        public void SetRegisterType(RegiterType type)
        {
            _mCmdSetRegisterType?.setParameter(type);
            _mCmdSetRegisterType?.Execute();
        }

        public void SetRegister(int address, ushort value)
        {
            _mCmdSetRegister?.setParameter(Tuple.Create(address, value));
            _mCmdSetRegister?.Execute();
        } 
    }

}
