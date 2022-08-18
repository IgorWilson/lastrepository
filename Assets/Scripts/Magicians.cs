using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magicians : MonoBehaviour
{
    [SerializeField] private Transform player;
    const double EPS = 0.3;
    const float lenForTest = 4;
    double timer = 5;

    void Start()
    {
    }
    
    bool IsVectorGood(Vector2 vec)
    {
        return true;
    }

    void Teleportation()
    {
        int countChild = this.gameObject.transform.childCount;
        List<(float, Vector2)> vectors = new List<(float, Vector2)>();
        for(int i = 0; i<100; ++i)
        {
            Vector2 randomVector = MyVectors.CreateSingleVector();

            bool f = true;
            float corner = Mathf.Atan2(randomVector.y, randomVector.x);
            for(int j = 0; j<vectors.Count; ++j){
                if(MyVectors.RotationAngleBetweenVectors(randomVector, vectors[j].Item2)<EPS||!IsVectorGood(randomVector))
                    f = false;
            }
            if(f)
                vectors.Add((corner, randomVector));
        }

        //vectors.Sort();
        //Debug.Log(vectors.Count);
        for(int i = 0; i<countChild; ++i)
        {
            Transform child = this.gameObject.transform.GetChild(i);
            Vector2 positionTeleportation = Vector2.zero;
            positionTeleportation.x = player.position.x+vectors[i].Item2.x*lenForTest;
            positionTeleportation.y = player.position.y+vectors[i].Item2.y*lenForTest;
            child.GetComponent<Magician>().Teleportation(positionTeleportation, 1+0.1*i);
        }

    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0){
            timer = 5;
            Teleportation();
        }
    }
}
