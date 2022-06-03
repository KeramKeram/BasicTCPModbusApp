using System;

namespace BasicTCPModbusApp.Modbus
{
    public interface IInvoker
    {
        public void SetNetworkParameters(string ipAddress, int ipPort);

        public void SetAmountElementsToPoll(int number);

        public bool StartPooling();

        public bool StopPooling();

        public void SetRegisterType(RegiterType type);
    };

    public class ModbusInvoker : IInvoker
    {
        public ModbusInvoker() { }
        public ICommand<string>? _mCmdSetIpAddress { private get; set; }
        public ICommand<int>? _mCmdSetIpPort { private get; set; }
        public ICommand<int>? _mCmdSetAmountToPollCommand { private get; set; }
        public ICommand<Boolean>? _mCmdStartPolling { private get; set; }
        public ICommand<Boolean>? _mCmdStopPolling { private get; set; }

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

        public bool StartPooling()
        {
            return true;
        }

        public bool StopPooling()
        {
            return true;
        }

        public void SetRegisterType(RegiterType type)
        {

        }
    }

}
