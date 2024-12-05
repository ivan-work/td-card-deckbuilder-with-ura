// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using Unity.VisualScripting;
// using UnityEngine;
//
// namespace Components {
//   public interface IHasIntent {
//     IntentEffect getIntent();
//   }
//
//   public interface IEffect {
//     IEnumerable start();
//   }
//
//   public class MoveEffect : IEffect {
//     public IEnumerable start() {
//       yield break;
//     }
//   }
//
//   public class ForceEffect : IEffect {
//     public IEnumerable start() {
//       yield break;
//     }
//   }
//
//   public class IntentEffect : IEffect {
//     public IEnumerable start() {
//       yield break;
//     }
//   }
//
//   public class ActorManager : MonoBehaviour {
//     private readonly HashSet<ActorComponent> actors = new();
//     private readonly Queue<IEffect> effects = new();
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
//     public void exec() {
//       foreach (var effect in actors.Select(actor => actor.effects).SelectMany(actorEffects => actorEffects)) {
//         effects.Enqueue(effect);
//       }
//
//       var hasEffect = effects.TryDequeue(out var nextEffect);
//       if (hasEffect) {
//         nextEffect.start();
//       }
//     }
//   }
//
//   public class ActorComponent : MonoBehaviour {
//     private ActorManager actorManager;
//     public readonly List<IEffect> effects = new();
//
//     private void Awake() {
//       actorManager = this.GetAssertComponentInParent<ActorManager>();
//     }
//
//     public void getIntent() {
//       var intents = GetComponents<IHasIntent>()
//         .Select(intentMaker => intentMaker.getIntent());
//
//       effects.AddRange(intents);
//     }
//   }
// }