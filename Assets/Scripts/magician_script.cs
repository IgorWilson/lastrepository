using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magician_script : MonoBehaviour
{
    private double TimeTeleport = 0;
    Vector2 PlaceTeleportation;
    void Start()
    {
        
    }

    public void Teleportation(Vector2 vec, double t)
    {
        PlaceTeleportation = vec;
        TimeTeleport = t;
    }

    void Update()
    {
        if(TimeTeleport>0){
            TimeTeleport-=Time.fixedDeltaTime;
            
            if(TimeTeleport<=0){
                this.gameObject.transform.position = PlaceTeleportation;
            }
        }
    }
}
