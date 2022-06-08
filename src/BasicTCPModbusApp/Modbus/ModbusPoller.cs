using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTCPModbusApp.Modbus
{
    public class ModbusPoller
    {
        public ModbuClient _mModbusClient { get; init; } = new();
        public int _mAmountToPool { private get; set; } = 0;

        public RegiterType _mRegisterType { private get; set; } = RegiterType.Coils;

        public void StartPolling()
        {
            _mModbusClient.Connect();
        }

        public void StopPolling()
        {
            _mModbusClient.Disconnect();
        }

        public void SetRegister(int address, int value)
        {

        }
    }
}
