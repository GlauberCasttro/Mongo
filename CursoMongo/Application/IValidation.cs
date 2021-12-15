using FluentValidation.Results;
using System.Collections.Generic;

namespace Application
{
    public interface IValidation
    {
        bool Invalid { get; }
         List<ValidationFailure> Errors { get;}
    }
}