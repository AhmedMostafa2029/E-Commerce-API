using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentGateway
    {
        Task<PaymentIntentResult> CreatePaymentIntentAsync(decimal amount, string currency, CancellationToken ct = default);
        Task<PaymentIntentResult> UpdatePaymentIntentAsync(string PaymentIntentId,decimal amount, CancellationToken ct = default);

    }
}
