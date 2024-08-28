namespace WeatherForecastProvider.Services
{
    public interface IIssueTimeChecker
    {
        bool IsTheSameIssueTime(string airportCode, DateTime issueTime);
    }
    public class IssueTimeChecker : IIssueTimeChecker
    {
        private readonly Dictionary<string, DateTime> _issueTimeStore;

        public IssueTimeChecker()
        {
            _issueTimeStore = new Dictionary<string, DateTime>();
        }

        public bool IsTheSameIssueTime(string airportCode, DateTime issueTime)
        {
            if (airportCode == null)
            {
                return false;
            }

            if (_issueTimeStore.TryGetValue(airportCode, out var currentIssueTime))
            {
                if (currentIssueTime == issueTime)
                {
                    return true;
                }

                if (issueTime > currentIssueTime)
                {
                    _issueTimeStore[airportCode] = issueTime;
                }

                return false;
            }

            _issueTimeStore.Add(airportCode, issueTime);
            return false;
        }
    }
}
