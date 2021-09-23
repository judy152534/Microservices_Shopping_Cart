using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderHandler> _logger;

        public CheckoutOrderHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var result = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {result.ID} created successfully.");

            await SendEmail(result);
            return result.ID;
        }

        private async Task SendEmail(Order order) 
        {
            var mail = new Email() { To = "Timdd@gmail.com", Subject = "Order was create", Body = $"Hello,{order.UserName}! Your order was created." };
            try
            {
                await _emailService.SendEmail(mail);
            }
            catch
            {
                _logger.LogError($"Fail to send mail to order {order.ID}: username {order.UserName}");
            }
        }
    }
}
