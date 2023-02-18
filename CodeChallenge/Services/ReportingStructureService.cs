using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IReportingStructureRepository reportingStructureRepository)
        {
            _reportingStructureRepository = reportingStructureRepository;
            _logger = logger;
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _reportingStructureRepository.GetReportingStructure(id);
            }

            return null;
        }
    }
}

