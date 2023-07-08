using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmInContactWithFloor : MonoBehaviour
{
    public static bool RightArmIsInContactWithFloor;
    public static bool LeftArmIsInContactWithFloor;

    private void Awake()
    {
        RightArmIsInContactWithFloor = true;
        LeftArmIsInContactWithFloor = true;
    }
}
