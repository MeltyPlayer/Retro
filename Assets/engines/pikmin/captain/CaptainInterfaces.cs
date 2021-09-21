using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public interface ICaptain {
    Vector3 GlobalPosition { get; }
    Vector3 LocalPosition { get; }

    ICaptainMotor Motor { get; }
    ICaptainCursor Cursor { get; }
    Animator Animator { get; }

    void MovePolar(float direction, float distance);
  }

  public struct CursorDelta {
    public float RemainingMagnitude { get; set; }
    public float NewDirection { get; set; }
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
    CursorDelta MovePolar(float direction, float magnitude);
  }

  public interface ICaptainMotor {
    void MovePolar(float direction, float distance);

    void PressAction();
    void ReleaseAction();
  }
}
