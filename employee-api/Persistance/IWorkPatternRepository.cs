using Microsoft.AspNetCore.Mvc;
using employee_api.Models;
using Microsoft.AspNetCore.Http;
using employee_api.Data;
using employee_api.Persistance;

namespace employee_api.Persistance
{
    public interface IWorkPatternRepository
    {
        public Task<WorkPattern> CreateWorkPatternAsync(WorkPattern workPattern, CancellationToken cancellationToken);
        public Task<WorkPattern> UpdateWorkPatternAsync(WorkPattern workPattern, CancellationToken cancellationToken);
        public Task<WorkPattern> DeleteWorkPatternAsync(Guid id, CancellationToken cancellationToken);
        public Task<WorkPattern> GetWorkPatternAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<WorkPattern>> GetAllWorkPatternsAsync(CancellationToken cancellationToken);


    }
}
