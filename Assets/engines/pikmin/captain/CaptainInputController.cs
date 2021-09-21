using System;

using UnityEngine;

namespace Assets.engines.pikmin.olimar {
  public class CaptainInputController : MonoBehaviour {
    private Camera camera_;

    // Start is called before the first frame update
    public void Start() {
      this.camera_ = Camera.main;
    }

    // Update is called once per frame
    public void Update() {
      var cameraDirection = this.camera_.transform.rotation.eulerAngles.y;

      var rawHeldHorizontalAxis = Input.GetAxis("Horizontal");
      var rawHeldVerticalAxis = Input.GetAxis("Vertical");
      var rawHeldDirection =
          Mathf.Atan2(rawHeldVerticalAxis, rawHeldHorizontalAxis) *
          Mathf.Rad2Deg;

      var heldDirection = cameraDirection + 180 + rawHeldDirection;
      var heldMagnitude =
          Mathf.Sqrt(rawHeldHorizontalAxis * rawHeldHorizontalAxis +
                     rawHeldVerticalAxis * rawHeldVerticalAxis);

      this.Motor?.MovePolar(heldDirection, heldMagnitude);
    }

    public ICaptainMotor Motor { get; set; }
  }
}