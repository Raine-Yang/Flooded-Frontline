using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightControl : MonoBase
{
    void Awake()
    {
        SensorManager.Instance.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveMessage(Message message)
    {
        Debug.Log("receive message");
        if (message.Command == MessageType.Sensor_Spotlight)
        {
            Debug.Log("receive valid message");
            transform.Find("Camera").Find("Spot Light").gameObject.SetActive((bool)message.Content);
        }
    }
}
