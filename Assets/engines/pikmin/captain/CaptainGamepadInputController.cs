using UnityEngine;

namespace Assets.engines.pikmin.captain {
  public class CaptainGamepadInputController : MonoBehaviour, IRequiresCaptainState {
    private Camera camera_;
    private CaptainStateBundle stateBundle_;

    // Start is called before the first frame update
    public void Start() {
      this.camera_ = Camera.main;
    }

    public void Init(CaptainStateBundle stateBundle)
      => this.stateBundle_ = stateBundle;

    // Update is called once per frame
    public void Update() {
      this.UpdateMovement_();

      // TODO: Handle button clicks.
    }

    private void UpdateMovement_() {
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

      this.stateBundle_.MovementController.MovePolar(
          heldDirection,
          heldMagnitude);
    }
  }
}