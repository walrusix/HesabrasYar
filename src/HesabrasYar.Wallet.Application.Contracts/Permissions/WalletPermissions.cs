namespace HesabrasYar.Wallet.Permissions;

public static class WalletPermissions
{
    public const string GroupName = "Wallet";

    public static class Wallets
    {
        public const string Default = GroupName + ".Wallets";
        public const string Create = Default + ".Create";
    }
}
