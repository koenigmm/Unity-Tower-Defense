using System.Collections;
using UnityEngine;

public class MoverForSimplePaths : MoverAbstract
{
    private void Awake() => SetReferences();

    private void OnEnable()
    {
        ResetSpeed();
        GetStartAndEndPointFromPath();
        TeleportToFirstWaypoint();
        StartCoroutine(Move());
    }

    private void OnDisable() => StopAllCoroutines();

    IEnumerator Move()
    {
        foreach (var waypoint in _waypoints)
        {
            if (waypoint.b_isStart) continue;

            Vector3 startPosition = transform.position;
            float travelPercentage = 0f;

            transform.LookAt(waypoint.transform.position);

            while (travelPercentage < 1f)
            {
                travelPercentage += Time.deltaTime * currentSpeed;
                transform.position = Vector3.Lerp(startPosition, waypoint.transform.position, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
        DeactivateEnemyAndStealLive();
    } 
    override protected void GetStartAndEndPointFromPath()
    {
        _startPosition = _waypoints[0].transform.position;
        _endPosition = _waypoints[_waypoints.Count - 1].transform.position;
    }  
}
