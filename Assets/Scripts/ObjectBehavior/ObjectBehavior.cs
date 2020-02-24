using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ObjectBehavior : MonoBehaviour
{
    protected List<GameObject> ais = new List<GameObject>();
    [SerializeField] protected Color initialColor = Color.blue;
    [SerializeField] protected Color changeColor = (Color.red + Color.yellow)/2;


    [SerializeField] protected float radius = 15;

    protected GameObject[] allAis;
    //protected void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.gameObject.name + " is enter, plus this is a " + other.gameObject.tag);
    //    if (other.CompareTag("AI"))
    //    {
            
    //        ais.Add(other.gameObject);
            
    //    }
    //}
    
    //protected void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("AI"))
    //    {
    //        ais.Remove(other.gameObject);
            
    //    }
    //}

    [Button]
    public virtual void Action()
    {
        Debug.Log(this.name + " his doing his action to " + ais.Count + " ais !!!");
    }

    protected void Start()
    {
        allAis = GameObject.FindGameObjectsWithTag("AI");
    }
    protected virtual void Update()
    {
        Transform trans = transform;
        //float radius = GetComponent<SphereCollider>().radius;
        foreach (var randomAi in allAis)
        {
            bool alreadyExist = false;
            foreach (var ai in ais)
            {
                if (ai == randomAi)
                {
                    alreadyExist = true;
                    break;
                }
            }


            if (Vector3.Distance(randomAi.transform.position,trans.position) <= radius && !alreadyExist)
            {
                ais.Add(randomAi);
            }
            else if (Vector3.Distance(randomAi.transform.position, trans.position) > radius && alreadyExist)
            {
                ais.Remove(randomAi);
            }
        }


        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (ais.Count == 0)
        {
            meshRenderer.material.color = initialColor;
        }
        else
        {
            meshRenderer.material.color = changeColor;
        }
    }
}
