using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour 
{
    public static List<Transform> leftWaypoints;
    public static List<Transform> rightWaypoints;

    [SerializeField] GameObject leftGameObject;
    [SerializeField] GameObject rightGameObject;

    int leftCount;
    int rightCount;

    private void Awake()
    {
        leftWaypoints = new List<Transform>();
        rightWaypoints = new List<Transform>();

        leftCount = leftGameObject.transform.childCount;
        rightCount = rightGameObject.transform.childCount;

        for(int i = 0; i < leftCount; i++)
        {
            leftWaypoints.Add(leftGameObject.transform.GetChild(i));
        }

        for(int i = 0; i < rightCount; i++)
        {
            rightWaypoints.Add(rightGameObject.transform.GetChild(i));
        }
    }
}
