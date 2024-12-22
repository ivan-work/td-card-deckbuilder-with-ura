using Architecture;

namespace Effects {
  public abstract class BaseEffect {
    public bool isActive = false;
    public bool waitForOthers = true;

    public abstract void start(ActorManager am, GridSystem gridSystem);

    protected virtual void animate() { }

    public void update() {
      if (isActive) {
        animate();
      }
    }
  }
}
