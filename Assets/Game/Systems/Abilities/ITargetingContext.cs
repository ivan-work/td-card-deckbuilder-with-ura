namespace Abilities {
  public interface ITargetingContext {
    void OnHoverStart(AbilityContext context, ITarget target);
    void OnHoverStop(AbilityContext context, ITarget target);
    bool ConfirmTarget(AbilityContext context, ITarget target);
    void Stop(AbilityContext context);
  }
}
