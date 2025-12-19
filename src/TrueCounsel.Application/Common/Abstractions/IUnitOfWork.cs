using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Application.Common.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get;  }
        /// <summary>
        /// Commits all changes in a single transaction.
        /// </summary>
        Task CommitAsync();

    }
}
