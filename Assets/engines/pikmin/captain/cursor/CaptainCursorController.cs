using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.engines.pikmin.captain.cursor {
  public class CaptainCursorController : MonoBehaviour, ICaptainCursor {
    private const float MAX_DISTANCE_ = 15;
    private const float CURSOR_SPEED_ = .5f;

    private GameObject cursorMesh_;

    // Start is called before the first frame update
    public void Start() {
      this.cursorMesh_ = this.transform.GetChild(0).gameObject;
      Assert.AreEqual(this.cursorMesh_.name, "cursor", "Cursor is not the right object!");
    }

    // Update is called once per frame
    public void Update() {
      this.UpdateTransform_();
    }

    public Vector3 GlobalPosition => this.transform.position;
    public Vector3 LocalPosition => this.transform.localPosition;

    public float Direction { get; set; }
    public float Distance { get; set; }

    public float Radius { get; set; }

    public CursorDelta MovePolar(float direction, float magnitude) {
      var initX = this.Distance * Mathf.Cos(this.Direction * Mathf.Deg2Rad);
      var initY = this.Distance * Mathf.Sin(this.Direction * Mathf.Deg2Rad);

      var maxSpeed = CaptainCursorController.CURSOR_SPEED_ *
                     100 *
                     Time.deltaTime;
      var speed = magnitude * maxSpeed;
      var deltaX = speed * Mathf.Cos(direction * Mathf.Deg2Rad);
      var deltaY = speed * Mathf.Sin(direction * Mathf.Deg2Rad);

      var newX = initX + deltaX;
      var newY = initY + deltaY;

      var rawNewDistance = Mathf.Sqrt(newX * newX + newY * newY);

      this.Distance =
          Mathf.Min(rawNewDistance, CaptainCursorController.MAX_DISTANCE_);
      this.Direction = Mathf.Atan2(newY, newX) * Mathf.Rad2Deg;

      var remainingMagnitude =
          Mathf.Max(0,
                    (rawNewDistance - CaptainCursorController.MAX_DISTANCE_) /
                    maxSpeed);

      return new CursorDelta {
          NewDirection = this.Direction,
          RemainingMagnitude = remainingMagnitude,
      };
    }

    private void UpdateTransform_() {
      var originalPosition = this.transform.position;

      var height = 30;

      var totalNormal = new Vector3();
      var totalPoint = new Vector3();

      var radius = 2;
      var resolution = 4;
      for (var i = 0; i < resolution; ++i) {
        var f = i / resolution;
        var angle = f * 2 * Mathf.PI;

        var deltaX = radius * Mathf.Cos(angle);
        var deltaY = radius * Mathf.Sin(angle);

        var from = originalPosition + new Vector3(deltaX, height, deltaY);

        var ray = new Ray(from, Vector3.down);
        Physics.Raycast(ray, out var hitInfo);

        totalNormal += hitInfo.normal;
        totalPoint += hitInfo.point;
      }

      totalNormal.Normalize();
      totalPoint /= resolution;

      var x = originalPosition.x -
              this.transform.localPosition.x +
              this.Distance * Mathf.Cos(this.Direction * Mathf.Deg2Rad);
      var y = totalPoint.y;
      var z = originalPosition.z -
              this.transform.localPosition.z +
              this.Distance * Mathf.Sin(this.Direction * Mathf.Deg2Rad);

      this.transform.position = new Vector3(x, y, z);

      this.cursorMesh_.transform.rotation =
          Quaternion.Euler(-90, -this.Direction + 90, 0);
      this.cursorMesh_.transform.rotation =
          Quaternion.FromToRotation(this.cursorMesh_.transform.up,
                                    totalNormal) *
          this.cursorMesh_.transform.rotation *
          Quaternion.Euler(-90, 0, 0);
    }
  }
}