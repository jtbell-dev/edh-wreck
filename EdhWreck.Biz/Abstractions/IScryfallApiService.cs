using EdhWreck.Biz.Models;

namespace EdhWreck.Biz.Abstractions
{
    public interface IScryfallApiService
    {
        Task<CardResponse> CardSearchAsync(CardRequest request);
    }
}
