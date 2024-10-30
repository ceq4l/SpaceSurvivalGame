using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public BuildClass CurrentBuild;
    BuildClass LastBuild;

    GameObject BuildPreview;

    GameObject MainPlatform;

    private void Start()
    {
        MainPlatform = GameObject.FindGameObjectWithTag("MainPlatform");
    }

    private void Update()
    {
        if (InventoryManager.instance.SelectedItem && InventoryManager.instance.SelectedItem.GetBuild())
            CurrentBuild = InventoryManager.instance.SelectedItem.GetBuild();
        else
            CurrentBuild = null;

        if (LastBuild != CurrentBuild)
        {
            LastBuild = CurrentBuild;
            UpdateBuilds();
        }

        if (CurrentBuild)
        {
            if (BuildPreview && Input.GetMouseButtonDown(0) && !PlayerMovement.Instance.PauseInput)
                PlaceBuild();

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (CurrentBuild.buildType == BuildClass.BuildType.BasePart)
                {
                    if (hit.transform.GetComponent<SnapPoint>() && !hit.transform.GetComponent<SnapPoint>().FilledBuild)
                    {
                        SnapPoint currentSnap = hit.transform.GetComponent<SnapPoint>();

                        for (int i = 0; i < currentSnap.AcceptedBuilds.Count; i++)
                        {
                            if (currentSnap.AcceptedBuilds[i] == CurrentBuild)
                            {
                                if (!BuildPreview)
                                    BuildPreview = Instantiate(CurrentBuild.BuildPreview);

                                BuildPreview.transform.position = currentSnap.transform.position;
                                BuildPreview.transform.rotation = currentSnap.transform.rotation;

                                return;
                            }
                        }
                    }
                }
                else if (CurrentBuild.buildType == BuildClass.BuildType.Machine)
                {
                    if (hit.transform.tag == "Platform")
                    {
                        if (!BuildPreview)
                            BuildPreview = Instantiate(CurrentBuild.BuildPreview);

                        Vector3 SnapPositon = new Vector3(Mathf.Round(hit.point.x * 10) / 10, hit.point.y, Mathf.Round(hit.point.z * 10) / 10);

                        BuildPreview.transform.position = SnapPositon;
                        return;
                    }
                }
                
            }
        }

        if (BuildPreview)
            Destroy(BuildPreview);
    }

    public void PlaceBuild()
    {
        InventoryManager.instance.ConsumeSelectedItem();
        Instantiate(CurrentBuild.BuildPrefab, BuildPreview.transform.position, BuildPreview.transform.rotation, MainPlatform.transform);
    }

    void UpdateBuilds()
    {
        if (BuildPreview)
            Destroy(BuildPreview);

        GameObject[] FoundGrids = GameObject.FindGameObjectsWithTag("Grid");

        foreach (GameObject grid in FoundGrids)
        {
            if (CurrentBuild && CurrentBuild.buildType == BuildClass.BuildType.Machine)
                grid.GetComponent<MeshRenderer>().enabled = true;
            else
                grid.GetComponent<MeshRenderer>().enabled = false;
        }

        if (CurrentBuild)
        {
            GameObject[] FoundSnapPointObjects = GameObject.FindGameObjectsWithTag("SnapPoint");

            foreach (GameObject SnapObject in FoundSnapPointObjects)
            {
                SnapPoint objectSnap = SnapObject.GetComponent<SnapPoint>();
                SnapObject.transform.GetComponent<BoxCollider>().enabled = false;

                for (int i = 0; i < objectSnap.AcceptedBuilds.Count; i++)
                {
                    if (objectSnap.AcceptedBuilds[i] == CurrentBuild)
                    {
                        SnapObject.transform.GetComponent<BoxCollider>().enabled = true;
                        break;
                    }
                }
            }
        }
    }
}
