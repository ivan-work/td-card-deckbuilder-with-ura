using Abilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card")]
public class Card : ScriptableObject {
  [SerializeField] public string Name;
  [SerializeField] public Ability Ability;
}
