using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject MeteoritePrefab;
    public float Amount;

    private void Start()
    {
        for (int i = 0; i < Amount; i++)
        {
            float xPos = Random.Range(-300f, 300f);
            float yPos = Random.Range(-300f, 300f);
            float zPos = Random.Range(-300f, 300f);

            float xRot = Random.Range(0, 360);
            float yRot = Random.Range(0, 360);
            float zRot = Random.Range(0, 360);

            float Scale = Random.Range(1f, 2f);

            GameObject CurrentMeteore = Instantiate(MeteoritePrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRot, yRot, zRot));
            CurrentMeteore.transform.localScale = new Vector3(Scale, Scale, Scale);
        }
    }
}
