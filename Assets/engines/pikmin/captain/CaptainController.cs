using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.engines.pikmin.olimar {
  public class CaptainController : MonoBehaviour, ICaptain {
    // Start is called before the first frame update
    public void Start() {
      this.Cursor = this.gameObject
                        .GetComponentInChildren<CaptainCursorController>();
      Assert.IsNotNull(this.Cursor, "Could not find cursor!");

      this.Animator = this.GetComponentInChildren<Animator>();

      this.gameObject.GetComponent<CaptainInputController>().Motor =
          this.Motor = new CaptainMotorImpl(this);
    }

    // Update is called once per frame
    public void Update() {}

    public Vector3 GlobalPosition => this.transform.position;
    public Vector3 LocalPosition => this.transform.localPosition;

    public ICaptainMotor Motor { get; private set; }

    public ICaptainCursor Cursor { get; private set; }
    public Animator Animator { get; private set; }


    public void MovePolar(float direction, float distance) {
      var x = distance * Mathf.Cos(direction * Mathf.Deg2Rad);
      var y = distance * Mathf.Sin(direction * Mathf.Deg2Rad);

      this.transform.position += new Vector3(x, 0, y);
    }
  }
}