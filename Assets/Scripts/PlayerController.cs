using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject[] shoulders;
    GameObject activeShoulder;
    SpriteRenderer[] renderers;

    [SerializeField]
    SO_PlayerSettings playerSettings;
    [SerializeField] float rotationSpeed;
    [SerializeField] float innerAngle;
    [SerializeField] float outerAngle;
    // Raise Modifier = +-1 and is used to make sure the active arm always moves upward when scrolling up
    float raiseModifier;
    [SerializeField]
    Vector3 currentRotation;
  
    // Start is called before the first frame update
    void Start()
    {
        this.rotationSpeed = playerSettings.rotationSpeed;
        this.outerAngle = playerSettings.outerAngle;
        this.innerAngle = playerSettings.innerAngle;
<<<<<<< Updated upstream
        leftIdleRotation = shoulders[0].transform.rotation;
        rightIdleRotation = shoulders[1].transform.rotation;
=======
>>>>>>> Stashed changes
        activeShoulder = shoulders[0];
        activeShoulder.transform.localRotation = Quaternion.Euler(currentRotation);
        raiseModifier = 1;
    }

    void Update()     
    {
        SelectArm();
        RenderActiveArm();
        // Moves the selected arm
<<<<<<< Updated upstream

        shoulders[0].transform.rotation = Quaternion.Lerp(shoulders[0].transform.rotation, leftIdleRotation, 3f*Time.deltaTime);
        shoulders[1].transform.rotation = Quaternion.Lerp(shoulders[1].transform.rotation, rightIdleRotation, 3f*Time.deltaTime);

        if (Input.GetKey(KeyCode.Space)){
            activeShoulder.transform.Rotate(rotationSpeed * raiseModifier * Vector3.forward);
=======

            activeShoulder.transform.Rotate(-rotationSpeed * raiseModifier * Input.GetAxis("Mouse ScrollWheel") * Vector3.forward);
>>>>>>> Stashed changes
            currentRotation = activeShoulder.transform.rotation.eulerAngles;
        if(activeShoulder == shoulders[0])
            currentRotation.z = Mathf.Clamp(currentRotation.z, outerAngle, innerAngle);

<<<<<<< Updated upstream
            if (activeShoulder == shoulders[1])
            {
                ArmInContactWithFloor.RightArmIsInContactWithFloor = false;
                currentRotation.z = Mathf.Clamp(currentRotation.z, 360 - innerAngle, 360 - outerAngle);
            }
=======
       /* if (activeShoulder == shoulders[1])
        {
            if (currentRotation.z < 180 - innerAngle || currentRotation.z > 180)
                currentRotation.z = 180 - innerAngle;
>>>>>>> Stashed changes

            if (currentRotation.z > 180 - outerAngle)
                currentRotation.z = 180 - outerAngle;

        }*/

        activeShoulder.transform.localRotation = Quaternion.Euler(currentRotation);

        
    }

    int Heavi(float x)
    {
        if (x < 0)
            return 0;
        else
            return 1;
    }

    float RectStep(float a, float b)
    {
        return Heavi(a) - Heavi(b);
    }

    void SelectArm()
    {
        //On left mouse click, the left arm will be movable
        if (Input.GetMouseButton(0))
        {
            activeShoulder = shoulders[0];
            raiseModifier = 1;
        }

        // The same with the right mouse click and right arm
        else if (Input.GetMouseButton(1))
        {
            activeShoulder = shoulders[1];
            raiseModifier = -1;
        }
    }
    void RenderActiveArm()
    {
        foreach (GameObject shoulder in shoulders)
        {
            renderers = shoulder.GetComponentsInChildren<SpriteRenderer>();
            if (shoulder == activeShoulder)
            {
                foreach (SpriteRenderer ren in renderers)
                {
                    ren.color = Color.red;
                }
            }
            else
            {
                foreach (SpriteRenderer ren in renderers)
                {
                    ren.color = Color.grey;
                }
            }

        }
    }
}
