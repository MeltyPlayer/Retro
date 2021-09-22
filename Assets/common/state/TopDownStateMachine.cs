using System;
using System.Collections.Generic;

namespace Assets.common.state {
  public class TopDownStateMachine<TState> {
    private TState state_;

    public delegate void OnEnterOrExit(TState state);

    public delegate void OnTransition(TState from, TState to);

    private readonly IDictionary<TState, IList<OnEnterOrExit>>
        onExitHandlers_ =
            new Dictionary<TState, IList<OnEnterOrExit>>();

    private readonly IDictionary<TState, IList<OnEnterOrExit>>
        onEnterHandlers_ =
            new Dictionary<TState, IList<OnEnterOrExit>>();

    private readonly IDictionary<Tuple<TState, TState>, IList<OnTransition>>
        onTransitionHandlers_ =
            new Dictionary<Tuple<TState, TState>, IList<OnTransition>>();

    private TopDownStateMachine(
        IDictionary<TState, IList<OnEnterOrExit>> onExitHandlers,
        IDictionary<TState, IList<OnEnterOrExit>> onEnterHandlers,
        IDictionary<Tuple<TState, TState>, IList<OnTransition>>
            onTransitionHandlers,
        TState initial) {
      this.onExitHandlers_ = onExitHandlers;
      this.onEnterHandlers_ = onEnterHandlers;
      this.onTransitionHandlers_ = onTransitionHandlers;
      this.state_ = initial;
    }

    public TState State {
      get => this.state_;
      set {
        var from = this.state_;
        var to = value;

        this.state_ = to;

        if (this.onExitHandlers_.TryGetValue(from, out var onExitHandlers)) {
          foreach (var onExitHandler in onExitHandlers) {
            onExitHandler.Invoke(from);
          }
        }

        if (this.onEnterHandlers_.TryGetValue(to, out var onEnterHandlers)) {
          foreach (var onEnterHandler in onEnterHandlers) {
            onEnterHandler.Invoke(to);
          }
        }

        if (this.onTransitionHandlers_.TryGetValue(
            Tuple.Create(from, to),
            out var onTransitionHandlers)) {
          foreach (var onTransitionHandler in onTransitionHandlers) {
            onTransitionHandler.Invoke(from, to);
          }
        }
      }
    }

    public class Builder {
      private readonly IDictionary<TState, IList<OnEnterOrExit>>
          onExitHandlers_ =
              new Dictionary<TState, IList<OnEnterOrExit>>();

      private readonly IDictionary<TState, IList<OnEnterOrExit>>
          onEnterHandlers_ =
              new Dictionary<TState, IList<OnEnterOrExit>>();

      private readonly IDictionary<Tuple<TState, TState>, IList<OnTransition>>
          onTransitionHandlers_ =
              new Dictionary<Tuple<TState, TState>, IList<OnTransition>>();

      public Builder OnExit(TState state, OnEnterOrExit handler) {
        if (!this.onExitHandlers_.TryGetValue(state, out var list)) {
          list = this.onExitHandlers_[state] = new List<OnEnterOrExit>();
        }
        list.Add(handler);
        return this;
      }

      public Builder OnEnter(TState state, OnEnterOrExit handler) {
        if (!this.onEnterHandlers_.TryGetValue(state, out var list)) {
          list = this.onEnterHandlers_[state] = new List<OnEnterOrExit>();
        }
        list.Add(handler);
        return this;
      }

      public Builder OnTransition(
          TState from,
          TState to,
          OnTransition handler) {
        var key = Tuple.Create(from, to);
        if (!this.onTransitionHandlers_.TryGetValue(key, out var list)) {
          list = this.onTransitionHandlers_[key] = new List<OnTransition>();
        }
        list.Add(handler);
        return this;
      }

      public TopDownStateMachine<TState> Build(TState initial)
        => new TopDownStateMachine<TState>(this.onExitHandlers_,
                                           this.onEnterHandlers_,
                                           this.onTransitionHandlers_,
                                           initial);
    }
  }
}