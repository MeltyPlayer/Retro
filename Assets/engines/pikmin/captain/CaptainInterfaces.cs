using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public interface ICaptain {
    Vector3 GlobalPosition { get; }

    Vector3 LocalPosition { get; }

    float Direction { get; }

    ICaptainMotor Motor { get; }
    ICaptainCursor Cursor { get; }

    void MovePolar(float direction, float distance);
  }

  public interface ICaptainCursor {
    Vector3 GlobalPosition { get; }
    Vector3 LocalPosition { get; }

    float Direction { get; set; }
    float Distance { get; set; }
    float Radius { get; set; }

    /// <summary>
    ///   Returns the remainder of the distance that couldn't be moved.
    /// </summary>
    float MovePolar(float direction, float distance);
  }

  public interface ICaptainMotor {
    void MovePolar(float direction, float distance);

    void PressAction();
    void ReleaseAction();
  }
}
