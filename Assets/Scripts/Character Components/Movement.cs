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

        [SerializeField] private float baseSpeed = 5.3f;
        private float currentSpeed;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentSpeed = baseSpeed;
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

        public void SetSpeed(float value)
        {
            currentSpeed = value;
        }

        public float GetBaseSpeed()
        {
            return baseSpeed;
        }
        
        public void SetInjured(bool value)
        {
            _animator.SetBool("injured", value);
        }
    }
}

