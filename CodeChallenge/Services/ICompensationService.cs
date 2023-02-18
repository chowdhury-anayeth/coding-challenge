using System;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation GetCompensation(string id);
        Compensation CreateCompensation(Compensation compensation);
    }
}

