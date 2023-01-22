using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Inerhitance | Create base class for this class and MoverForSimplePaths
public class MoverForFlyingEnemy : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float altitude = 10f;
    [SerializeField] float endPointDetectionTolerance = 0.5f;
    [SerializeField] private string pathTag = "Path";
    private List<Cell> _waypoints = new();
    private LivePoints _livePoints;
    private Vector3 _startPosition, _endPosition;

    private void Awake()
    {
        _livePoints = GameObject.FindObjectOfType<LivePoints>();
        FindPathAndFillList();
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        GetStartAndEndPointFromPath();
        // if (transform.position != _waypoints[0].transform.position) 
        TeleportToFirstWaypoint();
    }

    private void TeleportToFirstWaypoint() => transform.position = _startPosition;

    private void DeactivateEnemyAndStealLive()
    {
        gameObject.SetActive(false);
        _livePoints.DecreaseLivePoints();
    }

    private void Move()
    {
        transform.LookAt(_endPosition);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (CloseToEndPoint()) DeactivateEnemyAndStealLive();
    }

    private void GetStartAndEndPointFromPath()
    {

        _startPosition = _waypoints[0].transform.position;
        _endPosition = _waypoints[_waypoints.Count - 1].transform.position;
        _startPosition.y = altitude;

        // TODO Should flying enemies land?
        _endPosition.y = altitude;
        // _endPosition.y = 1f;
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

    private bool CloseToEndPoint()
    {
        return Vector3.Distance(transform.position, _endPosition) <= endPointDetectionTolerance;
    }
}
