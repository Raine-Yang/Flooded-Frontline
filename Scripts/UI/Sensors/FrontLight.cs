using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLight : SensorBase
{
    public override void ActivateSensor()
    {
        if (state == SensorState.recharge)
        {
            MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_Sensor, MessageType.Sensor_Spotlight, true));
            state = SensorState.discharge;
        }
        else if (state == SensorState.discharge)
        {
            DeactivateSensor();
        }
    }

    public override void DeactivateSensor()
    {
        MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_Sensor, MessageType.Sensor_Spotlight, false));
        state = SensorState.recharge;
    }
}
