namespace Library.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }


        #region Create

        [HttpPost("AddMembers")]

        public async Task<ActionResult<MemberResponse>> AddMember([FromBody] CreateMemberRequest member)
        {

            var books = await _memberService.AddMemberAsync(member);

            var response = new MemberResponse
            {
    
                Name = member.Name,
                Email = member.Email,
                MembershipType = member.MembershipType,
                IsActive = member.IsActive,
                CreatedAt = DateTime.Now,

            };

            return Ok(response);


        }

        #endregion

        #region Read

        [HttpGet("GetMembers")]

        public async Task<IActionResult> GetMember()
        {

            var members = await _memberService.GetAllMemberAsync();

            if (members == null || !members.Any())
            {
                return NotFound("No members found.");
            }

            var response = members.Select(m => new MemberResponse
            {
                MemberId = m.MemberId,
                Name = m.Name,
                Email = m.Email,
                MembershipType = m.MembershipType,
                IsActive = m.IsActive,
                CreatedAt = m.CreatedAt,
                Borrowings = m.Borrowings,
                Fines = m.Fines,
            });

            return Ok(response);
        }

        #endregion

        #region Update

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateMembers(int id, UpdateMemberRequest updateMembersRequest)
        {
            if (id != updateMembersRequest.MemberId)
            {
                return BadRequest();
            }
            await _memberService.UpdateMemberAsync(updateMembersRequest);
            return NoContent();
        }

        #endregion
    }
}
