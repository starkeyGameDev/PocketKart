using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DriveController : MonoBehaviour {
  public float torque = 500f;
  public float maxAngle = 10f;
  public float maxRPM = 200f;

  public TextMeshProUGUI speed;
  public Transform CoG;

  Wheel[] colliders;
  Wheel driveWheel;

  void Start() {
    colliders = GetComponentsInChildren<Wheel>();
    GetComponent<Rigidbody>().centerOfMass = CoG.localPosition;
    driveWheel = getDrivingWheel();
  }

  void FixedUpdate() {
    float acc = Input.GetAxis("Vertical");
    float turn = Input.GetAxis("Horizontal");

    foreach (Wheel w in colliders) {
      if (w.isDrive) {
        Go(w, acc);
      }
      if (w.isSteer) {
        Turn(w, turn);
      }
      w.UpdateModel();
    }
    speed.text = getSpeed().ToString() + " km/h";
  }

  void Go(Wheel w, float acc) {
    acc = Mathf.Clamp(acc, -1, 1);
    float thrust = acc * torque;
    if (Mathf.Abs(w.collider.rpm) < maxRPM)
      w.collider.motorTorque = thrust;
    else
      w.collider.motorTorque = 0;
  }

  void Turn(Wheel w, float turn) {
    turn = Mathf.Clamp(turn, -1, 1) * maxAngle;
    w.collider.steerAngle = turn;
  }

  int getSpeed() {
    if (driveWheel != null) {
      return Mathf.RoundToInt(
        driveWheel.collider.radius * Mathf.PI * driveWheel.collider.rpm // meters per minute
        * 60 / 1000 // kilometers per hour
      );
    }
    return 0;
  }

  Wheel getDrivingWheel() {
    foreach (Wheel w in colliders) {
      if (w.isDrive) {
        return w;
      }
    }
    return null;
  }
}
