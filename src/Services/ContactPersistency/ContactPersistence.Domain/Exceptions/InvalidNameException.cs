﻿namespace ContactPersistence.Domain.Exceptions;

public class InvalidNameException : DomainException
{
    public InvalidNameException()
        : base("Name is required.")
    { }
}