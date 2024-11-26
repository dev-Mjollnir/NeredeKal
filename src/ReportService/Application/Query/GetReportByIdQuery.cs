using MediatR;
using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using NeredeKal.Infrastructure.Infrastructure.Models;
using ReportService.Application.Dtos;

namespace ReportService.Application.Query
{
    public class GetReportByIdQuery : IRequest<Response<ReportDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, Response<ReportDto>>
    {
        private readonly IReportRepository _reportRepository;

        public GetReportByIdQueryHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Response<ReportDto>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
            var report = await _reportRepository.GetReportByIdAsync(request.Id);
            if (report is null)
                return Response<ReportDto>.Fail(ErrorMessage.ReportNotFound);
            return Response<ReportDto>.Success(ReportDto.Map(report));
        }
    }
}
