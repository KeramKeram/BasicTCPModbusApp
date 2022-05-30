using System.Net;
using FluentModbus;

namespace BasicTCPModbusApp.Modbus
{
    class ModbuClient
    {
        private ModbusTcpClient _mClient = new ModbusTcpClient();
        public string _mAddress { get; set; } = "127.0.0.1";
        public int _mPort { get; set; } = 5020;

        ModbuClient() => _mClient.Connect(new IPEndPoint(IPAddress.Parse(_mAddress), _mPort));

        ~ModbuClient() => _mClient.Disconnect();

        public int ReadHoldingRegister(int unitIdent, int startingAddres)
        {
            var intData = _mClient.ReadHoldingRegisters<int>(unitIdent, startingAddres, 1);
            return intData[0];
        }

        public int ReadCoil(int unitIdent, int startingAddres)
        {
            var boolData = _mClient.ReadCoils(unitIdent, startingAddres, 1);
            bool boolValue = ((boolData[0] >> 0) & 1) > 0;
            return boolValue ? 1 : 0;
        }

        public int ReadDiscreteInputs(int unitIdent, int startingAddres)
        {
            var intData = _mClient.ReadDiscreteInputs(unitIdent, startingAddres, 1);
            return intData[0];
        }

        public int ReadInputRegisters(int unitIdent, int startingAddres)
        {
            var intData = _mClient.ReadInputRegisters<int>(unitIdent, startingAddres, 1);
            return intData[0];
        }
    }
}
