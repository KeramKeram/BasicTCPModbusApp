using System.Net;
using FluentModbus;

namespace BasicTCPModbusApp.Modbus
{
    public class ModbuClient
    {
        private ModbusTcpClient _mClient = new ModbusTcpClient();
        public string _mAddress { get; set; } = "127.0.0.1";
        public int _mPort { get; set; } = 5020;

        public ModbuClient() { }

        ~ModbuClient() => _mClient.Disconnect();

        public void Connect() => _mClient.Connect(new IPEndPoint(IPAddress.Parse(_mAddress), _mPort));

        public void Disconnect() => _mClient.Disconnect();

        public void SetHoldingRegister(int address, ushort value)
        {
            _mClient.WriteSingleRegister(0, address, value);
        }

        public void SetCoil(int address, bool value)
        {
            _mClient.WriteSingleCoil(0, address, value);
        }

        public int ReadHoldingRegister(int unitIdent, int startingAddres)
        {
            var intData = _mClient.ReadHoldingRegisters(Convert.ToByte(unitIdent), Convert.ToByte(startingAddres), 1);
            (intData[0], intData[1]) = (intData[1], intData[0]);
            int registerInt = BitConverter.ToUInt16(intData);
            return registerInt;
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

        public bool IsConnected() => _mClient.IsConnected;
    }
}
