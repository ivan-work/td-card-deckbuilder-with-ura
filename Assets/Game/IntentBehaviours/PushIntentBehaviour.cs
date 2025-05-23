using System;
using System.Linq;
using Architecture;
using Effects.EffectAnimations;
using GridSystem;
using IntentBehaviours;
using Intents.Engine;
using UnityEngine;

namespace Intents.IntentBehaviours {
  [CreateAssetMenu(fileName = "IntentBehaviours/PushIntentBehaviour")]
  public class PushIntentBehaviour : IntentBehaviour<PushIntentValues> {
    protected override void Perform(Intent<PushIntentValues> intent, IntentProgressContext context) {
      var entity = intent.Targets.GameObjects.MaybeFirst();
      var trajectory = intent.Targets.Locations;
      var initialForce = intent.Values.Force;
      var gridSystem = context.GlobalContext.GridSystem;
      
      if (entity && entity.TryGetComponent<GridComponent>(out var gridComponent) && trajectory.Length >= 2 && initialForce > 0) {
        for (var currentForce = 0; currentForce < initialForce; currentForce++) {
          var targetLoc = getTargetLoc(initialForce, currentForce, trajectory);
          var sourcePosition = intent.Source.transform.position;
          var targetPosition = gridComponent.GridSystem.GridLoc2World(targetLoc);
          
          

          if (MoveIntentBehaviour.IsCellPathable(gridSystem, targetLoc)) {
            context.Animation = new MoveAnimation(intent.Source, sourcePosition, targetPosition);
            gridComponent.MoveTo(targetLoc);
            MoveIntentBehaviour.SendEvents(context.GlobalContext, intent.Source, targetLoc);
          } else {
            context.Animation = new MoveAttemptAnimation(intent.Source, sourcePosition, targetPosition);
            
          }
        }
      }
    }

    //Попытка переписать Perform
    protected void PerformNew(Intent<PushIntentValues> intent, IntentProgressContext context) {
      /*
       1. Пришедшие начальные значения
       
       */
    }

    private Vector2Int getTargetLoc(int initialForce, int currentForce, Vector2Int[] trajectory) {
      var step = initialForce - currentForce;
      var trajectoryLength = trajectory.Length - 1;
      var index = (step % trajectoryLength) + 1;
      //#TODO Проверить, что берёт целочисленную часть деления
      var directionMultiplier = step / trajectoryLength;
      var direction = trajectory.Last() - trajectory.First();
      return directionMultiplier * direction + trajectory[index];
    }
  }

  [Serializable]
  public class PushIntentValues : IntentValues {
    public int Force = 0;

    public PushIntentValues() { }

    public PushIntentValues(int force) {
      Force = force;
    }
  }
}
