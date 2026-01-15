using EdhWreck.Biz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Abstractions
{
    public interface IScryfallApiService
    {
        Task<CardResponse> CardSearchAsync(CardRequest request);
    }
}
