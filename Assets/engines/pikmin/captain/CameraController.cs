using UnityEngine;

namespace Assets.engines.pikmin.captain {
  public class CameraController : MonoBehaviour {
    private const float CAMERA_DISTANCE = 100;
    private const float CAMERA_Z_DIRECTION = -17;

    // TODO: If still, pressing left trigger rotates towards the cursor, not the player.
    // TODO: Holding left trigger rotates w/ player movement, based on x component of horizontal

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

      var delta = new Vector3(x, z, y);

      var toPosition = this.transform.position;
      var fromPosition = toPosition - delta;

      this.camera_.transform.position =
          Vector3.Lerp(this.camera_.transform.position,
                       fromPosition,
                       .05f * Time.deltaTime * 100);
      this.camera_.transform.LookAt(this.camera_.transform.position + delta);
    }
  }
}