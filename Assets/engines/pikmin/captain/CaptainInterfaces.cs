using UnityEngine;

namespace Assets.engines.pikmin.captain {
  public interface ICaptain {
    Vector3 GlobalPosition { get; }
    Vector3 LocalPosition { get; }

    void MovePolar(float direction, float distance);
  }


  public struct CaptainState {
    public ICaptain Captain { get; set; }
    public BCaptainMotor Motor { get; set; }
    public ICaptainAnimator Animator { get; set; }
  }

  public interface IRequiresCaptainState {
    void Init(CaptainState state);
  }


  public abstract class BCaptainMotor : MonoBehaviour, IRequiresCaptainState {
    public abstract void Init(CaptainState state);
 
    public abstract void MovePolar(float direction, float distance);

    public abstract void PressAction();
    public abstract void ReleaseAction();
  }

  public interface ICaptainAnimator {
    float Direction { get; set; }
    float RunningMagnitude { get; set; }
  }
}
