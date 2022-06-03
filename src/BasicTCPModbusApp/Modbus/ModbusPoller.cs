using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTCPModbusApp.Modbus
{
    public class ModbusPoller
    {
        private ModbuClient _mModbusClient;
        public int _mAmountToPool { private get; set; } = 0;

        public ModbusPoller(ModbuClient client)
        {
            _mModbusClient = client;
        }
    }
}
