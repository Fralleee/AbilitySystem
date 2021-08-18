using UnityEngine;

namespace Fralle.AbilitySystem
{
  public abstract class ActiveAbility : Ability
  {
    public float Cooldown = 0f;
    internal float CooldownTimer;

    [HideInInspector] public bool IsActive;
    public bool IsReady => !IsActive && Time.time > CooldownTimer;

    public virtual void Perform()
    {
      CooldownTimer = Time.time + Cooldown;
    }

    public abstract void Abort();
  }
}
