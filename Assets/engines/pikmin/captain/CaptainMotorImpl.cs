using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public class CaptainMotorImpl : ICaptainMotor {
    private const float CURSOR_SPEED_ = .1f;

    private readonly ICaptain captain_;

    public CaptainMotorImpl(ICaptain captain) {
      this.captain_ = captain;
    }

    public void MovePolar(float direction, float magnitude) {
      var cursorDelta =
          this.captain_.Cursor.MovePolar(direction, magnitude * CURSOR_SPEED_);

      var newDirection = cursorDelta.NewDirection;
      var remainingDistance = cursorDelta.RemainingDistance;

      if (remainingDistance > 0) {
        this.captain_.MovePolar(newDirection, remainingDistance);

        this.captain_.Animator.gameObject.transform.rotation =
            Quaternion.AngleAxis(-newDirection + 90, Vector3.up);
        this.captain_.Animator.SetFloat("Magnitude", magnitude);
      } else {
        this.captain_.Animator.SetFloat("Magnitude", 0);
      }

      Debug.Log(this.captain_.Animator.GetFloat("Magnitude"));
    }

    public void PressAction() {
      throw new NotImplementedException();
    }

    public void ReleaseAction() {
      throw new NotImplementedException();
    }
  }
}