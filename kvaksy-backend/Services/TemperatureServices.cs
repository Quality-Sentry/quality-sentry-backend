using kvaksy_backend.data.models;
using kvaksy_backend.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

namespace kvaksy_backend.Services
{
    public interface ITemperatureServices
    {
        bool updateTemperatureOnReport(Guid id, int temperature);
    }

    public class TemperatureServices : ITemperatureServices
    {
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly IReportRepository _reportRepository;

        public TemperatureServices(ITemperatureRepository temperatureRepository, IReportRepository reportRepository)
        { 
            _temperatureRepository = temperatureRepository;
            _reportRepository = reportRepository;
        }

        public bool updateTemperatureOnReport(Guid id, int temperature)
        {
            var report = _reportRepository.GetReport(id);
            if (report == null)
            {
                throw new Exception("Unable to find report with given id");
            }

            // get id for temperature field
            foreach (var field in report.Fields)
            {
                if(field is TemperatureField)
                {
                    return true;
                }
            }

            throw new Exception("Unable to find a temperature field in the given report");
        }
    }
}
