using System;
using Unity.Mathematics;

namespace GridSystem {
  public readonly struct Hex {
    public readonly int[] V;

    public Hex(int q, int r) {
      V = new[] { q, r, -q - r };
    }

    public int Q => V[0];
    public int X => V[0];
    public int R => V[1];
    public int Y => V[1];
    public int S => V[2];
    public int Z => V[2];

    public bool Equals(Hex p) => (Q == p.Q) && (R == p.R);
    public static bool operator ==(Hex a, Hex b) => a.Equals(b);
    public static bool operator !=(Hex a, Hex b) => !(a == b);
    public static Hex operator +(Hex a, Hex b) => new Hex(a.Q + b.Q, a.R + b.R);
    public static Hex operator -(Hex a) => new Hex(-a.Q, -a.R);
    public static Hex operator -(Hex a, Hex b) => a + -b;

    public override int GetHashCode() => (Q, R).GetHashCode();
  };

  public class HexGridStructure {
    public void test() {
      var hex1 = new Hex(1, 1);
      var hex2 = new Hex(1, 1);
      var res = hex1 == hex2;
      int2 hex;
    }
  }
}
