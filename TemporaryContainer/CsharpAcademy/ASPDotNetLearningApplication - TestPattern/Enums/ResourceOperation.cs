using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASPDotNetLearningApplication
{
    //Enum po to aby okreœliæ jak¹ akcjê chcemy wykonaæ. Zwi¹zane z klas¹ ResourceOperationRequirement
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