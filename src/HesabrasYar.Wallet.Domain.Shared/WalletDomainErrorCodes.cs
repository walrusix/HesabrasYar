namespace HesabrasYar.Wallet;

public static class WalletDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */

    public const string DuplicateWalletName = "Wallet:00001";

    public const string ParentWalletNotFound = "Wallet:00002";

    public const string WalletCannotBeItsOwnParent = "Wallet:00003";

    public const string InsufficientWalletBalance = "Wallet:00004";

    public const string WalletBalanceAlreadyInitialized = "Wallet:00005";

    public const string WalletBalanceNotInitialized = "Wallet:00006";

    public const string ParentWalletOwnerMismatch = "Wallet:00007";
    
    public const string WalletNotFound = "Wallet:00008";
}
