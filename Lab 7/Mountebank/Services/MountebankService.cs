using Microsoft.EntityFrameworkCore;
using Mountebank.Data;
using Mountebank.Data.Configurations;
using Mountebank.Exceptions;

namespace Mountebank.Services;

public class MountebankService
{
    private readonly AppDbContext _db;

    public MountebankService(AppDbContext db)
    {
        _db = db;
    }
    
    public Currency GetById(int id)
    {
        var currency = _db.Currencies.FirstOrDefault(_ => _.Id == id)
                       ?? throw new RecordNotFound($"Валюта с Id \"{id}\" не найдена");
        return currency;
    }

    public Currency GetByName(string name)
    {
        var currency = _db.Currencies.FirstOrDefault(_ => _.Name.ToLower().Trim() == name.ToLower().Trim())
                       ?? throw new RecordNotFound($"Валюта с названием \"{name}\" не найдена");
        return currency;
    }

    public IEnumerable<Currency> GetAll()
    {
        return _db.Currencies.ToList();
    }
}