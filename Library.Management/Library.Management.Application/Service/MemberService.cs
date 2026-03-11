namespace Library.Management.Application.Service
{
    public class MemberService : IMemberService
    {
        private readonly ILibraryDbContext _libraryDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(ILibraryDbContext libraryDbContext, IUnitOfWork unitOfWork)
        {
            _libraryDbContext = libraryDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<MemberResponse> AddMemberAsync(CreateMemberRequest request)
        {
            var members = new Members(

                request.Name,
                request.Email,
                request.MembershipType,
                request.IsActive
                );

            await _libraryDbContext.Members.AddAsync( members );

            await _unitOfWork.CommitAsync();


            return new MemberResponse
            {
                Name = request.Name,
                Email = request.Email,
                MembershipType = request.MembershipType,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task<List<Members>> GetAllMemberAsync()
        {
           var members = await _libraryDbContext.Members.ToListAsync();

            return members;
        }

        public async Task UpdateMemberAsync(UpdateMemberRequest members)
        {
            var existingmember = await _libraryDbContext.Members.FirstOrDefaultAsync(b => b.MemberId == members.MemberId );

            if (existingmember == null)
            {
                throw new InvalidOperationException("Book Not Found");
            }

            existingmember.UpdateBook(

                existingmember.Name,
                existingmember.Email,
                existingmember.MembershipType,
                existingmember.IsActive

                );

            await _unitOfWork.CommitAsync();
        }
    }
}
