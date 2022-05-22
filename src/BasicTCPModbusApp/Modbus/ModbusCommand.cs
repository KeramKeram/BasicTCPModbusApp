using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTCPModbusApp.Modbus
{
    public enum RegiterType
    {
        Coils,
        Status,
        InputRegister,
        HoldingRegister
    }

    public interface ICommand
    {
        void Execute();
    }

    class ChangeRegisterTypeCommand : ICommand
    {
        private RegiterType _mType { get; set; }

        public ChangeRegisterTypeCommand(RegiterType type)
        {
            _mType = type;
        }
        public void Execute()
        {

        }
    }

    class SetRegisterCommand : ICommand
    {
        private int _mAddress { get; set; }
        private int _mValue { get; set; }

        public SetRegisterCommand(int address, int value)
        {
            _mAddress = address;
            _mValue = value;
        }

        public void Execute()
        {

        }
    }

    class DisplayLengthCommand : ICommand
    {
        private int _mLength { get; set; }

        DisplayLengthCommand(int mLength)
        {
            _mLength = mLength;
        }

        public void Execute()
        {

        }
    }

    class StartPoolingCommand : ICommand
    {
        public void Execute()
        {

        }
    }

}
