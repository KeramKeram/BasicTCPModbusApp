using System.Net;
using FluentModbus;

namespace BasicTCPModbusApp.Modbus
{
    class ModbuClient
    {
        private ModbusTcpClient? _mClient;
        public string _mAddress = "127.0.0.1";
        public int _mPort = 5020;

        ModbuClient()
        {
            _mClient = new ModbusTcpClient();
            _mClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 502));
        }
    }
}
