﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
          logger.LogInformation("[START] Handle request={Request} - Response={Response} data={request}"
              , typeof(TRequest).Name,typeof(TResponse).Name,request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timetaken = timer.Elapsed;
            if(timetaken.Seconds > 3)
            {
                logger.LogInformation("[PERFORMANCE] The  request {Request} took {TimeTaken}"
                 , typeof(TRequest).Name, timetaken.Seconds);
            }

            logger.LogInformation("[END] Handle {Request} with {Response}"
                , typeof(TRequest).Name, typeof(TResponse).Name);

            return response;


        }
    }
}
