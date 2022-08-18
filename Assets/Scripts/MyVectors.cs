using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVectors : MonoBehaviour
{
    const float PI = 3.1415926F;

    public static float RotationAngleBetweenVectors(Vector2 a, Vector2 b){
        float angleInRadiansA = Mathf.Atan2(a.y, a.x);
        float angleInRadiansB = Mathf.Atan2(b.y, b.x);
        Debug.Log(Mathf.Min(Mathf.Abs(angleInRadiansA-angleInRadiansB), PI*2-Mathf.Abs(angleInRadiansA-angleInRadiansB)));
        return Mathf.Min(Mathf.Abs(angleInRadiansA-angleInRadiansB), PI*2-Mathf.Abs(angleInRadiansA-angleInRadiansB));
    }

    public static Vector2 CreateSingleVector(){
        Vector2 randomVector = Vector2.zero;
        randomVector.x = Random.Range(-1000, 1000);
        randomVector.y = Random.Range(-1000, 1000);
        float len = Mathf.Sqrt(randomVector.x*randomVector.x+randomVector.y*randomVector.y);
        randomVector.x = randomVector.x/len;
        randomVector.y = randomVector.y/len;
        return randomVector;
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
