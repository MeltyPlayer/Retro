using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public class CaptainCursorController : MonoBehaviour, ICaptainCursor {
    private const float MAX_DISTANCE_ = 7;

    private bool automaticallyUpdatePosition_ = false;

    // Start is called before the first frame update
    public void Start() {}

    // Update is called once per frame
    public void Update() {}

    public Vector3 GlobalPosition => this.transform.position;
    public Vector3 LocalPosition => this.transform.localPosition;

    public float Direction {
      get => this.direction_;
      set {
        this.direction_ = value;
        this.UpdatePosition_();
      }
    }

    private float direction_;

    public float Distance {
      get => this.distance_;
      set {
        this.distance_ = value;
        this.UpdatePosition_();
      }
    }

    private float distance_;

    public float Radius { get; set; }

    public CursorDelta MovePolar(float direction, float distance) {
      var initX = this.Distance * Mathf.Cos(this.Direction * Mathf.Deg2Rad);
      var initY = this.Distance * Mathf.Sin(this.Direction * Mathf.Deg2Rad);

      var deltaX = distance * Mathf.Cos(direction * Mathf.Deg2Rad);
      var deltaY = distance * Mathf.Sin(direction * Mathf.Deg2Rad);

      var newX = initX + deltaX;
      var newY = initY + deltaY;

      var rawNewDistance = Mathf.Sqrt(newX * newX + newY * newY);

      this.automaticallyUpdatePosition_ = false;
      this.Distance =
          Mathf.Min(rawNewDistance, CaptainCursorController.MAX_DISTANCE_);
      this.automaticallyUpdatePosition_ = true;
      this.Direction = Mathf.Atan2(newY, newX) * Mathf.Rad2Deg;

      var remainingDistance =
          Mathf.Max(0, rawNewDistance - CaptainCursorController.MAX_DISTANCE_);
      return new CursorDelta {
          NewDirection = this.Direction,
          RemainingDistance = remainingDistance,
      };
    }

    private void UpdatePosition_() {
      if (!this.automaticallyUpdatePosition_) {
        return;
      }

      var x = this.Distance * Mathf.Cos(this.Direction * Mathf.Deg2Rad);
      var y = this.Distance * Mathf.Sin(this.Direction * Mathf.Deg2Rad);

      this.transform.localPosition = new Vector3(x, 0, y);
      this.transform.rotation = Quaternion.Euler(-90, -this.Direction + 90, 0);
    }
  }
}