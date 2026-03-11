namespace Library.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowservice;

        public BorrowController(IBorrowService service)
        {
            _borrowservice = service;
        }

        #region Create

        [HttpPost("Borrow")]
        [EnableRateLimiting("BorrowPolicy")]
        public async Task<IActionResult> Borrow(int memberId, int bookId, CancellationToken cancellationToken)
        {
            var borrow = await _borrowservice.BorrowAsync(memberId, bookId, cancellationToken);

            var response = new BorrowResponse
            {
                BookId = borrow.BookId,
                BorrowingId = borrow.BorrowingId,
                MemberId = borrow.MemberId,
                Status = borrow.Status,
                DueDate = borrow.DueDate,
                BorrowedDate = borrow.BorrowedDate,
                ReturnedDate = borrow.ReturnedDate,
            };

            return Ok(response);
        }

        [HttpPost("Return")]
        public async Task<IActionResult> Return(int BorrowId)
        {
           var Bookreturn = await _borrowservice.ReturnAsync(BorrowId);

            var response = new BorrowResponse
            {
                BookId = Bookreturn.BookId,
                BorrowingId = Bookreturn.BorrowingId,
                MemberId = Bookreturn.MemberId,
                Status = Bookreturn.Status,
                DueDate = Bookreturn.DueDate,
                BorrowedDate = Bookreturn.BorrowedDate,
                ReturnedDate = Bookreturn.ReturnedDate,
            };

            return Ok(response);

        }

        #endregion

        #region Read

        [HttpGet("GetBorrow")]

        public async Task<IActionResult> GetBorrow(CancellationToken cancellationToken)
        {

            var Borrow = await _borrowservice.GetAllBorrow(cancellationToken);

            return Ok(Borrow);

        }
        #endregion

    }
}
