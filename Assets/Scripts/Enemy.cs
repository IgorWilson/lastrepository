using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _pointForSearching;
    [HideInInspector] public Transform PointForSearching { get => _pointForSearching; private set => _pointForSearching = value; }
}
