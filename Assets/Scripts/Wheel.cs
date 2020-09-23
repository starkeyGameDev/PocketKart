using UnityEngine;

public class Wheel : MonoBehaviour {
  public bool isSteer = true;
  public bool isDrive = true;
  public GameObject model;
  public WheelCollider collider;

  private void Start() {
    collider = GetComponent<WheelCollider>();
  }

  public void UpdateModel() {
    Quaternion quat;
    Vector3 pos;
    collider.GetWorldPose(out pos, out quat);
    model.transform.position = pos;
    model.transform.rotation = quat;
  }
}
