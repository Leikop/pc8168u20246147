using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using pc8168u2024614.Application.Errors;
using pc8168u2024614.Domain.Model.Aggregates;
using pc8168u2024614.Openpay.Platform.Resources;
using pc8168u2024614.Openpay.Platform.Shared.Application.Patterns;

namespace pc8168u2024614.Interfaces.REST.Transform;

public static class ActionResultFromCreateDisbursementOrderResultAssembler
{
    public static ActionResult ToActionResultFromCreateDisbursementOrderResult(
        Result<DisbursementOrder, CreateDisbursementOrderError> result,
        ControllerBase controller,
        IStringLocalizer<SharedResource> localizer) =>
        result switch
        {
            Result<DisbursementOrder, CreateDisbursementOrderError>.Success success =>
                controller.StatusCode(StatusCodes.Status201Created,
                    DisbursementOrderResourceFromEntityAssembler.ToResourceFromEntity(success.Value)),

            Result<DisbursementOrder, CreateDisbursementOrderError>.Failure
            {
                Error: CreateDisbursementOrderError.DuplicateOrderTrackingCode
            } =>
                controller.Conflict(localizer["DisbursementOrderTrackingCodeDuplicated"].Value),

            _ => controller.Problem(
                title: localizer["UnexpectedServerError"].Value,
                detail: localizer["UnexpectedErrorCreatingDisbursementOrder"].Value,
                statusCode: StatusCodes.Status500InternalServerError)
        };
}
