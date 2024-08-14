﻿using CashFlow.Payment.API.Application.CashOperation.Commands.Responses.Common;
using MediatR;

namespace CashFlow.Payment.API.Application.CashOperation.Commands.Requests
{
    public class CreditCashOperationCommandsRequest : IRequest<CashOperationCommandResponse>
    {
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
