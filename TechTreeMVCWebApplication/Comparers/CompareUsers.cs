namespace TechTreeMVCWebApplication.Comparers
{
    using System.Diagnostics.CodeAnalysis;
    using TechTreeMVCWebApplication.Areas.Admin.Models;

    public class CompareUsers
    {
        public bool Equals(UserModel x, UserModel y)
        {
            if (y == null) return false;

            if (x.Id == y.Id)
                return true;

            return false;

        }

        public int GetHashCode([DisallowNull] UserModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
