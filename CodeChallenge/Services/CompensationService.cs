using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public CompensationService(ILogger<EmployeeService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation CreateCompensation(Compensation compensation)
        {
            if(compensation != null)
            {
                _compensationRepository.AddCompensation(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetCompensation(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetCompensation(id);
            }

            return null;
        }
    }
}

