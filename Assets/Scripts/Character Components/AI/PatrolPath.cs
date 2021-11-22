using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character.AI
{
    public class PatrolPath : MonoBehaviour
    {
        private List<Waypoint> _waypoints = new List<Waypoint>();

        private void Awake()
        {
            foreach (var waypoint in GetComponentsInChildren<Waypoint>())
            {
                _waypoints.Add(waypoint);
            }
        }

        public int GetNextWaypoint(int index)
        {
            if (index + 1 == _waypoints.Count) //reached waypoint limit
            {
                return 0;
            }

            return index + 1;
        }

        public Vector3 GetCurrentWaypoint(int index)
        {
            return _waypoints[index].transform.position;
        }
    }
}