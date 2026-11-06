
using Backend.Namespace;
namespace Backend.Services;



public class PeopleService: IPeopleService
 {
        // si cumple esto o es mayor a 100 que no lo deje 

    public bool Validate(PeopleController.People people)
    {
        if (string.IsNullOrEmpty(people.Name) || people.Name.Length >100)
        {
            return false;
        }
        return true;
    }
    // si cumple esto o es mayor a 100 que no lo deje 
}
