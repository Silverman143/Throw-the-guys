using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class Vibration 
{
    public static void Hit()
    {
        MMVibrationManager.TransientHaptic(0.3f, 0.2f);
    }

    public static void Spikes()
    {
        MMVibrationManager.TransientHaptic(0.56f, 0.4f);
    }
}
