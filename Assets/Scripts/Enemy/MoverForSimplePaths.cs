using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverForSimplePaths : MonoBehaviour
{
    [SerializeField] private List<Cell> waypoints = new();
    [SerializeField] float timeBetweenTwoWaypoints = 1.0f;

    private void Start()
    {
       StartCoroutine(PrintWaypointsNames());
    }

    private void Update()
    {
        
    }

    IEnumerator PrintWaypointsNames()
    {
        foreach (var waypoint in waypoints)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(timeBetweenTwoWaypoints);
        }
    }
}
