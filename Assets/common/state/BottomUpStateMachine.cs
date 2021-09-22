using System;
using System.Collections.Generic;

namespace Assets.common.state {
  public interface IBottomUpStateMachine {
    IBottomUpState State { get; set; }
    IBottomUpState Create();
  }

  public interface IBottomUpState {
    IBottomUpState OnEnter(Action handler);
    IBottomUpState OnExit(Action handler);
  }


  public class BottomUpStateMachine : IBottomUpStateMachine {
    private BottomUpState state_;

    public IBottomUpState State {
      get => this.state_;
      set {
        var from = this.state_;
        var to = value as BottomUpState;

        this.state_ = to;

        foreach (var onExitHandler in from.onExitHandlers) {
          onExitHandler.Invoke();
        }

        foreach (var onEnterHandler in to.onEnterHandlers) {
          onEnterHandler.Invoke();
        }
      }
    }

    public IBottomUpState Create() => new BottomUpState();

    private class BottomUpState : IBottomUpState {
      public IList<Action> onEnterHandlers = new List<Action>();
      public IList<Action> onExitHandlers = new List<Action>();

      public IBottomUpState OnEnter(Action handler) {
        this.onEnterHandlers.Add(handler);
        return this;
      }

      public IBottomUpState OnExit(Action handler) {
        this.onExitHandlers.Add(handler);
        return this;
      }
    }
  }
}