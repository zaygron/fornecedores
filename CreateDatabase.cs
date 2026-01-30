using Npgsql;

// Script para criar o banco de dados PostgreSQL
// Execute: dotnet run CreateDatabase.cs

var connectionString = "Host=localhost;Port=5433;Username=postgres;Password=Admin@123;Database=postgres";

try
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("✓ Conectado ao PostgreSQL com sucesso!");

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE DATABASE cadastro_fornecedores;";
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("✓ Banco de dados 'cadastro_fornecedores' criado com sucesso!");
            }
            catch (PostgresException ex) when (ex.SqlState == "42P04")
            {
                Console.WriteLine("✓ Banco de dados 'cadastro_fornecedores' já existe!");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"✗ Erro: {ex.Message}");
    Environment.Exit(1);
}
