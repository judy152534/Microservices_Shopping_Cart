using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderHandler> _logger;

        public DeleteOrderHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var toDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (toDelete == null) 
            {
                _logger.LogError("Order does not exsist on database.");
            }
            await _orderRepository.DeleteAsync(toDelete);
            _logger.LogInformation($"Order delete successfully: {request.Id}");
            return Unit.Value;
        }
    }
}
