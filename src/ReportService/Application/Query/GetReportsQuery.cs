using MediatR;
using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using NeredeKal.Infrastructure.Infrastructure.Models;
using ReportService.Application.Dtos;

namespace ReportService.Application.Query
{
    public class GetReportsQuery : IRequest<Response<List<ReportDto>>>
    {
    }

    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, Response<List<ReportDto>>>
    {
        private readonly IReportRepository _reportRepository;

        public GetReportsQueryHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Response<List<ReportDto>>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            var reports = await _reportRepository.GetReportsAsync();
            if (!reports.Any())
                return Response<List<ReportDto>>.Fail(ErrorMessage.ReportsNotFound);
            return Response<List<ReportDto>>.Success(reports.Select(ReportDto.Map).ToList());
        }
    }
}
