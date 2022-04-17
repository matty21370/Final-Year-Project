using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.UI
{
    public class WaypointPath : MonoBehaviour
    {
        private static WaypointPath _instance;

        public static WaypointPath Instance => _instance;

        private LineRenderer _lineRenderer;
        private NavMeshAgent _meshAgent;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _meshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetDestination(Transform destination)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _meshAgent.SetDestination(destination.position);
            
            SetPath();
            
            _meshAgent.isStopped = true;
        }

        private void SetPath()
        {
            _lineRenderer.positionCount = _meshAgent.path.corners.Length;

            for (int i = 0; i < _meshAgent.path.corners.Length; i++)
            {
                _lineRenderer.SetPosition(i, _meshAgent.path.corners[i]);
            }
        }
    }
}