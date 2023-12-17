using UnityEngine;
using UnityEngine.AI;


public class UnitComponentNavMesh : UnitComponent
    {
        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        protected override void UpdatePosition()
        {
            agent.destination = GetFinalPosition();
        }

        protected override void OnCollisionEnter(Collision collision)
        {
           base.OnCollisionEnter(collision);
            if (!collision.gameObject.CompareTag("Plane"))
            {
                agent.isStopped = true;
            }
        }
    }