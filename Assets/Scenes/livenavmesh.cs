using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class livenavmesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NavMeshSurface nms = this.gameObject.GetComponent<NavMeshSurface>();

        nms.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
