using System;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        ReportingStructure GetReportingStructure(string id);
    }
}

