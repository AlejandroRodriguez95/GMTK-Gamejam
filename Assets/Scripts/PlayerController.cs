using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject[] shoulders;
    GameObject activeShoulder;
    SpriteRenderer[] renderers;

    //For changing shoulder active/inactive sprites
    public GameObject L_ForeArm;
    public GameObject R_ForeArm;
    public GameObject L_Arm;
    public GameObject R_Arm;
    public Sprite L_ForeArmOFF;
    public Sprite L_ArmOFF;
    public Sprite R_ForeArmOFF;
    public Sprite R_ArmOFF;
    public Sprite L_ForeArmON;
    public Sprite L_ArmON;
    public Sprite R_ForeArmON;
    public Sprite R_ArmON;
    SpriteRenderer L_ForeArmRenderer;
    SpriteRenderer L_ArmRenderer;
    SpriteRenderer R_ArmRenderer;
    SpriteRenderer R_ForeArmRenderer;
    
    

    [SerializeField]
    SO_PlayerSettings playerSettings;
    float rotationSpeed;
    [SerializeField]float innerAngle;
    [SerializeField]float outerAngle;
    // Raise Modifier = +-1 and is used to make sure the active arm always moves upward when scrolling up
    float raiseModifier;
    Quaternion leftIdleRotation;
    Quaternion rightIdleRotation;
    [SerializeField]
    Vector3 currentRotation;
  
    // Start is called before the first frame update
    void Start()
    {
        this.rotationSpeed = playerSettings.rotationSpeed;
        this.outerAngle = playerSettings.outerAngle;
        this.innerAngle = playerSettings.innerAngle;
        leftIdleRotation = shoulders[0].transform.rotation;
        rightIdleRotation = shoulders[1].transform.rotation;
        activeShoulder = shoulders[0];
        activeShoulder.transform.rotation = Quaternion.Euler(currentRotation);
        raiseModifier = 1;

        //For changing shoulder active/inactive sprites    
        L_ArmRenderer = L_Arm.GetComponent<SpriteRenderer>();
        L_ForeArmRenderer = L_ForeArm.GetComponent<SpriteRenderer>();
        R_ArmRenderer = R_Arm.GetComponent<SpriteRenderer>();
        R_ForeArmRenderer = R_ForeArm.GetComponent<SpriteRenderer>();
        L_ArmRenderer.sprite = L_ArmON;
        L_ForeArmRenderer.sprite = L_ForeArmON;
        R_ArmRenderer.sprite = R_ArmOFF;
        R_ForeArmRenderer.sprite = R_ForeArmOFF;
    }
    void FixedUpdate()
    {

    }
    void Update()     
    {
        SelectArm();
        // Returns arm to idle position
        shoulders[0].transform.rotation = Quaternion.Lerp(shoulders[0].transform.rotation, leftIdleRotation, Time.deltaTime);
        shoulders[1].transform.rotation = Quaternion.Lerp(shoulders[1].transform.rotation, rightIdleRotation, Time.deltaTime);

        if (Input.GetKey(KeyCode.Space)){
            Debug.Log("Space Pressed");
            activeShoulder.transform.Rotate(rotationSpeed * raiseModifier * Vector3.forward);
            currentRotation = activeShoulder.transform.rotation.eulerAngles;
            
           if (activeShoulder == shoulders[0])
            {
                ArmInContactWithFloor.LeftArmIsInContactWithFloor = false;
                currentRotation.z = Mathf.Clamp(currentRotation.z, outerAngle, innerAngle);
            }

           if (activeShoulder == shoulders[1])
            {
                ArmInContactWithFloor.RightArmIsInContactWithFloor = false;
                currentRotation.z = Mathf.Clamp(currentRotation.z, 360 - innerAngle, 360 - outerAngle);
            }

            activeShoulder.transform.rotation = Quaternion.Euler(currentRotation);

        }

        if (shoulders[0].transform.rotation == leftIdleRotation && ArmInContactWithFloor.LeftArmIsInContactWithFloor == false)
            ArmInContactWithFloor.LeftArmIsInContactWithFloor = true;

        if (shoulders[1].transform.rotation == rightIdleRotation && ArmInContactWithFloor.RightArmIsInContactWithFloor == false)
            ArmInContactWithFloor.RightArmIsInContactWithFloor = true;


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
            L_ArmRenderer.sprite = L_ArmON;
            L_ForeArmRenderer.sprite = L_ForeArmON;
            R_ArmRenderer.sprite = R_ArmOFF;
            R_ForeArmRenderer.sprite = R_ForeArmOFF;
        }

        // The same with the right mouse click and right arm
        else if (Input.GetMouseButton(1))
        {
            activeShoulder = shoulders[1];
            raiseModifier = -1;
            L_ArmRenderer.sprite = L_ArmOFF;
            L_ForeArmRenderer.sprite = L_ForeArmOFF;
            R_ArmRenderer.sprite = R_ArmON;
            R_ForeArmRenderer.sprite = R_ForeArmON;
        }
    }
   
}
