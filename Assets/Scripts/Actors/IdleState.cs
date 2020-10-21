using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private Actors _actor;


    public IdleState(Actors actor) : base(actor.gameObject)
    {
        _actor = actor;
    }

    public override Type Tick()
    {
        
        if (_actor.aggresive)
        {
            var _chaseTarget = GetAggro();
        }

        return null;
    }

    public Transform GetAggro()
    {
        // Need to get a collider set up for aggro DEF NOT READY TO IMPLEMENT
        return null;
    }
}
