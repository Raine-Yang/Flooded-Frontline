using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SensorState
{
    recharge,
    discharge
}

public class SensorBase : MonoBehaviour
{ 

    public Slider condition;

    public float reduction; // the energy cost per use
    public float recovery;  // the energy recovered per second
    private float current;  // the current condition of the sensor
    public SensorState state;

    // Start is called before the first frame update
    void Start()
    {
        condition = transform.Find("Condition").GetComponent<Slider>();
        state = SensorState.recharge;
        current = condition.maxValue;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void UpdateSensorState()
    {
        if (state == SensorState.recharge)
        {
            current = Mathf.Clamp(current + recovery * Time.deltaTime, condition.minValue, condition.maxValue);
        }
        else if (state == SensorState.discharge)
        {
            current = Mathf.Clamp(current - reduction * Time.deltaTime, condition.minValue, condition.maxValue);
            if (current <= condition.minValue)
            {
                DeactivateSensor();
                state = SensorState.recharge;
            }
        }
        condition.value = current;
    }

    // send sensor message
    public void Invoke(Message message)
    {
        if (current - reduction < condition.minValue)
        {
            return;
        }
        current -= reduction;
        MessageCenter.Instance.SendCustomMessage(message);
    }

    // call Invoke to send sensor message, override in children classes
    public virtual void ActivateSensor()
    {

    }

    // call this method when the energy is used up, override in children classes
    public virtual void DeactivateSensor()
    {

    }
}
