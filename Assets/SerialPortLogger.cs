using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialPortLogger : MonoBehaviour
{
    public string Directory;
    public string Name;
    public string Postfix;

    private TextWriter writer;

    public string Port;

    private SerialPort serialPort;
    private Thread readWorker;

    public bool Running { get; private set; }
    public int Records { get; private set; }
    public string LastEntry { get; private set; }

    public void Begin()
    {
        readWorker = new Thread(Read);

        // Create a new SerialPort object with default settings.
        serialPort = new SerialPort();

        // Allow the user to set the appropriate properties.
        serialPort.PortName = Port;
        serialPort.BaudRate = 115200;
        serialPort.Parity = Parity.None;
        serialPort.DataBits = 8;
        serialPort.StopBits = StopBits.One;
        serialPort.Handshake = Handshake.None;

        // Set the read/write timeouts
        serialPort.ReadTimeout = 500;
        serialPort.WriteTimeout = 500;

        serialPort.Open();
        Running = true;

        writer = Utils.CreateStreamWriter(Directory, Name, Postfix, false);

        readWorker.Start();
    }

    public void Read()
    {
        while (Running)
        {
            try
            {
                LastEntry = $"{DateTime.Now.Ticks} {serialPort.ReadLine()}";
                writer.WriteLine(LastEntry);
                writer.Flush();
                Records++;
            }
            catch (TimeoutException) 
            {   
                break;
            }
            catch (ThreadAbortException) 
            {
                break;
            }
        }
        Running = false;
    }

    public void OnDestroy()
    {
        Running = false;
        try
        {
            if (readWorker != null)
            {
                readWorker.Abort();
                readWorker.Join();
                Debug.Log($"Closed {Port} gracefully");
            }
        }
        finally
        {
            if (serialPort != null)
            {
                serialPort.Close();
            }
        }

        readWorker = null;
        serialPort = null;

        if (writer != null)
        {
            writer.Dispose();
            writer = null;
        }
    }
}
