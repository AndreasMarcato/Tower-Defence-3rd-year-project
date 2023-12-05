using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    public Collapsible subscribe;
    public NavMeshSurface _surface;

    // Start is called before the first frame update
    private void Start()
    {
        //subscribe.Rebuild += RebuildNavMesh;
    }

    private void RebuildNavMesh()
    {
        _surface.BuildNavMesh();
    }

}
