using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actors : MonoBehaviour
{
    [SerializeField] private Faction _faction;

    public int actorID;
    public string actorName;
    public Vector3 spawnLocation;
    public float attackRange;
    public bool aggresive;
    public float moveSpeed;

    public Faction faction => _faction; // So it can't be changed
    public StateMachine stateMachine => GetComponent<StateMachine>();
    
    public Transform target { get; private set; }

    private void Awake()
    {
        spawnLocation = transform.position;
    }

    public void Initialize(int _id, string _actorName)
    {
        actorID = _id;
        actorName = _actorName;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(IdleState),new IdleState(this) }
        };
    }
}
