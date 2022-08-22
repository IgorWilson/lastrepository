using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : MonoBehaviour
{
    private double TimeTeleport = 0;
    Vector2 PlaceTeleportation;
    [SerializeField] private Transform player;

    void Start()
    {
        
    }

    void Shot(){

    }

    public void Teleportation(Vector2 vec, double t)
    {
        PlaceTeleportation = vec;
        TimeTeleport = t;
    }

    void Update()
    {
        if(TimeTeleport>0){
            TimeTeleport-=Time.deltaTime;
            
            if(TimeTeleport<=0){
                this.gameObject.transform.position = PlaceTeleportation;
            }
        }
    }
}
