namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private IContactService _ContactService;
    private IMapper _mapper;

    public ContactsController(
        IContactService ContactService,
        IMapper mapper)
    {
        _ContactService = ContactService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var Contacts = _ContactService.GetAll();
        return Ok(Contacts);
    }

    [HttpGet("GetByLastNameList/{lastName}")]
    public IActionResult GetByLastNameList(string lastName)
    {
        var Contacts = _ContactService.GetAllByLastName(lastName);
        return Ok(Contacts);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var Contact = _ContactService.GetById(id);
        return Ok(Contact);
    }

    [HttpPost]
    public IActionResult Create(CreateRequest model)
    {
        _ContactService.Create(model);
        return Ok(new { message = "Contact created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _ContactService.Update(id, model);
        return Ok(new { message = "Contact updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _ContactService.Delete(id);
        return Ok(new { message = "Contact deleted" });
    }
}