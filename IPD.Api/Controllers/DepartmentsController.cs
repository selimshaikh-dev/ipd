using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DepartmentsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DepartmentsController> logger;

        /// <summary>
        /// constructor for DepartmentsController
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DepartmentsController(IUnitOfWork unitOfWork, ILogger<DepartmentsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Load all Department
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadDepartments()
        {
            try
            {
                var departments = await unitOfWork.DepartmentRepository
                    .GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(departments);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find department by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDepartmentByKey(Guid key)
        {
            try
            {
                var department = await unitOfWork.DepartmentRepository.GetByIdAsync(key);
                if (department == null)
                    return NotFound();

                return Ok(department);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Add an Department.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                var departmentAdded = unitOfWork.DepartmentRepository.Add(department);
                await unitOfWork.SaveChangesAsync();

                var departmentToReturn = await unitOfWork.DepartmentRepository.GetByIdAsync(departmentAdded.DepartmentID);

                return Ok(departmentToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Edit an department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            try
            {
                var departmentEntity = await unitOfWork.DepartmentRepository.GetByIdAsync(department.DepartmentID);
                if (departmentEntity == null)
                    return NotFound();

                departmentEntity.DepartmentID = department.DepartmentID;
                departmentEntity.DepartmentName = department.DepartmentName;

                var departmentUpdated = unitOfWork.DepartmentRepository.Update(departmentEntity);
                await unitOfWork.SaveChangesAsync();

                var departmentToReturn = await unitOfWork.DepartmentRepository.GetByIdAsync(departmentUpdated.DepartmentID);

                return Ok(departmentToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}