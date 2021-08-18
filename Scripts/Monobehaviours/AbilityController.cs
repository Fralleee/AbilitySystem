using Fralle.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Fralle.AbilitySystem
{
  public class AbilityController : MonoBehaviour
  {
    [field: SerializeField]
    public ActiveAbility MovementAbility { get; private set; }

    [field: SerializeField]
    public ActiveAbility AttackAbility { get; private set; }

    [field: SerializeField]
    public ActiveAbility UltimateAbility { get; private set; }

    public List<PassiveAbility> PassiveAbilities;

    [HideInInspector] public PostProcessController postProcessController;

    public void NewAbility(Ability ability, AbilityType abilityType)
    {
      switch (abilityType)
      {
        case AbilityType.Movement:
          MovementAbility = (ActiveAbility)SetupAbility(ability);
          break;
        case AbilityType.Attack:
          AttackAbility = (ActiveAbility)SetupAbility(ability);
          break;
        case AbilityType.Ultimate:
          UltimateAbility = (ActiveAbility)SetupAbility(ability);
          break;
        case AbilityType.Passive:
          PassiveAbilities.Add((PassiveAbility)SetupAbility(ability));
          break;
      }
    }

    protected virtual void Awake()
    {
      postProcessController = GetComponentInChildren<PostProcessController>();

      SetupAbilities();
    }

    void SetupAbilities()
    {
      MovementAbility = (ActiveAbility)SetupAbility(MovementAbility);
      AttackAbility = (ActiveAbility)SetupAbility(AttackAbility);
      UltimateAbility = (ActiveAbility)SetupAbility(UltimateAbility);


      List<PassiveAbility> temp = new List<PassiveAbility>();
      foreach (PassiveAbility ability in PassiveAbilities)
      {
        PassiveAbility instance = (PassiveAbility)SetupAbility(ability);
        temp.Add(instance);
      }
      PassiveAbilities = temp;
    }

    Ability SetupAbility(Ability ability)
    {
      if (ability == null)
        return null;

      Ability instance = Instantiate(ability);
      instance.Setup(this);
      return instance;
    }
  }
}
