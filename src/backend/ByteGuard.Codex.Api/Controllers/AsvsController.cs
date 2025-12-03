using ByteGuard.Codex.Api.Parameters;
using ByteGuard.Codex.Api.Responses;
using ByteGuard.Codex.Core.Exceptions;
using ByteGuard.Codex.Core.Models;
using ByteGuard.Codex.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ByteGuard.Codex.Api.Controllers
{
    /// <summary>
    /// The ASVS objects represents a given application security verification standard. OWASP ASVS comes prepacked together with ByteGuard Codex
    /// but you can create your own custom organizational ASVS. This is even endorsed by OWASP.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AsvsController : ControllerBase
    {
        private readonly AsvsService _asvsService;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public AsvsController(AsvsService asvsService)
        {
            _asvsService = asvsService;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Get ASVS versions
        /// </summary>
        /// <response code="200">Returns the metadata about all the currently registered and configure ASVS versions.</response>
        [HttpGet]
        [ProducesResponseType<List<AsvsVersionMetadata>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsvsVersionsMetadata()
        {
            var versionsMetadata = await _asvsService.GetVersionsMetadataAsync();

            return Ok(versionsMetadata);
        }

        /// <summary>
        /// Get ASVS version
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <response code="200">Returns ASVS version details.</response>
        /// <response code="404">ASVS version with the given identifier could not be found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType<AsvsVersionDetails>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsvsVersion(Guid id)
        {
            var version = await _asvsService.GetVersionAsync(id);

            if (version is null)
            {
                return NotFound();
            }

            return Ok(version);
        }

        /// <summary>
        /// Create version
        /// </summary>
        /// <remarks>
        /// Create a custom ASVS version.
        /// </remarks>
        /// <param name="parameters">ASVS version details.</param>
        /// <response code="200">ASVS version was successfully created. The ASVS details can be found in the body of the response.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        [HttpPost]
        [ProducesResponseType<AsvsVersionMetadata>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsvsVersion(CreateAsvsVersionParameters parameters)
        {
            var version = await _asvsService.CreateAsvsVersionAsync(
                parameters.VersionNumber,
                parameters.Name,
                parameters.Description);

            return CreatedAtAction(nameof(GetAsvsVersion), new { id = version.Id }, version);
        }

        /// <summary>
        /// Update version
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="parameters">New ASVS version details.</param>
        /// <response code="201">The ASVS version has been succesfully updated.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        /// <response code="404">The ASVS version could not be found.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsvsVersion(Guid id, CreateAsvsVersionParameters parameters)
        {
            try
            {
                await _asvsService.UpdateAsvsVersionAsync(id,
                    parameters.VersionNumber,
                    parameters.Name,
                    parameters.Description);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }

        /// <summary>
        /// Create chapter
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="parameters">Chapter details.</param>
        /// <response code="200">ASVS chapter was successfully created. The chapter details can be found in the body of the response.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        [HttpPost("{id}/chapters")]
        [ProducesResponseType<AsvsChapterDetails>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateChapter(Guid id, CreateAsvsChapterParameters parameters)
        {
            try
            {
                var chapter = await _asvsService.CreateChapterAsync(
                    id,
                    parameters.Title,
                    parameters.Description);

                return Created((string?)null, chapter);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }

        /// <summary>
        /// Update chapter
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="chapterId">Chapter identifier.</param>
        /// <param name="parameters">New chapter details.</param>
        /// <response code="201">The chapter has been succesfully updated.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        /// <response code="404">The chapter or ASVS version could not be found.</response>
        [HttpPut("{id}/chapters/{chapterId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateChapter(Guid id, Guid chapterId, CreateAsvsChapterParameters parameters)
        {
            try
            {
                await _asvsService.UpdateChapterAsync(
                    id,
                    chapterId,
                    parameters.Title,
                    parameters.Description);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }

        /// <summary>
        /// Create section
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="chapterId">Chapter identifier.</param>
        /// <param name="parameters">Section details.</param>
        /// <response code="200">ASVS section was successfully created. The section details can be found in the body of the response.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        [HttpPost("{id}/chapters/{chapterId}/sections")]
        [ProducesResponseType<AsvsSectionDetails>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSection(Guid id, Guid chapterId, CreateAsvsSectionParamters parameters)
        {
            try
            {
                var section = await _asvsService.CreateSectionAsync(
                    id,
                    chapterId,
                    parameters.Name);

                return Created((string?)null, section);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }

        /// <summary>
        /// Update section
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="chapterId">Chapter identifier.</param>
        /// <param name="sectionId">Section identifier.</param>
        /// <param name="parameters">New section details.</param>
        /// <response code="201">The section has been succesfully updated.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        /// <response code="404">The section, chapter or ASVS version could not be found.</response>
        [HttpPut("{id}/chapters/{chapterId}/sections/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSection(Guid id, Guid chapterId, Guid sectionId, CreateAsvsSectionParamters parameters)
        {
            try
            {
                await _asvsService.UpdateSectionAsync(
                    id,
                    chapterId,
                    sectionId,
                    parameters.Name);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }

        /// <summary>
        /// Create requirement
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="chapterId">Chapter identifier.</param>
        /// <param name="sectionId">Section identifier.</param>
        /// <param name="parameters">Requirement details.</param>
        /// <response code="200">ASVS requirement was successfully created. The requirement details can be found in the body of the response.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        [HttpPost("{id}/chapters/{chapterId}/sections/{sectionId}/requirements")]
        [ProducesResponseType<AsvsRequirementDetails>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRequirements(Guid id, Guid chapterId, Guid sectionId, CreateAsvsRequirementParameters parameters)
        {
            try
            {
                var requirement = await _asvsService.CreateRequirementAsync(
                    id,
                    chapterId,
                    sectionId,
                    parameters.Description,
                    parameters.Level);

                return Created((string?)null, requirement);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }

        /// <summary>
        /// Update requirement
        /// </summary>
        /// <param name="id">ASVS version identifier.</param>
        /// <param name="chapterId">Chapter identifier.</param>
        /// <param name="sectionId">Section identifier.</param>
        /// <param name="requirementId">Requirement identifier.</param>
        /// <param name="parameters">New requirement details.</param>
        /// <response code="201">The requirement has been succesfully updated.</response>
        /// <response code="400">The request is invalid. Inspect the response body for details.</response>
        /// <response code="404">The requirement, section, chapter or ASVS version could not be found.</response>
        [HttpPut("{id}/chapters/{chapterId}/sections/{sectionId}/requirements/{requirementId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRequirements(Guid id, Guid chapterId, Guid sectionId, Guid requirementId, CreateAsvsRequirementParameters parameters)
        {
            try
            {
                await _asvsService.UpdateRequirementAsync(
                    id,
                    chapterId,
                    sectionId,
                    requirementId,
                    parameters.Description,
                    parameters.Level);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
        }
    }
}
