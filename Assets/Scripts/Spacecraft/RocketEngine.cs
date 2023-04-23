using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
    public float thrust;

    private float throttle;

    private ParticleSystem exhaust;
    private ParticleSystem.MainModule main;
    private Rigidbody2D rigidbody2d;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        exhaust = GetComponentInChildren<ParticleSystem>();
        main = exhaust.main;
    }

    public void SetThrottle(float percent) {
        throttle = percent;
    }

    void FixedUpdate() {
        main.startSpeed = Mathf.Lerp(0, 50, throttle);
        rigidbody2d.AddForce(transform.up * thrust * Mathf.Clamp01(throttle));
    }
}
