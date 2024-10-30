using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public List<BuildClass> AcceptedBuilds = new List<BuildClass>();
    public BuildClass FilledBuild;

    GameObject Check;

    public LayerMask CheckLayer;

    private void Start()
    {
        Check = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!FilledBuild)
        {
            RaycastHit hit;
            if (Physics.Raycast(Check.transform.position, Vector3.down, out hit, 0.1f, CheckLayer))
            {
                if (hit.transform.parent.parent.GetComponent<BuildObject>())
                    FilledBuild = hit.transform.parent.parent.GetComponent<BuildObject>().Build;
            }
        }
    }
}