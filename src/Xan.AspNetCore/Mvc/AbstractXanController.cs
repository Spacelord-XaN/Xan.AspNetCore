using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Xan.AspNetCore.Mvc;

public abstract class AbstractXanController
    : Controller
{
    public IActionResult RedirectToReferer()
    {
        string? referer = Request.Headers.Referer;
        if (referer == null)
        {
            throw new InvalidOperationException("No referer to redirect to");
        }
        return Redirect(referer);
    }

    public void SetValidationResult(ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        ModelState.Clear();
        if (!validationResult.IsValid)
        {
            foreach (ValidationFailure error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
