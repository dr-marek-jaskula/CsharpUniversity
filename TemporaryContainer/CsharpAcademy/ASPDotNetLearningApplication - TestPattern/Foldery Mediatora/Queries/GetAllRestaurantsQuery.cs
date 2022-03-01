using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {

    }
}
