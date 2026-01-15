using EdhWreck.Biz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Abstractions
{
    public class ScryfallApiService : IScryfallApiService
    {
        private const string ScryfallApiBaseUrl = "https://api.scryfall.com";
        private const string ScryfallCardEndpoint = "/cards/search";

        private readonly IHttpClientFactory _httpClientFactory;
        public ScryfallApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CardResponse> CardSearchAsync(CardRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(ScryfallApiBaseUrl);
            var query = new SearchQuery
            {
                RootExpression = new AndExpression(new UsdPriceExpression(ValueOperator.LessThanOrEqual, request.MaxCardCost), new ColorExpression(ValueOperator.LessThanOrEqual, request.Colors))
            };
            var parameters = new Dictionary<string, string>
            {
                { "q", query.GetRawText() }
            };
            var rawParameterString = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var httpEncodedParameterString = System.Net.WebUtility.UrlEncode(rawParameterString);
            var endpointUrl = $"{ScryfallApiBaseUrl}{ScryfallCardEndpoint}?q={httpEncodedParameterString}";
            
            var response = await client.GetAsync(endpointUrl);
            throw new NotImplementedException();
        }
    }
}
