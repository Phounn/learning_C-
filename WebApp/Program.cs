using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Path.StartsWithSegments("/"))
    {
        context.Response.Headers["Content-Type"] = "text/html";
        await context.Response.WriteAsync($"The method is : {context.Request.Method}<br/>");
        await context.Response.WriteAsync($"The Url is: {context.Request.Path}<br/>");


        await context.Response.WriteAsync($"<b>Headers</b>:<br/>");
        await context.Response.WriteAsync($"<ul>");

        foreach (var key in context.Request.Headers.Keys)
        {
            await context.Response.WriteAsync($"<li><b>{key}</b>: {context.Request.Headers[key]}</li>");
        }
        await context.Response.WriteAsync($"<ul/>");
    }
    else if (context.Request.Path.StartsWithSegments("/employees"))
    {
        if (context.Request.Method == "GET")
        {

            if (context.Request.Query.ContainsKey("id"))
            {
                var id = context.Request.Query["id"];
                if (int.TryParse(id, out int employeeId))
                {
                    var employee = EmployeesRepository.GetEmpById(employeeId);
                    context.Response.Headers["Content-Type"] = "text/html";
                    if (employee is not null)
                    {
                        await context.Response.WriteAsync($"{employee.Name}: {employee.Position}\r\n");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Employee not found");
                        context.Response.StatusCode = 404;
                    }
                }
            }
            else
            {
                var employees = EmployeesRepository.GetEmployees();
                foreach (var employee in employees)
                {
                    await context.Response.WriteAsync($"{employee.Name}: {employee.Position}\r\n");

                }
            }
        }
        else if (context.Request.Method == "POST")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(body);
            try
            {

                if (employee is null || employee.Id < 0)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid Employee data");
                    return;
                }
                EmployeesRepository.AddEmployee(employee);
                context.Response.StatusCode = 201;
                await context.Response.WriteAsync("Employee added successfully");
            }
            catch (Exception ex) { context.Response.StatusCode = 400; }

        }
        else if (context.Request.Method == "PUT")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(body);

            var result = EmployeesRepository.UpdateEmployee(employee);
            if (result)
            {
                await context.Response.WriteAsync("Employee updated successfully");
                context.Response.StatusCode = 204;
                return;
            }
            else
            {
                await context.Response.WriteAsync("Employee not found");
            }
        }
        else if (context.Request.Method == "DELETE")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                var id = context.Request.Query["id"];
                if (int.TryParse(id, out int employeeId))
                {
                    if (context.Request.Headers["Authorization"] == "frank")
                    {
                        var result = EmployeesRepository.DeleteEmployee(employeeId);
                        if (result)
                        {
                            await context.Response.WriteAsync("Employee Deleted successfully");
                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                            await context.Response.WriteAsync("Employee not found");
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("You are not logging");
                    }


                }
            }
        }
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Not Found");
    }
});

app.Run();

static class EmployeesRepository
{
    public static List<Employee> Employees = new List<Employee>
    {
        new Employee(1, "John Doe", "Developer", 1000),
        new Employee(2, "Jane Doe", "Developer", 1200),
        new Employee(3, "Alice", "Manager", 1500),
        new Employee(4, "Bob", "Manager", 2000),
    };
    public static List<Employee> GetEmployees() => Employees;
    public static Employee? GetEmpById(int id)
    {
        return Employees.FirstOrDefault(e => e.Id == id);
    }
    public static void AddEmployee(Employee? employee)
    {
        if (employee is not null)
        {
            Employees.Add(employee);
        }
    }
    public static bool UpdateEmployee(Employee? employee)
    {
        if (employee is not null)
        {
            var emp = Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (emp is not null)
            {
                emp.Name = employee.Name;
                emp.Position = employee.Position;
                emp.Salary = employee.Salary;
                return true;
            }
        }
        return false;
    }
    public static bool DeleteEmployee(int id)
    {
        var emp = Employees.FirstOrDefault(e => e.Id == id);
        if (emp is not null)
        {
            Employees.Remove(emp);
            return true;
        }
        return false;
    }
}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }
    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
}
