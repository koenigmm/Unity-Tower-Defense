using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverForSimplePaths : MonoBehaviour
{
    [SerializeField] private List<Cell> waypoints = new();
    [SerializeField][Range(0f, 5f)] float speed = 1.0f;

    private void Awake()
    {
        if (transform.position != waypoints[0].transform.position) TeleportToFirstWaypoint();
    }

    private void Start()
    {
        StartCoroutine(PrintWaypointsNames());
    }
    IEnumerator PrintWaypointsNames()
    {
        foreach (var waypoint in waypoints)
        {
            Vector3 startPosition = transform.position;
            float travelPercentage = 0f;

            transform.LookAt(waypoint.transform.position);

            while (travelPercentage < 1f)
            {
                travelPercentage += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, waypoint.transform.position, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void TeleportToFirstWaypoint() => transform.position = waypoints[0].transform.position;
}
