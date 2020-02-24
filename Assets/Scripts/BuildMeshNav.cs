using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildMeshNav : MonoBehaviour
{
        
    void Awake()
    {
        var surface = GetComponent(typeof(NavMeshSurface)) as NavMeshSurface;
        surface.BuildNavMesh();
    }
}
