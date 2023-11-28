using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Vector3 L_enemyPos;
    public Vector3 R_enemyPos;
    public float attackDelay = 1;
    public bool L_preparingAttack;
    public bool R_preparingAttack;
    public bool L_currentlyAttacking;
    public bool R_currentlyAttacking;
    public Animator cameraAnimator;
    public int l_health;
    public int r_health;
    public static int health;
    public EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        L_preparingAttack = false;
        R_preparingAttack = false;
        L_currentlyAttacking = false;
        R_currentlyAttacking = false;

        leftIdlePos = L_Target.transform.position;
        rightIdlePos = R_Target.transform.position;
    }

    void Update()     
    {
        //return to idle pos
        L_Target.transform.position = Vector3.Lerp(L_Target.transform.position, leftIdlePos, returnScale*Time.fixedDeltaTime);
        R_Target.transform.position = Vector3.Lerp(R_Target.transform.position, rightIdlePos, returnScale*Time.fixedDeltaTime);
    }
    public static void GetDamaged()
    {
        health--;
        print(health);
        if(health <= -15)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public IEnumerator RaiseArmThenSmash(string enemyType)
    {
        Vector3 L_aboveEnemyPos = new Vector3(L_enemyPos.x, L_enemyPos.y + 2.4f, L_enemyPos.z);
        Vector3 L_belowEnemyPos = new Vector3(L_enemyPos.x, L_enemyPos.y - 3, L_enemyPos.z);
        Vector3 R_aboveEnemyPos = new Vector3(R_enemyPos.x, R_enemyPos.y + 2.4f, R_enemyPos.z);
        Vector3 R_belowEnemyPos = new Vector3(R_enemyPos.x, R_enemyPos.y - 3, R_enemyPos.z);
        float timePassed = 0;
        switch(enemyType)
        {
            case "l_ground": case "l_sky": case "l_shield": case "ml_ground": case "ml_shield":
                L_preparingAttack = true;
            break;

            case "r_ground": case "r_sky": case "r_shield": case "mr_ground": case "mr_shield":
                R_preparingAttack = true;
            break;
        }

        while (timePassed < attackDelay)
        {
            timePassed += Time.deltaTime;

            // Calculate the interpolation factor based on time passed
            float t = timePassed / attackDelay;
            switch(enemyType)
            {
                case "l_ground": case "ml_ground": case "l_shield": case "ml_shield":
                    // Use Vector3.Lerp to smoothly interpolate between positions
                    L_Target.transform.position = Vector3.Lerp(L_Target.transform.position, L_aboveEnemyPos, t);
                    yield return null;
                    L_Target.transform.position = L_aboveEnemyPos;
                break;

                case "l_sky":
                    // Use Vector3.Lerp to smoothly interpolate between positions
                    L_Target.transform.position = Vector3.Lerp(L_Target.transform.position, L_belowEnemyPos, t);
                    yield return null;
                    L_Target.transform.position = L_belowEnemyPos;
                break;

                case "r_ground": case "mr_ground": case "r_shield": case "mr_shield":
                    R_Target.transform.position = Vector3.Lerp(R_Target.transform.position, R_aboveEnemyPos, t);
                    yield return null;
                    R_Target.transform.position = R_aboveEnemyPos;
                break;

                case "r_sky":
                    // Use Vector3.Lerp to smoothly interpolate between positions
                    R_Target.transform.position = Vector3.Lerp(R_Target.transform.position, R_belowEnemyPos, t);
                    yield return null;
                    R_Target.transform.position = R_belowEnemyPos;
                break;
            }
        }
        yield return new WaitForSeconds(0.1f);

        // Set the final position to the original position

        switch(enemyType)
            {
                case "l_ground": case "l_sky": case "ml_ground": case "l_shield": case "ml_shield":
                L_Target.transform.position = L_enemyPos;
                L_currentlyAttacking = true;
                yield return new WaitForSeconds(0.1f);
                L_preparingAttack = false;
                break;

                case "r_ground": case "r_sky": case "mr_ground": case "r_shield": case "mr_shield":
                R_Target.transform.position = R_enemyPos;
                R_currentlyAttacking = true;
                yield return new WaitForSeconds(0.1f);
                R_preparingAttack = false;
                break;
            }
        cameraAnimator.Play("ScreenShake");
        source.clip = this.smashSounds[Random.Range(0, smashSounds.Length)];
        source.PlayOneShot(this.source.clip);
    }
}
