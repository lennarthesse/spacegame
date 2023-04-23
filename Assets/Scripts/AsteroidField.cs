using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    [SerializeField] private int number;
    [SerializeField] private int radius;

    void Start() {
        for (int i = 0; i < number; i++) {
            Instantiate(asteroid, Random.insideUnitCircle * radius, Quaternion.identity);
        }
    }
}
