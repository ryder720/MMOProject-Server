using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    public GameObject actorPrefab;
    Actors actor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying");
            Destroy(this);
        }
    }

    private void Start()
    {
        actor = InstantiateActor(); 
        actor.Initialize(1, "TestDude"); 
        Server.actors.Add(actor.actorID, actor);
        /* Remove this and make script to spawn all actors.
         * Maybe use a database of some kind to store a ton of actors
         * Then get a unique id for all of them maybe in storage
         */
    }


    public Actors InstantiateActor()
    {
        return Instantiate(actorPrefab, new Vector3(4, 50f, 0), Quaternion.identity).GetComponent<Actors>();
    }



}
