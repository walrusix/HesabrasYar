using AutoMapper;

namespace HesabrasYar.Wallet.Wallets;

public class WalletsAutoMapperProfile : Profile
{
    public WalletsAutoMapperProfile()
    {
        CreateMap<Wallet, WalletDto>()
            .ForMember(d => d.Balance, opt => opt.MapFrom(s => s.Balance != null ? s.Balance.Balance : 0m));
    }
}
