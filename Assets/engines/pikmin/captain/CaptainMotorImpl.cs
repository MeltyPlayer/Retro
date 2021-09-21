using System;

using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public class CaptainMotorImpl : ICaptainMotor {
    private const float WALK_SPEED_ = .15f;

    private readonly ICaptain captain_;

    public CaptainMotorImpl(ICaptain captain) {
      this.captain_ = captain;
    }

    public void MovePolar(float direction, float magnitude) {
      var cursorDelta =
          this.captain_.Cursor.MovePolar(direction, magnitude);

      var newDirection = cursorDelta.NewDirection;
      var remainingMagnitude = cursorDelta.RemainingMagnitude;

      if (remainingMagnitude > 0) {
        this.captain_.MovePolar(newDirection,
                                remainingMagnitude *
                                CaptainMotorImpl.WALK_SPEED_ *
                                100 *
                                Time.deltaTime);

        this.captain_.Animator.gameObject.transform.rotation =
            Quaternion.AngleAxis(-newDirection + 90, Vector3.up);
        this.captain_.Animator.SetFloat("Magnitude", magnitude);
      } else {
        this.captain_.Animator.SetFloat("Magnitude", 0);
      }
    }

    public void PressAction() {
      throw new NotImplementedException();
    }

    public void ReleaseAction() {
      throw new NotImplementedException();
    }
  }
}