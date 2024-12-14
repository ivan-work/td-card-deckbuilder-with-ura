public abstract class BaseEffect {
  public bool active = false;

  public abstract void start();

  protected virtual void animate() { }

  public void update() {
    if (active) {
      animate();
    }
  }
}