﻿using Microsoft.AspNetCore.Mvc;
using Mountebank.Data;
using Mountebank.Data.Configurations;
using Mountebank.Exceptions;
using Mountebank.Services;

namespace Mountebank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MountebankController : ControllerBase
{
    private readonly MountebankService _mountebankService;

    public MountebankController(AppDbContext db)
    {
        _mountebankService = new MountebankService(db);
    }

    [ProducesResponseType(typeof(Currency), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public ActionResult GetCurrencyById(int id)
    {
        try
        {
            var currency = _mountebankService.GetById(id);
            return Ok(currency);
        }
        catch (RecordNotFound e)
        {
            return NotFound(e.Message);
        }
    }
    
    [ProducesResponseType(typeof(Currency), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{name}")]
    public ActionResult GetCurrencyByName(string name)
    {
        try
        {
            var currency = _mountebankService.GetByName(name);
            return Ok(currency);
        }
        catch (RecordNotFound e)
        {
            return NotFound(e.Message);
        }
    }
    
    [ProducesResponseType(typeof(Currency), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public ActionResult GetAllCurrencies()
    {
        var currencies = _mountebankService.GetAll();
        return Ok(currencies);
    }
}