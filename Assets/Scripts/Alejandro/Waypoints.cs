using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour 
{
    public static List<Transform> LeftWaypoints;
    public static List<Transform> LeftArmWaypoints;
    public static List<Transform> RightWaypoints;
    public static List<Transform> RightArmWaypoints;
    public static List<Transform> SecondSegmentLeft;
    public static List<Transform> SecondSegmentRight;
    public static List<Transform> LastSegmentRight;
    public static List<Transform> LastSegmentLeft;

    [SerializeField] GameObject leftArmWaypointsGameObject;
    [SerializeField] GameObject leftGameObject;
    [SerializeField] GameObject rightArmWaypointsGameObject;
    [SerializeField] GameObject rightGameObject;
    [SerializeField] GameObject secondSegmentLeft;
    [SerializeField] GameObject secondSegmentRight;
    [SerializeField] GameObject lastSegmentRightGO;
    [SerializeField] GameObject lastSegmentLeftGO;

    int leftCount;
    int leftArmCount;
    int rightArmCount;
    int rightCount;
    int secondSegmentLeftCount;
    int secondSegmentRightCount;
    int lastSegmentLeftCount;
    int lastSegmentRightCount;

    private void Awake()
    {
        LeftWaypoints = new List<Transform>();
        RightWaypoints = new List<Transform>();
        LeftArmWaypoints = new List<Transform>();
        RightArmWaypoints = new List<Transform>();
        SecondSegmentLeft = new List<Transform>();
        SecondSegmentRight = new List<Transform>();
        LastSegmentLeft = new List<Transform>();
        LastSegmentRight = new List<Transform>();
        

        secondSegmentLeftCount = secondSegmentLeft.transform.childCount;
        secondSegmentRightCount = secondSegmentRight.transform.childCount;
        leftCount = leftGameObject.transform.childCount;
        leftArmCount = leftArmWaypointsGameObject.transform.childCount;
        rightArmCount = rightArmWaypointsGameObject.transform.childCount;
        rightCount = rightGameObject.transform.childCount;
        lastSegmentLeftCount = lastSegmentLeftGO.transform.childCount;
        lastSegmentRightCount = lastSegmentRightGO.transform.childCount;

        for(int i = 0; i < leftCount; i++)
        {
            LeftWaypoints.Add(leftGameObject.transform.GetChild(i));
        }

        for(int i=0; i < rightArmCount; i++)
        {
            RightArmWaypoints.Add(rightArmWaypointsGameObject.transform.GetChild(i));
        }        
        
        for(int i=0; i < leftArmCount; i++)
        {
            LeftArmWaypoints.Add(leftArmWaypointsGameObject.transform.GetChild(i));
        }
        
        for(int i=0; i<secondSegmentLeftCount; i++)
        {
            SecondSegmentLeft.Add(secondSegmentLeft.transform.GetChild(i));
        }

        for(var i=0; i < secondSegmentRightCount; i++)
        {
            SecondSegmentRight.Add(secondSegmentRight.transform.GetChild(i));
        }

        for(int i=0; i < lastSegmentRightCount; i++)
        {
            LastSegmentRight.Add(lastSegmentRightGO.transform.GetChild(i));
        }

        for(int i=0;i < lastSegmentLeftCount; i++)
        {
            LastSegmentLeft.Add(lastSegmentLeftGO.transform.GetChild(i));
        }

        for(int i = 0; i < rightCount; i++)
        {
            RightWaypoints.Add(rightGameObject.transform.GetChild(i));
        }
    }
}