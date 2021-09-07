using Fralle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public List<PassiveAbility> passiveAbilities;

    [HideInInspector] public PostProcessController postProcessController;

    // ReSharper disable once UnusedMember.Global
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
          passiveAbilities.Add((PassiveAbility)SetupAbility(ability));
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null);
      }
    }

    protected virtual void Awake()
    {
      postProcessController = FindObjectOfType<PostProcessController>();
    }

    protected virtual void Start()
    {
      SetupAbilities();
    }

    void SetupAbilities()
    {
      MovementAbility = (ActiveAbility)SetupAbility(MovementAbility);
      AttackAbility = (ActiveAbility)SetupAbility(AttackAbility);
      UltimateAbility = (ActiveAbility)SetupAbility(UltimateAbility);


      List<PassiveAbility> temp = passiveAbilities.Select(ability => (PassiveAbility)SetupAbility(ability)).ToList();
      passiveAbilities = temp;
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
