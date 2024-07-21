public class TowerAttackController
{
    private int _goldPerKill;
    private float _damage;


    public TowerAttackController(float damage, int goldPerKill)
    {
        _damage = damage;
        _goldPerKill = goldPerKill;
    }

    public Attack GetAttack() => new Attack(_damage, _goldPerKill);

    public void AddDamage(float amount)
    {
        if (amount <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        _damage += amount;
    }

    public void AddGoldEaring(int amount)
    {
        if (amount <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        _goldPerKill += amount;
    }
}