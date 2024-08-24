public interface IWashable
{
    float WashSpeed { get; set; }
    float UpgradeAmount { get; }
    int UpgradeCost { get; set; }
    bool IsUnlocked { get; set; }

    void Upgrade();
    void Unlock();
}
