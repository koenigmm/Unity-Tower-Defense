using UnityEngine;

public class MoverForFlyingEnemy : MoverAbstract
{
    [SerializeField] private float altitude = 10f;
    [SerializeField] private float endPointDetectionTolerance = 0.5f;


    private void Awake() => SetReferences();

    private void Update() => Move();

    private void OnEnable()
    {
        ResetSpeed();
        GetStartAndEndPointFromPath();
        TeleportToFirstWaypoint();
    }

    private void Move()
    {
        transform.LookAt(_endPosition);
        transform.Translate(Vector3.forward * (currentSpeed * Time.deltaTime));
        if (CloseToEndPoint()) DeactivateEnemyAndStealLive();
    }

    override protected void GetStartAndEndPointFromPath()
    {

        _startPosition = _waypoints[0].transform.position;
        _endPosition = _waypoints[_waypoints.Count - 1].transform.position;
        _startPosition.y = altitude;

        // TODO Should flying enemies land?
        _endPosition.y = altitude;
        // _endPosition.y = 1f;
    }

    private bool CloseToEndPoint() =>
         Vector3.Distance(transform.position, _endPosition) <= endPointDetectionTolerance;
}
