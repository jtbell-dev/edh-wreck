using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class LegalStatusExtensions
    {
        extension(LegalStatus status)
        {
            public string ToQueryString() => status switch
            {
                LegalStatus.Legal => "f",
                LegalStatus.Banned => "banned",
                LegalStatus.Restricted => "restricted",
                _ => throw new ArgumentOutOfRangeException(nameof(status), $"Not expected legal status value: {status}"),
            };
        }
    }
}