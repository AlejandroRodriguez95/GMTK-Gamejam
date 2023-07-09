using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftHandHitbox"))
        {
            ArmInContactWithFloor.LeftArmIsInContactWithFloor = true;
        }
        if (collision.CompareTag("RightHandHitbox"))
        {
            ArmInContactWithFloor.RightArmIsInContactWithFloor = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftHandHitbox"))
        {
            ArmInContactWithFloor.LeftArmIsInContactWithFloor = false;
        }
        if (collision.CompareTag("RightHandHitbox"))
        {
            ArmInContactWithFloor.RightArmIsInContactWithFloor = false;
        }
    }
}
