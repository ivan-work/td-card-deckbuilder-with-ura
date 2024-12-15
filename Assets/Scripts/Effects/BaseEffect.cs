public abstract class BaseEffect {
  public bool isActive = false;
  public bool waitForOthers = true;

  public abstract void start(ActorManager am);

  protected virtual void animate() { }

  public void update() {
    if (isActive) {
      animate();
    }
  }
}