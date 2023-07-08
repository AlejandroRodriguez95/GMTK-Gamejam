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
    
  
    // Start is called before the first frame update
    void Start()
    {
        this.rotationSpeed = playerSettings.rotationSpeed;
        this.outerAngle = playerSettings.outerAngle;
        this.innerAngle = playerSettings.innerAngle;
        activeShoulder = shoulders[0];
        raiseModifier = 1;
    }

    void Update()     
    {
        SelectArm();
        RenderActiveArm();
        // Moves the selected arm
        activeShoulder.transform.Rotate(- rotationSpeed * raiseModifier * Input.GetAxis("Mouse ScrollWheel") * Vector3.forward);
        Vector3 currentRotation = activeShoulder.transform.rotation.eulerAngles;
        if (activeShoulder == shoulders[0])
        {
            currentRotation.z = Mathf.Clamp(currentRotation.z, outerAngle, innerAngle);
        } else
        {
            currentRotation.z = Mathf.Clamp(currentRotation.z, -innerAngle+180, -outerAngle+180);
        }
        
        activeShoulder.transform.rotation = Quaternion.Euler(currentRotation);

        
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
