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
        public void StartPolling(Action<LinkedList<string>> regisersListCallback)
        {
            if (!_mModbusClient.IsConnected())
            {
                _mModbusClient.Connect();
                if (!_mModbusClient.IsConnected())
                {
                    throw new Exception($"Can't connect to server: {_mModbusClient._mAddress} on port: {_mModbusClient._mPort}" );
                }
            }
            regisersListCallback(readCurrentRegisterTable());
        }

        public void StopPolling()
        {
            _mModbusClient.Disconnect();
        }

        public void SetRegister(int address, ushort value)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);
            (valueBytes[0], valueBytes[1]) = (valueBytes[1], valueBytes[0]);
            ushort convertedValue = BitConverter.ToUInt16(valueBytes);
            switch (_mRegisterType)
            {
                case RegiterType.Coils: _mModbusClient.SetCoil(address, Convert.ToBoolean(convertedValue)); break;
                case RegiterType.HoldingRegister: _mModbusClient.SetHoldingRegister(address, convertedValue); break;
            }
        }

        private LinkedList<string> readCurrentRegisterTable()
        {
            LinkedList<string> registers = new ();
            for (int i = 0; i < _mAmountToPool; i++)
            {
                switch (_mRegisterType)
                { 
                    case RegiterType.Coils: registers.AddLast(_mModbusClient.ReadCoil(0, i).ToString()); break;
                    case RegiterType.Status: registers.AddLast(_mModbusClient.ReadDiscreteInputs(0, i).ToString()); break;
                    case RegiterType.InputRegister: registers.AddLast(_mModbusClient.ReadInputRegisters(0, i).ToString()); break;
                    case RegiterType.HoldingRegister: registers.AddLast(_mModbusClient.ReadHoldingRegister(0, i).ToString()); break;
                } 
            }
            return registers;
        }

    }
}
