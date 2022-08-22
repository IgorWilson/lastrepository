using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magicians : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _magician;
    const double EPS = 0.3;
    const float lenForTest = 4;
    private double _timer = 5;

    void Start()
    {
    }
    
    bool IsVectorGood(Vector2 vec, Vector2 poz)
    {
        return MyPhysics.MyRectangularReycast(poz.x, poz.y, 0.7F, 0.5F);
    }

    void Teleportation()
    {
        Vector2 playerPosition = Vector2.zero;
        playerPosition.x = _player.position.x;
        playerPosition.y = _player.position.y;
        int ukChild = 0, countChild = this.gameObject.transform.childCount;
        List<(float, Vector2)> vectors = new List<(float, Vector2)>();
        for(int i = 0; i<100&&ukChild<countChild; ++i)
        {
            Vector2 randomVector = MyVectors.CreateSingleVector();
            if(!IsVectorGood(randomVector, playerPosition+randomVector*lenForTest))
                continue;
            bool f = true;
            float corner = Mathf.Atan2(randomVector.y, randomVector.x);
            for(int j = 0; j<vectors.Count; ++j){
                if(MyVectors.RotationAngleBetweenVectors(randomVector, vectors[j].Item2)<EPS||!IsVectorGood(randomVector, playerPosition+randomVector*lenForTest))
                    f = false;
            }
            if(f){
                Transform child = this.gameObject.transform.GetChild(ukChild);
                ++ukChild;
                child.GetComponent<Magician>().Teleportation(playerPosition+randomVector*lenForTest, 1+0.1*i);
            }
        }
    }

    void Generic(int count){
        Vector2 positionBehindTheScenes = Vector2.zero;
        positionBehindTheScenes.x = -1000;
        positionBehindTheScenes.y = -1000;
        for(int i = 0; i<count; ++i){
            //Instantiate(_magician, positionBehindTheScenes);
        }
    }

    void Update()
    {
        Debug.Log(IsVectorGood(Vector2.zero, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        _timer -= Time.deltaTime;
        if(_timer<0){
            _timer = 5;
            Teleportation();
        }
    }
}
