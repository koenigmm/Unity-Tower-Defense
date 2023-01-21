using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverForSimplePaths : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1.0f;
    [SerializeField] private string pathTag = "Path";
    private List<Cell> _waypoints = new();


    private void Awake()
    {
        FindPathAndFillList();
    }

    private void OnEnable()
    {
        if (transform.position != _waypoints[0].transform.position) TeleportToFirstWaypoint();
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
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
                travelPercentage += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, waypoint.transform.position, travelPercentage);
                yield return new WaitForEndOfFrame();
            }

        }
        // TODO Steal lives
        gameObject.SetActive(false);
    }

    private void FindPathAndFillList()
    {
        var path = GameObject.FindGameObjectWithTag(pathTag);
        var cells = path.GetComponentsInChildren<Cell>();

        foreach (var cell in cells)
        {
            _waypoints.Add(cell);
        }
    }

    private void TeleportToFirstWaypoint() => transform.position = _waypoints[0].transform.position;
}
