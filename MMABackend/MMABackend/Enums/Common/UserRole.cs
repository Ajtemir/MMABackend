using System.ComponentModel;

namespace MMABackend.Enums.Common
{
    public enum UserRole
    {
        [Description(nameof(Admin))]
        Admin = 0,
        [Description(nameof(Buyer))]
        Buyer = 1,
        [Description(nameof(Seller))]
        Seller = 2,
    }
}