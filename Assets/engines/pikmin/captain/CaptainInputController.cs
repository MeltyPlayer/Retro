using UnityEngine;

namespace Assets.engines.pikmin.captain {
  public class CaptainInputController : MonoBehaviour, IRequiresCaptainState {
    private Camera camera_;
    private CaptainState state_;

    // Start is called before the first frame update
    public void Start() {
      this.camera_ = Camera.main;
    }

    public void Init(CaptainState state) {
      this.state_ = state;
    }

    // Update is called once per frame
    public void Update() {
      var cameraDirection = this.camera_.transform.rotation.eulerAngles.y;

      var rawHeldHorizontalAxis = Input.GetAxis("Horizontal");
      var rawHeldVerticalAxis = Input.GetAxis("Vertical");
      var rawHeldDirection =
          Mathf.Atan2(rawHeldVerticalAxis, rawHeldHorizontalAxis) *
          Mathf.Rad2Deg;

      var heldDirection = cameraDirection + 180 + rawHeldDirection;
      var heldMagnitude =
          Mathf.Sqrt(rawHeldHorizontalAxis * rawHeldHorizontalAxis +
                     rawHeldVerticalAxis * rawHeldVerticalAxis);

      this.state_.Motor.MovePolar(heldDirection, heldMagnitude);

      // TODO: Handle button clicks.
    }
  }
}