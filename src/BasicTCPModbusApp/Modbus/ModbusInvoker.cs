﻿using System;

namespace BasicTCPModbusApp.Modbus
{
    public interface IInvoker
    {
        public void SetNetworkParameters(string ipAddress, int ipPort);
    };
    public class ModbusInvoker : IInvoker
    {
        public ModbusInvoker() { }
        public ICommand<string>? _mCmdSetIpAddress { get; set; }
        public ICommand<int>? _mCmdSetIpPort { get; set; }

        public void SetNetworkParameters(string ipAddress, int ipPort)
        {
            _mCmdSetIpAddress?.setParameter(ipAddress);
            _mCmdSetIpAddress?.Execute();
            _mCmdSetIpPort?.setParameter(ipPort);
            _mCmdSetIpPort?.Execute();
        }
    }
}