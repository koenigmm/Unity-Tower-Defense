using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoverAbstract : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] protected float speed = 1.0f;
    [SerializeField] protected string pathTag = "Path";
    protected List<Cell> _waypoints = new();
    protected LivePoints _livePoints;
    protected Vector3 _startPosition, _endPosition;

    protected void DeactivateEnemyAndStealLive()
    {
        gameObject.SetActive(false);
        _livePoints.DecreaseLivePoints();
    }

    protected void FindPathAndFillList()
    {
        var path = GameObject.FindGameObjectWithTag(pathTag);
        var cells = path.GetComponentsInChildren<Cell>();

        foreach (var cell in cells)
        {
            _waypoints.Add(cell);
        }
    }

    protected void TeleportToFirstWaypoint() => transform.position = _startPosition;

    protected void SetReferences()
    {
        _livePoints = GameObject.FindObjectOfType<LivePoints>();
        FindPathAndFillList();
    }

    protected abstract void GetStartAndEndPointFromPath();
}