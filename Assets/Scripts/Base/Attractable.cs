using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        Universe.attractables.Add(this);
    }

    void OnDisable() {
        Universe.attractables.Remove(this);
    }
}
