using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public class CameraController : MonoBehaviour {
    private const float CAMERA_DISTANCE = 20;
    private const float CAMERA_Z_DIRECTION = -35;

    private Camera camera_;

    private float direction_;

    public void Start() {
      this.camera_ = Camera.main;
    }

    public void Update() {
      var x = CAMERA_DISTANCE *
              Mathf.Cos(CAMERA_Z_DIRECTION * Mathf.Deg2Rad) *
              Mathf.Cos(this.direction_ * Mathf.Deg2Rad);
      var y = CAMERA_DISTANCE *
              Mathf.Cos(CAMERA_Z_DIRECTION * Mathf.Deg2Rad) *
              Mathf.Sin(this.direction_ * Mathf.Deg2Rad);
      var z = CAMERA_DISTANCE * Mathf.Sin(CAMERA_Z_DIRECTION * Mathf.Deg2Rad);

      var toPosition = this.transform.position;
      var fromPosition = toPosition - new Vector3(x, z, y);

      this.camera_.transform.position = fromPosition;
      this.camera_.transform.LookAt(toPosition);
    }
  }
}