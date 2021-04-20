using UnityEngine;

namespace Fralle.AbilitySystem
{
	public class ActiveAbility : Ability
	{
		public float Cooldown = 0f;
		internal float CooldownTimer;

		public bool IsReady => Time.time > CooldownTimer;

		public virtual void Perform()
		{
			CooldownTimer = Time.time + Cooldown;
		}

		public override void Setup(AbilityController abilityController) { }
	}
}
