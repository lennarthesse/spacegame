using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCSThruster : MonoBehaviour
{
    public float thrust;

    private ParticleSystem exhaust;
    private Rigidbody2D rigidbody2d;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        exhaust = GetComponentInChildren<ParticleSystem>();
    }

    public void Thrust() {
        exhaust.Play();
        rigidbody2d.AddForce(transform.up * thrust);
    }
}
