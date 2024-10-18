public interface IWeapon
{
    public void Shoot();
    public void Reload();
    float GetFireRate();
    int GetBulletsCount();
    FireMode GetFireMode();
    public void ChangeFireMode();
}
public enum FireMode
{
    AUTO,
    MANUAL
}
