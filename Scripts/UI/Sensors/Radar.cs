using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : SensorBase
{
    
    public override void ActivateSensor()
    {
        Invoke(new Message(MessageType.Type_Sensor, MessageType.Sensor_Radar, false));
    }
}
