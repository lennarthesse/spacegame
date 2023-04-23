using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int vertices;
    private LineRenderer lineRenderer;
    private Rigidbody2D rigidbody2d;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector3[] trajectory = CalculateTrajectory(
            this.rigidbody2d.velocity,
            this.rigidbody2d.position,
            this.rigidbody2d.mass
        );
        DrawOrbit(trajectory);
    }

    Vector3[] CalculateTrajectory(Vector2 velocity, Vector2 position, float mass) {
        Vector3[] trajectory = new Vector3[vertices];
        Vector2 vel = new Vector3(velocity.x, velocity.y);
        Vector2 pos = new Vector3(position.x, position.y);

        for (int i = 0; i < trajectory.Length; i++) {
            trajectory[i] = new Vector3(pos.x, pos.y, 0);
            foreach (Attractor attractor in Universe.attractors) {
                vel += attractor.Attract(vel, pos, mass);
            }
            pos += vel * Time.fixedDeltaTime;
            //if (pos.Equals(new Vector3(position.x, position.y, 0))) return trajectory;
        }
        return trajectory;
    }

    void DrawOrbit(Vector3[] points) {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
