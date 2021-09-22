namespace Assets.engines.pikmin.captain.cursor {
  /// <summary>
  ///   Helper component for the control type where the captain is controlled
  ///   via a cursor, i.e. Pikmin 1 & 2.
  /// </summary>
  public class CaptainCursorBasedMovement : BCaptainMovement {
    private CaptainState state_;
    private ICaptainCursor cursor_;

    public void Start() {
      this.cursor_ = this.GetComponentInChildren<CaptainCursorController>();
    }

    public override void Init(CaptainState state) {
      this.state_ = state;
    }

    public override void MovePolar(float direction, float magnitude) {
      var cursorDelta = this.cursor_.MovePolar(direction, magnitude);

      // If the magnitude is less than some cutoff, then only the cursor moves.
      var minimumRunMagnitude = .5f;
      if (magnitude < minimumRunMagnitude) {
        // TODO: Player walks in place towards the cursor if less than cutoff
        // TODO: Rotates player towards stick more quickly than cursor moves
        // TODO: After a second of inaction, the player turns back towards the cursor.
        this.state_.Captain.FaceTowards(cursorDelta.NewDirection, magnitude);
      } else {
        this.state_.Captain.MovePolar(direction, magnitude);
      }
    }
  }
}