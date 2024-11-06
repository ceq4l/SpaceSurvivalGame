using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonScriptHandler : MonoBehaviour
{
    PhotonView pv;

    public List<MonoBehaviour> ScriptsToDelete = new List<MonoBehaviour>();
    public List<GameObject> ObjectsToDelete = new List<GameObject>();
    public List<GameObject> ObjectsToKeep = new List<GameObject>();

    public GameObject PlayerModelHolder;
    public GameObject PlayerModel;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        if (!pv.IsMine)
        {
            for (int i = 0; i < ScriptsToDelete.Count; i++)
            {
                Destroy(ScriptsToDelete[i]);
            }

            for (int i = 0; i < ObjectsToDelete.Count; i++)
            {
                Destroy(ObjectsToDelete[i]);
            }

            Instantiate(PlayerModel, PlayerModelHolder.transform);
        }

        if (pv.IsMine)
        {
            for (int i = 0; i < ObjectsToKeep.Count; i++)
            {
                Destroy(ObjectsToKeep[i]);
            }
        }
    }
}
