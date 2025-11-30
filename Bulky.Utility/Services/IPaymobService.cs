using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulkyBook.Models;
using System.Threading.Tasks;


public interface IPaymobService
{
    Task<(Payment payment, string redirectUrl)> CreatePaymentForOrderAsync(int orderHeaderId, string paymentMethod, decimal amount);
    Task<OrderHeader> MarkPaymentSuccessAsync(string merchantOrderId);
    Task<OrderHeader> MarkPaymentFailedAsync(string merchantOrderId);
    string ComputeHmacSHA512(string data, string secret);
}
