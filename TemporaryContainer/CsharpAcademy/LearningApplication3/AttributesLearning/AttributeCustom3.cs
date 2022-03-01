using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace LearningApplication3
{
    class AttributeCustom3
    {
        public static void AtriCustom1()
        {

            var a = typeof(AnimalTypeTestClass).GetMethod("DogMethod").GetCustomAttributesData();
            var a2 = typeof(AnimalTypeTestClass).GetMethod("CatMethod").GetCustomAttributesData();
            var a3 = (Animal)(typeof(AnimalTypeTestClass).GetMethod("CatMethod").GetCustomAttributesData()[0].ConstructorArguments[0].Value);


            #region Animal connected to method Test
            AnimalTypeTestClass testClass = new AnimalTypeTestClass();
            Type type = testClass.GetType();
            // Iterate through all the methods of the class.
            foreach (MethodInfo mInfo in type.GetMethods())
            {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    // Check for the AnimalType attribute.
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                        Console.WriteLine("Method {0} has a pet {1} attribute.",
                                            mInfo.Name, ((AnimalTypeAttribute)attr).Pet);
                }
            }
            #endregion

            #region Custom Validator

            AuthorTestClassForCustomAtri author3 = new AuthorTestClassForCustomAtri();
            author3.FirstName = "";
            author3.LastName = null;
            ValidationContext context3 = new ValidationContext(author3, null, null);
            List<ValidationResult> validationResults3 = new List<ValidationResult>();
            bool valid2 = Validator.TryValidateObject(author3, context3, validationResults3, true);

            if (!valid2)
            {
                foreach (ValidationResult validationResult3 in validationResults3)
                {
                    Console.WriteLine("{0}", validationResult3.ErrorMessage);
                }
            }

            #endregion

        }
    }

    #region Custom Attribute that connects animal with method (metadata)
    // An enumeration of animals. Start at 1 (0 = uninitialized).
    public enum Animal
    {
        // Pets.
        Dog = 1,
        Cat,
        Bird,
    }

    // A custom attribute to allow a target to have a pet.
    [AttributeUsage(AttributeTargets.Method)]
    public class AnimalTypeAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public AnimalTypeAttribute(Animal pet)
        {
            thePet = pet;
        }

        // Keep a variable internally ...
        protected Animal thePet;

        // .. and show a copy to the outside world.
        public Animal Pet
        {
            get { return thePet; }
            set { thePet = value; }
        }
    }

    // A test class where each method has its own pet.
    class AnimalTypeTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod() { }

        [AnimalType(Animal.Cat)]
        public void CatMethod() { }

        [AnimalType(Animal.Bird)]
        public void BirdMethod() { }
    }

    #endregion

    #region Custom Validation Attribute

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IsEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            return !string.IsNullOrEmpty(inputValue);
        }
    }

    public class AuthorTestClassForCustomAtri
    {
        [IsEmpty(ErrorMessage = "Should not be null or empty.")]
        public string FirstName { get; set; }
        [IsEmpty(ErrorMessage = "Should not be null or empty.")]
        public string LastName { get; set; }
    }
    #endregion

}
