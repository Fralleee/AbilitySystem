using UnityEngine;

namespace Fralle.AbilitySystem
{
	public class ActiveAbility : Ability
	{
		public float cooldown = 0f;
		internal float cooldownTimer = 0f;

		public bool IsReady => Time.time > cooldownTimer;

		public virtual void Perform()
		{
			cooldownTimer = Time.time + cooldown;
		}

		public override void Setup(AbilityController abilityController) { }
	}
}
