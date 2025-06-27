using FluentAssertions;
using employee_api.Models;
using employee_api.Timers;
using Xunit;

namespace employee_api.Tests.Timers
{
    public class FindLowestDelayTests
    {
        private readonly TimerService _timerService;

        public FindLowestDelayTests()
        {
            _timerService = new TimerService(null,null,null);
        }
    }
}
