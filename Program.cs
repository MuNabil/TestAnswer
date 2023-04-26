
using TestAnswer.Helpers;


var employees = await EmployeesHelper.GetEmployeesFromApiAsync();

// a) Generate the HTML Table and save it in a file.

var htmlTable = EmployeesHtmlTableHelper.GetEmplyeesHtmlTable(employees);
System.IO.File.WriteAllText("employees.html", htmlTable);

// b) Drow the pie chart and generate a PNG
PieChartHelper.GeneratePieChartPNG(employees);

