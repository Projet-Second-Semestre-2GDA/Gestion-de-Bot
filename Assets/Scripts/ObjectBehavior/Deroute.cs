using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deroute : ObjectBehavior
{
    [SerializeField] float timeStun = 5f;
    public override void Action()
    {
        base.Action();
        foreach (var ai in ais)
        {
            AIBehavior aiBis = ai.GetComponent<AIBehavior>();
            aiBis.GoToRandomPosition();
            aiBis.DontMove(timeStun);


        }
            ais.Clear();
    }
}
