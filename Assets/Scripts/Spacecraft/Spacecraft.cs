using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Spacecraft : MonoBehaviour
{
    // Spacecraft metrics //
    public float shipLength;
    public float shipWidth;
    public float shipMass;

    // Spacecraft parts //
    public RocketEngine mainDrive;
    public RocketEngine[] boosters = new RocketEngine[2];
    public RCSThruster rcs1;
    public RCSThruster rcs2;
    public RCSThruster rcs3;
    public RCSThruster rcs4;
    public RCSThruster rcs5;
    public RCSThruster rcs6;
    public RCSThruster rcs7;
    public RCSThruster rcs8;
    public RCSThruster rcs9;
    public RCSThruster rcs10;
    public RCSThruster rcs11;
    public RCSThruster rcs12;

    // HUD elements //
    private Slider throttleSlider;
    private Toggle toggleBoosters;
    private Toggle toggleSAS;
    private Dropdown navigation;
    private Text IMUDisplay;
    private Text metricsDisplay;

    // Internal //
    private Rigidbody2D rigidbody2d;
    private Vector2 lastVelocity = new Vector2();
    private float throttleIncrease = 0.02f;
    private float SASThreshold = 0.15f;

    void Start() {
        throttleSlider = GameObject.Find("/Canvas/Throttle").GetComponent<Slider>();
        toggleBoosters = GameObject.Find("/Canvas/ToggleBoosters").GetComponent<Toggle>();
        toggleSAS = GameObject.Find("/Canvas/ToggleSAS").GetComponent<Toggle>();
        navigation = GameObject.Find("/Canvas/Directions").GetComponent<Dropdown>();
        IMUDisplay = GameObject.Find("/Canvas/IMU").GetComponent<Text>();
        metricsDisplay = GameObject.Find("/Canvas/Metrics").GetComponent<Text>();

        rigidbody2d = this.GetComponent<Rigidbody2D>();

        shipMass = 0f;
        foreach (Rigidbody2D rb2d in GetComponentsInChildren<Rigidbody2D>()) {
            shipMass += rb2d.mass;
        }

        metricsDisplay.text = "Ship Length: " + shipLength + " m\nShip Width: " + shipWidth + " m\nShip Mass: " + (shipMass * 1000).ToString("N0") + " kg";
    }

    void FixedUpdate() {
        // Forward
        if (Input.GetKey(KeyCode.W)) {
            rcs3.Thrust();
            rcs6.Thrust();
            rcs9.Thrust();
            rcs12.Thrust();
        }

        // Backward
        if (Input.GetKey(KeyCode.S)) {
            rcs1.Thrust();
            rcs4.Thrust();
            rcs7.Thrust();
            rcs10.Thrust();
        }

        // Left
        if (Input.GetKey(KeyCode.A)) {
            rcs8.Thrust();
            rcs11.Thrust();
        }

        // Right
        if (Input.GetKey(KeyCode.D)) {
            rcs2.Thrust();
            rcs5.Thrust();
        }

        // Rotate left
        if (Input.GetKey(KeyCode.Q)) {
            rcs5.Thrust();
            rcs8.Thrust();
        }

        // Rotate right
        if (Input.GetKey(KeyCode.E)) {
            rcs2.Thrust();
            rcs11.Thrust();
        }

        // SAS
        if (!(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) && toggleSAS.isOn) {
            if (rigidbody2d.angularVelocity < 0 - SASThreshold) {
                rcs5.Thrust();
                rcs8.Thrust();
            } else if (rigidbody2d.angularVelocity > 0 + SASThreshold) {
                rcs2.Thrust();
                rcs11.Thrust();
            }
        }
    }

    void Update() {
        // SAS Toggle
        if (Input.GetKeyDown(KeyCode.R)) {
            if (toggleSAS.isOn) {
                toggleSAS.isOn = false;
            } else {
                toggleSAS.isOn = true;
            }
        }

        // Main throttle & boosters
        if (Input.GetKeyDown(KeyCode.B)) {
            if (toggleBoosters.isOn) {
                toggleBoosters.isOn = false;
                foreach (RocketEngine booster in boosters) {
                    booster.SetThrottle(throttleSlider.minValue);
                }
            } else {
                toggleBoosters.isOn = true;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            throttleSlider.value += throttleIncrease;
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            throttleSlider.value -= throttleIncrease;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            throttleSlider.value = throttleSlider.maxValue;
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            throttleSlider.value = throttleSlider.minValue;
        }

        mainDrive.SetThrottle(throttleSlider.value);
        if (toggleBoosters.isOn) {
            foreach (RocketEngine booster in boosters) {
                booster.SetThrottle(throttleSlider.value);
            }
        }

        UpdateIMU();
    }

    void UpdateIMU() {
        float acceleration = (rigidbody2d.velocity - lastVelocity).magnitude / Time.fixedDeltaTime;
        lastVelocity = rigidbody2d.velocity;
        IMUDisplay.text = "Velocity: " + rigidbody2d.velocity.magnitude.ToString("N2") + " m/s\nAngular Vel.: " + rigidbody2d.angularVelocity.ToString("N2") + "deg/s\nAcceleration: " + acceleration.ToString("N2") + " m/s^2";
    }
}
