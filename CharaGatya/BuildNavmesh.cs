
using UnityEngine;
using UnityEngine.AI;

public class BuildNavmesh : MonoBehaviour
{
    public void Start()
    {
        // NavMeshをビルドする
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}