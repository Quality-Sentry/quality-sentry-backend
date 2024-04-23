using kvaksy_backend.data.models;
using kvaksy_backend.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

namespace kvaksy_backend.Services
{
    public interface IWeightServices
    {
        Task<WeightField> updateWeightOnReport(Guid id, double weight);
    }

    public class WeightServices : IWeightServices
    {
        private readonly IWeightRepository _weightRepository;
        private readonly IReportRepository _reportRepository;

        public WeightServices(IWeightRepository weightRepository, IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
            _weightRepository = weightRepository;
        }

        public async Task<WeightField> updateWeightOnReport(Guid id, double weight)
        {
            var report = _reportRepository.GetReport(id);
            if (report == null)
            {
                throw new Exception("Unable to find report with given id");
            }

            // get id for temperature field
            foreach (var field in report.Fields)
            {
                if(field is WeightField weightField)
                {
                    var updatedField = weightField;
                    updatedField.Weight = weight;

                    var updated = await _weightRepository.UpdateWeight(updatedField);
                    return updated;
                }
            }

            throw new Exception("Unable to find a temperature field in the given report");
        }
    }
}
