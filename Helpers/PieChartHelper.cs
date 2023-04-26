using System.Drawing;
using TestAnswer.Classes;

namespace TestAnswer.Helpers;

public class PieChartHelper
{
    public static void GeneratePieChartPNG(List<EmployeeResult> employees)
    {
        List<double> percentages = CalculatePercentagesForPieChart(employees);

        int width = 600;
        int height = 480;
        using (Bitmap bitmap = new Bitmap(width, height))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                DrawPieChart(employees, percentages, width, height, graphics);
                DrawLegend(width, height, graphics);

            }

            // Save pie chart as PNG image file
            string filename = "pie-chart.png";
            bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine("Pie chart saved as {0}", filename);
        }
    }

    private static List<double> CalculatePercentagesForPieChart(List<EmployeeResult> employees)
    {
        var totalTimeForAllEmployees = employees.Sum(emp => emp.TotalTime);
        List<double> percentages = new();
        employees.ForEach(emp => percentages.Add((double)emp.TotalTime / totalTimeForAllEmployees * 100));
        return percentages;
    }

    private static void DrawLegend(int width, int height, Graphics graphics)
    {
        List<Color> colors = new();
        colors.Add(Color.Red);
        colors.Add(Color.Blue);

        int legendSize = 12;
        int x0 = width - 100;
        int y0 = height - 50;
        int dy = 25;
        string[] labels = { "Time < 100", "Time >= 100" };
        for (int j = 0; j < colors.Count; j++)
        {
            Brush brush = new SolidBrush(colors[j]);
            graphics.FillRectangle(brush, x0, y0 + j * dy, legendSize, legendSize);
            graphics.DrawString(labels[j], new Font("Arial", 10), Brushes.Black, x0 + legendSize + 10, y0 + j * dy);
        }
    }

    private static void DrawPieChart(List<EmployeeResult> employees, List<double> percentages, int width, int height, Graphics graphics)
    {
        graphics.Clear(Color.White);

        Rectangle rect = new Rectangle(0, 0, width - 1, height - 1);
        float startAngle = 0.0f;
        float gapAngle = 1.5f;
        int i = 0;

        foreach (float percentage in percentages)
        {
            float sweepAngle = 360 * percentage / 100;
            var areaColor = Brushes.Blue;
            if (employees[i++].TotalTime < 100) areaColor = Brushes.Red;
            graphics.FillPie(areaColor, rect, startAngle, sweepAngle - gapAngle);

            startAngle += sweepAngle + gapAngle;
        }
    }

}