namespace ApiTools
{
    public class NeedAuthenticationException : BusinessLogicException
    {
        public NeedAuthenticationException() : base("Seuls les utilisateurs authentifies peuvent effectuer cette action.")
        {
        }
    }
}
