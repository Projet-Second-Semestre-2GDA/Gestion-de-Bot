using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class AIBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool change;

    private Vector3 target;
    [SerializeField] float sizeOfGizmo = 1f;

    [SerializeField] private bool dontMove = false;
    float chronoDontMove;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (!agent.pathPending)
        {
            NavMeshUpdate();
        }
    }
    private void NavMeshUpdate()
    {
        //Assigner une nouvelle destionation a l'agent
        if (agent.remainingDistance <= 1)
        {
            GoToRandomPosition();
        }
        if (change && !dontMove)
        {
            agent.SetDestination(target);
            change = false;
        }
        //Stopper l'agent pendant x temps
        if (chronoDontMove < Time.time && dontMove)
        {
            dontMove = false;
        }
        //Stopper et resume les mouvements de l'agent
        if (dontMove && !agent.isStopped)
        {
            agent.isStopped = true;
        }
        else if (!dontMove && agent.isStopped)
        {
            agent.isStopped = false;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target, sizeOfGizmo);
        Gizmos.DrawLine(transform.position, target);
    }
    [Button(Style = ButtonStyle.FoldoutButton)]
    
    public void GoToRandomPosition()
    {
        Vector3 tempPosition = NavMeshUtil.GetRandomLocation();
        GoToPosition(tempPosition);
    }
    
    public void GoToPosition(Vector3 position)
    {
        target = position;
        change = true;
    }

    [Button(Style = ButtonStyle.FoldoutButton)]
    public void DontMove(float duree = 1f)
    {
        dontMove = true;
        chronoDontMove = Time.time + duree;
    }
}
