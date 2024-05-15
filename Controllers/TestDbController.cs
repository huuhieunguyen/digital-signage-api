using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CMS.Controllers
{
    [ApiController]
    [Route("api/v1")]
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
