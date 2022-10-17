using System;
using System.Collections.Generic;

namespace BL
{
    public interface IVIPCarOwnersRepository
    {
        List<VIPCarOwner> GetVIPCarOwners(int offset = 0, int limit = -1);
        VIPCarOwner GetVIPCarOwnerById(int id);
    }
}