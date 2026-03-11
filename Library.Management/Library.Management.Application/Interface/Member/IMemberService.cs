namespace Library.Management.Application.Interface.Member
{
    public interface IMemberService
    {
        Task<MemberResponse> AddMemberAsync(CreateMemberRequest request);

        Task<List<Members>> GetAllMemberAsync();

        Task UpdateMemberAsync(UpdateMemberRequest books);
    }
}
