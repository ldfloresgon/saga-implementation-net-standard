using System;

namespace Commands
{
    public interface ICommand
    {
        DateTime OcurredOn => DateTime.UtcNow;
    }
}
