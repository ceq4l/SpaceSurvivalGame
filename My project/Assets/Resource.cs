using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float Health;
    public ItemClass Drop;

    public float DropProgress;

    public bool MineRock;


    private void Update()
    {
        if (MineRock)
        {
            Health -= 5f * Time.deltaTime;
            DropProgress += 0.25f * Time.deltaTime;
        }

        if (Health <= 0)
            Destroy(gameObject);
    }
}
