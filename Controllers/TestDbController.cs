using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestDbController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestDbController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("test-db-connection")]
        public IActionResult TestDbConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new NpgsqlConnection(connectionString);

            try
            {
                connection.Open();
                return Ok("Database connection successful!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error connecting to the database: {ex.Message}");
            }
        }
    }
}
