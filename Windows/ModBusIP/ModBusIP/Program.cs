using System;
using System.Collections.Generic;
using System.Text;
using ModbusTCP;

namespace ModBusIP
{
    class Program
    {
        private static ModbusTCP.Master MBmaster;
        private static byte[] data;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ModbusTCP/IP Demo. Press any key to continue...");
            Console.ReadLine();
            Console.WriteLine("Trying to connect to 192.168.1.100...");

            try
            {
                // Create new modbus master and add event functions
                MBmaster = new Master("192.168.1.100", 502, true);
                MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
                MBmaster.OnException += new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
                Console.WriteLine("Successfully connected...");

                //Try to activate LEDs
                ushort ID = 1;
                //Unit
                byte unit = Convert.ToByte("1");
                //SlaveID
                ushort StartAddress = 1;
                bool data = true;

                //LED ein---------------------------------------------------------------------------------------
                Console.WriteLine("Trying to activate LED 1...");
                MBmaster.WriteSingleCoils(ID, unit, StartAddress, data);
                System.Threading.Thread.Sleep(2000);

                Console.WriteLine("Trying to activate LED 2...");
                //SlaveID
                StartAddress = 2;
                MBmaster.WriteSingleCoils(ID, unit, StartAddress, data);
                System.Threading.Thread.Sleep(2000);

                Console.WriteLine("Trying to activate LED 3...");
                //SlaveID
                StartAddress = 3;
                MBmaster.WriteSingleCoils(ID, unit, StartAddress, data);
                System.Threading.Thread.Sleep(2000);

                //LED aus-------------------------------------------------------------------------------------
                data = false;
                StartAddress = 1;
                Console.WriteLine("Trying to deactivate LED 1...");
                MBmaster.WriteSingleCoils(ID, unit, StartAddress, data);
                System.Threading.Thread.Sleep(2000);

                Console.WriteLine("Trying to deactivate LED 2...");
                //SlaveID
                StartAddress = 2;
                MBmaster.WriteSingleCoils(ID, unit, StartAddress, data);
                System.Threading.Thread.Sleep(2000);

                Console.WriteLine("Trying to deactivate LED 3...");
                //SlaveID
                StartAddress = 3;
                MBmaster.WriteSingleCoils(ID, unit, StartAddress, data);
                System.Threading.Thread.Sleep(2000);

                Console.WriteLine("Successfully!\nStarting servo test...");

                //Servo test---------------------------------------------------------------------------------
                Console.WriteLine("Servo closing...");
                StartAddress = 5;
                byte[] moredata = new byte[4];
                moredata[1] = 40;
                MBmaster.WriteSingleRegister(ID, unit, StartAddress, moredata);
                System.Threading.Thread.Sleep(3000);

                Console.WriteLine("Servo open...");
                moredata[1] = 140;
                MBmaster.WriteSingleRegister(ID, unit, StartAddress, moredata);
                System.Threading.Thread.Sleep(3000);

                Console.WriteLine("Demo finished...");
                Console.ReadLine();



            }
            catch (SystemException error)
            {
                Console.WriteLine("Error: " + error.Message);
            }


        }

        // ------------------------------------------------------------------------
        // Event for response data
        // ------------------------------------------------------------------------
        private static void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            // ------------------------------------------------------------------------
            // Identify requested data
            switch (ID)
            {
                case 1:
                    Console.WriteLine("Recv: Read coils");
                    data = values;
                    break;
                case 2:
                    Console.WriteLine("Recv: Read discrete inputs");
                    data = values;
                    break;
                case 3:
                    Console.WriteLine("Recv: Read holding register");
                    data = values;
                    break;
                case 4:
                    Console.WriteLine("Recv: Read input register");
                    data = values;
                    break;
                case 5:
                    Console.WriteLine("Recv: Write single coil");
                    break;
                case 6:
                    Console.WriteLine("Recv: Write multiple coils");
                    break;
                case 7:
                    Console.WriteLine("Recv: Write single register");
                    break;
                case 8:
                    Console.WriteLine("Recv: Write multiple register");
                    break;
            }
        }

        // ------------------------------------------------------------------------
        // Modbus TCP slave exception
        // ------------------------------------------------------------------------
        private static void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
        {
            string exc = "Modbus says error: ";
            switch (exception)
            {
                case Master.excIllegalFunction: exc += "Illegal function!"; break;
                case Master.excIllegalDataAdr: exc += "Illegal data adress!"; break;
                case Master.excIllegalDataVal: exc += "Illegal data value!"; break;
                case Master.excSlaveDeviceFailure: exc += "Slave device failure!"; break;
                case Master.excAck: exc += "Acknoledge!"; break;
                case Master.excGatePathUnavailable: exc += "Gateway path unavailbale!"; break;
                case Master.excExceptionTimeout: exc += "Slave timed out!"; break;
                case Master.excExceptionConnectionLost: exc += "Connection is lost!"; break;
                case Master.excExceptionNotConnected: exc += "Not connected!"; break;
            }

            Console.WriteLine(exc);
        }

    }
}
