using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject L_Target;
    public GameObject R_Target;

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
    [SerializeField] float returnScale;

    Vector3 leftIdlePos;
    Vector3 rightIdlePos;

    [SerializeField] private AudioClip[] smashSounds;
    private AudioSource source;
    public Vector3 enemyPos;
    public float attackDelay = 1;
    public bool currentlyAttacking;
    public Animator cameraAnimator;
    public int l_health;
    public int r_health;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

        currentlyAttacking = false;

        leftIdlePos = L_Target.transform.position;
        rightIdlePos = R_Target.transform.position;

        //For changing shoulder active/inactive sprites    
        L_ArmRenderer = L_Arm.GetComponent<SpriteRenderer>();
        L_ForeArmRenderer = L_ForeArm.GetComponent<SpriteRenderer>();
        R_ArmRenderer = R_Arm.GetComponent<SpriteRenderer>();
        R_ForeArmRenderer = R_ForeArm.GetComponent<SpriteRenderer>();
    }

    void Update()     
    {
        //return to idle pos
        if (!Input.GetMouseButton(0)) {
            L_Target.transform.position = Vector3.Lerp(L_Target.transform.position, leftIdlePos, returnScale*Time.fixedDeltaTime);
        }
        if (!Input.GetMouseButton(1)) {
            R_Target.transform.position = Vector3.Lerp(R_Target.transform.position, rightIdlePos, returnScale*Time.fixedDeltaTime);
        }

        SelectArm();
    }
    
    public IEnumerator RaiseArmThenSmash()
    {
        Vector3 aboveEnemyPos = new Vector3(enemyPos.x, enemyPos.y + 3, enemyPos.z);
        float timePassed = 0;

        while (timePassed < attackDelay)
        {
            timePassed += Time.deltaTime;

            // Calculate the interpolation factor based on time passed
            float t = timePassed / attackDelay;

            // Use Vector3.Lerp to smoothly interpolate between positions
            L_Target.transform.position = Vector3.Lerp(L_Target.transform.position, aboveEnemyPos, t);

            yield return null;
        }

        // Ensure the final position is exactly where you want it
        L_Target.transform.position = aboveEnemyPos;

        // Wait for a short duration
        yield return new WaitForSeconds(0.1f);

        // Set the final position to the original position
        L_Target.transform.position = enemyPos;
        currentlyAttacking = true;
        cameraAnimator.Play("ScreenShake");
        source.clip = this.smashSounds[Random.Range(0, smashSounds.Length)];
        source.PlayOneShot(this.source.clip);
    }
    void SelectArm()
    {
        //On left mouse click, the left arm will be movable
        if (Input.GetMouseButton(0))
        {
            L_ArmRenderer.sprite = L_ArmON;
            L_ForeArmRenderer.sprite = L_ForeArmON;
            R_ArmRenderer.sprite = R_ArmOFF;
            R_ForeArmRenderer.sprite = R_ForeArmOFF;
        }

        // The same with the right mouse click and right arm
        else if (Input.GetMouseButton(1))
        {
            L_ArmRenderer.sprite = L_ArmOFF;
            L_ForeArmRenderer.sprite = L_ForeArmOFF;
            R_ArmRenderer.sprite = R_ArmON;
            R_ForeArmRenderer.sprite = R_ForeArmON;
        }
    }
}
