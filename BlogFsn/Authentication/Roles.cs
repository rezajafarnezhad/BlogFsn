using System.Linq;

namespace BlogFsn.Authentication
{
    public class Roles
    {

        public const string AdminPage = "AdminPage";

        #region AccessLevels

        public const string CanManageAccessLevel = "CanManageAccessLevel";
        public const string CanViewListAccessLevel = "CanViewListAccessLevel";
        public const string CanAddAccessLevel = "CanAddAccessLevel";
        public const string CanEditAccessLevel = "CanEditAccessLevel";
        public const string CanRemoveAccessLevel = "CanRemoveAccessLevel";

        #endregion

        #region Categories

        public const string CanManageCategories = "CanManageCategories";
        public const string CanViewListCategories = "CanViewListCategories";
        public const string CanAddCategory = "CanAddCategory";
        public const string CanEditCategory = "CanEditCategory";
        public const string CanRemoveCategory = "CanRemoveCategory";
        public const string CanChangeStatusCategory = "CanChangeStatusCategory";

        #endregion

        #region Categories

        public const string CanManageArticle = "CanManageArticle";
        public const string CanViewListArticle = "CanViewListArticle";
        public const string CanAddArticle = "CanAddArticle";
        public const string CanEditArticle = "CanEditArticle";
        public const string CanRemoveArticle = "CanRemoveArticle";
        public const string CanChangeStatusArticle = "CanChangeStatusArticle";

        #endregion
        #region Users
        public const string CanManageUsers = "CanManageUsers";
        public const string CanViewListUsers = "CanViewListUsers";
        public const string CanAddUsers = "CanAddUsers";
        public const string CanEditUsers = "CanEditUsers";
        public const string CanRemoveUsers = "CanRemoveUsers";
        public const string CanChangeUsersStatus = "CanChangeUsersStatus";
        public const string CanChangeUsersAccessLevel = "CanChangeUsersAccessLevel";
        #endregion
    }
}


     
