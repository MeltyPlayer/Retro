using UnityEngine;

namespace Assets.engines.pikmin.captain.cursor {
  public struct CursorDelta {
    public float RemainingMagnitude { get; set; }
    public float NewDirection { get; set; }
  }

  public interface ICaptainCursor {
    Vector3 GlobalPosition { get; }
    Vector3 LocalPosition { get; }

    float Direction { get; set; }
    float Distance { get; set; }

    /// <summary>
    ///   Returns the remainder of the distance that couldn't be moved.
    /// </summary>
    CursorDelta MovePolar(float direction, float magnitude);
  }
}
