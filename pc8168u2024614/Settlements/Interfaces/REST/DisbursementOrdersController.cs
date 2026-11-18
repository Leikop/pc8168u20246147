using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using pc8168u2024614.Application.Services;
using pc8168u2024614.Interfaces.REST.Resources;
using pc8168u2024614.Interfaces.REST.Transform;
using pc8168u2024614.Openpay.Platform.Resources;

namespace pc8168u2024614.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Disbursement Orders")]
public class DisbursementOrdersController(
    IDisbursementOrderCommandService disbursementOrderCommandService,
    IStringLocalizer<SharedResource> localizer,
    ILogger<DisbursementOrdersController> logger)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a disbursement order")]
    [EndpointDescription("Registers a disbursement order. Status is Queued when it is not specified.")]
    [ProducesResponseType(typeof(DisbursementOrderResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateDisbursementOrder([FromBody] CreateDisbursementOrderResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CreateDisbursementOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await disbursementOrderCommandService.Handle(command, cancellationToken);
            return ActionResultFromCreateDisbursementOrderResultAssembler
                .ToActionResultFromCreateDisbursementOrderResult(result, this, localizer);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation failed while creating disbursement order {OrderTrackingCode}",
                resource.OrderTrackingCode);
            return BadRequest(localizer[ToMessageKey(ex)].Value);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating disbursement order {OrderTrackingCode}",
                resource.OrderTrackingCode);
            return Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingDisbursementOrder"].Value,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static string ToMessageKey(ArgumentException exception) => exception.ParamName switch
    {
        "scheduledTo" => "InvalidExecutionSchedule",
        "amount" => "InvalidTransferCapAmount",
        "currencyCode" => "InvalidTransferCapCurrencyCode",
        "status" => "InvalidStatus",
        "User" => "InvalidUser",
        "Remarks" => "InvalidRemarks",
        _ => "InvalidDisbursementOrderRequest"
    };
}
