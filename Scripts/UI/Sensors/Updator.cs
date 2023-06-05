using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updator : MonoBehaviour
{

    private SensorController sensorController;
    // Start is called before the first frame update
    void Start()
    {
        sensorController = transform.Find("SensorPanal").GetComponent<SensorController>();
    }

    // Update is called once per frame
    void Update()
    {
        sensorController.UpdateSensors();
    }
}
