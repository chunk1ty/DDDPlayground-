﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using Application.Features.CarAds.Queries.GetCarAdDetails;

namespace IntegrationTests.Application.Features.CarAds.Queries
{
    [TestFixture]
    public class GetCarAdDetailsTest : BaseDatabaseFixture
    {
        [Test]
        public async Task Handle_WithCorrectQuery_ShouldReturn()
        {
            // Arrange
            IServiceScope scope = CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            var command = new GetCarDetailQuery()
            {
                Id = 14,
                DealerId = 1
            };

            CarAdDetailResponse carAds = await mediator.Send(command);

            // Assert
            carAds.Should().NotBeNull();
        }
    }
}
