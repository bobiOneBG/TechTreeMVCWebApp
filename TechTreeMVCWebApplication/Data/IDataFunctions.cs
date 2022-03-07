namespace TechTreeMVCWebApplication.Data
{
    using TechTreeMVCWebApplication.Entities;

    public interface IDataFunctions
    {
        Task UpdateUserCategoryEntityAsync(List<UserCategory> userCategoryItemsToDelete, List<UserCategory> userCategoryItemsToAdd);
    }
}
