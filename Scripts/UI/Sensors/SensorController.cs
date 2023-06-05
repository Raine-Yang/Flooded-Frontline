using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : UIController
{

    public List<SensorBase> sensors = new List<SensorBase>();


    public override void ReceiveMessage(Message message)
    {
        if (message.Command == MessageType.UI_OpenSensor) 
        {
            Manager.m_UI.SetActive("SensorPanal", true);
        } else if (message.Command == MessageType.UI_CloseSensor)
        {
            Manager.m_UI.SetActive("SensorPanal", false);
        } else if (message.Command == MessageType.UI_Sensor1)
        {
            sensors[0].ActivateSensor();
        } else if (message.Command == MessageType.UI_Sensor2)
        {
            sensors[1].ActivateSensor();
        } else if (message.Command == MessageType.UI_Sensor3)
        {
            sensors[2].ActivateSensor();
        }

    }

    public void UpdateSensors()
    {
        foreach (SensorBase sensor in sensors)
        {
            sensor.UpdateSensorState();
        }
    }
}
