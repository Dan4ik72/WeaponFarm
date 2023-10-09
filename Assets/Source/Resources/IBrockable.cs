public interface IBrockable : IInteraction
{
    public void ApplyDamage(int damage);

    public int GetCurrentDurability();

    public int GetEnergyEffect();
}