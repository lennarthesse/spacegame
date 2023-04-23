using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] private float mass;
    public Rigidbody2D rigidbody2d;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.mass = mass;
    }

    void FixedUpdate() {
        foreach (Attractable attractable in Universe.attractables) {
            attractable.rigidbody2d.velocity += Attract(
                attractable.rigidbody2d.velocity,
                attractable.rigidbody2d.position,
                attractable.rigidbody2d.mass
            );
        }
    }

    void OnEnable() {
        Universe.attractors.Add(this);
    }

    void OnDisable() {
        Universe.attractors.Remove(this);
    }

    public Vector2 Attract(Vector2 velocity, Vector2 position, float mass) {
        Vector2 direction = this.rigidbody2d.position - position;
        float distance = direction.magnitude;
        
        if (distance != 0f) {
            float forceMagnitude = (this.rigidbody2d.mass * mass * Universe.G) / Mathf.Pow(distance, 2);
            Vector2 force = forceMagnitude * direction.normalized;

            velocity = (force / mass) * Time.fixedDeltaTime;
            return velocity;
        }

        return new Vector2();
    }
}
