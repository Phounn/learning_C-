using LearnDi.Models;

namespace LearnDi
{
    public class ContactService
    {
        public List<Contact> ContactList = new();
        public void AddContact(Contact contact)
        {
            ContactList.Add(contact);
        }
    }
}
