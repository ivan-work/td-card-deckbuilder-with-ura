public abstract class BaseEffect {
  public bool active = false;
  
  public abstract void start();
  
  protected abstract void animate();

  public void update() {
    if (active) {
      animate();
    }
  }
}