using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HardwareHostBehaviour : MonoBehaviour
{
    public string portName;
    public int baudRate;
    public string handshake;
    public string expectedResponse;
        
    private static SerialPort serialPort;
    private static Dictionary<HardwareCommand, string> serialCommands = new Dictionary<HardwareCommand, string>();        
    private static bool isConnected = false;

    private void Start()
    {        
        serialCommands.Add(HardwareCommand.TurnFirstFanOn, "FAN1_ON");
        serialCommands.Add(HardwareCommand.TurnFirstFanOff, "FAN1_OFF");
        serialCommands.Add(HardwareCommand.TurnSecondFanOn, "FAN2_ON");
        serialCommands.Add(HardwareCommand.TurnSecondFanOff, "FAN2_OFF");
        serialCommands.Add(HardwareCommand.Kick, "KICK");

        if (SerialPort.GetPortNames().Contains(portName))
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
        }
    }

    private void Update()
    {
        if (!isConnected && serialPort != null && serialPort.IsOpen && serialPort.BytesToRead != 0)
        {            
            var readLine = serialPort.ReadLine();
            Debug.Log($"Received response on serial port {portName}: {readLine}");
            if (readLine == expectedResponse)
            {                
                isConnected = true;
            }
            else
            {
                serialPort.Write($"{handshake}\n");
            }
        }
    }

    public static void SendCommand(HardwareCommand command)
    {
        var commandToSend = serialCommands[command];
        Debug.Log($"SendCommand called for {commandToSend} command.");

        if (isConnected)
        {            
            serialPort.Write($"{commandToSend}\n");
            Debug.Log($"Writing command {commandToSend} to port {serialPort.PortName}.");
        }
    }

    private void OnDestroy()
    {
        serialPort?.Close();        
    }
}
