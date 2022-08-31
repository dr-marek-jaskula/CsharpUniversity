using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using AutoMapper;
using EFCore;
using LanguageExt.Common;

namespace ASP.NETCoreWebAPI.Services;

//This is the approach to avoid throwing the exception. Exceptions throwing can decrease the performance of the program
//Here we use the "LanguageExt" package for "NoThrow" approach
//We can use also well designed NuGet Package "ErrorOr" created by "Amichai"
//Other way is to implement Result (Result<TValue>), Error approach - custom implementation, good approach can be found in work by Milan Jovanović ("https://www.youtube.com/watch?v=KgfzM0QWHrQ&t=448s")

//Pros of no throwing an exceptions:
//Expressiveness, Performance, Self-documenting
//Cons of no throwing an exceptions:
//No stack trace, hard to debug and determine the source of the problem, more complexity 

public interface INoThrowService
{
    Result<CustomerDto> GetById(int id);
}

public class NoThrowService : INoThrowService
{
    private readonly MyDbContext _dbContex;
    private readonly IMapper _mapper;

    public NoThrowService(MyDbContext dbContex, IMapper mapper)
    {
        _dbContex = dbContex;
        _mapper = mapper;
    }

    //Result is a structure from the LanguageExt.Common NuGet Package. This class has implicit conversion to the generic type it gets
    //Moreover, this class can store either the value of given type (CustomerDto) or a exception.
    public Result<CustomerDto> GetById(int id)
    {
        var customer = _dbContex.Customers
            .FirstOrDefault(c => c.Id == id);

        if (customer is null)
        {
            var notFoundException = new NotFoundException("Customer not found");
            //The Result will store info like "exception would be thrown when CustomerDto object was supposed to be returned"
            return new Result<CustomerDto>(notFoundException);
        }

        var result = _mapper.Map<CustomerDto>(customer);
        return result;
    }
}