// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics.CodeAnalysis;
// using System.Linq;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.Events;
//
// namespace Components {
//   public interface IHasIntent {
//     // IntentEffect getIntent();
//   }
//
//   public abstract class BaseEffect {
//     private ActorComponent actor;
//     protected ActorManager actorManager;
//
//     public void apply(ActorManager _actorManager) {
//       actorManager = _actorManager;
//     }
//
//     public void start() { }
//     
//     public abstract void update();
//
//     public void end() {
//       actorManager.onEffectEnd();
//     }
//   }
//
//   public class MoveEffect : BaseEffect {
//     private readonly Vector3 from;
//     private readonly Vector3 to;
//     private readonly float speed;
//     private readonly float distance;
//     private readonly float duration;
//     private float time;
//     private bool passed20 = false;
//     private bool passed50 = false;
//
//     public MoveEffect(Vector3 from, Vector3 to, float speed) {
//       this.from = from;
//       this.to = to;
//       this.speed = speed;
//       this.distance = Vector3.Distance(from, to);
//       this.duration = (distance + 0.001f) / speed;
//       time = 0;
//     }
//
//     public override void update() {
//       time += Time.deltaTime;
//       var timeInterpolated = Math.Min(1, time / duration);
//       if (timeInterpolated > 0.2 && !passed20) {
//         passed20 = true;
//         actorManager.onEffectNext();
//       }
//       if (timeInterpolated > 0.5 && !passed50) {
//         passed50 = true;
//         actorManager.onEffectEnd();
//       }
//       var value = Vector3.Lerp(from, to, timeInterpolated);
//     }
//   }
//
//   // public class ForceEffect : BaseEffect {
//   //   public void start() {
//   //   }
//   // }
//   //
//   // public class IntentEffect : BaseEffect {
//   //   public void start() {
//   //   }
//   // }
//   
//   public class ActorManager : MonoBehaviour {
//     private readonly HashSet<ActorComponent> actors = new();
//     private readonly LinkedList<BaseEffect> effectsQueue = new();
//     private readonly HashSet<BaseEffect> activeEffects = new();
//     // private readonly event ask = new UnityEvent();
//     
//
//     public void register(ActorComponent actorComponent) {
//       actors.Add(actorComponent);
//     }
//
//     public void unregister(ActorComponent actorComponent) {
//       actors.Remove(actorComponent);
//     }
//
//     public void ask() {
//       foreach (var actor in actors) {
//         actor.getIntent();
//       }
//     }
//
//     public void onEffectNext() {
//       var nextEffect = effectsQueue.FirstOrDefault();
//       if (nextEffect != null) {
//         nextEffect.apply(this);
//         activeEffects.Add(nextEffect);
//       }
//     }
//
//     private void Update() {
//       foreach (var activeEffect in activeEffects) {
//         activeEffect.update();
//       }
//     }
//
//     public void onEffectEnd() { }
//
//     public void exec() {
//       foreach (var effect in actors.Select(actor => actor.effects).SelectMany(actorEffects => actorEffects)) {
//         effectsQueue.AddLast(effect);
//       }
//
//       onEffectNext();
//     }
//   }
//
//   public class ActorComponent : MonoBehaviour {
//     private ActorManager actorManager;
//     public readonly List<BaseEffect> effects = new();
//
//     private void Awake() {
//       actorManager = FindObjectOfType<ActorManager>();
//       actorManager.register(this);
//     }
//
//     public BaseEffect getIntent() {
//       // return new MoveEffect();
//       // var intents = GetComponents<IHasIntent>()
//       //   .Select(intentMaker => intentMaker.getIntent());
//       //
//       // effects.AddRange(intents);
//       return null;
//     }
//   }
// }