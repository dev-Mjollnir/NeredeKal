using MediatR;
using NeredeKal.Infrastructure.Infrastructure.Models;
using NeredeKal.Infrastructure.Enums;
using NeredeKal.RabbitMQ.Models;
using NeredeKal.RabbitMQ.Services;
using System.Text.Json;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using NeredeKal.Infrastructure.Constants.Enums;

namespace HotelService.Application.Report.Command
{
    public class GetHotelReportCommand : IRequest<Response<bool>>
    {
        public string Location { get; set; }
    }

    public class GetHotelReportCommandHandler : IRequestHandler<GetHotelReportCommand, Response<bool>>
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IReportRepository _reportRepository;

        public GetHotelReportCommandHandler(RabbitMQClientService rabbitMQClientService, IReportRepository reportRepository)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _reportRepository = reportRepository;
        }

        public async Task<Response<bool>> Handle(GetHotelReportCommand request, CancellationToken cancellationToken)
        {
            var report = new ReportEntity { EventType = EventType.HotelReportEvent, Query = $"{nameof(request.Location)}:{request.Location}", Status = ReportStatus.Preparing };
            await _reportRepository.AddReportAsync(report);
            var eventModel = new EventModel { EventType = EventType.HotelReportEvent, Id = report.Id };
            _rabbitMQClientService.PublishMessage(JsonSerializer.Serialize(eventModel));
            return Response<bool>.Success(true);
        }
    }
}
