
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext context;
            private readonly ILogger<List> logger;
            public Handler(DataContext context, ILogger<List> logger)
            {
                this.logger = logger;
                this.context = context;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    for (var i = 0; i < 10; i++)
                    {

                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        this.logger.LogInformation($"Task {i} has completed");

                    }

                }
                catch (Exception ex) when (ex is TaskCanceledException)
                {
                    this.logger.LogInformation("Task was cancelled");
                }
                return await this.context.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}