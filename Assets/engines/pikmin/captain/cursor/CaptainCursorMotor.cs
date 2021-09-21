using System;

using UnityEngine;

namespace Assets.engines.pikmin.captain.cursor {
  public class CaptainCursorMotor : BCaptainMotor {
    private CaptainState state_;
    private ICaptainCursor cursor_;

    public void Start() {
      this.cursor_ = this.GetComponentInChildren<CaptainCursorController>();
    }

    public override void Init(CaptainState state) {
      this.state_ = state;
    }

    public override void MovePolar(float direction, float magnitude) {
      var cursorDelta = this.cursor_.MovePolar(direction, magnitude);

      var newDirection = cursorDelta.NewDirection;
      var remainingMagnitude = cursorDelta.RemainingMagnitude;

      this.state_.Captain.MovePolar(newDirection, remainingMagnitude);
    }

    public override void PressAction() {
      throw new NotImplementedException();
    }

    public override void ReleaseAction() {
      throw new NotImplementedException();
    }
  }
}