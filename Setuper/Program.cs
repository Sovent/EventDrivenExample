using System;
using System.Data.SqlClient;
using Dapper;

namespace Setuper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Введите путь к mdf-базе данных без кавычек: ");
	        var dbPath = Console.ReadLine();
	        var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;Connect Timeout=30";

	        try
	        {
		        using (var sqlConnection = new SqlConnection(connectionString))
		        {
			        sqlConnection.Execute(
				        "INSERT INTO VirtualMachines (Id) VALUES (@id)",
				        new {id = Guid.Parse("28014e66-f5f0-49f4-a951-59bf590c7e06")});
		        }
	        }
	        catch (Exception e)
	        {
		        Console.WriteLine("Не удалось инициализировать БД по причине: " + e.Message);
				Console.WriteLine(e.StackTrace);
		        return;
	        }

			Console.WriteLine("БД успешно инициализирована, добавлена одна строка");
        }
    }
}
