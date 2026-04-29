using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhs.Test.Api.BusinessService;
using Nhs.Test.Api.Model;
using System;

namespace Nhs.Test.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class PatientController(IPatientService patientService, ILogger<PatientController> logger, AppDbContext appDbContext) : ControllerBase
    {


        [HttpGet("{id:int}")]
        [BasicAuthorization]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                var result = await patientService.GetByIdAsync(id);

                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()

        {
            try
            {
                var result = await patientService.ListAsync();

                if (result is null || !result.Any())
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500);
            }
        }
    }
}
