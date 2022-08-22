using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : MonoBehaviour
{
    public static bool MyRectangularReycast(float x, float y, float width, float height){
        float xL = x-width/2, xR = x+width/2;
        float yD = y-height/2, yU = y+height/2;
        RaycastHit2D tr; 
        //Debug.Log(x+" "+y+" "+width+" "+height);
        tr = Physics2D.Raycast(MyVectors.createVector2(xL, yD), MyVectors.createVector2(xR-xL, yU-yD), Mathf.Sqrt(width*width+height*height));
        Debug.Log(MyVectors.createVector2(xL, yD).x);
        if(tr.collider != null){
            return false;
        }
        return true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
