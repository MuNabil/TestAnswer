using TestAnswer.Classes;

namespace TestAnswer.Helpers;

public class EmployeesHelper
{
    public static async Task<List<EmployeeResult>> GetEmployeesFromApiAsync()
    {
        const string apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
        using var http = new HttpClient();
        var response = await http.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error occure while getting data from the API");

        // Serialize the HTTP response to a string
        var json = await response.Content.ReadAsStringAsync();

        // Deserializes the json data to the Employee class.
        var employees = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(json)!;

        return DataFiltering(employees);
    }

    private static List<EmployeeResult> DataFiltering(List<Employee>? data)
    {
        return data!
            .Where(emp => emp.EmployeeName is not null)
            .GroupBy(emp => emp.EmployeeName)
            .Select(g =>
            {
                var totalTime = g.Sum(x => (x.EndTimeUtc - x.StarTimeUtc).TotalHours);
                return new EmployeeResult { EmployeeName = g.Key, TotalTime = totalTime };
            })
            .OrderByDescending(emp => emp.TotalTime).ToList();
    }
}