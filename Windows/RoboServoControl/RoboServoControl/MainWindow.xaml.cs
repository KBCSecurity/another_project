﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using ModbusTCP;

namespace RoboServoControl
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ModbusTCP.Master MBmaster;
        private static byte[] data;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            MBmaster = new Master(ip.Text, 502, true);
            MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
            MBmaster.OnException += new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
            MessageBox.Show("Connected to " + ip.Text + ".\nHave a nice day!", "Connected successfully!", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void greifersend_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse(greiferval.Text);
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
        }

        private void greifsend_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse(greifval.Text);
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 14, moredata);
        }

        private void schwenksend_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse(schwenkval.Text);
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 10, moredata);
        }

        private void forwardsend_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse(forwardval.Text);
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 12, moredata);
        }

        private void updownsend_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse(updownval.Text);
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
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
                    //Console.WriteLine("Recv: Read coils");
                    data = values;
                    break;
                case 2:
                    //Console.WriteLine("Recv: Read discrete inputs");
                    data = values;
                    break;
                case 3:
                    //Console.WriteLine("Recv: Read holding register");
                    data = values;
                    break;
                case 4:
                    //Console.WriteLine("Recv: Read input register");
                    data = values;
                    break;
                case 5:
                    //Console.WriteLine("Recv: Write single coil");
                    break;
                case 6:
                    //Console.WriteLine("Recv: Write multiple coils");
                    break;
                case 7:
                    //Console.WriteLine("Recv: Write single register");
                    break;
                case 8:
                    //Console.WriteLine("Recv: Write multiple register");
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

            MessageBox.Show(exc, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            if(MBmaster.connected)
            {
                MBmaster.disconnect();
                MessageBox.Show("Disconnected from " + ip.Text + ".\n", "Disconnected successfully!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("You are not connected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);


        }

        private void greiferauf_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse("90"); // Auf (max 140)
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
        }

        private void greiferzu_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse("50"); // Zu mit einem kleinen Buffer (max 40)
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse("0");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            moredata[1] = byte.Parse("90"); // Auf (max 140)
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("30");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 14, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("115");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 12, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("160");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 10, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("30");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("40");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            Thread.Sleep(1000);
        }

        private void kugel_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse("5");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("125");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 14, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("160");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 10, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("92");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 12, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("25");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("50"); // Zu mit einem kleinen Buffer (max 40)
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("5");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            Thread.Sleep(1000);
        }

        private void links_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse("30");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 14, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("132");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 10, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("35");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 12, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("90"); // Auf (max 140)
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
            Thread.Sleep(1000);
        }

        private void rechts_Click(object sender, RoutedEventArgs e)
        {
            byte[] moredata = new byte[4];
            moredata[1] = byte.Parse("30");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 14, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("110");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 10, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("72");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 12, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("17");
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 11, moredata);
            Thread.Sleep(1000);
            moredata[1] = byte.Parse("90"); // Auf (max 140)
            MBmaster.WriteSingleRegister(1, Convert.ToByte("1"), 13, moredata);
            Thread.Sleep(1000);
        }

        private void work_Click(object sender, RoutedEventArgs e)
        {
            //IDLE
            button_Click(null, null);
            //5x LINKS
            for(int i = 0;i<5;i++)
            {
                kugel_Click(null, null);
                links_Click(null, null);
            }
            //5x RECHTS
            for (int i = 0; i < 5; i++)
            {
                kugel_Click(null, null);
                rechts_Click(null, null);
            }

        }
    }
}
