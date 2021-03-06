using UnityEngine;

namespace Assets.engines.pikmin.captain {
  public interface ICaptain {
    Vector3 GlobalPosition { get; }
    Vector3 LocalPosition { get; }

    void MovePolar(float direction, float magnitude);
    void FaceTowards(float direction, float magnitude);
  }


  public struct CaptainStateBundle {
    public ICaptain Captain { get; set; }
    public BCaptainMovementController MovementController { get; set; }
    public ICaptainAnimator Animator { get; set; }
  }

  public interface IRequiresCaptainState {
    void Init(CaptainStateBundle stateBundle);
  }


  public abstract class BCaptainMovementController : MonoBehaviour, IRequiresCaptainState {
    public abstract void Init(CaptainStateBundle stateBundle);
 
    public abstract void MovePolar(float direction, float magnitude);

    /*
    public abstract boolean Interact();
    public abstract void Punch();
    
    public abstract void Whistle();
    public abstract void Dismiss();
    
    public abstract void SwarmPolar(float direction, float magnitude);

    public abstract void HoldPikmin();
    public abstract void LetGoOfPikmin();
    public abstract void SwapPikminType(newType);
    public abstract void ThrowPikmin(Vector3 target);
    */
  }


  public abstract class BCaptainPikminInteractionController {

  }


  public interface ICaptainAnimator {
    float Direction { get; set; }
    float RunningMagnitude { get; set; }
    float WalkingInPlaceMagnitude { get; set; }
  }
}
