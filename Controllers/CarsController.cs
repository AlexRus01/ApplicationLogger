using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.Tasks;

namespace LogApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            Log.Information("Entering GetCars method"); // Log entering the method
            try
            {
                var cars = await _context.Cars.ToListAsync();
                Log.Information("Retrieved {Count} cars from the database", cars.Count); // Log number of cars retrieved
                return Ok(cars);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving cars"); // Log any errors
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/cars
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            Log.Information("Entering CreateCar method with model: {Model}", car.Model); // Log entering the method with parameters
            try
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();
                Log.Information("Car with model {Model} created successfully", car.Model); // Log success
                return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "Database update error while creating car with model {Model}", car.Model); // Log DB errors
                return StatusCode(500, "Internal server error");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while creating a car"); 
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/cars/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            Log.Information("Entering GetCarById method with id: {Id}", id); 
            try
            {
                var car = await _context.Cars.FindAsync(id);
                if (car == null)
                {
                    Log.Warning("Car with id {Id} not found", id); 
                    return NotFound();
                }

                Log.Information("Car with id {Id} retrieved successfully", id); 
                return Ok(car);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving car with id {Id}", id); 
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
