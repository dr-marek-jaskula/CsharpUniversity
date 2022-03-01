using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASPDotNetLearningApplication
{
    //Enum po to aby okre�li� jak� akcj� chcemy wykona�. Zwi�zane z klas� ResourceOperationRequirement
    public enum ResourceOperation
    {
        Create, Read, Update, Delete
}
}

public enum Genre
{
    Classic,
    [Display(Name = "Post Classic")]
    PostClassic,
    Modern,
    [Display(Name = "Post Modern")] //??
    PostModern,
    Contemporary
}