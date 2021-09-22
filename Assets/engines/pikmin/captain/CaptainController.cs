using Assets.common.math;
using Assets.engines.pikmin.captain.cursor;

using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.engines.pikmin.captain {
  [RequireComponent(typeof(BCaptainMovement))]
  [RequireComponent(typeof(CaptainInputController))]
  public class CaptainController : MonoBehaviour, ICaptain {
    private CaptainState state_;

    // Start is called before the first frame update
    public void Start() {
      var animator = this.GetComponentInChildren<CaptainAnimator>();
      Assert.IsNotNull(animator, "Animator is null!");

      var input = this.GetComponent<CaptainInputController>();
      Assert.IsNotNull(input, "Input is null!");

      var motor = this.GetComponent<BCaptainMovement>();
      Assert.IsNotNull(motor, "Motor is null!");

      this.state_ = new CaptainState {
          Captain = this,
          Animator = animator,
          Movement = motor,
      };

      input.Init(this.state_);
      motor.Init(this.state_);
    }

    // Update is called once per frame
    public void Update() {}

    public Vector3 GlobalPosition => this.transform.position;
    public Vector3 LocalPosition => this.transform.localPosition;

    public void MovePolar(float direction, float magnitude) {
      var animator = this.state_.Animator;

      animator.WalkingInPlaceMagnitude = 0;
      if (magnitude > 0) {
        // Lerps rotation.
        var fromDirection = animator.Direction;
        var toDirection = direction;
        animator.Direction =
            Interpolation.Degrees(fromDirection,
                                  toDirection,
                                  .1f * 100 * Time.deltaTime);

        // Moves player.
        var maxSpeed = CaptainConstants.WALK_SPEED *
                       100 *
                       Time.deltaTime;
        var x = maxSpeed * magnitude * Mathf.Cos(direction * Mathf.Deg2Rad);
        var y = maxSpeed * magnitude * Mathf.Sin(direction * Mathf.Deg2Rad);

        this.transform.position += new Vector3(x, 0, y);
        animator.RunningMagnitude = magnitude;
      } else {
        animator.RunningMagnitude = 0;
      }
    }

    public void FaceTowards(float direction, float magnitude) {
      var animator = this.state_.Animator;

      animator.RunningMagnitude = 0;
      if (magnitude > 0) {
        animator.Direction = direction;
        animator.WalkingInPlaceMagnitude = 1;
      } else {
        animator.WalkingInPlaceMagnitude = 0;
      }
    }
  }
}