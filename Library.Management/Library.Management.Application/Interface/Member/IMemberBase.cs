namespace Library.Management.Application.Interface.Member
{
    public abstract class IMemberBase
    {
        protected IMemberBase(int memberId)
        {
            MemberId = memberId;
        }

        public int MemberId { get; }

        public abstract int MaxBooksAllowed { get; }
    }

}
