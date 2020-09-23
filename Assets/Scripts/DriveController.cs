using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : MonoBehaviour {
  public float torque = 200f;
  public float maxAngle = 30;

  Wheel[] colliders;


  void Start() {
    colliders = GetComponentsInChildren<Wheel>();
  }

  void Update() {
    float acc = Input.GetAxis("Vertical");
    float turn = Input.GetAxis("Horizontal");

    foreach(Wheel w in colliders){
      if (w.isDrive) {
        Go(w, acc);
      } if (w.isSteer) {
        Turn(w, turn);
      }
      w.UpdateModel();
    }
  }

  void Go(Wheel w, float acc) {
    acc = Mathf.Clamp(acc, -1, 1);
    float thrust = acc * torque;
    w.collider.motorTorque = thrust;
  }

  void Turn(Wheel w, float turn) {
    turn = Mathf.Clamp(turn, -1, 1) * maxAngle;
    w.collider.steerAngle = turn;
  }
}
