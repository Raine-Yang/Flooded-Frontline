using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthVision : SensorBase
{

    public override void ActivateSensor()
    {
        if (state == SensorState.recharge) {
            MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_Sensor, MessageType.Sensor_StealthVision, true));
            state = SensorState.discharge;
        } else if (state == SensorState.discharge)
        {
            DeactivateSensor();
        }
    }

    public override void DeactivateSensor()
    {
        MessageCenter.Instance.SendCustomMessage(new Message(MessageType.Type_Sensor, MessageType.Sensor_StealthVision, false));
        state = SensorState.recharge;
    }
}
