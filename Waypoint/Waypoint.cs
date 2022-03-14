using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurrentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

    // Start is called before the first frame update
    private void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }
    // this method allows us to know where the position is of the index
    public Vector3 GetWaypointPosition(int index)
    {
        return CurrentPosition + Points[index];
    }
    private void OnDrawGizmos()
    {
        if (!_gameStarted && transform.hasChanged)
        {
            _currentPosition = transform.position;
        }
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPosition, 0.5f);

            if (i < points.Length - 1)
                // not last position on the land
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);
                // draws line between gizmos
            }
        }
    }
}
