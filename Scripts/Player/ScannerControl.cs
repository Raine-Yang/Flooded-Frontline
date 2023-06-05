using OD.Effect.HDRP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerControl : MonoBase
{

    private ScanAnimation scanAnimation;

    void Awake()
    {
        SensorManager.Instance.Register(this);  
    }

    // Start is called before the first frame update
    void Start()
    {
        scanAnimation = GetComponent<ScanAnimation>();
        scanAnimation.customInput = true;
    }


    public override void ReceiveMessage(Message message)
    {
        if (message.Command == MessageType.Sensor_Radar)
        {
            scanAnimation.mode = ScanAnimation.Mode.Scan;
            Scan((bool)message.Content);
        } else if (message.Command == MessageType.Sensor_StealthVision)
        {
            scanAnimation.mode = ScanAnimation.Mode.StealthVision;
            Scan((bool)message.Content);
        }
    }


    void Scan(bool openStealthVision)
    {
        //Scan Mode
        if (scanAnimation.mode == ScanAnimation.Mode.Scan)
        {
            scanAnimation.StartScan(); // start the Scan
        }
        else
        {
            //Steath Vision Mode
            if (openStealthVision &&  scanAnimation.CurrentState == ScanAnimation.CurState.End)
                scanAnimation.StartStealthVision(); // start Steath Vision
            else if (!openStealthVision && scanAnimation.CurrentState == ScanAnimation.CurState.StartSteatlhVision)
                scanAnimation.EndStealthVision(); // end Steath Vision

        }
    }
}
