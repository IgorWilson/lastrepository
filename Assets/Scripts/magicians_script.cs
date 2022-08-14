using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicians_script : MonoBehaviour
{
    private Transform player;
    const double PI = 3.1415926535*2;
    const double EPS = PI/100;

    void Start()
    {
        Teleportation();
    }
    
    bool IsItVectorGood(Vector2 vec){
        return true;
    }

    void Teleportation()
    {
        int countChild = this.gameObject.transform.childCount;
        List<KeyValuePair<float, Vector2>> vectors = new List<KeyValuePair<float, Vector2>>();
        for(int i = 0; i<100; ++i){

            Vector2 newVector = Vector2.zero;
            newVector.x = Random.Range(-1000, 1000);
            newVector.y = Random.Range(-1000, 1000);
            float len = Mathf.Sqrt(newVector.x*newVector.x+newVector.y*newVector.y);
            newVector.x = newVector.x/len;
            newVector.y = newVector.y/len;
            float corner = Mathf.Atan2(newVector.y, newVector.x);

            bool f = true;
            for(int j = 0; j<vectors.Count; ++j){
                double diff = Mathf.Abs(vectors[j].Key-corner);
                if(diff<EPS||PI-diff<EPS)
                    f = false;
            }
            if(f)
                vectors.Add(new KeyValuePair<float, Vector2>(corner, newVector));
        }

        //vectors.Sort();
        int uk = 0;
        for(int i = 0; i<countChild; ++i){
            while(!IsItVectorGood(vectors[uk].Value)){
                ++uk;
            }
            Transform child = this.gameObject.transform.GetChild(i);
            child.GetComponent<magician_script>().Teleportation(Vector2.zero, 1);
            ++uk;
        }

    }

    void Update()
    {
        //Teleportation();
    }
}
