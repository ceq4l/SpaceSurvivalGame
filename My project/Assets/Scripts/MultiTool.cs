using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTool : MonoBehaviour
{
    public ParticleSystem Lazer;
    public float Distance;
    public float Damage;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Lazer.Play();

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Distance))
            {
                if (hit.transform && hit.transform.GetComponent<Resource>())
                {
                    Resource Target = hit.transform.GetComponent<Resource>();
                    Target.Health -= Damage * Time.deltaTime;
                    Target.DropProgress += Damage / 2 * Time.deltaTime;

                    if (Target.DropProgress > 1)
                    {
                        Target.DropProgress = 0;

                        GameObject DropObject = Instantiate(Target.Drop.GetMisc().Drop);
                        DropObject.transform.position = hit.point;
                        DropObject.transform.rotation = Quaternion.Euler(hit.normal);

                        DropObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 1, ForceMode.Impulse);
                    }
                }
            }
        }
        else
            Lazer.Stop();
    }
}
