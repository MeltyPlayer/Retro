namespace Assets.common.math {
  public static class Interpolation {
    private static float DifferenceInDegrees_(float degLhs, float degRhs) {
      return ((((degLhs - degRhs) % 360) + 540) % 360) - 180;
    }

    public static float Degrees(float start, float finish, float frac) {
      return start + Interpolation.DifferenceInDegrees_(finish, start) * frac;
    }
  }
}