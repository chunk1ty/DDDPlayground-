﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates.CarAdAggregate;
using Domain.Aggregates.CarAdAggregate.Contracts;

namespace Application.Features.CarAds.Commands.Delete
{
    public class DeleteCarAdCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteCarAdCommandHandler : IRequestHandler<DeleteCarAdCommand, bool>
    {
        private readonly ICarAdWriteRepository _carAdWriteRepository;
        private readonly ICarAdReadRepository _carAdReadRepository;

        public DeleteCarAdCommandHandler(ICarAdWriteRepository carAdWriteRepository, ICarAdReadRepository carAdReadRepository)
        {
            _carAdWriteRepository = carAdWriteRepository;
            _carAdReadRepository = carAdReadRepository;
        }

        public async Task<bool> Handle(DeleteCarAdCommand request, CancellationToken cancellationToken)
        {
            CarAd carAd = await _carAdReadRepository.GetCarAdById(request.Id, cancellationToken);

            carAd.Delete();

            await _carAdWriteRepository.Delete(carAd, cancellationToken);

            return true;
        }
    }
}
