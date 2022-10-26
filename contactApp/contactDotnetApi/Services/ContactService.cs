namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

public interface IContactService
{
    IEnumerable<Contact> GetAll();
    IEnumerable<Contact> GetAllByLastName(string lastName);
    Contact GetById(int id);
    void Create(CreateRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
}

public class ContactService : IContactService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public ContactService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Contact> GetAll()
    {
        return _context.Contacts;
    }
    
    public IEnumerable<Contact> GetAllByLastName(string lastName)
    {
        
        var contactsSearchLastName = _context.Contacts.Where(x=>x.LastName.ToLower()==lastName.ToLower());
        return contactsSearchLastName;
    }

    public Contact GetById(int id)
    {
        return getContact(id);
    }

    public void Create(CreateRequest model)
    {
        // validate
        if (_context.Contacts.Any(x => x.PhoneNumber == model.PhoneNumber))
            throw new AppException("Contact with the PhoneNumber '" + model.PhoneNumber + "' already exists");

        // map model to new Contact object
        var Contact = _mapper.Map<Contact>(model);

        // save Contact
        _context.Contacts.Add(Contact);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var Contact = getContact(id);

        // validate
        if (model.PhoneNumber != Contact.PhoneNumber && _context.Contacts.Any(x => x.PhoneNumber == model.PhoneNumber))
            throw new AppException("Contact with the Phone Number '" + model.PhoneNumber + "' already exists");


        // copy model to Contact and save
        _mapper.Map(model, Contact);
        _context.Contacts.Update(Contact);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var Contact = getContact(id);
        _context.Contacts.Remove(Contact);
        _context.SaveChanges();
    }

    // helper methods

    private Contact getContact(int id)
    {
        var Contact = _context.Contacts.Find(id);
        if (Contact == null) throw new KeyNotFoundException("Contact not found");
        return Contact;
    }
}