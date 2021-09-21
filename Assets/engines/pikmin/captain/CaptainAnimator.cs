using UnityEngine;

namespace Assets.engines.pikmin.captain {
  [RequireComponent(typeof(Animator))]
  public class CaptainAnimator : MonoBehaviour, ICaptainAnimator {
    private Animator impl_;

    public void Start() {
      this.impl_ = this.GetComponent<Animator>();
    }

    public float Direction {
      get => this.transform.eulerAngles.y;
      set => this.transform.rotation =
                 Quaternion.AngleAxis(-value + 90, Vector3.up);
    }

    public float RunningMagnitude {
      get => this.impl_.GetFloat("RunningMagnitude");
      set => this.impl_.SetFloat("RunningMagnitude", value);
    }
  }
}