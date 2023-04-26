
using TestAnswer.Classes;

namespace TestAnswer.Helpers;

public class EmployeesHtmlTableHelper
{
    public static string GetEmplyeesHtmlTable(List<EmployeeResult> employees)
    {
        var html = "<table><thead><tr><th>Employee Name</th><th>Total Total time worked</th></tr></thead><tbody>";
        foreach (var employee in employees)
        {
            var row = "<tr";
            if (employee.TotalTime < 100)
                row += $" style='background-color: red;'";
            row += $"><td>{employee.EmployeeName}</td><td>{employee.TotalTime}</td></tr>";
            html += row;
        }
        html += "</tbody></table>";

        return html;
    }
}