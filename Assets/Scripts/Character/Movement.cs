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

        private Animator _animator;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Animate();
        }

        public void Move(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        public void Stop()
        {
            _agent.SetDestination(transform.position);
        }

        private void Animate()
        {
            Vector3 velocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat("speed", velocity.z);
        }
    }
}

