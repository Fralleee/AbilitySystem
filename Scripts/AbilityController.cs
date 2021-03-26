using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

		public GameObject postProcess;

		PlayerInput playerInput;

		public void NewAbility(Ability ability, AbilityType abilityType)
		{
			switch (abilityType)
			{
				case AbilityType.MOVEMENT:
					MovementAbility = (ActiveAbility)SetupAbility(ability);
					break;
				case AbilityType.ATTACK:
					AttackAbility = (ActiveAbility)SetupAbility(ability);
					break;
				case AbilityType.ULTIMATE:
					UltimateAbility = (ActiveAbility)SetupAbility(ability);
					break;
				case AbilityType.PASSIVE:
					PassiveAbilities.Add((PassiveAbility)SetupAbility(ability));
					break;
			}
		}

		void Awake()
		{
			SetupAbilities();

			playerInput = GetComponent<PlayerInput>();
			playerInput.actions["MovementAbility"].performed += OnMovementAbility;
			playerInput.actions["AttackAbility"].performed += OnAttackAbility;
			playerInput.actions["UltimateAbility"].performed += OnUltimateAbility;
		}

		void SetupAbilities()
		{
			MovementAbility = (ActiveAbility)SetupAbility(MovementAbility);
			AttackAbility = (ActiveAbility)SetupAbility(AttackAbility);
			UltimateAbility = (ActiveAbility)SetupAbility(UltimateAbility);


			var temp = new List<PassiveAbility>();
			foreach (PassiveAbility ability in PassiveAbilities)
			{
				var instance = (PassiveAbility)SetupAbility(ability);
				temp.Add(instance);
			}
			PassiveAbilities = temp;
		}

		Ability SetupAbility(Ability ability)
		{
			if (ability == null)
				return null;

			var instance = Instantiate(ability);
			instance.Setup(this);
			return instance;
		}

		void OnMovementAbility(InputAction.CallbackContext context)
		{
			if (MovementAbility != null && MovementAbility.IsReady)
				MovementAbility.Perform();
		}

		void OnAttackAbility(InputAction.CallbackContext context)
		{
			if (AttackAbility != null && AttackAbility.IsReady)
				AttackAbility.Perform();
		}

		void OnUltimateAbility(InputAction.CallbackContext context)
		{
			if (UltimateAbility != null && UltimateAbility.IsReady)
				UltimateAbility.Perform();
		}

		void OnDestroy()
		{
			playerInput.actions["MovementAbility"].performed -= OnMovementAbility;
			playerInput.actions["AttackAbility"].performed -= OnAttackAbility;
			playerInput.actions["UltimateAbility"].performed -= OnUltimateAbility;
		}
	}
}
