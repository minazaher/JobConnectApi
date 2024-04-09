using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers;

public class ErrorController: ControllerBase
{
    [Route("/error")]
    [HttpGet]
    public IActionResult Error()
    {
        return Problem();
    } 
}