using UnityEngine;

namespace Fralle.AbilitySystem
{
  public abstract class ActiveAbility : Ability
  {
    public float cooldown = 0f;
    internal float CooldownTimer;

    [HideInInspector] public bool isActive;
    public bool IsReady => !isActive && Time.time > CooldownTimer;

    public virtual void Perform()
    {
      CooldownTimer = Time.time + cooldown;
    }

    // ReSharper disable once UnusedMember.Global
    public abstract void Abort();
  }
}
