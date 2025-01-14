﻿using System.Threading.Tasks;
using Application.Features.CarAds.Commands.Create;
using Domain.Aggregates.CarAdAggregate;
using FluentAssertions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.Application.Features.CarAds.Commands
{
    [TestFixture]
    public class CreateCarAdTest : BaseDatabaseFixture
    {
        [Test]
        public async Task Handle_WithCorrectCommand_ShouldCreateCarAd()
        {
            // Arrange
            IServiceScope scope = CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            // Category and Dealer should be seed in advanced 
            var command = new CreateCarAdCommand()
            {
                DealerId = 1,
                Model = "Passat",
                CategoryId = 1,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a2/2010_Volkswagen_Passat_Highline_TDi_140_2.0_Front.jpg",
                PricePerDay = 17,
                HasClimateControl = true,
                NumberOfSeats = 5,
                TransmissionType = TransmissionType.Automatic,
                ManufacturerName = "Volkswagen"
            };

            var response = await mediator.Send(command);

            // Assert
            using (var db = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>())
            {
                var carAd = await db.CarAds.SingleOrDefaultAsync(c => c.Id == response.Id);

                carAd.Model.Should().Be("Passat");
                carAd.Manufacturer.Name.Should().Be("Volkswagen");
                carAd.Category.Id.Should().Be(1);
                carAd.PricePerDay.Should().Be(17);
                carAd.Options.HasClimateControl.Should().Be(true);
                carAd.Options.NumberOfSeats.Should().Be(5);
                carAd.Options.TransmissionType.Should().Be(TransmissionType.Automatic);
            }
        }
    }
}
