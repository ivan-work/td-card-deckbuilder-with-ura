using Architecture;

namespace Effects {
  public interface IHasIntent {
    bool isActiveAndEnabled { get; }
    void getIntents(ActorManager actorManager);
  }
}
