namespace Abilities
{
    public interface IAbility
    {
        public bool CheckAndUseAbility(ICaster caster);
        public void UseAbility(ICaster caster);

    }
}
