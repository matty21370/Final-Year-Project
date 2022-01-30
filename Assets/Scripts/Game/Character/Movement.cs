using System;
using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Character
{
    public class Movement : MonoBehaviour, ISaveable
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

        public void Move(float x, float y, float z)
        {
            _agent.SetDestination(new Vector3(x, y, z));
        }

        public void Move(GameObject g)
        {
            _agent.SetDestination(g.transform.position);
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
            _agent.speed = currentSpeed;
        }

        public float GetBaseSpeed()
        {
            return baseSpeed;
        }
        
        public void SetInjured(bool value)
        {
            _animator.SetBool("injured", value);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 serializableVector3 = (SerializableVector3) state;
            _agent.Warp(serializableVector3.ToVector());
        }
    }
}

