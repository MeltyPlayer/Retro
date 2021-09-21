using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.engines.pikmin.olimar {
  public class CaptainMotorImpl : ICaptainMotor {
    private readonly ICaptain captain_;

    public CaptainMotorImpl(ICaptain captain) {
      this.captain_ = captain;
    }

    public void MovePolar(float direction, float distance) {
      var remainingDistance =
          this.captain_.Cursor.MovePolar(direction, distance);

      this.captain_.MovePolar(direction, remainingDistance);
    }

    public void PressAction() {
      throw new NotImplementedException();
    }

    public void ReleaseAction() {
      throw new NotImplementedException();
    }
  }
}