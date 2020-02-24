using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulse : ObjectBehavior
{
    [SerializeField] float addForceDuration = 1;
    float chrono = 0;
    [SerializeField] float puissanceMin = 10;
    [SerializeField] float puissanceMax = 100;

    List<GameObject> objectAddForce = new List<GameObject>();
    List<Vector3> directionAddForce = new List<Vector3>();
    List<float> puissanceAddForce = new List<float>();

    public override void Action()
    {
        base.Action();
        if (ais.Count > 0)
        {
            objectAddForce.Clear();
            directionAddForce.Clear();
            puissanceAddForce.Clear();

            foreach (var ai in ais)
            {
                Vector3 direction = ai.transform.position - transform.position;
                float puissance = Mathf.Lerp(puissanceMin, puissanceMax, 1 - (direction.magnitude / radius));

                objectAddForce.Add(ai);
                directionAddForce.Add(direction);
                puissanceAddForce.Add(puissance);
                //ai.GetComponent<Rigidbody>().AddForce(direction.normalized * puissance,ForceMode.Impulse);
            }
            chrono = Time.time + addForceDuration;

        ais.Clear();
        }
    }

    protected override void Update()
    {
        float deltaTime = Time.deltaTime;
        float time = Time.time;
        base.Update();
        if (time <= chrono)
        {
            for (int i = 0; i < objectAddForce.Count; i++)
            {
                objectAddForce[i].transform.position += directionAddForce[i].normalized * deltaTime * (puissanceAddForce[i] * Mathf.Abs(time - chrono));
            }
        }
    }

}
