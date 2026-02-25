namespace KnightMoves.KnightLight.Pet;

public class Trainer{
    public string Listen(Animal[] animals) {
        string sounds = "";

        foreach(Animal animal in animals){
            if(typeof(Cat).IsInstanceOfType(animal))
            {
                sounds += ((Cat) animal).Say() + ", ";
            } 
            else if (typeof(Dog).IsInstanceOfType(animal))
            {   
                sounds += ((Dog) animal).Talk() + ", ";
            } 
            else if (typeof(Bird).IsInstanceOfType(animal))
            {
                sounds += ((Bird) animal).Sing() + ", ";
            }
        }

        return sounds;
    }
}
