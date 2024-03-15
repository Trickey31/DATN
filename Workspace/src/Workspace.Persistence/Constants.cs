namespace Workspace.Persistence
{
    public static class Constants
    {
        #region *********** TableNames ***********

        // *********** Plural Nouns ***********
        internal const string Actions = nameof(Actions);
        internal const string Functions = nameof(Functions);
        internal const string ActionInFunctions = nameof(ActionInFunctions);
        internal const string Permissions = nameof(Permissions);

        internal const string Users = nameof(Users);
        internal const string Roles = nameof(Roles);
        internal const string UserRoles = nameof(UserRoles);

        internal const string UserClaims = nameof(UserClaims); // IdentityUserClaim
        internal const string RoleClaims = nameof(RoleClaims); // IdentityRoleClaim
        internal const string UserLogins = nameof(UserLogins); // IdentityRoleClaim
        internal const string UserTokens = nameof(UserTokens); // IdentityUserToken

        // *********** Singular Nouns ***********
        internal const string Task = nameof(Task);
        internal const string Project = nameof(Project);

        #endregion
    }
}
