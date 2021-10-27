using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Character
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void Move(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        public void Stop()
        {
            _agent.SetDestination(transform.position);
        }
    }
}

