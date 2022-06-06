using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTCPModbusApp.Modbus
{
    public class ModbusPoller
    {
        public ModbuClient _mModbusClient { get; init; }
        public int _mAmountToPool { private get; set; } = 0;

        public RegiterType _mRegisterType { private get; set; } = RegiterType.Coils;

        public ModbusPoller(ModbuClient client)
        {
            _mModbusClient = client;
        }

        public void StartPolling()
        {

        }

        public void StopPolling()
        {

        }

        public void SetRegister(int address, int value)
        {

        }
    }
}
