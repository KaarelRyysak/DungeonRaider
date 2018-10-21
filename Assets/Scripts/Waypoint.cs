using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public Waypoint Next;

    void Start(){}
    void Update(){}
    public Waypoint GetNextWaypoint()
    {
        return Next;
    }

    void OnDrawGizmos()
    {
        if (Next == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, Next.transform.position);
    }

}
