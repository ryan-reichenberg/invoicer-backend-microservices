using System;

namespace Invoicer.Common.Types.DDD
{
    public class InvalidAggregateIdException: Exception
    {

        public InvalidAggregateIdException() : base("Invalid aggregate id.")
        {
        }
    }
}